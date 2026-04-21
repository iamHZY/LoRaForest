namespace SerialTool;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private System.Windows.Forms.TableLayoutPanel mainTableLayout;
    private System.Windows.Forms.GroupBox parametersGroupBox;
    private System.Windows.Forms.TableLayoutPanel sensorsTableLayout;
    private System.Windows.Forms.Label labelLight;
    private System.Windows.Forms.TextBox textBoxLight;
    private System.Windows.Forms.Label labelRain;
    private System.Windows.Forms.TextBox textBoxRain;
    private System.Windows.Forms.Label labelTemperature;
    private System.Windows.Forms.TextBox textBoxTemperature;
    private System.Windows.Forms.Label labelPressure;
    private System.Windows.Forms.TextBox textBoxPressure;
    private System.Windows.Forms.Label labelWindSpeed;
    private System.Windows.Forms.TextBox textBoxWindSpeed;
    private System.Windows.Forms.Label labelHumidity;
    private System.Windows.Forms.TextBox textBoxHumidity;
    private System.Windows.Forms.Label labelStatus;
    private System.Windows.Forms.Button buttonConnect;
    private System.Windows.Forms.GroupBox groupBoxRawData;
    private System.Windows.Forms.TextBox textBoxRawData;
    private System.IO.Ports.SerialPort serialPort;

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.parametersGroupBox = new System.Windows.Forms.GroupBox();
        this.sensorsTableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.labelLight = new System.Windows.Forms.Label();
        this.textBoxLight = new System.Windows.Forms.TextBox();
        this.labelRain = new System.Windows.Forms.Label();
        this.textBoxRain = new System.Windows.Forms.TextBox();
        this.labelTemperature = new System.Windows.Forms.Label();
        this.textBoxTemperature = new System.Windows.Forms.TextBox();
        this.labelPressure = new System.Windows.Forms.Label();
        this.textBoxPressure = new System.Windows.Forms.TextBox();
        this.labelWindSpeed = new System.Windows.Forms.Label();
        this.textBoxWindSpeed = new System.Windows.Forms.TextBox();
        this.labelHumidity = new System.Windows.Forms.Label();
        this.textBoxHumidity = new System.Windows.Forms.TextBox();
        this.labelStatus = new System.Windows.Forms.Label();
        this.buttonConnect = new System.Windows.Forms.Button();
        this.groupBoxRawData = new System.Windows.Forms.GroupBox();
        this.textBoxRawData = new System.Windows.Forms.TextBox();
        this.serialPort = new System.IO.Ports.SerialPort(this.components);
        this.mainTableLayout.SuspendLayout();
        this.parametersGroupBox.SuspendLayout();
        this.sensorsTableLayout.SuspendLayout();
        this.SuspendLayout();
        //
        // mainTableLayout
        //
        this.mainTableLayout.ColumnCount = 1;
        this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainTableLayout.Controls.Add(this.parametersGroupBox, 0, 0);
        this.mainTableLayout.Controls.Add(this.groupBoxRawData, 0, 1);
        this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
        this.mainTableLayout.Name = "mainTableLayout";
        this.mainTableLayout.RowCount = 2;
        this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
        this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
        this.mainTableLayout.Size = new System.Drawing.Size(1000, 700);
        this.mainTableLayout.TabIndex = 0;
        //
        // parametersGroupBox
        //
        this.parametersGroupBox.Controls.Add(this.sensorsTableLayout);
        this.parametersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.parametersGroupBox.Location = new System.Drawing.Point(3, 3);
        this.parametersGroupBox.Name = "parametersGroupBox";
        this.parametersGroupBox.Size = new System.Drawing.Size(994, 134);
        this.parametersGroupBox.TabIndex = 0;
        this.parametersGroupBox.TabStop = false;
        this.parametersGroupBox.Text = "环境参数监测";
        //
        // sensorsTableLayout
        //
        this.sensorsTableLayout.ColumnCount = 6;
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
        this.sensorsTableLayout.Controls.Add(this.labelLight, 0, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxLight, 0, 1);
        this.sensorsTableLayout.Controls.Add(this.labelRain, 1, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxRain, 1, 1);
        this.sensorsTableLayout.Controls.Add(this.labelTemperature, 2, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxTemperature, 2, 1);
        this.sensorsTableLayout.Controls.Add(this.labelPressure, 3, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxPressure, 3, 1);
        this.sensorsTableLayout.Controls.Add(this.labelWindSpeed, 4, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxWindSpeed, 4, 1);
        this.sensorsTableLayout.Controls.Add(this.labelHumidity, 5, 0);
        this.sensorsTableLayout.Controls.Add(this.textBoxHumidity, 5, 1);
        this.sensorsTableLayout.Controls.Add(this.labelStatus, 0, 2);
        this.sensorsTableLayout.Controls.Add(this.buttonConnect, 5, 2);
        this.sensorsTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.sensorsTableLayout.Location = new System.Drawing.Point(3, 19);
        this.sensorsTableLayout.Name = "sensorsTableLayout";
        this.sensorsTableLayout.RowCount = 3;
        this.sensorsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
        this.sensorsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
        this.sensorsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.sensorsTableLayout.Size = new System.Drawing.Size(988, 112);
        this.sensorsTableLayout.TabIndex = 0;
        //
        // labelLight
        //
        this.labelLight.AutoSize = true;
        this.labelLight.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelLight.Location = new System.Drawing.Point(3, 0);
        this.labelLight.Name = "labelLight";
        this.labelLight.Size = new System.Drawing.Size(158, 25);
        this.labelLight.TabIndex = 0;
        this.labelLight.Text = "光照强度 (Lux):";
        this.labelLight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxLight
        //
        this.textBoxLight.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxLight.Location = new System.Drawing.Point(3, 28);
        this.textBoxLight.Name = "textBoxLight";
        this.textBoxLight.ReadOnly = true;
        this.textBoxLight.Size = new System.Drawing.Size(158, 23);
        this.textBoxLight.TabIndex = 1;
        this.textBoxLight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelRain
        //
        this.labelRain.AutoSize = true;
        this.labelRain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelRain.Location = new System.Drawing.Point(167, 0);
        this.labelRain.Name = "labelRain";
        this.labelRain.Size = new System.Drawing.Size(158, 25);
        this.labelRain.TabIndex = 2;
        this.labelRain.Text = "降雨量 (mm/h):";
        this.labelRain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxRain
        //
        this.textBoxRain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxRain.Location = new System.Drawing.Point(167, 28);
        this.textBoxRain.Name = "textBoxRain";
        this.textBoxRain.ReadOnly = true;
        this.textBoxRain.Size = new System.Drawing.Size(158, 23);
        this.textBoxRain.TabIndex = 3;
        this.textBoxRain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelTemperature
        //
        this.labelTemperature.AutoSize = true;
        this.labelTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelTemperature.Location = new System.Drawing.Point(331, 0);
        this.labelTemperature.Name = "labelTemperature";
        this.labelTemperature.Size = new System.Drawing.Size(158, 25);
        this.labelTemperature.TabIndex = 4;
        this.labelTemperature.Text = "气温 (°C):";
        this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxTemperature
        //
        this.textBoxTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxTemperature.Location = new System.Drawing.Point(331, 28);
        this.textBoxTemperature.Name = "textBoxTemperature";
        this.textBoxTemperature.ReadOnly = true;
        this.textBoxTemperature.Size = new System.Drawing.Size(158, 23);
        this.textBoxTemperature.TabIndex = 5;
        this.textBoxTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelPressure
        //
        this.labelPressure.AutoSize = true;
        this.labelPressure.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelPressure.Location = new System.Drawing.Point(495, 0);
        this.labelPressure.Name = "labelPressure";
        this.labelPressure.Size = new System.Drawing.Size(158, 25);
        this.labelPressure.TabIndex = 6;
        this.labelPressure.Text = "气压 (hPa):";
        this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxPressure
        //
        this.textBoxPressure.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxPressure.Location = new System.Drawing.Point(495, 28);
        this.textBoxPressure.Name = "textBoxPressure";
        this.textBoxPressure.ReadOnly = true;
        this.textBoxPressure.Size = new System.Drawing.Size(158, 23);
        this.textBoxPressure.TabIndex = 7;
        this.textBoxPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelWindSpeed
        //
        this.labelWindSpeed.AutoSize = true;
        this.labelWindSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelWindSpeed.Location = new System.Drawing.Point(659, 0);
        this.labelWindSpeed.Name = "labelWindSpeed";
        this.labelWindSpeed.Size = new System.Drawing.Size(158, 25);
        this.labelWindSpeed.TabIndex = 8;
        this.labelWindSpeed.Text = "风速 (m/s):";
        this.labelWindSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxWindSpeed
        //
        this.textBoxWindSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxWindSpeed.Location = new System.Drawing.Point(659, 28);
        this.textBoxWindSpeed.Name = "textBoxWindSpeed";
        this.textBoxWindSpeed.ReadOnly = true;
        this.textBoxWindSpeed.Size = new System.Drawing.Size(158, 23);
        this.textBoxWindSpeed.TabIndex = 9;
        this.textBoxWindSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelHumidity
        //
        this.labelHumidity.AutoSize = true;
        this.labelHumidity.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelHumidity.Location = new System.Drawing.Point(823, 0);
        this.labelHumidity.Name = "labelHumidity";
        this.labelHumidity.Size = new System.Drawing.Size(162, 25);
        this.labelHumidity.TabIndex = 10;
        this.labelHumidity.Text = "空气湿度 (%):";
        this.labelHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // textBoxHumidity
        //
        this.textBoxHumidity.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxHumidity.Location = new System.Drawing.Point(823, 28);
        this.textBoxHumidity.Name = "textBoxHumidity";
        this.textBoxHumidity.ReadOnly = true;
        this.textBoxHumidity.Size = new System.Drawing.Size(162, 23);
        this.textBoxHumidity.TabIndex = 11;
        this.textBoxHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        //
        // labelStatus
        //
        this.labelStatus.AutoSize = true;
        this.sensorsTableLayout.SetColumnSpan(this.labelStatus, 5);
        this.labelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelStatus.Location = new System.Drawing.Point(3, 55);
        this.labelStatus.Name = "labelStatus";
        this.labelStatus.Size = new System.Drawing.Size(812, 57);
        this.labelStatus.TabIndex = 12;
        this.labelStatus.Text = "状态: 未连接";
        this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // buttonConnect
        //
        this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
        this.buttonConnect.Location = new System.Drawing.Point(823, 58);
        this.buttonConnect.Name = "buttonConnect";
        this.buttonConnect.Size = new System.Drawing.Size(162, 51);
        this.buttonConnect.TabIndex = 13;
        this.buttonConnect.Text = "连接";
        this.buttonConnect.UseVisualStyleBackColor = true;
        //
        // groupBoxRawData
        //
        this.groupBoxRawData.SuspendLayout();
        this.groupBoxRawData.Controls.Add(this.textBoxRawData);
        this.groupBoxRawData.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBoxRawData.Location = new System.Drawing.Point(3, 143);
        this.groupBoxRawData.Name = "groupBoxRawData";
        this.groupBoxRawData.Size = new System.Drawing.Size(994, 554);
        this.groupBoxRawData.TabIndex = 1;
        this.groupBoxRawData.TabStop = false;
        this.groupBoxRawData.Text = "原始数据（调试用）";
        //
        // textBoxRawData
        //
        this.textBoxRawData.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBoxRawData.Location = new System.Drawing.Point(3, 19);
        this.textBoxRawData.Multiline = true;
        this.textBoxRawData.Name = "textBoxRawData";
        this.textBoxRawData.ReadOnly = true;
        this.textBoxRawData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBoxRawData.Size = new System.Drawing.Size(988, 532);
        this.textBoxRawData.TabIndex = 0;
        //
        // serialPort
        //
        this.serialPort.BaudRate = 115200;
        this.serialPort.PortName = "COM6";
        this.serialPort.DataBits = 8;
        this.serialPort.Parity = System.IO.Ports.Parity.None;
        this.serialPort.StopBits = System.IO.Ports.StopBits.One;
        this.serialPort.ReadTimeout = 1000;
        this.serialPort.WriteTimeout = 1000;
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000, 700);
        this.Controls.Add(this.mainTableLayout);
        this.Name = "Form1";
        this.Text = "LoRa Forest 环境监测上位机";
        this.mainTableLayout.ResumeLayout(false);
        this.parametersGroupBox.ResumeLayout(false);
        this.sensorsTableLayout.ResumeLayout(false);
        this.sensorsTableLayout.PerformLayout();
        this.groupBoxRawData.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion
}
