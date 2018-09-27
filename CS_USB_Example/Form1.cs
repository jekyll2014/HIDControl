using HIDInterface;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HIDControl
{
    public partial class Form1 : Form
    {
        private HIDDevice.interfaceDetails[] HID_LIST;
        private HIDDevice selected_HID;
        int packetMaxOut = 0;
        int packetMaxIn = 0;

        private void ReadHID(byte[] message)
        {
            if (checkBox_hexTerminal.Checked) CollectBuffer(Accessory.ConvertByteArrayToHex(message, message.Length), Port1DataIn);
            else CollectBuffer(Encoding.GetEncoding(Properties.Settings.Default.CodePage).GetString(message), Port1DataIn);
        }

        private void RefreshHidList()
        {
            comboBox_HIDdevices.Items.Clear();
            HID_LIST = HIDDevice.getConnectedDevices();
            for (int i = 0; i < HID_LIST.Length; i++)
            {
                comboBox_HIDdevices.Items.Add(" [VID " + HID_LIST[i].VID.ToString("d4") + "; PID " + HID_LIST[i].PID.ToString("d4") + "]" +
                     HID_LIST[i].manufacturer + " / " +
                     HID_LIST[i].product + " / " +
                     HID_LIST[i].versionNumber.ToString() + " / " +
                     HID_LIST[i].serialNumber.ToString() +
                     " (maxIN=" + HID_LIST[i].IN_reportByteLength.ToString() + " / maxOUT=" + HID_LIST[i].OUT_reportByteLength.ToString() + ")");
            }
            if (HID_LIST.Length > 0)
            {
                comboBox_HIDdevices.SelectedIndex = 0;
                button_Open.Enabled = true;
            }
            else
            {
                comboBox_HIDdevices.Items.Add("No HID devices found");
                comboBox_HIDdevices.SelectedIndex = 0;
                button_Open.Enabled = false;
            }
        }

        private int txtOutState = 0;
        private long oldTicks = DateTime.Now.Ticks, limitTick = 200;
        private int LogLinesLimit = 100;
        public const byte Port1DataIn = 11;
        public const byte Port1DataOut = 12;
        public const byte Port1Error = 15;

        private delegate void SetTextCallback1(string text);
        private void SetText(string text)
        {
            text = Accessory.FilterZeroChar(text);
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textBox_terminal1.InvokeRequired)
            if (textBox_terminal.InvokeRequired)
            {
                SetTextCallback1 d = new SetTextCallback1(SetText);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                int pos = textBox_terminal.SelectionStart;
                textBox_terminal.AppendText(text);
                if (textBox_terminal.Lines.Length > LogLinesLimit)
                {
                    StringBuilder tmp = new StringBuilder();
                    for (int i = textBox_terminal.Lines.Length - LogLinesLimit; i < textBox_terminal.Lines.Length; i++)
                    {
                        tmp.Append(textBox_terminal.Lines[i]);
                        tmp.Append("\r\n");
                    }
                    textBox_terminal.Text = tmp.ToString();
                }
                if (checkBox_autoscroll.Checked)
                {
                    textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
                    textBox_terminal.ScrollToCaret();
                }
                else
                {
                    textBox_terminal.SelectionStart = pos;
                    textBox_terminal.ScrollToCaret();
                }
            }
        }

        private object threadLock = new object();
        public void CollectBuffer(string tmpBuffer, int state)
        {
            if (tmpBuffer != "")
            {
                string time = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString("D3");
                lock (threadLock)
                {
                    if (!(txtOutState == state && (DateTime.Now.Ticks - oldTicks) < limitTick && state != Port1DataOut))
                    {
                        if (state == Port1DataIn) tmpBuffer = "<< " + tmpBuffer;         //sending data
                        else if (state == Port1DataOut) tmpBuffer = ">> " + tmpBuffer;    //receiving data
                        else if (state == Port1Error) tmpBuffer = "!! " + tmpBuffer;    //error occured

                        if (checkBox_saveTime.Checked == true) tmpBuffer = time + " " + tmpBuffer;
                        tmpBuffer = "\r\n" + tmpBuffer;
                        txtOutState = state;
                    }
                    if ((checkBox_saveInput.Checked == true && state == Port1DataIn) || (checkBox_saveOutput.Checked == true && state == Port1DataOut))
                    {
                        try
                        {
                            File.AppendAllText(textBox_saveTo.Text, tmpBuffer, Encoding.GetEncoding(HIDControl.Properties.Settings.Default.CodePage));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_saveTo.Text + ": " + ex.Message);
                        }
                    }
                    SetText(tmpBuffer);
                    oldTicks = DateTime.Now.Ticks;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            RefreshHidList();
            ToolTipTerminal.SetToolTip(textBox_terminal, "Press left mouse button to read datas from USB manually");
        }

        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshHidList();
        }

        private void Button_Open_Click(object sender, EventArgs e)
        {
            selected_HID = new HIDDevice(HID_LIST[comboBox_HIDdevices.SelectedIndex].devicePath, true);
            selected_HID.dataReceived += new HIDDevice.dataReceivedEvent(ReadHID);
            int packetMaxOut = HID_LIST[comboBox_HIDdevices.SelectedIndex].OUT_reportByteLength;
            int packetMaxIn = HID_LIST[comboBox_HIDdevices.SelectedIndex].IN_reportByteLength;
            button_Refresh.Enabled = false;
            button_Open.Enabled = false;
            comboBox_HIDdevices.Enabled = false;
            button_closeport.Enabled = true;
            button_Send.Enabled = true;
            //button_sendFile.Enabled = true;
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            selected_HID.dataReceived -= new HIDDevice.dataReceivedEvent(ReadHID);
            if (selected_HID.deviceConnected) selected_HID.close();
            button_Refresh.Enabled = true;
            button_Open.Enabled = true;
            comboBox_HIDdevices.Enabled = true;
            button_closeport.Enabled = false;
            button_Send.Enabled = false;
        }

        private void Button_Write_Click(object sender, EventArgs e)
        {
            if (selected_HID != null)
            {
                if (textBox_command.Text + textBox_param.Text != "")
                {
                    string outStr;
                    if (checkBox_hexCommand.Checked) outStr = textBox_command.Text;
                    else outStr = Accessory.ConvertStringToHex(textBox_command.Text);
                    if (checkBox_hexParam.Checked) outStr += textBox_param.Text;
                    else outStr += Accessory.ConvertStringToHex(textBox_param.Text);
                    if (outStr != "")
                    {
                        textBox_command.AutoCompleteCustomSource.Add(textBox_command.Text);
                        textBox_param.AutoCompleteCustomSource.Add(textBox_param.Text);
                        selected_HID.writeLong(Accessory.ConvertHexToByteArray(outStr));
                            if (checkBox_hexTerminal.Checked) CollectBuffer(outStr, Port1DataOut);
                            else CollectBuffer(Accessory.ConvertHexToString(outStr), Port1DataOut);

                    }
                }
            }
            else Button_Close_Click(this, EventArgs.Empty);
        }

        private void CheckBox_hexCommand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.ConvertStringToHex(textBox_command.Text);
            else textBox_command.Text = Accessory.ConvertHexToString(textBox_command.Text);
        }

        private void TextBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
        }

        private void TextBox_param_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.CheckHexString(textBox_param.Text);
        }

        private void CheckBox_hexParam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.ConvertStringToHex(textBox_param.Text);
            else textBox_param.Text = Accessory.ConvertHexToString(textBox_param.Text);
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            textBox_terminal.Clear();
        }

        private void CheckBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveInput.Checked || checkBox_saveOutput.Checked) textBox_saveTo.Enabled = false;
            else textBox_saveTo.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HIDControl.Properties.Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            HIDControl.Properties.Settings.Default.textBox_command = textBox_command.Text;
            HIDControl.Properties.Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            HIDControl.Properties.Settings.Default.textBox_param = textBox_param.Text;
            HIDControl.Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_hexCommand.Checked = HIDControl.Properties.Settings.Default.checkBox_hexCommand;
            textBox_command.Text = HIDControl.Properties.Settings.Default.textBox_command;
            checkBox_hexParam.Checked = HIDControl.Properties.Settings.Default.checkBox_hexParam;
            textBox_param.Text = HIDControl.Properties.Settings.Default.textBox_param;
            limitTick = HIDControl.Properties.Settings.Default.LineBreakTimeout;
            limitTick *= 10000;
            LogLinesLimit = HIDControl.Properties.Settings.Default.LogLinesLimit;
        }

    }
}
