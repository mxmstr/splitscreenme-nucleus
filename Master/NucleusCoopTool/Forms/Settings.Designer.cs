using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Nucleus.Gaming.Windows.Interop;
using WindowScrape.Constants;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Text;
using System.Runtime.ExceptionServices;
using System.Reflection;
using Nucleus.Coop.Forms;
using Nucleus.Gaming.Coop.ProtoInput;
using Nucleus.Gaming;
namespace Nucleus.Coop
{
    partial class Settings
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
            this.save_Label = new System.Windows.Forms.Label();
            this.btn_credits = new System.Windows.Forms.Button();
            this.settingsCloseBtn = new System.Windows.Forms.Button();
            this.settingsSaveBtn = new System.Windows.Forms.Button();
            this.settingLabel_Container = new System.Windows.Forms.Panel();
            this.password_Label = new System.Windows.Forms.Label();
            this.nucUserPassTxt = new System.Windows.Forms.TextBox();
            this.themeLabel = new System.Windows.Forms.Label();
            this.themeCbx = new System.Windows.Forms.ComboBox();
            this.splashScreenChkB = new System.Windows.Forms.CheckBox();
            this.clickSoundChkB = new System.Windows.Forms.CheckBox();
            this.cmb_EpicLang = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.ignoreInputLockReminderCheckbox = new System.Windows.Forms.CheckBox();
            this.statusCheck = new System.Windows.Forms.CheckBox();
            this.cmb_Lang = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.debugLogCheck = new System.Windows.Forms.CheckBox();
            this.hotkeyBox = new System.Windows.Forms.GroupBox();
            this.swl_comboBox = new System.Windows.Forms.ComboBox();
            this.plus7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.swl_textBox = new System.Windows.Forms.TextBox();
            this.csm_comboBox = new System.Windows.Forms.ComboBox();
            this.plus6 = new System.Windows.Forms.Label();
            this.csm_label = new System.Windows.Forms.Label();
            this.csm_textBox = new System.Windows.Forms.TextBox();
            this.r1 = new System.Windows.Forms.ComboBox();
            this.plus5 = new System.Windows.Forms.Label();
            this.settingsFocusCmb = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.plus1 = new System.Windows.Forms.Label();
            this.r2 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.settingsFocusHKTxt = new System.Windows.Forms.TextBox();
            this.comboBox_lockKey = new System.Windows.Forms.ComboBox();
            this.label_lockKey = new System.Windows.Forms.Label();
            this.settingsTopCmb = new System.Windows.Forms.ComboBox();
            this.settingsStopCmb = new System.Windows.Forms.ComboBox();
            this.settingsTopTxt = new System.Windows.Forms.TextBox();
            this.settingsStopTxt = new System.Windows.Forms.TextBox();
            this.settingsCloseCmb = new System.Windows.Forms.ComboBox();
            this.plus4 = new System.Windows.Forms.Label();
            this.plus3 = new System.Windows.Forms.Label();
            this.plus2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.settingsCloseHKTxt = new System.Windows.Forms.TextBox();
            this.ctrlr_shorcuts = new System.Windows.Forms.Button();
            this.settingLabel_Container.SuspendLayout();
            this.hotkeyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // save_Label
            // 
            this.save_Label.BackColor = System.Drawing.Color.Transparent;
            this.save_Label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.save_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.save_Label.Location = new System.Drawing.Point(0, 0);
            this.save_Label.Margin = new System.Windows.Forms.Padding(0);
            this.save_Label.Name = "save_Label";
            this.save_Label.Size = new System.Drawing.Size(260, 15);
            this.save_Label.TabIndex = 62;
            this.save_Label.Text = "Save settings in order for them to take effect.";
            this.save_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_credits
            // 
            this.btn_credits.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_credits.BackColor = System.Drawing.Color.Transparent;
            this.btn_credits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_credits.FlatAppearance.BorderSize = 0;
            this.btn_credits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_credits.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_credits.Location = new System.Drawing.Point(195, 321);
            this.btn_credits.Margin = new System.Windows.Forms.Padding(2);
            this.btn_credits.Name = "btn_credits";
            this.btn_credits.Size = new System.Drawing.Size(80, 22);
            this.btn_credits.TabIndex = 21;
            this.btn_credits.Text = "Credits";
            this.btn_credits.UseVisualStyleBackColor = false;
            this.btn_credits.Click += new System.EventHandler(this.Btn_credits_Click);
            // 
            // settingsCloseBtn
            // 
            this.settingsCloseBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.settingsCloseBtn.BackColor = System.Drawing.Color.Transparent;
            this.settingsCloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settingsCloseBtn.FlatAppearance.BorderSize = 0;
            this.settingsCloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsCloseBtn.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsCloseBtn.Location = new System.Drawing.Point(279, 321);
            this.settingsCloseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.settingsCloseBtn.Name = "settingsCloseBtn";
            this.settingsCloseBtn.Size = new System.Drawing.Size(80, 22);
            this.settingsCloseBtn.TabIndex = 9;
            this.settingsCloseBtn.Text = "Close";
            this.settingsCloseBtn.UseVisualStyleBackColor = false;
            this.settingsCloseBtn.Click += new System.EventHandler(this.SettingsCloseBtn_Click);
            // 
            // settingsSaveBtn
            // 
            this.settingsSaveBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.settingsSaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.settingsSaveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settingsSaveBtn.FlatAppearance.BorderSize = 0;
            this.settingsSaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsSaveBtn.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsSaveBtn.Location = new System.Drawing.Point(111, 321);
            this.settingsSaveBtn.Margin = new System.Windows.Forms.Padding(2);
            this.settingsSaveBtn.Name = "settingsSaveBtn";
            this.settingsSaveBtn.Size = new System.Drawing.Size(80, 22);
            this.settingsSaveBtn.TabIndex = 8;
            this.settingsSaveBtn.Text = "Save";
            this.settingsSaveBtn.UseVisualStyleBackColor = false;
            this.settingsSaveBtn.Click += new System.EventHandler(this.SettingsSaveBtn_Click);
            // 
            // settingLabel_Container
            // 
            this.settingLabel_Container.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingLabel_Container.BackColor = System.Drawing.Color.Transparent;
            this.settingLabel_Container.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settingLabel_Container.Controls.Add(this.save_Label);
            this.settingLabel_Container.Location = new System.Drawing.Point(111, 301);
            this.settingLabel_Container.Name = "settingLabel_Container";
            this.settingLabel_Container.Size = new System.Drawing.Size(260, 15);
            this.settingLabel_Container.TabIndex = 37;
            // 
            // password_Label
            // 
            this.password_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.password_Label.AutoSize = true;
            this.password_Label.BackColor = System.Drawing.Color.Transparent;
            this.password_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_Label.Location = new System.Drawing.Point(19, 246);
            this.password_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.password_Label.Name = "password_Label";
            this.password_Label.Size = new System.Drawing.Size(138, 15);
            this.password_Label.TabIndex = 85;
            this.password_Label.Text = "Nucleus User Password";
            this.password_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nucUserPassTxt
            // 
            this.nucUserPassTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nucUserPassTxt.Location = new System.Drawing.Point(22, 265);
            this.nucUserPassTxt.Margin = new System.Windows.Forms.Padding(2);
            this.nucUserPassTxt.MaxLength = 127;
            this.nucUserPassTxt.Name = "nucUserPassTxt";
            this.nucUserPassTxt.PasswordChar = '*';
            this.nucUserPassTxt.Size = new System.Drawing.Size(197, 20);
            this.nucUserPassTxt.TabIndex = 74;
            this.nucUserPassTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // themeLabel
            // 
            this.themeLabel.AutoSize = true;
            this.themeLabel.BackColor = System.Drawing.Color.Transparent;
            this.themeLabel.Location = new System.Drawing.Point(19, 92);
            this.themeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(43, 13);
            this.themeLabel.TabIndex = 99;
            this.themeLabel.Text = "Theme:";
            this.themeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // themeCbx
            // 
            this.themeCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themeCbx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.themeCbx.FormattingEnabled = true;
            this.themeCbx.ItemHeight = 13;
            this.themeCbx.Location = new System.Drawing.Point(22, 109);
            this.themeCbx.Margin = new System.Windows.Forms.Padding(0);
            this.themeCbx.MaxDropDownItems = 10;
            this.themeCbx.Name = "themeCbx";
            this.themeCbx.Size = new System.Drawing.Size(142, 21);
            this.themeCbx.TabIndex = 98;
            // 
            // splashScreenChkB
            // 
            this.splashScreenChkB.AutoSize = true;
            this.splashScreenChkB.BackColor = System.Drawing.Color.Transparent;
            this.splashScreenChkB.Checked = true;
            this.splashScreenChkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splashScreenChkB.Location = new System.Drawing.Point(22, 201);
            this.splashScreenChkB.Margin = new System.Windows.Forms.Padding(2);
            this.splashScreenChkB.Name = "splashScreenChkB";
            this.splashScreenChkB.Size = new System.Drawing.Size(127, 17);
            this.splashScreenChkB.TabIndex = 97;
            this.splashScreenChkB.Text = "Enable splash screen";
            this.splashScreenChkB.UseVisualStyleBackColor = false;
            // 
            // clickSoundChkB
            // 
            this.clickSoundChkB.AutoSize = true;
            this.clickSoundChkB.BackColor = System.Drawing.Color.Transparent;
            this.clickSoundChkB.Checked = true;
            this.clickSoundChkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clickSoundChkB.Location = new System.Drawing.Point(22, 222);
            this.clickSoundChkB.Margin = new System.Windows.Forms.Padding(2);
            this.clickSoundChkB.Name = "clickSoundChkB";
            this.clickSoundChkB.Size = new System.Drawing.Size(116, 17);
            this.clickSoundChkB.TabIndex = 96;
            this.clickSoundChkB.Text = "Enable click sound";
            this.clickSoundChkB.UseVisualStyleBackColor = false;
            // 
            // cmb_EpicLang
            // 
            this.cmb_EpicLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_EpicLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_EpicLang.FormattingEnabled = true;
            this.cmb_EpicLang.Items.AddRange(new object[] {
            "Arabic",
            "Brazilian",
            "Bulgarian",
            "Chinese",
            "Czech",
            "Danish",
            "Dutch",
            "English",
            "Finnish",
            "French",
            "German",
            "Greek",
            "Hungarian",
            "Italian",
            "Japanese",
            "Koreana",
            "Norwegian",
            "Polish",
            "Portuguese",
            "Romanian",
            "Russian",
            "Spanish",
            "Swedish",
            "Thai",
            "Turkish",
            "Ukrainian"});
            this.cmb_EpicLang.Location = new System.Drawing.Point(22, 67);
            this.cmb_EpicLang.Margin = new System.Windows.Forms.Padding(0);
            this.cmb_EpicLang.MaxDropDownItems = 10;
            this.cmb_EpicLang.Name = "cmb_EpicLang";
            this.cmb_EpicLang.Size = new System.Drawing.Size(142, 21);
            this.cmb_EpicLang.TabIndex = 95;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(19, 48);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(116, 15);
            this.label46.TabIndex = 94;
            this.label46.Text = "Epic/Gog Language";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ignoreInputLockReminderCheckbox
            // 
            this.ignoreInputLockReminderCheckbox.AutoSize = true;
            this.ignoreInputLockReminderCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.ignoreInputLockReminderCheckbox.Location = new System.Drawing.Point(22, 180);
            this.ignoreInputLockReminderCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.ignoreInputLockReminderCheckbox.Name = "ignoreInputLockReminderCheckbox";
            this.ignoreInputLockReminderCheckbox.Size = new System.Drawing.Size(148, 17);
            this.ignoreInputLockReminderCheckbox.TabIndex = 93;
            this.ignoreInputLockReminderCheckbox.Text = "Ignore input lock reminder";
            this.ignoreInputLockReminderCheckbox.UseVisualStyleBackColor = false;
            // 
            // statusCheck
            // 
            this.statusCheck.AutoSize = true;
            this.statusCheck.BackColor = System.Drawing.Color.Transparent;
            this.statusCheck.Location = new System.Drawing.Point(22, 158);
            this.statusCheck.Margin = new System.Windows.Forms.Padding(2);
            this.statusCheck.Name = "statusCheck";
            this.statusCheck.Size = new System.Drawing.Size(197, 17);
            this.statusCheck.TabIndex = 92;
            this.statusCheck.Text = "Show Status Window (Experimental)";
            this.statusCheck.UseVisualStyleBackColor = false;
            // 
            // cmb_Lang
            // 
            this.cmb_Lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Lang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Lang.FormattingEnabled = true;
            this.cmb_Lang.Items.AddRange(new object[] {
            "Automatic",
            "Arabic",
            "Brazilian",
            "Bulgarian",
            "Schinese",
            "Tchinese",
            "Czech",
            "Danish",
            "Dutch",
            "English",
            "Finnish",
            "French",
            "German",
            "Greek",
            "Hungarian",
            "Italian",
            "Japanese",
            "Koreana",
            "Norwegian",
            "Polish",
            "Portuguese",
            "Romanian",
            "Russian",
            "Spanish",
            "Swedish",
            "Thai",
            "Turkish",
            "Ukrainian"});
            this.cmb_Lang.Location = new System.Drawing.Point(22, 22);
            this.cmb_Lang.Margin = new System.Windows.Forms.Padding(0);
            this.cmb_Lang.MaxDropDownItems = 10;
            this.cmb_Lang.Name = "cmb_Lang";
            this.cmb_Lang.Size = new System.Drawing.Size(142, 21);
            this.cmb_Lang.TabIndex = 91;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(19, 3);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(102, 15);
            this.label34.TabIndex = 90;
            this.label34.Text = "Steam Language";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // debugLogCheck
            // 
            this.debugLogCheck.AutoSize = true;
            this.debugLogCheck.BackColor = System.Drawing.Color.Transparent;
            this.debugLogCheck.Location = new System.Drawing.Point(22, 138);
            this.debugLogCheck.Margin = new System.Windows.Forms.Padding(2);
            this.debugLogCheck.Name = "debugLogCheck";
            this.debugLogCheck.Size = new System.Drawing.Size(115, 17);
            this.debugLogCheck.TabIndex = 87;
            this.debugLogCheck.Text = "Enable Debug Log";
            this.debugLogCheck.UseVisualStyleBackColor = false;
            // 
            // hotkeyBox
            // 
            this.hotkeyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hotkeyBox.BackColor = System.Drawing.Color.Transparent;
            this.hotkeyBox.Controls.Add(this.swl_comboBox);
            this.hotkeyBox.Controls.Add(this.plus7);
            this.hotkeyBox.Controls.Add(this.label6);
            this.hotkeyBox.Controls.Add(this.swl_textBox);
            this.hotkeyBox.Controls.Add(this.csm_comboBox);
            this.hotkeyBox.Controls.Add(this.plus6);
            this.hotkeyBox.Controls.Add(this.csm_label);
            this.hotkeyBox.Controls.Add(this.csm_textBox);
            this.hotkeyBox.Controls.Add(this.r1);
            this.hotkeyBox.Controls.Add(this.plus5);
            this.hotkeyBox.Controls.Add(this.settingsFocusCmb);
            this.hotkeyBox.Controls.Add(this.label5);
            this.hotkeyBox.Controls.Add(this.plus1);
            this.hotkeyBox.Controls.Add(this.r2);
            this.hotkeyBox.Controls.Add(this.label38);
            this.hotkeyBox.Controls.Add(this.label52);
            this.hotkeyBox.Controls.Add(this.settingsFocusHKTxt);
            this.hotkeyBox.Controls.Add(this.comboBox_lockKey);
            this.hotkeyBox.Controls.Add(this.label_lockKey);
            this.hotkeyBox.Controls.Add(this.settingsTopCmb);
            this.hotkeyBox.Controls.Add(this.settingsStopCmb);
            this.hotkeyBox.Controls.Add(this.settingsTopTxt);
            this.hotkeyBox.Controls.Add(this.settingsStopTxt);
            this.hotkeyBox.Controls.Add(this.settingsCloseCmb);
            this.hotkeyBox.Controls.Add(this.plus4);
            this.hotkeyBox.Controls.Add(this.plus3);
            this.hotkeyBox.Controls.Add(this.plus2);
            this.hotkeyBox.Controls.Add(this.label4);
            this.hotkeyBox.Controls.Add(this.label3);
            this.hotkeyBox.Controls.Add(this.label1);
            this.hotkeyBox.Controls.Add(this.settingsCloseHKTxt);
            this.hotkeyBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hotkeyBox.Location = new System.Drawing.Point(241, 39);
            this.hotkeyBox.Margin = new System.Windows.Forms.Padding(2);
            this.hotkeyBox.Name = "hotkeyBox";
            this.hotkeyBox.Padding = new System.Windows.Forms.Padding(2);
            this.hotkeyBox.Size = new System.Drawing.Size(236, 230);
            this.hotkeyBox.TabIndex = 86;
            this.hotkeyBox.TabStop = false;
            // 
            // swl_comboBox
            // 
            this.swl_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.swl_comboBox.FormattingEnabled = true;
            this.swl_comboBox.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.swl_comboBox.Location = new System.Drawing.Point(102, 172);
            this.swl_comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.swl_comboBox.Name = "swl_comboBox";
            this.swl_comboBox.Size = new System.Drawing.Size(67, 21);
            this.swl_comboBox.TabIndex = 94;
            // 
            // plus7
            // 
            this.plus7.AutoSize = true;
            this.plus7.ForeColor = System.Drawing.Color.Black;
            this.plus7.Location = new System.Drawing.Point(173, 175);
            this.plus7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus7.Name = "plus7";
            this.plus7.Size = new System.Drawing.Size(13, 13);
            this.plus7.TabIndex = 97;
            this.plus7.Text = "+";
            this.plus7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 15);
            this.label6.TabIndex = 96;
            this.label6.Text = "Switch Layouts :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // swl_textBox
            // 
            this.swl_textBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.swl_textBox.Location = new System.Drawing.Point(190, 173);
            this.swl_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.swl_textBox.MaxLength = 1;
            this.swl_textBox.Name = "swl_textBox";
            this.swl_textBox.ShortcutsEnabled = false;
            this.swl_textBox.Size = new System.Drawing.Size(36, 20);
            this.swl_textBox.TabIndex = 95;
            this.swl_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // csm_comboBox
            // 
            this.csm_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.csm_comboBox.FormattingEnabled = true;
            this.csm_comboBox.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.csm_comboBox.Location = new System.Drawing.Point(102, 148);
            this.csm_comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.csm_comboBox.Name = "csm_comboBox";
            this.csm_comboBox.Size = new System.Drawing.Size(67, 21);
            this.csm_comboBox.TabIndex = 90;
            // 
            // plus6
            // 
            this.plus6.AutoSize = true;
            this.plus6.ForeColor = System.Drawing.Color.Black;
            this.plus6.Location = new System.Drawing.Point(173, 151);
            this.plus6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus6.Name = "plus6";
            this.plus6.Size = new System.Drawing.Size(13, 13);
            this.plus6.TabIndex = 93;
            this.plus6.Text = "+";
            this.plus6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // csm_label
            // 
            this.csm_label.Location = new System.Drawing.Point(1, 151);
            this.csm_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.csm_label.Name = "csm_label";
            this.csm_label.Size = new System.Drawing.Size(94, 15);
            this.csm_label.TabIndex = 92;
            this.csm_label.Text = "Cutscenes Mode :";
            this.csm_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // csm_textBox
            // 
            this.csm_textBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.csm_textBox.Location = new System.Drawing.Point(190, 149);
            this.csm_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.csm_textBox.MaxLength = 1;
            this.csm_textBox.Name = "csm_textBox";
            this.csm_textBox.ShortcutsEnabled = false;
            this.csm_textBox.Size = new System.Drawing.Size(36, 20);
            this.csm_textBox.TabIndex = 91;
            this.csm_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // r1
            // 
            this.r1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.r1.FormattingEnabled = true;
            this.r1.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.r1.Location = new System.Drawing.Point(102, 124);
            this.r1.Margin = new System.Windows.Forms.Padding(2);
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(67, 21);
            this.r1.TabIndex = 86;
            // 
            // plus5
            // 
            this.plus5.AutoSize = true;
            this.plus5.ForeColor = System.Drawing.Color.Black;
            this.plus5.Location = new System.Drawing.Point(173, 127);
            this.plus5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus5.Name = "plus5";
            this.plus5.Size = new System.Drawing.Size(13, 13);
            this.plus5.TabIndex = 89;
            this.plus5.Text = "+";
            this.plus5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // settingsFocusCmb
            // 
            this.settingsFocusCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsFocusCmb.FormattingEnabled = true;
            this.settingsFocusCmb.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.settingsFocusCmb.Location = new System.Drawing.Point(102, 28);
            this.settingsFocusCmb.Margin = new System.Windows.Forms.Padding(2);
            this.settingsFocusCmb.Name = "settingsFocusCmb";
            this.settingsFocusCmb.Size = new System.Drawing.Size(67, 21);
            this.settingsFocusCmb.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 88;
            this.label5.Text = "Reset Windows:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // plus1
            // 
            this.plus1.AutoSize = true;
            this.plus1.ForeColor = System.Drawing.Color.Black;
            this.plus1.Location = new System.Drawing.Point(173, 31);
            this.plus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus1.Name = "plus1";
            this.plus1.Size = new System.Drawing.Size(13, 13);
            this.plus1.TabIndex = 34;
            this.plus1.Text = "+";
            this.plus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // r2
            // 
            this.r2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.r2.Location = new System.Drawing.Point(190, 125);
            this.r2.Margin = new System.Windows.Forms.Padding(2);
            this.r2.MaxLength = 1;
            this.r2.Name = "r2";
            this.r2.ShortcutsEnabled = false;
            this.r2.Size = new System.Drawing.Size(36, 20);
            this.r2.TabIndex = 87;
            this.r2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(3, 10);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(57, 15);
            this.label38.TabIndex = 28;
            this.label38.Text = "Hotkeys ";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label52
            // 
            this.label52.Location = new System.Drawing.Point(8, 31);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(87, 15);
            this.label52.TabIndex = 33;
            this.label52.Text = "Toggle Unfocus:";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // settingsFocusHKTxt
            // 
            this.settingsFocusHKTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingsFocusHKTxt.Location = new System.Drawing.Point(190, 29);
            this.settingsFocusHKTxt.Margin = new System.Windows.Forms.Padding(2);
            this.settingsFocusHKTxt.MaxLength = 1;
            this.settingsFocusHKTxt.Name = "settingsFocusHKTxt";
            this.settingsFocusHKTxt.ShortcutsEnabled = false;
            this.settingsFocusHKTxt.Size = new System.Drawing.Size(36, 20);
            this.settingsFocusHKTxt.TabIndex = 32;
            this.settingsFocusHKTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_lockKey
            // 
            this.comboBox_lockKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_lockKey.FormattingEnabled = true;
            this.comboBox_lockKey.Items.AddRange(new object[] {
            "End",
            "Home",
            "Delete",
            "Multiply",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12",
            "+",
            "-",
            "Numpad 0",
            "Numpad 1",
            "Numpad 2",
            "Numpad 3",
            "Numpad 4",
            "Numpad 5",
            "Numpad 6",
            "Numpad 7",
            "Numpad 8",
            "Numpad 9"});
            this.comboBox_lockKey.Location = new System.Drawing.Point(102, 196);
            this.comboBox_lockKey.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_lockKey.Name = "comboBox_lockKey";
            this.comboBox_lockKey.Size = new System.Drawing.Size(123, 21);
            this.comboBox_lockKey.TabIndex = 30;
            // 
            // label_lockKey
            // 
            this.label_lockKey.Location = new System.Drawing.Point(8, 197);
            this.label_lockKey.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_lockKey.Name = "label_lockKey";
            this.label_lockKey.Size = new System.Drawing.Size(86, 15);
            this.label_lockKey.TabIndex = 29;
            this.label_lockKey.Text = "Lock Input Key:";
            this.label_lockKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // settingsTopCmb
            // 
            this.settingsTopCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsTopCmb.FormattingEnabled = true;
            this.settingsTopCmb.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.settingsTopCmb.Location = new System.Drawing.Point(102, 100);
            this.settingsTopCmb.Margin = new System.Windows.Forms.Padding(2);
            this.settingsTopCmb.Name = "settingsTopCmb";
            this.settingsTopCmb.Size = new System.Drawing.Size(67, 21);
            this.settingsTopCmb.TabIndex = 5;
            // 
            // settingsStopCmb
            // 
            this.settingsStopCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsStopCmb.FormattingEnabled = true;
            this.settingsStopCmb.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.settingsStopCmb.Location = new System.Drawing.Point(102, 76);
            this.settingsStopCmb.Margin = new System.Windows.Forms.Padding(2);
            this.settingsStopCmb.Name = "settingsStopCmb";
            this.settingsStopCmb.Size = new System.Drawing.Size(67, 21);
            this.settingsStopCmb.TabIndex = 3;
            // 
            // settingsTopTxt
            // 
            this.settingsTopTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingsTopTxt.Location = new System.Drawing.Point(190, 101);
            this.settingsTopTxt.Margin = new System.Windows.Forms.Padding(2);
            this.settingsTopTxt.MaxLength = 1;
            this.settingsTopTxt.Name = "settingsTopTxt";
            this.settingsTopTxt.ShortcutsEnabled = false;
            this.settingsTopTxt.Size = new System.Drawing.Size(36, 20);
            this.settingsTopTxt.TabIndex = 6;
            this.settingsTopTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // settingsStopTxt
            // 
            this.settingsStopTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingsStopTxt.Location = new System.Drawing.Point(190, 77);
            this.settingsStopTxt.Margin = new System.Windows.Forms.Padding(2);
            this.settingsStopTxt.MaxLength = 1;
            this.settingsStopTxt.Name = "settingsStopTxt";
            this.settingsStopTxt.ShortcutsEnabled = false;
            this.settingsStopTxt.Size = new System.Drawing.Size(36, 20);
            this.settingsStopTxt.TabIndex = 4;
            this.settingsStopTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // settingsCloseCmb
            // 
            this.settingsCloseCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsCloseCmb.FormattingEnabled = true;
            this.settingsCloseCmb.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift"});
            this.settingsCloseCmb.Location = new System.Drawing.Point(102, 52);
            this.settingsCloseCmb.Margin = new System.Windows.Forms.Padding(2);
            this.settingsCloseCmb.Name = "settingsCloseCmb";
            this.settingsCloseCmb.Size = new System.Drawing.Size(67, 21);
            this.settingsCloseCmb.TabIndex = 1;
            // 
            // plus4
            // 
            this.plus4.AutoSize = true;
            this.plus4.ForeColor = System.Drawing.Color.Black;
            this.plus4.Location = new System.Drawing.Point(173, 103);
            this.plus4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus4.Name = "plus4";
            this.plus4.Size = new System.Drawing.Size(13, 13);
            this.plus4.TabIndex = 27;
            this.plus4.Text = "+";
            this.plus4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plus3
            // 
            this.plus3.AutoSize = true;
            this.plus3.ForeColor = System.Drawing.Color.Black;
            this.plus3.Location = new System.Drawing.Point(173, 78);
            this.plus3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus3.Name = "plus3";
            this.plus3.Size = new System.Drawing.Size(13, 13);
            this.plus3.TabIndex = 26;
            this.plus3.Text = "+";
            this.plus3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plus2
            // 
            this.plus2.AutoSize = true;
            this.plus2.ForeColor = System.Drawing.Color.Black;
            this.plus2.Location = new System.Drawing.Point(173, 55);
            this.plus2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.plus2.Name = "plus2";
            this.plus2.Size = new System.Drawing.Size(13, 13);
            this.plus2.TabIndex = 25;
            this.plus2.Text = "+";
            this.plus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Toggle Top Most:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Stop Session:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Close Nucleus:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // settingsCloseHKTxt
            // 
            this.settingsCloseHKTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingsCloseHKTxt.Location = new System.Drawing.Point(190, 53);
            this.settingsCloseHKTxt.Margin = new System.Windows.Forms.Padding(2);
            this.settingsCloseHKTxt.MaxLength = 1;
            this.settingsCloseHKTxt.Name = "settingsCloseHKTxt";
            this.settingsCloseHKTxt.ShortcutsEnabled = false;
            this.settingsCloseHKTxt.Size = new System.Drawing.Size(36, 20);
            this.settingsCloseHKTxt.TabIndex = 2;
            this.settingsCloseHKTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ctrlr_shorcuts
            // 
            this.ctrlr_shorcuts.BackColor = System.Drawing.Color.Transparent;
            this.ctrlr_shorcuts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ctrlr_shorcuts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ctrlr_shorcuts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ctrlr_shorcuts.Location = new System.Drawing.Point(241, 8);
            this.ctrlr_shorcuts.Name = "ctrlr_shorcuts";
            this.ctrlr_shorcuts.Size = new System.Drawing.Size(147, 27);
            this.ctrlr_shorcuts.TabIndex = 100;
            this.ctrlr_shorcuts.Text = "Gamepad Shortcuts Setup";
            this.ctrlr_shorcuts.UseVisualStyleBackColor = false;
            this.ctrlr_shorcuts.Click += new System.EventHandler(this.ctrlr_shorcuts_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.ctrlr_shorcuts);
            this.Controls.Add(this.nucUserPassTxt);
            this.Controls.Add(this.password_Label);
            this.Controls.Add(this.themeLabel);
            this.Controls.Add(this.themeCbx);
            this.Controls.Add(this.splashScreenChkB);
            this.Controls.Add(this.clickSoundChkB);
            this.Controls.Add(this.cmb_EpicLang);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.ignoreInputLockReminderCheckbox);
            this.Controls.Add(this.statusCheck);
            this.Controls.Add(this.cmb_Lang);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.debugLogCheck);
            this.Controls.Add(this.hotkeyBox);
            this.Controls.Add(this.settingLabel_Container);
            this.Controls.Add(this.settingsSaveBtn);
            this.Controls.Add(this.settingsCloseBtn);
            this.Controls.Add(this.btn_credits);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(491, 354);
            this.settingLabel_Container.ResumeLayout(false);
            this.hotkeyBox.ResumeLayout(false);
            this.hotkeyBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button settingsSaveBtn;
        private System.Windows.Forms.Button settingsCloseBtn;
        private System.Windows.Forms.Button btn_credits;
        private System.Windows.Forms.Label save_Label;
        private Panel settingLabel_Container;
        private Label password_Label;
        private TextBox nucUserPassTxt;
        private Label themeLabel;
        private ComboBox themeCbx;
        private CheckBox splashScreenChkB;
        private CheckBox clickSoundChkB;
        private ComboBox cmb_EpicLang;
        private Label label46;
        private CheckBox ignoreInputLockReminderCheckbox;
        private CheckBox statusCheck;
        private ComboBox cmb_Lang;
        private Label label34;
        private CheckBox debugLogCheck;
        private GroupBox hotkeyBox;
        private ComboBox r1;
        private Label plus5;
        private ComboBox settingsFocusCmb;
        private Label label5;
        private Label plus1;
        private TextBox r2;
        private Label label38;
        private Label label52;
        private TextBox settingsFocusHKTxt;
        private ComboBox comboBox_lockKey;
        private Label label_lockKey;
        private ComboBox settingsTopCmb;
        private ComboBox settingsStopCmb;
        private TextBox settingsTopTxt;
        private TextBox settingsStopTxt;
        private ComboBox settingsCloseCmb;
        private Label plus4;
        private Label plus3;
        private Label plus2;
        private Label label4;
        private Label label3;
        private Label label1;
        private TextBox settingsCloseHKTxt;
        private ComboBox csm_comboBox;
        private Label plus6;
        private Label csm_label;
        private TextBox csm_textBox;
        private ComboBox swl_comboBox;
        private Label plus7;
        private Label label6;
        private TextBox swl_textBox;
        private Button ctrlr_shorcuts;
    }
}