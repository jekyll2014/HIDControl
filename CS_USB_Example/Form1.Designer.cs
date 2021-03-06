﻿//This document contains programming examples.
//Custom S.P.A. grants you a nonexclusive copyright license to use all programming code examples from which you can generate similar function tailored to your own specific needs.
//All sample code is provided by Custom S.P.A. for illustrative purposes only. These examples have not been thoroughly tested under all conditions. 
//Custom S.P.A., therefore, cannot guarantee or imply reliability, serviceability, or function of these programs.
//In no event shall Custom S.P.A. be liable for any direct, indirect, incidental, special, exemplary, or consequential damages (including, but not limited to, procurement of substitute goods or services; loss of use, data, or profits; or business interruption) however caused and on any theory of liability, whether in contract, strict liability, or tort 
//(including negligence or otherwise) arising in any way out of the use of this software, even if advised of the possibility of such damage.
//All programs contained herein are provided to you "as is" without any warranties of any kind. 
//The implied warranties of non-infringement, merchantability and fitness for a particular purpose are expressly disclaimed.

using System.Windows.Forms;

namespace HIDControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_HIDdevices = new System.Windows.Forms.ComboBox();
            this.label_UsbPrnList = new System.Windows.Forms.Label();
            this.button_Open = new System.Windows.Forms.Button();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_closeport = new System.Windows.Forms.Button();
            this.button_Send = new System.Windows.Forms.Button();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.textBox_terminal = new System.Windows.Forms.TextBox();
            this.checkBox_hexCommand = new System.Windows.Forms.CheckBox();
            this.checkBox_autoscroll = new System.Windows.Forms.CheckBox();
            this.checkBox_hexTerminal = new System.Windows.Forms.CheckBox();
            this.checkBox_hexParam = new System.Windows.Forms.CheckBox();
            this.textBox_param = new System.Windows.Forms.TextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.checkBox_saveInput = new System.Windows.Forms.CheckBox();
            this.textBox_saveTo = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox_saveTime = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBox_HIDdevices
            // 
            this.comboBox_HIDdevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_HIDdevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_HIDdevices.FormattingEnabled = true;
            this.comboBox_HIDdevices.Location = new System.Drawing.Point(12, 43);
            this.comboBox_HIDdevices.Name = "comboBox_HIDdevices";
            this.comboBox_HIDdevices.Size = new System.Drawing.Size(560, 21);
            this.comboBox_HIDdevices.TabIndex = 1;
            // 
            // label_UsbPrnList
            // 
            this.label_UsbPrnList.AutoSize = true;
            this.label_UsbPrnList.Location = new System.Drawing.Point(12, 18);
            this.label_UsbPrnList.Name = "label_UsbPrnList";
            this.label_UsbPrnList.Size = new System.Drawing.Size(64, 13);
            this.label_UsbPrnList.TabIndex = 1;
            this.label_UsbPrnList.Text = "Devices list:";
            // 
            // button_Open
            // 
            this.button_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Open.Location = new System.Drawing.Point(350, 12);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(70, 25);
            this.button_Open.TabIndex = 2;
            this.button_Open.Text = "Open";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.Button_Open_Click);
            // 
            // button_Refresh
            // 
            this.button_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Refresh.Location = new System.Drawing.Point(502, 12);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(70, 25);
            this.button_Refresh.TabIndex = 0;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.Button_Refresh_Click);
            // 
            // button_closeport
            // 
            this.button_closeport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_closeport.Enabled = false;
            this.button_closeport.Location = new System.Drawing.Point(426, 12);
            this.button_closeport.Name = "button_closeport";
            this.button_closeport.Size = new System.Drawing.Size(70, 25);
            this.button_closeport.TabIndex = 16;
            this.button_closeport.Text = "Close";
            this.button_closeport.UseVisualStyleBackColor = true;
            this.button_closeport.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // button_Send
            // 
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(12, 70);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(70, 47);
            this.button_Send.TabIndex = 7;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.Button_Write_Click);
            // 
            // textBox_command
            // 
            this.textBox_command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_command.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_command.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_command.Location = new System.Drawing.Point(186, 70);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(386, 20);
            this.textBox_command.TabIndex = 4;
            this.textBox_command.Leave += new System.EventHandler(this.TextBox_command_Leave);
            // 
            // textBox_terminal
            // 
            this.textBox_terminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_terminal.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_terminal.Location = new System.Drawing.Point(12, 123);
            this.textBox_terminal.MaxLength = 3276700;
            this.textBox_terminal.Multiline = true;
            this.textBox_terminal.Name = "textBox_terminal";
            this.textBox_terminal.ReadOnly = true;
            this.textBox_terminal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_terminal.Size = new System.Drawing.Size(560, 46);
            this.textBox_terminal.TabIndex = 17;
            this.textBox_terminal.TextChanged += new System.EventHandler(this.TextBox_terminal_TextChanged);
            // 
            // checkBox_hexCommand
            // 
            this.checkBox_hexCommand.AutoSize = true;
            this.checkBox_hexCommand.Checked = true;
            this.checkBox_hexCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexCommand.Location = new System.Drawing.Point(88, 76);
            this.checkBox_hexCommand.Name = "checkBox_hexCommand";
            this.checkBox_hexCommand.Size = new System.Drawing.Size(92, 17);
            this.checkBox_hexCommand.TabIndex = 3;
            this.checkBox_hexCommand.Text = "hex command";
            this.checkBox_hexCommand.UseVisualStyleBackColor = true;
            this.checkBox_hexCommand.CheckedChanged += new System.EventHandler(this.CheckBox_hexCommand_CheckedChanged);
            // 
            // checkBox_autoscroll
            // 
            this.checkBox_autoscroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_autoscroll.AutoSize = true;
            this.checkBox_autoscroll.Checked = true;
            this.checkBox_autoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_autoscroll.Location = new System.Drawing.Point(12, 183);
            this.checkBox_autoscroll.Name = "checkBox_autoscroll";
            this.checkBox_autoscroll.Size = new System.Drawing.Size(75, 17);
            this.checkBox_autoscroll.TabIndex = 12;
            this.checkBox_autoscroll.Text = "Autoscroll;";
            this.checkBox_autoscroll.UseVisualStyleBackColor = true;
            this.checkBox_autoscroll.CheckedChanged += new System.EventHandler(this.CheckBox_autoscroll_CheckedChanged);
            // 
            // checkBox_hexTerminal
            // 
            this.checkBox_hexTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_hexTerminal.AutoSize = true;
            this.checkBox_hexTerminal.Checked = true;
            this.checkBox_hexTerminal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexTerminal.Location = new System.Drawing.Point(90, 183);
            this.checkBox_hexTerminal.Name = "checkBox_hexTerminal";
            this.checkBox_hexTerminal.Size = new System.Drawing.Size(48, 17);
            this.checkBox_hexTerminal.TabIndex = 13;
            this.checkBox_hexTerminal.Text = "Hex;";
            this.checkBox_hexTerminal.UseVisualStyleBackColor = true;
            this.checkBox_hexTerminal.CheckedChanged += new System.EventHandler(this.CheckBox_hexTerminal_CheckedChanged);
            // 
            // checkBox_hexParam
            // 
            this.checkBox_hexParam.AutoSize = true;
            this.checkBox_hexParam.Checked = true;
            this.checkBox_hexParam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexParam.Location = new System.Drawing.Point(88, 99);
            this.checkBox_hexParam.Name = "checkBox_hexParam";
            this.checkBox_hexParam.Size = new System.Drawing.Size(93, 17);
            this.checkBox_hexParam.TabIndex = 5;
            this.checkBox_hexParam.Text = "hex parameter";
            this.checkBox_hexParam.UseVisualStyleBackColor = true;
            this.checkBox_hexParam.CheckedChanged += new System.EventHandler(this.CheckBox_hexParam_CheckedChanged);
            // 
            // textBox_param
            // 
            this.textBox_param.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_param.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_param.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_param.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_param.Location = new System.Drawing.Point(186, 97);
            this.textBox_param.Name = "textBox_param";
            this.textBox_param.Size = new System.Drawing.Size(386, 20);
            this.textBox_param.TabIndex = 6;
            this.textBox_param.Leave += new System.EventHandler(this.TextBox_param_Leave);
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.Location = new System.Drawing.Point(502, 178);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(70, 25);
            this.button_Clear.TabIndex = 15;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // checkBox_saveInput
            // 
            this.checkBox_saveInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_saveInput.AutoSize = true;
            this.checkBox_saveInput.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_saveInput.Checked = true;
            this.checkBox_saveInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveInput.Location = new System.Drawing.Point(360, 183);
            this.checkBox_saveInput.Name = "checkBox_saveInput";
            this.checkBox_saveInput.Size = new System.Drawing.Size(68, 17);
            this.checkBox_saveInput.TabIndex = 98;
            this.checkBox_saveInput.Text = "Save log";
            this.checkBox_saveInput.UseVisualStyleBackColor = true;
            this.checkBox_saveInput.CheckedChanged += new System.EventHandler(this.CheckBox_saveTo_CheckedChanged);
            // 
            // textBox_saveTo
            // 
            this.textBox_saveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_saveTo.Enabled = false;
            this.textBox_saveTo.Location = new System.Drawing.Point(434, 181);
            this.textBox_saveTo.Name = "textBox_saveTo";
            this.textBox_saveTo.Size = new System.Drawing.Size(62, 20);
            this.textBox_saveTo.TabIndex = 14;
            this.textBox_saveTo.Text = "usb_rx.txt";
            this.textBox_saveTo.Leave += new System.EventHandler(this.TextBox_saveTo_Leave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox_saveTime
            // 
            this.checkBox_saveTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveTime.AutoSize = true;
            this.checkBox_saveTime.Checked = true;
            this.checkBox_saveTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveTime.Location = new System.Drawing.Point(144, 183);
            this.checkBox_saveTime.Name = "checkBox_saveTime";
            this.checkBox_saveTime.Size = new System.Drawing.Size(45, 17);
            this.checkBox_saveTime.TabIndex = 116;
            this.checkBox_saveTime.Text = "time";
            this.checkBox_saveTime.UseVisualStyleBackColor = true;
            this.checkBox_saveTime.CheckedChanged += new System.EventHandler(this.CheckBox_saveTime_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 212);
            this.Controls.Add(this.checkBox_saveTime);
            this.Controls.Add(this.textBox_saveTo);
            this.Controls.Add(this.checkBox_saveInput);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBox_param);
            this.Controls.Add(this.checkBox_hexParam);
            this.Controls.Add(this.checkBox_hexTerminal);
            this.Controls.Add(this.checkBox_autoscroll);
            this.Controls.Add(this.checkBox_hexCommand);
            this.Controls.Add(this.textBox_terminal);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.button_closeport);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_Open);
            this.Controls.Add(this.label_UsbPrnList);
            this.Controls.Add(this.comboBox_HIDdevices);
            this.MinimumSize = new System.Drawing.Size(600, 250);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HidControl (c) Andrey Kalugin (jekyll@mail.ru)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox comboBox_HIDdevices;
        private Label label_UsbPrnList;
        private Button button_Open;
        private Button button_Refresh;
        private Button button_closeport;
        private Button button_Send;
        private TextBox textBox_command;
        private TextBox textBox_terminal;
        private CheckBox checkBox_hexCommand;
        private CheckBox checkBox_autoscroll;
        private CheckBox checkBox_hexTerminal;
        private CheckBox checkBox_hexParam;
        private TextBox textBox_param;
        private Button button_Clear;
        private CheckBox checkBox_saveInput;
        private TextBox textBox_saveTo;
        private OpenFileDialog openFileDialog1;
        private CheckBox checkBox_saveTime;
        ToolTip ToolTipTerminal = new ToolTip();
    }
}

