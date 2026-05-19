use std::{env, fs::{File, OpenOptions}, io::Write, path::Path, sync::Mutex, time::Duration};
use chrono::Local;
use serialport::{self, DataBits, Parity, StopBits};
use tauri::Emitter;
use regex::Regex;

static OPENING: Mutex<bool> = Mutex::new(false);

#[tauri::command]
fn set_connecton_status(sta: bool){
    let mut flag = OPENING.lock().unwrap();
    *flag = sta;
}

//用于解析数据
fn analy_data(window: &tauri::Window, data: &String){

    //正则表达式
    let temperature_re: Regex = Regex::new(r"Temperature: (\d+?)C;").unwrap();
    let rain_re: Regex = Regex::new(r"Rain: (\d+?);").unwrap();
    let light_re: Regex = Regex::new(r"Light: (\d+\.?\d*);").unwrap();
    let press_re: Regex = Regex::new(r"Press: (\d+\.?\d*) hPa;").unwrap();
    let humidity_re: Regex = Regex::new(r"Humidity: (\d+?)%;").unwrap();

    if let Some(cap) = temperature_re.captures(&data){
        let _ = window.emit("Temp", &cap[1]);
    }
    if let Some(cap) = rain_re.captures(&data){
        let rain_adc: f64 = (&cap[1]).trim().parse().expect("");
        let rain_ph:f64 = (3940.0 - rain_adc ).abs() / 100.0;
        let rain:f64 = if rain_ph > 1.0 {rain_ph} else {0.0};
        let _ = window.emit("Rain",format!("{:.2}", rain));
    }
    if let Some(cap) = light_re.captures(&data){
        let light_adc: f64 = (&cap[1]).trim().parse().expect("");
        let v_adc_mv: f64 = light_adc * 3300.0 / 4095.0;
        let r_lux: f64 = 10000.0 * (v_adc_mv / (3300.0 - v_adc_mv));
        let lux: f64 = 10.0 * (7500.0 / r_lux).powi(2);
        let _ = window.emit("Light", format!("{:.2}", lux));
    }
    if let Some(cap) = press_re.captures(&data){
        let press_adc: f64 = (&cap[1]).trim().parse().expect("");
        let _ = window.emit("Pres",format!("{:.2}", press_adc));
    }
    if let Some(cap) = humidity_re.captures(&data){
        let _ = window.emit("Humi", &cap[1]);
    }
}

#[tauri::command]
fn start_reading(window: tauri::Window, port: String) -> bool{
    let serial_port = serialport::new(port, 115200)
    .data_bits(DataBits::Eight)
    .stop_bits(StopBits::One)
    .timeout(Duration::from_millis(1000))
    .parity(Parity::None);
    set_connecton_status(true);
    match serial_port.open(){
        Ok(mut p) => {
            std::thread::spawn(move ||{
            let mut raw_buffer: [u8; 64] = [0; 64];
            let mut line_buffer: String = String::new();
            let savepath = Path::new("data");
            if !savepath.exists() || savepath.is_dir(){
                match File::create(savepath){
                    Ok(_) => {},
                    Err(e) =>{
                        let _ = window.emit("error", e.to_string());
                    }
                }
            }
            let mut sf = OpenOptions::new().append(true).open(savepath).unwrap();
            while *OPENING.lock().unwrap() {
                match &mut p.read(&mut raw_buffer){
                    Ok(len) =>{
                        if *len > 0 {
                            let chunk = String::from_utf8_lossy(&mut raw_buffer[0..*len]);
                            line_buffer.push_str(&chunk);
                            if let Some(new_line) = line_buffer.find("\n"){
                                let now = Local::now().format("[%Y/%m/%d-%H:%M:%S]");
                                let mut complete_line = now.to_string();
                                complete_line.push_str(&line_buffer.drain(..=new_line).collect::<String>());
                                if !complete_line.len() - now.to_string().len() > 0{
                                    let _ = window.emit("serial-data", &complete_line);
                                    sf.write(&complete_line.as_bytes()).unwrap();
                                    analy_data(&window, &complete_line);
                                }
                            }
                        }
                    },
                    Err(e) =>{
                        let _ = window.emit("error", e.to_string());
                    }
                }
            }
        });
        return true;
        },
        Err(e) =>{
            let _ = window.emit("error", e.to_string());
            false
        }
    }
}

#[tauri::command]
fn get_availabel_ports(window: tauri::Window) -> Vec<String> {
    let mut ports_name = Vec::new();
    match serialport::available_ports() {
        Ok(ports) => {
            for p in ports {
                ports_name.push(p.port_name);
            }
        },
        Err(e) =>{
            let _ = window.emit("error", e.to_string());
        }
    }
    ports_name
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![get_availabel_ports, start_reading,set_connecton_status])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
