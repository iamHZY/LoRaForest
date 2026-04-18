using System.IO.Ports;

namespace SerialTool;

public partial class Form1 : Form
{
    private bool isPortOpen = false;
    private List<string> receivedData = new List<string>();

    public Form1()
    {
        InitializeComponent();
        InitializeComboBoxes();
        SubscribeEvents();
        UpdateControls();
    }

    private void InitializeComboBoxes()
    {
        // 填充串口列表
        comboBoxPort.Items.Clear();
        comboBoxPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        if (comboBoxPort.Items.Count > 0)
            comboBoxPort.SelectedIndex = 0;

        // 设置默认值
        numericUpDownBaudRate.Value = 9600;
        comboBoxDataBits.SelectedItem = "8";
        comboBoxStopBits.SelectedItem = "1";
        comboBoxParity.SelectedItem = "None";
        comboBoxTimeout.SelectedItem = "1000";
        comboBoxDisplayMode.SelectedItem = "ASCII";

        // 设置单选按钮
        radioButtonReceive.Checked = true;
    }

    private void SubscribeEvents()
    {
        buttonOpenClose.Click += ButtonOpenClose_Click;
        buttonSend.Click += ButtonSend_Click;
        buttonSave.Click += ButtonSave_Click;
        radioButtonReceive.CheckedChanged += RadioButton_CheckedChanged;
        radioButtonSend.CheckedChanged += RadioButton_CheckedChanged;
        serialPort.DataReceived += SerialPort_DataReceived;
    }

    private void UpdateControls()
    {
        bool isReceiveMode = radioButtonReceive.Checked;
        textBoxReceived.Enabled = isReceiveMode;
        buttonSave.Enabled = isReceiveMode && receivedData.Count > 0;
        textBoxSend.Enabled = !isReceiveMode;
        buttonSend.Enabled = !isReceiveMode;

        buttonOpenClose.Text = isPortOpen ? "关闭串口" : "打开串口";
        comboBoxPort.Enabled = !isPortOpen;
        numericUpDownBaudRate.Enabled = !isPortOpen;
        comboBoxDataBits.Enabled = !isPortOpen;
        comboBoxStopBits.Enabled = !isPortOpen;
        comboBoxParity.Enabled = !isPortOpen;
        comboBoxTimeout.Enabled = !isPortOpen;
        comboBoxDisplayMode.Enabled = !isPortOpen;
        radioButtonReceive.Enabled = !isPortOpen;
        radioButtonSend.Enabled = !isPortOpen;
    }

    private void RadioButton_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateControls();
    }

    private void ButtonOpenClose_Click(object? sender, EventArgs e)
    {
        if (isPortOpen)
        {
            ClosePort();
        }
        else
        {
            OpenPort();
        }
        UpdateControls();
    }

    private void OpenPort()
    {
        if (comboBoxPort.SelectedItem == null)
        {
            MessageBox.Show("请选择串口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            serialPort.PortName = comboBoxPort.SelectedItem.ToString();
            serialPort.BaudRate = (int)numericUpDownBaudRate.Value;
            serialPort.DataBits = int.Parse(comboBoxDataBits.SelectedItem?.ToString() ?? "8");
            serialPort.StopBits = GetStopBits(comboBoxStopBits.SelectedItem?.ToString());
            serialPort.Parity = GetParity(comboBoxParity.SelectedItem?.ToString());
            serialPort.ReadTimeout = int.Parse(comboBoxTimeout.SelectedItem?.ToString() ?? "1000");
            serialPort.WriteTimeout = serialPort.ReadTimeout;

            serialPort.Open();
            isPortOpen = true;
            AppendReceivedText($"[{DateTime.Now:HH:mm:ss}] 串口已打开\r\n");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"打开串口失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClosePort()
    {
        try
        {
            serialPort.Close();
            isPortOpen = false;
            AppendReceivedText($"[{DateTime.Now:HH:mm:ss}] 串口已关闭\r\n");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"关闭串口失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private StopBits GetStopBits(string? stopBits)
    {
        return stopBits switch
        {
            "1.5" => StopBits.OnePointFive,
            "2" => StopBits.Two,
            _ => StopBits.One,
        };
    }

    private Parity GetParity(string? parity)
    {
        return parity switch
        {
            "Odd" => Parity.Odd,
            "Even" => Parity.Even,
            "Mark" => Parity.Mark,
            "Space" => Parity.Space,
            _ => Parity.None,
        };
    }

    private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        try
        {
            int bytesToRead = serialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            serialPort.Read(buffer, 0, bytesToRead);

            // 跨线程更新UI
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessReceivedData(buffer)));
            }
            else
            {
                ProcessReceivedData(buffer);
            }
        }
        catch (Exception ex)
        {
            // 日志错误
            AppendReceivedText($"[{DateTime.Now:HH:mm:ss}] 接收错误: {ex.Message}\r\n");
        }
    }

    private void ProcessReceivedData(byte[] data)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string dataString;

        if (comboBoxDisplayMode.SelectedItem?.ToString() == "Hex")
        {
            dataString = BitConverter.ToString(data).Replace("-", " ");
        }
        else
        {
            dataString = System.Text.Encoding.ASCII.GetString(data);
            // 替换控制字符为可见表示
            dataString = new string(dataString.Select(c => char.IsControl(c) ? '.' : c).ToArray());
        }

        string line = $"{timestamp},{dataString}";
        receivedData.Add(line);
        AppendReceivedText($"[{DateTime.Now:HH:mm:ss}] {dataString}\r\n");
    }

    private void AppendReceivedText(string text)
    {
        if (textBoxReceived.InvokeRequired)
        {
            textBoxReceived.Invoke(new Action(() => AppendReceivedText(text)));
            return;
        }

        textBoxReceived.AppendText(text);
        // 自动滚动
        textBoxReceived.SelectionStart = textBoxReceived.Text.Length;
        textBoxReceived.ScrollToCaret();
    }

    private void ButtonSend_Click(object? sender, EventArgs e)
    {
        if (!isPortOpen)
        {
            MessageBox.Show("请先打开串口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string text = textBoxSend.Text.Trim();
        if (string.IsNullOrEmpty(text))
        {
            MessageBox.Show("发送内容不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            byte[] data;
            if (comboBoxDisplayMode.SelectedItem?.ToString() == "Hex")
            {
                // 尝试解析十六进制字符串，例如 "41 42 43" 或 "414243"
                string hex = text.Replace(" ", "").Replace("-", "");
                if (hex.Length % 2 != 0)
                {
                    MessageBox.Show("十六进制数据长度必须为偶数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                data = new byte[hex.Length / 2];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
            }
            else
            {
                data = System.Text.Encoding.ASCII.GetBytes(text);
            }

            serialPort.Write(data, 0, data.Length);
            AppendReceivedText($"[{DateTime.Now:HH:mm:ss}] [发送] {text}\r\n");
            textBoxSend.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"发送失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ButtonSave_Click(object? sender, EventArgs e)
    {
        if (receivedData.Count == 0)
        {
            MessageBox.Show("没有数据可保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*";
        saveFileDialog.DefaultExt = "csv";
        saveFileDialog.FileName = $"serial_data_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                // 添加表头
                var lines = new List<string> { "Timestamp,Data" };
                lines.AddRange(receivedData);
                File.WriteAllLines(saveFileDialog.FileName, lines);
                MessageBox.Show($"数据已保存到 {saveFileDialog.FileName}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (isPortOpen)
        {
            ClosePort();
        }
        base.OnFormClosing(e);
    }
}
