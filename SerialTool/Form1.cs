using System;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;

namespace SerialTool;

public partial class Form1 : Form
{
    private bool isConnected = false;
    private StringBuilder receivedBuffer = new StringBuilder();

    // 传感器数据
    private double lightValue = 0; // Lux
    private double rainValue = 0;  // mm/h
    private double temperatureValue = 0; // °C
    private double pressureValue = 0; // hPa
    private double humidityValue = 0; // %

    public Form1()
    {
        InitializeComponent();
        SubscribeEvents();
        UpdateStatus();
    }

    private void SubscribeEvents()
    {
        buttonConnect.Click += ButtonConnect_Click;
        serialPort.DataReceived += SerialPort_DataReceived;
    }

    private void UpdateStatus()
    {
        buttonConnect.Text = isConnected ? "断开连接" : "连接";
        labelStatus.Text = isConnected ? $"状态: 已连接到 {serialPort.PortName}" : "状态: 未连接";
    }

    private void ButtonConnect_Click(object? sender, EventArgs e)
    {
        if (isConnected)
        {
            Disconnect();
        }
        else
        {
            Connect();
        }
        UpdateStatus();
    }

    private void Connect()
    {
        try
        {
            // 串口参数已在Designer中固定设置
            serialPort.Open();
            isConnected = true;
            ClearSensorDisplays();
            ClearRawData();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Disconnect()
    {
        try
        {
            serialPort.Close();
            isConnected = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"断开连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearSensorDisplays()
    {
        UpdateTextBox(textBoxLight, "N/A");
        UpdateTextBox(textBoxRain, "N/A");
        UpdateTextBox(textBoxTemperature, "N/A");
        UpdateTextBox(textBoxPressure, "N/A");
        UpdateTextBox(textBoxWindSpeed, "N/A");
        UpdateTextBox(textBoxHumidity, "N/A");
    }

    private void ClearRawData()
    {
        if (textBoxRawData.InvokeRequired)
        {
            textBoxRawData.Invoke(new Action(() => ClearRawData()));
            return;
        }

        textBoxRawData.Clear();
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            int bytesToRead = serialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            serialPort.Read(buffer, 0, bytesToRead);

            string data = Encoding.ASCII.GetString(buffer);

            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessReceivedData(data)));
            }
            else
            {
                ProcessReceivedData(data);
            }
        }
        catch (Exception ex)
        {
            UpdateStatusLabel($"接收错误: {ex.Message}");
        }
    }

    private void ProcessReceivedData(string data)
    {
        // 显示原始数据用于调试
        AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 收到: {data}\r\n");

        // 添加到缓冲区
        receivedBuffer.Append(data);

        // 按行分割处理
        string bufferStr = receivedBuffer.ToString();
        string[] lines = bufferStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // 处理完整的行
        foreach (string line in lines)
        {
            ParseSensorData(line);
        }

        // 保留未处理完的部分
        if (bufferStr.EndsWith("\r") || bufferStr.EndsWith("\n"))
        {
            receivedBuffer.Clear();
        }
        else if (lines.Length > 0)
        {
            // 保留最后一行不完整的部分
            receivedBuffer.Clear();
            receivedBuffer.Append(lines[lines.Length - 1]);
        }
    }

    private void ParseSensorData(string line)
    {
        bool matched = false;

        // 使用正则表达式匹配各种传感器数据格式
        // 光照: "Light: adc读到的值;电压值"
        // 降雨: "Rain: adc的值;电压值"
        // 气压: "Press: adc的值;"
        // 温度: "Temperature:温度值C"
        // 湿度: "Humidity: 数值%"

        // 光照强度
        Match lightMatch = Regex.Match(line, @"Light:\s*([\d\.]+);([\d\.]+)");
        if (lightMatch.Success)
        {
            matched = true;
            if (double.TryParse(lightMatch.Groups[1].Value, out double adcValue) &&
                double.TryParse(lightMatch.Groups[2].Value, out double voltageValue))
            {
                // TODO: 根据ADC值和电压值计算光照强度(Lux)
                // 假设转换公式: Lux = (voltage * 1000) / 2.0  (示例公式)
                lightValue = (voltageValue * 1000) / 2.0;
                UpdateTextBox(textBoxLight, $"{lightValue:F1} Lux");
                AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 解析光照: ADC={adcValue}, 电压={voltageValue}V => {lightValue:F1} Lux\r\n");
            }
        }

        // 降雨量 - 放宽匹配模式，可能没有分号或格式不同
        Match rainMatch = Regex.Match(line, @"Rain:\s*([\d\.]+)[;\s]*([\d\.]*)");
        if (rainMatch.Success)
        {
            matched = true;
            if (double.TryParse(rainMatch.Groups[1].Value, out double adcValue))
            {
                // 尝试解析电压值（可能不存在）
                double voltageValue = 0;
                if (rainMatch.Groups.Count > 2 && !string.IsNullOrEmpty(rainMatch.Groups[2].Value))
                {
                    double.TryParse(rainMatch.Groups[2].Value, out voltageValue);
                }

                // TODO: 根据ADC值和电压值计算降雨量(mm/h)
                // 假设转换公式: mm/h = voltage * 10.0  (示例公式)
                rainValue = voltageValue * 10.0;
                UpdateTextBox(textBoxRain, $"{rainValue:F2} mm/h");
                AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 解析降雨: ADC={adcValue}, 电压={voltageValue}V => {rainValue:F2} mm/h\r\n");
            }
        }

        // 气压 - 放宽匹配模式，可能没有分号或格式不同
        Match pressureMatch = Regex.Match(line, @"Press:\s*-?([\d\.]+)[;\s]*");
        if (pressureMatch.Success)
        {
            matched = true;
            if (double.TryParse(pressureMatch.Groups[1].Value, out double adcValue))
            {
                // TODO: 根据ADC值计算气压(hPa)
                // 假设转换公式: hPa = adcValue * 0.1 + 900  (示例公式)
                pressureValue = adcValue * 0.1 + 900;
                UpdateTextBox(textBoxPressure, $"{pressureValue:F1} hPa");
                AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 解析气压: ADC={adcValue} => {pressureValue:F1} hPa\r\n");
            }
        }

        // 温度
        Match tempMatch = Regex.Match(line, @"Temperature:\s*([\d\.]+)C");
        if (tempMatch.Success)
        {
            matched = true;
            if (double.TryParse(tempMatch.Groups[1].Value, out double temp))
            {
                temperatureValue = temp;
                UpdateTextBox(textBoxTemperature, $"{temperatureValue:F1} °C");
                AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 解析温度: {temperatureValue:F1} °C\r\n");
            }
        }

        // 湿度
        Match humidityMatch = Regex.Match(line, @"Humidity:\s*([\d\.]+)%");
        if (humidityMatch.Success)
        {
            matched = true;
            if (double.TryParse(humidityMatch.Groups[1].Value, out double humidity))
            {
                humidityValue = humidity;
                UpdateTextBox(textBoxHumidity, $"{humidityValue:F1} %");
                AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 解析湿度: {humidityValue:F1} %\r\n");
            }
        }

        // 风速 (暂未解析，保留占位)
        // Match windMatch = Regex.Match(line, @"WindSpeed:\s*([\d\.]+)m/s");
        // if (windMatch.Success) { ... }

        // 如果没有匹配任何模式，显示原始行用于调试
        if (!matched)
        {
            AppendRawDataText($"[{DateTime.Now:HH:mm:ss}] 未匹配: {line}\r\n");
        }
    }

    private void UpdateTextBox(TextBox textBox, string value)
    {
        if (textBox.InvokeRequired)
        {
            textBox.Invoke(new Action(() => UpdateTextBox(textBox, value)));
            return;
        }

        textBox.Text = value;
    }

    private void AppendRawDataText(string text)
    {
        if (textBoxRawData.InvokeRequired)
        {
            textBoxRawData.Invoke(new Action(() => AppendRawDataText(text)));
            return;
        }

        textBoxRawData.AppendText(text);
        // 自动滚动
        textBoxRawData.SelectionStart = textBoxRawData.Text.Length;
        textBoxRawData.ScrollToCaret();
    }

    private void UpdateStatusLabel(string message)
    {
        if (labelStatus.InvokeRequired)
        {
            labelStatus.Invoke(new Action(() => UpdateStatusLabel(message)));
            return;
        }

        labelStatus.Text = $"状态: {message}";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (isConnected)
        {
            Disconnect();
        }
        base.OnFormClosing(e);
    }
}
