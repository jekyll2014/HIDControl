using HIDInterface;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HIDControl.Properties;

namespace HIDControl
{
    public partial class Form1 : Form
    {
        private HIDDevice.interfaceDetails[] HID_LIST;
        private HIDDevice selected_HID;

        private void ReadHID(byte[] message)
        {
            _logger.AddText(Encoding.GetEncoding(Settings.Default.CodePage).GetString(message),
                (byte)DataDirection.Received, DateTime.Now);
        }

        private void RefreshHidList()
        {
            comboBox_HIDdevices.Items.Clear();
            HID_LIST = HIDDevice.getConnectedDevices();
            for (var i = 0; i < HID_LIST.Length; i++)
                comboBox_HIDdevices.Items.Add(" [VID " + HID_LIST[i].VID.ToString("d4") + "; PID " +
                                              HID_LIST[i].PID.ToString("d4") + "]" +
                                              HID_LIST[i].manufacturer + " / " +
                                              HID_LIST[i].product + " / " +
                                              HID_LIST[i].versionNumber + " / " +
                                              HID_LIST[i].serialNumber +
                                              " (maxIN=" + HID_LIST[i].IN_reportByteLength + " / maxOUT=" +
                                              HID_LIST[i].OUT_reportByteLength + ")");
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

        private TextLogger.TextLogger _logger;

        private enum DataDirection
        {
            Received,
            Sent,
            Info,
            Error
        }

        private readonly Dictionary<byte, string> _directions = new Dictionary<byte, string>
        {
            {(byte) DataDirection.Received, "<<"},
            {(byte) DataDirection.Sent, ">>"},
            {(byte) DataDirection.Info, "**"},
            {(byte) DataDirection.Error, "!!"}
        };

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
            try
            {
                selected_HID = new HIDDevice(HID_LIST[comboBox_HIDdevices.SelectedIndex].devicePath, true);
            }
            catch (Exception exception)
            {
                _logger.AddText("Error opening device: " + exception, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                return;
            }

            selected_HID.dataReceived += ReadHID;
            var packetMaxOut = HID_LIST[comboBox_HIDdevices.SelectedIndex].OUT_reportByteLength;
            var packetMaxIn = HID_LIST[comboBox_HIDdevices.SelectedIndex].IN_reportByteLength;
            button_Refresh.Enabled = false;
            button_Open.Enabled = false;
            comboBox_HIDdevices.Enabled = false;
            button_closeport.Enabled = true;
            button_Send.Enabled = true;
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            selected_HID.dataReceived -= ReadHID;
            try
            {
                if (selected_HID.deviceConnected) selected_HID.close();
            }
            catch (Exception exception)
            {
                _logger.AddText("Error closing device: " + exception, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
            }
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
                        _logger.AddText(Accessory.ConvertHexToString(outStr), (byte)DataDirection.Sent, DateTime.Now);
                        try
                        {
                            selected_HID.write(Accessory.ConvertHexToByteArray(outStr));
                        }
                        catch (Exception exception)
                        {
                            _logger.AddText("Error writing to device: " + exception, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                        }
                    }
                }
            }
            else
            {
                Button_Close_Click(this, EventArgs.Empty);
            }
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
            _logger.Clear();
        }

        private void CheckBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            textBox_saveTo.Enabled = !checkBox_saveInput.Checked;
            _logger.AutoSave = checkBox_saveInput.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            Settings.Default.textBox_command = textBox_command.Text;
            Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            Settings.Default.textBox_param = textBox_param.Text;
            Settings.Default.Save();
        }

        private void CheckBox_autoscroll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autoscroll.Checked)
            {
                _logger.AutoScroll = true;
                textBox_terminal.TextChanged += TextBox_terminal_TextChanged;
            }
            else
            {
                _logger.AutoScroll = false;
                textBox_terminal.TextChanged -= TextBox_terminal_TextChanged;
            }
        }

        private void TextBox_terminal_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_autoscroll.Checked)
            {
                textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
                textBox_terminal.ScrollToCaret();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_hexCommand.Checked = Settings.Default.checkBox_hexCommand;
            textBox_command.Text = Settings.Default.textBox_command;
            checkBox_hexParam.Checked = Settings.Default.checkBox_hexParam;
            textBox_param.Text = Settings.Default.textBox_param;

            _logger = new TextLogger.TextLogger(this)
            {
                Channels = _directions,
                FilterZeroChar = false
            };
            textBox_terminal.DataBindings.Add("Text", _logger, "Text", false, DataSourceUpdateMode.OnPropertyChanged);

            _logger.LineTimeLimit = Settings.Default.LineBreakTimeout;
            _logger.LineLimit = Settings.Default.LogLinesLimit;
            _logger.AutoSave = checkBox_saveInput.Checked;
            _logger.LogFileName = textBox_saveTo.Text;

            _logger.DefaultTextFormat = checkBox_hexTerminal.Checked
                ? TextLogger.TextLogger.TextFormat.Hex
                : TextLogger.TextLogger.TextFormat.AutoReplaceHex;

            _logger.DefaultTimeFormat =
                checkBox_saveTime.Checked
                    ? TextLogger.TextLogger.TimeFormat.LongTime
                    : TextLogger.TextLogger.TimeFormat.None;

            _logger.DefaultDateFormat =
                checkBox_saveTime.Checked
                    ? TextLogger.TextLogger.DateFormat.ShortDate
                    : TextLogger.TextLogger.DateFormat.None;

            _logger.AutoScroll = checkBox_autoscroll.Checked;

            CheckBox_autoscroll_CheckedChanged(null, EventArgs.Empty);
        }

        private void CheckBox_hexTerminal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexTerminal.Checked)
                _logger.DefaultTextFormat = TextLogger.TextLogger.TextFormat.Hex;
            else
                _logger.DefaultTextFormat = TextLogger.TextLogger.TextFormat.AutoReplaceHex;
        }

        private void CheckBox_saveTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveTime.Checked)
            {
                _logger.DefaultDateFormat = TextLogger.TextLogger.DateFormat.ShortDate;
                _logger.DefaultTimeFormat = TextLogger.TextLogger.TimeFormat.LongTime;
            }
            else
            {
                _logger.DefaultDateFormat = TextLogger.TextLogger.DateFormat.None;
                _logger.DefaultTimeFormat = TextLogger.TextLogger.TimeFormat.None;
            }
        }

        private void TextBox_saveTo_Leave(object sender, EventArgs e)
        {
            _logger.LogFileName = textBox_saveTo.Text;
        }
    }
}
