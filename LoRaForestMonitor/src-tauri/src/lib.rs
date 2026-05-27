use regex::Regex;
use std::io::Read;
use std::net::TcpStream;
use std::time::Duration;
use std::{env, io::Write, sync::Mutex};
use tauri::{Emitter, Manager, WindowEvent};

static OPENING: Mutex<bool> = Mutex::new(false);
static CMD_BUFFER: Mutex<String> = Mutex::new(String::new());

#[tauri::command]
fn set_connecton_status(sta: bool) {
    let mut flag = OPENING.lock().unwrap();
    *flag = sta;
}

//用于解析数据
fn analy_data(window: &tauri::Window, data: String) {
    //正则表达式
    let temperature_re: Regex = Regex::new(r"Temperature: (\d+?)C;").unwrap();
    let rain_re: Regex = Regex::new(r"Rain: (\d+?);").unwrap();
    let light_re: Regex = Regex::new(r"Light: (\d+\.?\d*);").unwrap();
    let press_re: Regex = Regex::new(r"Press: (\d+\.?\d*) hPa;").unwrap();
    let humidity_re: Regex = Regex::new(r"Humidity: (\d+?)%;").unwrap();

    if let Some(cap) = temperature_re.captures(&data) {
        let _ = window.emit("Temp", &cap[1]);
    }
    if let Some(cap) = rain_re.captures(&data) {
        let rain_adc: f64 = (&cap[1]).trim().parse().expect("");
        let rain_ph: f64 = (3940.0 - rain_adc).abs() / 100.0;
        let rain: f64 = if rain_ph > 1.0 { rain_ph } else { 0.0 };
        let _ = window.emit("Rain", format!("{:.2}", rain));
    }
    if let Some(cap) = light_re.captures(&data) {
        let light_adc: f64 = (&cap[1]).trim().parse().expect("");
        let v_adc_mv: f64 = light_adc * 3300.0 / 4095.0;
        let r_lux: f64 = 10000.0 * (v_adc_mv / (3300.0 - v_adc_mv));
        let lux: f64 = 10.0 * (7500.0 / r_lux).powi(2);
        let _ = window.emit("Light", format!("{:.2}", lux));
    }
    if let Some(cap) = press_re.captures(&data) {
        let press_adc: f64 = (&cap[1]).trim().parse().expect("");
        let _ = window.emit("Pres", format!("{:.2}", press_adc));
    }
    if let Some(cap) = humidity_re.captures(&data) {
        let _ = window.emit("Humi", &cap[1]);
    }
}

// TCP通信
fn recive_data(stream: &TcpStream) -> String {
    let mut stream = stream;
    let mut buffer = [0; 128];
    let size = stream.read(&mut buffer).unwrap();
    String::from_utf8_lossy(&mut buffer[..size]).to_string()
}

fn send_data(stream: &TcpStream, data: &[u8]) -> bool {
    let mut stream = stream;
    match stream.write(&data) {
        Ok(_) => true,
        Err(e) => {
            println!("发送失败，{}", e.to_string());
            false
        }
    }
}

fn tcp_receive_handle(window: tauri::Window, stream: TcpStream) {
    std::thread::spawn(move || loop {
        if !*OPENING.lock().unwrap() {
            let _ = window.emit("connect-status", "false");
            break;
        }
        let data = recive_data(&stream);
        let _ = window.emit("serial-data", &data);
        analy_data(&window, data.clone());
    });
}

fn tcp_send_handle(stream: TcpStream) {
    std::thread::spawn(move || loop {
        if !*OPENING.lock().unwrap() {
            send_data(&stream, b"Disconnect");
            break;
        }
        if !(*CMD_BUFFER.lock().unwrap()).is_empty() {
            send_data(&stream, (*CMD_BUFFER.lock().unwrap()).as_bytes());
            (*CMD_BUFFER.lock().unwrap()).clear();
        }
    });
}

fn start_tcp_server(window: tauri::Window, addr: String) {
    match TcpStream::connect(addr) {
        Ok(send_stream) => {
            let receive_stream = send_stream.try_clone().unwrap();
            //与服务端握手认证
            send_data(&send_stream, b"It's from LoRaForest Client!");
            let data = recive_data(&send_stream);
            if data == "It's from LoRaForest Server!" {
                send_data(&send_stream, b"Ok");
                set_connecton_status(true);
                let _ = window.emit("connect-status", "true");
                tcp_receive_handle(window, receive_stream);
                tcp_send_handle(send_stream);
            }
        }
        Err(e) => {
            let _ = window.emit("error", e.to_string());
        }
    };
}

fn close_check() {
    if *OPENING.lock().unwrap() {
        *OPENING.lock().unwrap() = false;
        //因为是异步线程，等待一下确保TCP线程被关闭
        std::thread::sleep(Duration::from_millis(200));
    }
}

#[tauri::command]
fn start_reading(window: tauri::Window, port: String) {
    start_tcp_server(window, port);
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .setup(|app| {
            if let Some(window) = app.get_webview_window("main") {
                window.on_window_event(move |event| {
                    if let WindowEvent::CloseRequested { .. } = event {
                        //在软件关闭前检查连接是否断开，否则会导致服务端错误
                        close_check();
                    }
                });
            }
            Ok(())
        })
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![
            start_reading,
            set_connecton_status
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
