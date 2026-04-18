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
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label labelPort;
    private System.Windows.Forms.ComboBox comboBoxPort;
    private System.Windows.Forms.Label labelBaudRate;
    private System.Windows.Forms.NumericUpDown numericUpDownBaudRate;
    private System.Windows.Forms.Label labelDataBits;
    private System.Windows.Forms.ComboBox comboBoxDataBits;
    private System.Windows.Forms.Label labelStopBits;
    private System.Windows.Forms.ComboBox comboBoxStopBits;
    private System.Windows.Forms.Label labelParity;
    private System.Windows.Forms.ComboBox comboBoxParity;
    private System.Windows.Forms.Label labelTimeout;
    private System.Windows.Forms.ComboBox comboBoxTimeout;
    private System.Windows.Forms.Label labelDisplayMode;
    private System.Windows.Forms.ComboBox comboBoxDisplayMode;
    private System.Windows.Forms.Button buttonOpenClose;
    private System.Windows.Forms.RadioButton radioButtonReceive;
    private System.Windows.Forms.RadioButton radioButtonSend;
    private System.Windows.Forms.TableLayoutPanel bottomTableLayout;
    private System.Windows.Forms.GroupBox groupBoxReceived;
    private System.Windows.Forms.TextBox textBoxReceived;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.GroupBox groupBoxSend;
    private System.Windows.Forms.TextBox textBoxSend;
    private System.Windows.Forms.Button buttonSend;
    private System.IO.Ports.SerialPort serialPort;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.parametersGroupBox = new System.Windows.Forms.GroupBox();
        this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        this.labelPort = new System.Windows.Forms.Label();
        this.comboBoxPort = new System.Windows.Forms.ComboBox();
        this.labelBaudRate = new System.Windows.Forms.Label();
        this.numericUpDownBaudRate = new System.Windows.Forms.NumericUpDown();
        this.labelDataBits = new System.Windows.Forms.Label();
        this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
        this.labelStopBits = new System.Windows.Forms.Label();
        this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
        this.labelParity = new System.Windows.Forms.Label();
        this.comboBoxParity = new System.Windows.Forms.ComboBox();
        this.labelTimeout = new System.Windows.Forms.Label();
        this.comboBoxTimeout = new System.Windows.Forms.ComboBox();
        this.labelDisplayMode = new System.Windows.Forms.Label();
        this.comboBoxDisplayMode = new System.Windows.Forms.ComboBox();
        this.buttonOpenClose = new System.Windows.Forms.Button();
        this.radioButtonReceive = new System.Windows.Forms.RadioButton();
        this.radioButtonSend = new System.Windows.Forms.RadioButton();
        this.bottomTableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.groupBoxReceived = new System.Windows.Forms.GroupBox();
        this.textBoxReceived = new System.Windows.Forms.TextBox();
        this.buttonSave = new System.Windows.Forms.Button();
        this.groupBoxSend = new System.Windows.Forms.GroupBox();
        this.textBoxSend = new System.Windows.Forms.TextBox();
        this.buttonSend = new System.Windows.Forms.Button();
        this.serialPort = new System.IO.Ports.SerialPort(this.components);
        this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        this.mainTableLayout.SuspendLayout();
        this.parametersGroupBox.SuspendLayout();
        this.flowLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaudRate)).BeginInit();
        this.bottomTableLayout.SuspendLayout();
        this.groupBoxReceived.SuspendLayout();
        this.groupBoxSend.SuspendLayout();
        this.SuspendLayout();
        //
        // mainTableLayout
        //
        this.mainTableLayout.ColumnCount = 1;
        this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainTableLayout.Controls.Add(this.parametersGroupBox, 0, 0);
        this.mainTableLayout.Controls.Add(this.bottomTableLayout, 0, 1);
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
        this.parametersGroupBox.Controls.Add(this.flowLayoutPanel1);
        this.parametersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.parametersGroupBox.Location = new System.Drawing.Point(3, 3);
        this.parametersGroupBox.Name = "parametersGroupBox";
        this.parametersGroupBox.Size = new System.Drawing.Size(994, 134);
        this.parametersGroupBox.TabIndex = 0;
        this.parametersGroupBox.TabStop = false;
        this.parametersGroupBox.Text = "串口参数";
        //
        // flowLayoutPanel1
        //
        this.flowLayoutPanel1.Controls.Add(this.labelPort);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxPort);
        this.flowLayoutPanel1.Controls.Add(this.labelBaudRate);
        this.flowLayoutPanel1.Controls.Add(this.numericUpDownBaudRate);
        this.flowLayoutPanel1.Controls.Add(this.labelDataBits);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxDataBits);
        this.flowLayoutPanel1.Controls.Add(this.labelStopBits);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxStopBits);
        this.flowLayoutPanel1.Controls.Add(this.labelParity);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxParity);
        this.flowLayoutPanel1.Controls.Add(this.labelTimeout);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxTimeout);
        this.flowLayoutPanel1.Controls.Add(this.labelDisplayMode);
        this.flowLayoutPanel1.Controls.Add(this.comboBoxDisplayMode);
        this.flowLayoutPanel1.Controls.Add(this.buttonOpenClose);
        this.flowLayoutPanel1.Controls.Add(this.radioButtonReceive);
        this.flowLayoutPanel1.Controls.Add(this.radioButtonSend);
        this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 19);
        this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        this.flowLayoutPanel1.Size = new System.Drawing.Size(988, 112);
        this.flowLayoutPanel1.TabIndex = 0;
        //
        // labelPort
        //
        this.labelPort.AutoSize = true;
        this.labelPort.Location = new System.Drawing.Point(3, 6);
        this.labelPort.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelPort.Name = "labelPort";
        this.labelPort.Size = new System.Drawing.Size(44, 15);
        this.labelPort.TabIndex = 0;
        this.labelPort.Text = "串口：";
        //
        // comboBoxPort
        //
        this.comboBoxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxPort.FormattingEnabled = true;
        this.comboBoxPort.Location = new System.Drawing.Point(53, 3);
        this.comboBoxPort.Name = "comboBoxPort";
        this.comboBoxPort.Size = new System.Drawing.Size(100, 23);
        this.comboBoxPort.TabIndex = 1;
        //
        // labelBaudRate
        //
        this.labelBaudRate.AutoSize = true;
        this.labelBaudRate.Location = new System.Drawing.Point(159, 6);
        this.labelBaudRate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelBaudRate.Name = "labelBaudRate";
        this.labelBaudRate.Size = new System.Drawing.Size(56, 15);
        this.labelBaudRate.TabIndex = 2;
        this.labelBaudRate.Text = "波特率：";
        //
        // numericUpDownBaudRate
        //
        this.numericUpDownBaudRate.Location = new System.Drawing.Point(221, 3);
        this.numericUpDownBaudRate.Maximum = new decimal(new int[] {
        921600,
        0,
        0,
        0});
        this.numericUpDownBaudRate.Minimum = new decimal(new int[] {
        300,
        0,
        0,
        0});
        this.numericUpDownBaudRate.Name = "numericUpDownBaudRate";
        this.numericUpDownBaudRate.Size = new System.Drawing.Size(120, 23);
        this.numericUpDownBaudRate.TabIndex = 3;
        this.numericUpDownBaudRate.Value = new decimal(new int[] {
        9600,
        0,
        0,
        0});
        //
        // labelDataBits
        //
        this.labelDataBits.AutoSize = true;
        this.labelDataBits.Location = new System.Drawing.Point(347, 6);
        this.labelDataBits.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelDataBits.Name = "labelDataBits";
        this.labelDataBits.Size = new System.Drawing.Size(56, 15);
        this.labelDataBits.TabIndex = 4;
        this.labelDataBits.Text = "数据位：";
        //
        // comboBoxDataBits
        //
        this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxDataBits.FormattingEnabled = true;
        this.comboBoxDataBits.Items.AddRange(new object[] {
        "5",
        "6",
        "7",
        "8"});
        this.comboBoxDataBits.Location = new System.Drawing.Point(409, 3);
        this.comboBoxDataBits.Name = "comboBoxDataBits";
        this.comboBoxDataBits.Size = new System.Drawing.Size(80, 23);
        this.comboBoxDataBits.TabIndex = 5;
        //
        // labelStopBits
        //
        this.labelStopBits.AutoSize = true;
        this.labelStopBits.Location = new System.Drawing.Point(495, 6);
        this.labelStopBits.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelStopBits.Name = "labelStopBits";
        this.labelStopBits.Size = new System.Drawing.Size(56, 15);
        this.labelStopBits.TabIndex = 6;
        this.labelStopBits.Text = "停止位：";
        //
        // comboBoxStopBits
        //
        this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxStopBits.FormattingEnabled = true;
        this.comboBoxStopBits.Items.AddRange(new object[] {
        "1",
        "1.5",
        "2"});
        this.comboBoxStopBits.Location = new System.Drawing.Point(557, 3);
        this.comboBoxStopBits.Name = "comboBoxStopBits";
        this.comboBoxStopBits.Size = new System.Drawing.Size(80, 23);
        this.comboBoxStopBits.TabIndex = 7;
        //
        // labelParity
        //
        this.labelParity.AutoSize = true;
        this.labelParity.Location = new System.Drawing.Point(643, 6);
        this.labelParity.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelParity.Name = "labelParity";
        this.labelParity.Size = new System.Drawing.Size(68, 15);
        this.labelParity.TabIndex = 8;
        this.labelParity.Text = "校验方式：";
        //
        // comboBoxParity
        //
        this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxParity.FormattingEnabled = true;
        this.comboBoxParity.Items.AddRange(new object[] {
        "None",
        "Odd",
        "Even",
        "Mark",
        "Space"});
        this.comboBoxParity.Location = new System.Drawing.Point(717, 3);
        this.comboBoxParity.Name = "comboBoxParity";
        this.comboBoxParity.Size = new System.Drawing.Size(80, 23);
        this.comboBoxParity.TabIndex = 9;
        //
        // labelTimeout
        //
        this.labelTimeout.AutoSize = true;
        this.labelTimeout.Location = new System.Drawing.Point(3, 32);
        this.labelTimeout.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelTimeout.Name = "labelTimeout";
        this.labelTimeout.Size = new System.Drawing.Size(68, 15);
        this.labelTimeout.TabIndex = 10;
        this.labelTimeout.Text = "超时时间：";
        //
        // comboBoxTimeout
        //
        this.comboBoxTimeout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxTimeout.FormattingEnabled = true;
        this.comboBoxTimeout.Items.AddRange(new object[] {
        "100",
        "500",
        "1000",
        "2000",
        "5000"});
        this.comboBoxTimeout.Location = new System.Drawing.Point(77, 29);
        this.comboBoxTimeout.Name = "comboBoxTimeout";
        this.comboBoxTimeout.Size = new System.Drawing.Size(100, 23);
        this.comboBoxTimeout.TabIndex = 11;
        //
        // labelDisplayMode
        //
        this.labelDisplayMode.AutoSize = true;
        this.labelDisplayMode.Location = new System.Drawing.Point(183, 32);
        this.labelDisplayMode.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
        this.labelDisplayMode.Name = "labelDisplayMode";
        this.labelDisplayMode.Size = new System.Drawing.Size(92, 15);
        this.labelDisplayMode.TabIndex = 12;
        this.labelDisplayMode.Text = "数据显示方式：";
        //
        // comboBoxDisplayMode
        //
        this.comboBoxDisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxDisplayMode.FormattingEnabled = true;
        this.comboBoxDisplayMode.Items.AddRange(new object[] {
        "ASCII",
        "Hex"});
        this.comboBoxDisplayMode.Location = new System.Drawing.Point(281, 29);
        this.comboBoxDisplayMode.Name = "comboBoxDisplayMode";
        this.comboBoxDisplayMode.Size = new System.Drawing.Size(100, 23);
        this.comboBoxDisplayMode.TabIndex = 13;
        //
        // buttonOpenClose
        //
        this.buttonOpenClose.Location = new System.Drawing.Point(387, 29);
        this.buttonOpenClose.Name = "buttonOpenClose";
        this.buttonOpenClose.Size = new System.Drawing.Size(100, 23);
        this.buttonOpenClose.TabIndex = 14;
        this.buttonOpenClose.Text = "打开串口";
        this.buttonOpenClose.UseVisualStyleBackColor = true;
        //
        // radioButtonReceive
        //
        this.radioButtonReceive.AutoSize = true;
        this.radioButtonReceive.Checked = true;
        this.radioButtonReceive.Location = new System.Drawing.Point(493, 32);
        this.radioButtonReceive.Name = "radioButtonReceive";
        this.radioButtonReceive.Size = new System.Drawing.Size(73, 19);
        this.radioButtonReceive.TabIndex = 15;
        this.radioButtonReceive.TabStop = true;
        this.radioButtonReceive.Text = "接收数据";
        this.radioButtonReceive.UseVisualStyleBackColor = true;
        //
        // radioButtonSend
        //
        this.radioButtonSend.AutoSize = true;
        this.radioButtonSend.Location = new System.Drawing.Point(572, 32);
        this.radioButtonSend.Name = "radioButtonSend";
        this.radioButtonSend.Size = new System.Drawing.Size(73, 19);
        this.radioButtonSend.TabIndex = 16;
        this.radioButtonSend.Text = "发送数据";
        this.radioButtonSend.UseVisualStyleBackColor = true;
        //
        // bottomTableLayout
        //
        this.bottomTableLayout.ColumnCount = 2;
        this.bottomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.bottomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.bottomTableLayout.Controls.Add(this.groupBoxReceived, 0, 0);
        this.bottomTableLayout.Controls.Add(this.groupBoxSend, 1, 0);
        this.bottomTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.bottomTableLayout.Location = new System.Drawing.Point(3, 143);
        this.bottomTableLayout.Name = "bottomTableLayout";
        this.bottomTableLayout.RowCount = 1;
        this.bottomTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.bottomTableLayout.Size = new System.Drawing.Size(994, 554);
        this.bottomTableLayout.TabIndex = 1;
        //
        // groupBoxReceived
        //
        this.groupBoxReceived.Controls.Add(this.textBoxReceived);
        this.groupBoxReceived.Controls.Add(this.buttonSave);
        this.groupBoxReceived.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBoxReceived.Location = new System.Drawing.Point(3, 3);
        this.groupBoxReceived.Name = "groupBoxReceived";
        this.groupBoxReceived.Size = new System.Drawing.Size(491, 548);
        this.groupBoxReceived.TabIndex = 0;
        this.groupBoxReceived.TabStop = false;
        this.groupBoxReceived.Text = "接收数据显示";
        //
        // textBoxReceived
        //
        this.textBoxReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxReceived.Location = new System.Drawing.Point(6, 22);
        this.textBoxReceived.Multiline = true;
        this.textBoxReceived.Name = "textBoxReceived";
        this.textBoxReceived.ReadOnly = true;
        this.textBoxReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBoxReceived.Size = new System.Drawing.Size(479, 490);
        this.textBoxReceived.TabIndex = 0;
        //
        // buttonSave
        //
        this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonSave.Location = new System.Drawing.Point(410, 518);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(75, 23);
        this.buttonSave.TabIndex = 1;
        this.buttonSave.Text = "保存数据";
        this.buttonSave.UseVisualStyleBackColor = true;
        //
        // groupBoxSend
        //
        this.groupBoxSend.Controls.Add(this.textBoxSend);
        this.groupBoxSend.Controls.Add(this.buttonSend);
        this.groupBoxSend.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBoxSend.Location = new System.Drawing.Point(500, 3);
        this.groupBoxSend.Name = "groupBoxSend";
        this.groupBoxSend.Size = new System.Drawing.Size(491, 548);
        this.groupBoxSend.TabIndex = 1;
        this.groupBoxSend.TabStop = false;
        this.groupBoxSend.Text = "发送数据";
        //
        // textBoxSend
        //
        this.textBoxSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxSend.Location = new System.Drawing.Point(6, 22);
        this.textBoxSend.Multiline = true;
        this.textBoxSend.Name = "textBoxSend";
        this.textBoxSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBoxSend.Size = new System.Drawing.Size(479, 490);
        this.textBoxSend.TabIndex = 0;
        //
        // buttonSend
        //
        this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonSend.Location = new System.Drawing.Point(410, 518);
        this.buttonSend.Name = "buttonSend";
        this.buttonSend.Size = new System.Drawing.Size(75, 23);
        this.buttonSend.TabIndex = 1;
        this.buttonSend.Text = "发送";
        this.buttonSend.UseVisualStyleBackColor = true;
        //
        // serialPort
        //
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
        this.Text = "LoRa Forest 串口工具";
        this.mainTableLayout.ResumeLayout(false);
        this.parametersGroupBox.ResumeLayout(false);
        this.flowLayoutPanel1.ResumeLayout(false);
        this.flowLayoutPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaudRate)).EndInit();
        this.bottomTableLayout.ResumeLayout(false);
        this.groupBoxReceived.ResumeLayout(false);
        this.groupBoxReceived.PerformLayout();
        this.groupBoxSend.ResumeLayout(false);
        this.groupBoxSend.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion
}
