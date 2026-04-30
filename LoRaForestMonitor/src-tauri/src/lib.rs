use std::time::Duration;
// Learn more about Tauri commands at https://tauri.app/develop/calling-rust/
use serialport::{self, DataBits, Parity, StopBits};
use tauri::Emitter;
use regex::Regex;

#[tauri::command]
fn openSerialPort(port: String) -> bool {
    match serialport::new(port, 115200).open(){
        Ok(port) => true,
        Err(e) => false
    }
}

//用于解析数据
fn analy_data(window: &tauri::Window, data: &String){
    let temperature_re = Regex::new(r"Temperature:(\d+)C").unwrap();
    let rain_re = Regex::new(r"Rain:(\d+),(.*)V").unwrap();
    let light_re = Regex::new(r"Light:(\d+);(.*)V").unwrap();
    let press_re = Regex::new(r"Press: (.*) hPa").unwrap();
    let humidity_re = Regex::new(r"Humidity: (\d+)%").unwrap();

    if let Some(cap) = temperature_re.captures(&data){
        window.emit("Temp", &cap[1]);
    }
    if let Some(cap) = rain_re.captures(&data){
        let rain_adc: f64 = (&cap[2]).trim().parse().expect("");
        window.emit("Rain",format!("{:.2}", rain_adc * 33000.0 / 4095.0));
    }
    if let Some(cap) = light_re.captures(&data){
        let light_adc: f64 = (&cap[2]).trim().parse().expect("");
        let vol = light_adc * 3000.0 / 4095.0;
        let lux = 110.0 / vol;
        window.emit("Light", format!("{:.2}", lux));
    }
    if let Some(cap) = press_re.captures(&data){
        let press_adc: f64 = (&cap[1]).trim().parse().expect("");
        window.emit("Pres",format!("{:.2}", press_adc / 10.0 + 900.0));
    }
    if let Some(cap) = humidity_re.captures(&data){
        window.emit("Humi", &cap[1]);
    }
}

#[tauri::command]
fn start_reading(window: tauri::Window, port: String) -> bool{
    let serial_port = serialport::new(port, 115200)
    .data_bits(DataBits::Eight)
    .stop_bits(StopBits::One)
    .timeout(Duration::from_millis(1000))
    .parity(Parity::None);
    match serial_port.open(){
        Ok(mut p) =>{
            std::thread::spawn(move ||{
            let mut raw_buffer: [u8; 64] = [0; 64];
            let mut line_Buffer: String = String::new();
            loop{
                match &mut p.read(&mut raw_buffer){
                    Ok(len) =>{
                        if *len > 0 {
                            let chunk = String::from_utf8_lossy(&mut raw_buffer[0..*len]);
                            line_Buffer.push_str(&chunk);
                            if let Some(newLine) = line_Buffer.find("\n"){
                                let complete_line = line_Buffer.drain(..=newLine).collect::<String>();
                                if !complete_line.is_empty(){
                                    window.emit("serial-data", &complete_line);
                                    analy_data(&window, &complete_line);
                                }
                            }
                        }
                    },
                    Err(e) =>{}
                }
            }
        });
        return true;
        },
        Err(e) => false
    }
}

#[tauri::command]
fn getAvailabelPorts() -> Vec<String> {
    let mut Ports_name = Vec::new();
    match serialport::available_ports() {
        Ok(ports) => {
            for p in ports {
                Ports_name.push(p.port_name);
            }
        },
        Err(e) => print!("Err")
    }
    Ports_name
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![openSerialPort, getAvailabelPorts,start_reading])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
