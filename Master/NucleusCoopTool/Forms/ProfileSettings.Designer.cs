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
    partial class ProfileSettings
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.sharedBtn = new System.Windows.Forms.Button();
            this.playersBtn = new System.Windows.Forms.Button();
            this.audioBtn = new System.Windows.Forms.Button();
            this.prioritiesBtn = new System.Windows.Forms.Button();
            this.saveBtnPicture = new System.Windows.Forms.PictureBox();
            this.audioBtnPicture = new System.Windows.Forms.PictureBox();
            this.playersBtnPicture = new System.Windows.Forms.PictureBox();
            this.sharedBtnPicture = new System.Windows.Forms.PictureBox();
            this.prioritiesBtnPicture = new System.Windows.Forms.PictureBox();
            this.closeBtnPicture = new System.Windows.Forms.PictureBox();
            this.modeLabel = new System.Windows.Forms.Label();
            this.priorities = new BufferedClientAreaPanel();
            this.PriorityClass32 = new System.Windows.Forms.ComboBox();
            this.PriorityClass31 = new System.Windows.Forms.ComboBox();
            this.PriorityClass30 = new System.Windows.Forms.ComboBox();
            this.PriorityClass29 = new System.Windows.Forms.ComboBox();
            this.PriorityClass28 = new System.Windows.Forms.ComboBox();
            this.PriorityClass27 = new System.Windows.Forms.ComboBox();
            this.PriorityClass26 = new System.Windows.Forms.ComboBox();
            this.PriorityClass25 = new System.Windows.Forms.ComboBox();
            this.PriorityClass16 = new System.Windows.Forms.ComboBox();
            this.PriorityClass15 = new System.Windows.Forms.ComboBox();
            this.PriorityClass14 = new System.Windows.Forms.ComboBox();
            this.PriorityClass13 = new System.Windows.Forms.ComboBox();
            this.PriorityClass12 = new System.Windows.Forms.ComboBox();
            this.PriorityClass11 = new System.Windows.Forms.ComboBox();
            this.PriorityClass10 = new System.Windows.Forms.ComboBox();
            this.PriorityClass9 = new System.Windows.Forms.ComboBox();
            this.PriorityClass24 = new System.Windows.Forms.ComboBox();
            this.PriorityClass23 = new System.Windows.Forms.ComboBox();
            this.PriorityClass22 = new System.Windows.Forms.ComboBox();
            this.PriorityClass21 = new System.Windows.Forms.ComboBox();
            this.PriorityClass20 = new System.Windows.Forms.ComboBox();
            this.PriorityClass19 = new System.Windows.Forms.ComboBox();
            this.PriorityClass18 = new System.Windows.Forms.ComboBox();
            this.PriorityClass17 = new System.Windows.Forms.ComboBox();
            this.PriorityClass8 = new System.Windows.Forms.ComboBox();
            this.PriorityClass7 = new System.Windows.Forms.ComboBox();
            this.PriorityClass6 = new System.Windows.Forms.ComboBox();
            this.PriorityClass5 = new System.Windows.Forms.ComboBox();
            this.PriorityClass4 = new System.Windows.Forms.ComboBox();
            this.PriorityClass3 = new System.Windows.Forms.ComboBox();
            this.PriorityClass2 = new System.Windows.Forms.ComboBox();
            this.PriorityClass1 = new System.Windows.Forms.ComboBox();
            this.Affinity32 = new System.Windows.Forms.ComboBox();
            this.Affinity31 = new System.Windows.Forms.ComboBox();
            this.Affinity30 = new System.Windows.Forms.ComboBox();
            this.Affinity29 = new System.Windows.Forms.ComboBox();
            this.Affinity28 = new System.Windows.Forms.ComboBox();
            this.Affinity27 = new System.Windows.Forms.ComboBox();
            this.Affinity26 = new System.Windows.Forms.ComboBox();
            this.Affinity25 = new System.Windows.Forms.ComboBox();
            this.Affinity24 = new System.Windows.Forms.ComboBox();
            this.Affinity23 = new System.Windows.Forms.ComboBox();
            this.Affinity22 = new System.Windows.Forms.ComboBox();
            this.Affinity21 = new System.Windows.Forms.ComboBox();
            this.Affinity20 = new System.Windows.Forms.ComboBox();
            this.Affinity19 = new System.Windows.Forms.ComboBox();
            this.Affinity18 = new System.Windows.Forms.ComboBox();
            this.Affinity17 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor32 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IdealProcessor31 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IdealProcessor30 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IdealProcessor29 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.IdealProcessor28 = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.IdealProcessor27 = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.IdealProcessor26 = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.IdealProcessor25 = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.IdealProcessor24 = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.IdealProcessor23 = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.IdealProcessor22 = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.IdealProcessor21 = new System.Windows.Forms.ComboBox();
            this.label66 = new System.Windows.Forms.Label();
            this.IdealProcessor20 = new System.Windows.Forms.ComboBox();
            this.label67 = new System.Windows.Forms.Label();
            this.IdealProcessor19 = new System.Windows.Forms.ComboBox();
            this.label68 = new System.Windows.Forms.Label();
            this.IdealProcessor18 = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.IdealProcessor17 = new System.Windows.Forms.ComboBox();
            this.label70 = new System.Windows.Forms.Label();
            this.Affinity16 = new System.Windows.Forms.ComboBox();
            this.Affinity15 = new System.Windows.Forms.ComboBox();
            this.Affinity14 = new System.Windows.Forms.ComboBox();
            this.Affinity13 = new System.Windows.Forms.ComboBox();
            this.Affinity12 = new System.Windows.Forms.ComboBox();
            this.Affinity11 = new System.Windows.Forms.ComboBox();
            this.Affinity10 = new System.Windows.Forms.ComboBox();
            this.Affinity9 = new System.Windows.Forms.ComboBox();
            this.Affinity8 = new System.Windows.Forms.ComboBox();
            this.Affinity7 = new System.Windows.Forms.ComboBox();
            this.Affinity6 = new System.Windows.Forms.ComboBox();
            this.Affinity5 = new System.Windows.Forms.ComboBox();
            this.Affinity4 = new System.Windows.Forms.ComboBox();
            this.Affinity3 = new System.Windows.Forms.ComboBox();
            this.Affinity2 = new System.Windows.Forms.ComboBox();
            this.Affinity_Label = new System.Windows.Forms.Label();
            this.Affinity1 = new System.Windows.Forms.ComboBox();
            this.PriorityClass_Label = new System.Windows.Forms.Label();
            this.IdealProcessor16 = new System.Windows.Forms.ComboBox();
            this.label76 = new System.Windows.Forms.Label();
            this.IdealProcessor15 = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.IdealProcessor14 = new System.Windows.Forms.ComboBox();
            this.label78 = new System.Windows.Forms.Label();
            this.IdealProcessor13 = new System.Windows.Forms.ComboBox();
            this.label79 = new System.Windows.Forms.Label();
            this.IdealProcessor12 = new System.Windows.Forms.ComboBox();
            this.label80 = new System.Windows.Forms.Label();
            this.IdealProcessor11 = new System.Windows.Forms.ComboBox();
            this.label81 = new System.Windows.Forms.Label();
            this.IdealProcessor10 = new System.Windows.Forms.ComboBox();
            this.label82 = new System.Windows.Forms.Label();
            this.IdealProcessor9 = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.IdealProcessor8 = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.IdealProcessor7 = new System.Windows.Forms.ComboBox();
            this.label85 = new System.Windows.Forms.Label();
            this.IdealProcessor6 = new System.Windows.Forms.ComboBox();
            this.label86 = new System.Windows.Forms.Label();
            this.IdealProcessor5 = new System.Windows.Forms.ComboBox();
            this.label87 = new System.Windows.Forms.Label();
            this.Instance_Label = new System.Windows.Forms.Label();
            this.IdealProcessor4 = new System.Windows.Forms.ComboBox();
            this.label89 = new System.Windows.Forms.Label();
            this.IdealProcessor3 = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.IdealProcessor2 = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.IdealProcessor_Label = new System.Windows.Forms.Label();
            this.IdealProcessor1 = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.Panel2 = new BufferedClientAreaPanel();
            this.steamid32 = new System.Windows.Forms.ComboBox();
            this.steamid31 = new System.Windows.Forms.ComboBox();
            this.steamid30 = new System.Windows.Forms.ComboBox();
            this.steamid29 = new System.Windows.Forms.ComboBox();
            this.steamid28 = new System.Windows.Forms.ComboBox();
            this.steamid27 = new System.Windows.Forms.ComboBox();
            this.steamid26 = new System.Windows.Forms.ComboBox();
            this.steamid25 = new System.Windows.Forms.ComboBox();
            this.steamid24 = new System.Windows.Forms.ComboBox();
            this.steamid23 = new System.Windows.Forms.ComboBox();
            this.steamid22 = new System.Windows.Forms.ComboBox();
            this.steamid21 = new System.Windows.Forms.ComboBox();
            this.steamid20 = new System.Windows.Forms.ComboBox();
            this.steamid19 = new System.Windows.Forms.ComboBox();
            this.steamid18 = new System.Windows.Forms.ComboBox();
            this.steamid17 = new System.Windows.Forms.ComboBox();
            this.player32N = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.player31N = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.player30N = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.player29N = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.player28N = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.player27N = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.player26N = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.player25N = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.player24N = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.player23N = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.player22N = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.player21N = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.player20N = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.player19N = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.player18N = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.player17N = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.def_sid_textBox_container = new System.Windows.Forms.Panel();
            this.default_sid_list_label = new System.Windows.Forms.Label();
            this.def_sid_comboBox = new System.Windows.Forms.ComboBox();
            this.steamid16 = new System.Windows.Forms.ComboBox();
            this.steamid15 = new System.Windows.Forms.ComboBox();
            this.steamid14 = new System.Windows.Forms.ComboBox();
            this.steamid13 = new System.Windows.Forms.ComboBox();
            this.steamid12 = new System.Windows.Forms.ComboBox();
            this.steamid11 = new System.Windows.Forms.ComboBox();
            this.steamid10 = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.steamid9 = new System.Windows.Forms.ComboBox();
            this.steamid8 = new System.Windows.Forms.ComboBox();
            this.steamid7 = new System.Windows.Forms.ComboBox();
            this.steamid6 = new System.Windows.Forms.ComboBox();
            this.steamid5 = new System.Windows.Forms.ComboBox();
            this.steamid4 = new System.Windows.Forms.ComboBox();
            this.steamid3 = new System.Windows.Forms.ComboBox();
            this.steamid2 = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.steamid1 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.player16N = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.player15N = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.player14N = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.player13N = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.player12N = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.player11N = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.player10N = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.player9N = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.player8N = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.player7N = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.player6N = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.player5N = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.player4N = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.player3N = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.player2N = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.player1N = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Panel1 = new BufferedClientAreaPanel();
            this.SplitColors = new Nucleus.Gaming.Controls.CustomComboBox();
            this.cmb_Network = new Nucleus.Gaming.Controls.CustomComboBox();
            this.autoPlay = new System.Windows.Forms.CheckBox();
            this.numMaxPlyrs = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.numUpDownHor = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.numUpDownVer = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.SplitDiv = new System.Windows.Forms.CheckBox();
            this.label49 = new System.Windows.Forms.Label();
            this.slitWarning_Label = new System.Windows.Forms.Label();
            this.layoutSizer = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.KeepSymLinkCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WIndowsSetupTiming_Label = new System.Windows.Forms.Label();
            this.WindowsSetupTiming_TextBox = new System.Windows.Forms.TextBox();
            this.pauseBetweenInstanceLaunch_TxtBox = new System.Windows.Forms.TextBox();
            this.pauseBetweenInstanceLauch_Label = new System.Windows.Forms.Label();
            this.scaleOptionCbx = new System.Windows.Forms.CheckBox();
            this.keepAccountsCheck = new System.Windows.Forms.CheckBox();
            this.label32 = new System.Windows.Forms.Label();
            this.useNicksCheck = new System.Windows.Forms.CheckBox();
            this.Panel3 = new BufferedClientAreaPanel();
            this.label39 = new System.Windows.Forms.Label();
            this.audioDefaultDevice = new System.Windows.Forms.Label();
            this.audioCustomSettingsBox = new System.Windows.Forms.GroupBox();
            this.AudioInstance8 = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.AudioInstance7 = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.AudioInstance6 = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.AudioInstance5 = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.AudioInstance4 = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.AudioInstance3 = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.AudioInstance2 = new System.Windows.Forms.ComboBox();
            this.AudioInstance1 = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.audioCustomSettingsRadio = new System.Windows.Forms.RadioButton();
            this.audioDefaultSettingsRadio = new System.Windows.Forms.RadioButton();
            this.audioRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.saveBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.audioBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playersBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prioritiesBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtnPicture)).BeginInit();
            this.priorities.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.def_sid_textBox_container.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.audioCustomSettingsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(438, 2);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(52, 23);
            this.closeBtn.TabIndex = 9;
            this.closeBtn.Text = "Close";
            this.closeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.SettingsCloseBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.saveBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveBtn.Location = new System.Drawing.Point(365, 2);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(0);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(52, 23);
            this.saveBtn.TabIndex = 8;
            this.saveBtn.Text = "Save";
            this.saveBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.SettingsSaveBtn_Click);
            // 
            // sharedBtn
            // 
            this.sharedBtn.BackColor = System.Drawing.Color.Transparent;
            this.sharedBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sharedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sharedBtn.Location = new System.Drawing.Point(1, 2);
            this.sharedBtn.Margin = new System.Windows.Forms.Padding(0);
            this.sharedBtn.Name = "sharedBtn";
            this.sharedBtn.Size = new System.Drawing.Size(70, 23);
            this.sharedBtn.TabIndex = 38;
            this.sharedBtn.Text = "Shared";
            this.sharedBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sharedBtn.UseVisualStyleBackColor = false;
            this.sharedBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // playersBtn
            // 
            this.playersBtn.BackColor = System.Drawing.Color.Transparent;
            this.playersBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.playersBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.playersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playersBtn.Location = new System.Drawing.Point(92, 2);
            this.playersBtn.Margin = new System.Windows.Forms.Padding(0);
            this.playersBtn.Name = "playersBtn";
            this.playersBtn.Size = new System.Drawing.Size(70, 23);
            this.playersBtn.TabIndex = 43;
            this.playersBtn.Text = "Players";
            this.playersBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.playersBtn.UseVisualStyleBackColor = false;
            this.playersBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // audioBtn
            // 
            this.audioBtn.BackColor = System.Drawing.Color.Transparent;
            this.audioBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.audioBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.audioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioBtn.Location = new System.Drawing.Point(183, 2);
            this.audioBtn.Margin = new System.Windows.Forms.Padding(0);
            this.audioBtn.Name = "audioBtn";
            this.audioBtn.Size = new System.Drawing.Size(70, 23);
            this.audioBtn.TabIndex = 44;
            this.audioBtn.Text = "Audio";
            this.audioBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.audioBtn.UseVisualStyleBackColor = false;
            this.audioBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // prioritiesBtn
            // 
            this.prioritiesBtn.BackColor = System.Drawing.Color.Transparent;
            this.prioritiesBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.prioritiesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.prioritiesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prioritiesBtn.Location = new System.Drawing.Point(274, 2);
            this.prioritiesBtn.Margin = new System.Windows.Forms.Padding(0);
            this.prioritiesBtn.Name = "prioritiesBtn";
            this.prioritiesBtn.Size = new System.Drawing.Size(70, 23);
            this.prioritiesBtn.TabIndex = 45;
            this.prioritiesBtn.Text = "Priorities";
            this.prioritiesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.prioritiesBtn.UseVisualStyleBackColor = false;
            this.prioritiesBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // saveBtnPicture
            // 
            this.saveBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.saveBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBtnPicture.Location = new System.Drawing.Point(418, 4);
            this.saveBtnPicture.Name = "saveBtnPicture";
            this.saveBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.saveBtnPicture.TabIndex = 293;
            this.saveBtnPicture.TabStop = false;
            // 
            // audioBtnPicture
            // 
            this.audioBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.audioBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.audioBtnPicture.Location = new System.Drawing.Point(254, 4);
            this.audioBtnPicture.Name = "audioBtnPicture";
            this.audioBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.audioBtnPicture.TabIndex = 294;
            this.audioBtnPicture.TabStop = false;
            // 
            // playersBtnPicture
            // 
            this.playersBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.playersBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playersBtnPicture.Location = new System.Drawing.Point(163, 4);
            this.playersBtnPicture.Name = "playersBtnPicture";
            this.playersBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.playersBtnPicture.TabIndex = 295;
            this.playersBtnPicture.TabStop = false;
            // 
            // sharedBtnPicture
            // 
            this.sharedBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.sharedBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sharedBtnPicture.Location = new System.Drawing.Point(72, 4);
            this.sharedBtnPicture.Name = "sharedBtnPicture";
            this.sharedBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.sharedBtnPicture.TabIndex = 296;
            this.sharedBtnPicture.TabStop = false;
            // 
            // prioritiesBtnPicture
            // 
            this.prioritiesBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.prioritiesBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.prioritiesBtnPicture.Location = new System.Drawing.Point(345, 4);
            this.prioritiesBtnPicture.Name = "prioritiesBtnPicture";
            this.prioritiesBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.prioritiesBtnPicture.TabIndex = 297;
            this.prioritiesBtnPicture.TabStop = false;
            // 
            // closeBtnPicture
            // 
            this.closeBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.closeBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtnPicture.Location = new System.Drawing.Point(491, 4);
            this.closeBtnPicture.Name = "closeBtnPicture";
            this.closeBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.closeBtnPicture.TabIndex = 298;
            this.closeBtnPicture.TabStop = false;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.BackColor = System.Drawing.Color.Transparent;
            this.modeLabel.ForeColor = System.Drawing.Color.Orange;
            this.modeLabel.Location = new System.Drawing.Point(546, 4);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(95, 16);
            this.modeLabel.TabIndex = 299;
            this.modeLabel.Text = "Creation Mode";
            // 
            // priorities
            // 
            this.priorities.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.priorities.Controls.Add(this.PriorityClass32);
            this.priorities.Controls.Add(this.PriorityClass31);
            this.priorities.Controls.Add(this.PriorityClass30);
            this.priorities.Controls.Add(this.PriorityClass29);
            this.priorities.Controls.Add(this.PriorityClass28);
            this.priorities.Controls.Add(this.PriorityClass27);
            this.priorities.Controls.Add(this.PriorityClass26);
            this.priorities.Controls.Add(this.PriorityClass25);
            this.priorities.Controls.Add(this.PriorityClass16);
            this.priorities.Controls.Add(this.PriorityClass15);
            this.priorities.Controls.Add(this.PriorityClass14);
            this.priorities.Controls.Add(this.PriorityClass13);
            this.priorities.Controls.Add(this.PriorityClass12);
            this.priorities.Controls.Add(this.PriorityClass11);
            this.priorities.Controls.Add(this.PriorityClass10);
            this.priorities.Controls.Add(this.PriorityClass9);
            this.priorities.Controls.Add(this.PriorityClass24);
            this.priorities.Controls.Add(this.PriorityClass23);
            this.priorities.Controls.Add(this.PriorityClass22);
            this.priorities.Controls.Add(this.PriorityClass21);
            this.priorities.Controls.Add(this.PriorityClass20);
            this.priorities.Controls.Add(this.PriorityClass19);
            this.priorities.Controls.Add(this.PriorityClass18);
            this.priorities.Controls.Add(this.PriorityClass17);
            this.priorities.Controls.Add(this.PriorityClass8);
            this.priorities.Controls.Add(this.PriorityClass7);
            this.priorities.Controls.Add(this.PriorityClass6);
            this.priorities.Controls.Add(this.PriorityClass5);
            this.priorities.Controls.Add(this.PriorityClass4);
            this.priorities.Controls.Add(this.PriorityClass3);
            this.priorities.Controls.Add(this.PriorityClass2);
            this.priorities.Controls.Add(this.PriorityClass1);
            this.priorities.Controls.Add(this.Affinity32);
            this.priorities.Controls.Add(this.Affinity31);
            this.priorities.Controls.Add(this.Affinity30);
            this.priorities.Controls.Add(this.Affinity29);
            this.priorities.Controls.Add(this.Affinity28);
            this.priorities.Controls.Add(this.Affinity27);
            this.priorities.Controls.Add(this.Affinity26);
            this.priorities.Controls.Add(this.Affinity25);
            this.priorities.Controls.Add(this.Affinity24);
            this.priorities.Controls.Add(this.Affinity23);
            this.priorities.Controls.Add(this.Affinity22);
            this.priorities.Controls.Add(this.Affinity21);
            this.priorities.Controls.Add(this.Affinity20);
            this.priorities.Controls.Add(this.Affinity19);
            this.priorities.Controls.Add(this.Affinity18);
            this.priorities.Controls.Add(this.Affinity17);
            this.priorities.Controls.Add(this.IdealProcessor32);
            this.priorities.Controls.Add(this.label1);
            this.priorities.Controls.Add(this.IdealProcessor31);
            this.priorities.Controls.Add(this.label3);
            this.priorities.Controls.Add(this.IdealProcessor30);
            this.priorities.Controls.Add(this.label4);
            this.priorities.Controls.Add(this.IdealProcessor29);
            this.priorities.Controls.Add(this.label5);
            this.priorities.Controls.Add(this.IdealProcessor28);
            this.priorities.Controls.Add(this.label30);
            this.priorities.Controls.Add(this.IdealProcessor27);
            this.priorities.Controls.Add(this.label56);
            this.priorities.Controls.Add(this.IdealProcessor26);
            this.priorities.Controls.Add(this.label60);
            this.priorities.Controls.Add(this.IdealProcessor25);
            this.priorities.Controls.Add(this.label62);
            this.priorities.Controls.Add(this.IdealProcessor24);
            this.priorities.Controls.Add(this.label63);
            this.priorities.Controls.Add(this.IdealProcessor23);
            this.priorities.Controls.Add(this.label64);
            this.priorities.Controls.Add(this.IdealProcessor22);
            this.priorities.Controls.Add(this.label65);
            this.priorities.Controls.Add(this.IdealProcessor21);
            this.priorities.Controls.Add(this.label66);
            this.priorities.Controls.Add(this.IdealProcessor20);
            this.priorities.Controls.Add(this.label67);
            this.priorities.Controls.Add(this.IdealProcessor19);
            this.priorities.Controls.Add(this.label68);
            this.priorities.Controls.Add(this.IdealProcessor18);
            this.priorities.Controls.Add(this.label69);
            this.priorities.Controls.Add(this.IdealProcessor17);
            this.priorities.Controls.Add(this.label70);
            this.priorities.Controls.Add(this.Affinity16);
            this.priorities.Controls.Add(this.Affinity15);
            this.priorities.Controls.Add(this.Affinity14);
            this.priorities.Controls.Add(this.Affinity13);
            this.priorities.Controls.Add(this.Affinity12);
            this.priorities.Controls.Add(this.Affinity11);
            this.priorities.Controls.Add(this.Affinity10);
            this.priorities.Controls.Add(this.Affinity9);
            this.priorities.Controls.Add(this.Affinity8);
            this.priorities.Controls.Add(this.Affinity7);
            this.priorities.Controls.Add(this.Affinity6);
            this.priorities.Controls.Add(this.Affinity5);
            this.priorities.Controls.Add(this.Affinity4);
            this.priorities.Controls.Add(this.Affinity3);
            this.priorities.Controls.Add(this.Affinity2);
            this.priorities.Controls.Add(this.Affinity_Label);
            this.priorities.Controls.Add(this.Affinity1);
            this.priorities.Controls.Add(this.PriorityClass_Label);
            this.priorities.Controls.Add(this.IdealProcessor16);
            this.priorities.Controls.Add(this.label76);
            this.priorities.Controls.Add(this.IdealProcessor15);
            this.priorities.Controls.Add(this.label77);
            this.priorities.Controls.Add(this.IdealProcessor14);
            this.priorities.Controls.Add(this.label78);
            this.priorities.Controls.Add(this.IdealProcessor13);
            this.priorities.Controls.Add(this.label79);
            this.priorities.Controls.Add(this.IdealProcessor12);
            this.priorities.Controls.Add(this.label80);
            this.priorities.Controls.Add(this.IdealProcessor11);
            this.priorities.Controls.Add(this.label81);
            this.priorities.Controls.Add(this.IdealProcessor10);
            this.priorities.Controls.Add(this.label82);
            this.priorities.Controls.Add(this.IdealProcessor9);
            this.priorities.Controls.Add(this.label83);
            this.priorities.Controls.Add(this.IdealProcessor8);
            this.priorities.Controls.Add(this.label84);
            this.priorities.Controls.Add(this.IdealProcessor7);
            this.priorities.Controls.Add(this.label85);
            this.priorities.Controls.Add(this.IdealProcessor6);
            this.priorities.Controls.Add(this.label86);
            this.priorities.Controls.Add(this.IdealProcessor5);
            this.priorities.Controls.Add(this.label87);
            this.priorities.Controls.Add(this.Instance_Label);
            this.priorities.Controls.Add(this.IdealProcessor4);
            this.priorities.Controls.Add(this.label89);
            this.priorities.Controls.Add(this.IdealProcessor3);
            this.priorities.Controls.Add(this.label90);
            this.priorities.Controls.Add(this.IdealProcessor2);
            this.priorities.Controls.Add(this.label91);
            this.priorities.Controls.Add(this.IdealProcessor_Label);
            this.priorities.Controls.Add(this.IdealProcessor1);
            this.priorities.Controls.Add(this.label93);
            this.priorities.Location = new System.Drawing.Point(6, 38);
            this.priorities.Name = "priorities";
            this.priorities.Size = new System.Drawing.Size(671, 401);
            this.priorities.TabIndex = 300;
            // 
            // PriorityClass32
            // 
            this.PriorityClass32.BackColor = System.Drawing.Color.White;
            this.PriorityClass32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass32.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass32.Location = new System.Drawing.Point(560, 342);
            this.PriorityClass32.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass32.MaxLength = 17;
            this.PriorityClass32.Name = "PriorityClass32";
            this.PriorityClass32.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass32.TabIndex = 323;
            // 
            // PriorityClass31
            // 
            this.PriorityClass31.BackColor = System.Drawing.Color.White;
            this.PriorityClass31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass31.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass31.Location = new System.Drawing.Point(560, 323);
            this.PriorityClass31.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass31.MaxLength = 17;
            this.PriorityClass31.Name = "PriorityClass31";
            this.PriorityClass31.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass31.TabIndex = 322;
            // 
            // PriorityClass30
            // 
            this.PriorityClass30.BackColor = System.Drawing.Color.White;
            this.PriorityClass30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass30.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass30.Location = new System.Drawing.Point(560, 304);
            this.PriorityClass30.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass30.MaxLength = 17;
            this.PriorityClass30.Name = "PriorityClass30";
            this.PriorityClass30.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass30.TabIndex = 321;
            // 
            // PriorityClass29
            // 
            this.PriorityClass29.BackColor = System.Drawing.Color.White;
            this.PriorityClass29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass29.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass29.Location = new System.Drawing.Point(560, 285);
            this.PriorityClass29.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass29.MaxLength = 17;
            this.PriorityClass29.Name = "PriorityClass29";
            this.PriorityClass29.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass29.TabIndex = 320;
            // 
            // PriorityClass28
            // 
            this.PriorityClass28.BackColor = System.Drawing.Color.White;
            this.PriorityClass28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass28.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass28.Location = new System.Drawing.Point(560, 266);
            this.PriorityClass28.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass28.MaxLength = 17;
            this.PriorityClass28.Name = "PriorityClass28";
            this.PriorityClass28.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass28.TabIndex = 319;
            // 
            // PriorityClass27
            // 
            this.PriorityClass27.BackColor = System.Drawing.Color.White;
            this.PriorityClass27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass27.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass27.Location = new System.Drawing.Point(560, 247);
            this.PriorityClass27.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass27.MaxLength = 17;
            this.PriorityClass27.Name = "PriorityClass27";
            this.PriorityClass27.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass27.TabIndex = 318;
            // 
            // PriorityClass26
            // 
            this.PriorityClass26.BackColor = System.Drawing.Color.White;
            this.PriorityClass26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass26.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass26.Location = new System.Drawing.Point(560, 228);
            this.PriorityClass26.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass26.MaxLength = 17;
            this.PriorityClass26.Name = "PriorityClass26";
            this.PriorityClass26.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass26.TabIndex = 317;
            // 
            // PriorityClass25
            // 
            this.PriorityClass25.BackColor = System.Drawing.Color.White;
            this.PriorityClass25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass25.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass25.Location = new System.Drawing.Point(560, 209);
            this.PriorityClass25.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass25.MaxLength = 17;
            this.PriorityClass25.Name = "PriorityClass25";
            this.PriorityClass25.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass25.TabIndex = 316;
            // 
            // PriorityClass16
            // 
            this.PriorityClass16.BackColor = System.Drawing.Color.White;
            this.PriorityClass16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass16.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass16.Location = new System.Drawing.Point(560, 175);
            this.PriorityClass16.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass16.MaxLength = 17;
            this.PriorityClass16.Name = "PriorityClass16";
            this.PriorityClass16.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass16.TabIndex = 315;
            // 
            // PriorityClass15
            // 
            this.PriorityClass15.BackColor = System.Drawing.Color.White;
            this.PriorityClass15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass15.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass15.Location = new System.Drawing.Point(560, 156);
            this.PriorityClass15.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass15.MaxLength = 17;
            this.PriorityClass15.Name = "PriorityClass15";
            this.PriorityClass15.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass15.TabIndex = 314;
            // 
            // PriorityClass14
            // 
            this.PriorityClass14.BackColor = System.Drawing.Color.White;
            this.PriorityClass14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass14.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass14.Location = new System.Drawing.Point(560, 137);
            this.PriorityClass14.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass14.MaxLength = 17;
            this.PriorityClass14.Name = "PriorityClass14";
            this.PriorityClass14.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass14.TabIndex = 313;
            // 
            // PriorityClass13
            // 
            this.PriorityClass13.BackColor = System.Drawing.Color.White;
            this.PriorityClass13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass13.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass13.Location = new System.Drawing.Point(560, 118);
            this.PriorityClass13.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass13.MaxLength = 17;
            this.PriorityClass13.Name = "PriorityClass13";
            this.PriorityClass13.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass13.TabIndex = 312;
            // 
            // PriorityClass12
            // 
            this.PriorityClass12.BackColor = System.Drawing.Color.White;
            this.PriorityClass12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass12.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass12.Location = new System.Drawing.Point(560, 99);
            this.PriorityClass12.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass12.MaxLength = 17;
            this.PriorityClass12.Name = "PriorityClass12";
            this.PriorityClass12.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass12.TabIndex = 311;
            // 
            // PriorityClass11
            // 
            this.PriorityClass11.BackColor = System.Drawing.Color.White;
            this.PriorityClass11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass11.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass11.Location = new System.Drawing.Point(560, 80);
            this.PriorityClass11.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass11.MaxLength = 17;
            this.PriorityClass11.Name = "PriorityClass11";
            this.PriorityClass11.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass11.TabIndex = 310;
            // 
            // PriorityClass10
            // 
            this.PriorityClass10.BackColor = System.Drawing.Color.White;
            this.PriorityClass10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass10.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass10.Location = new System.Drawing.Point(560, 61);
            this.PriorityClass10.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass10.MaxLength = 17;
            this.PriorityClass10.Name = "PriorityClass10";
            this.PriorityClass10.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass10.TabIndex = 309;
            // 
            // PriorityClass9
            // 
            this.PriorityClass9.BackColor = System.Drawing.Color.White;
            this.PriorityClass9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass9.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass9.Location = new System.Drawing.Point(560, 42);
            this.PriorityClass9.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass9.MaxLength = 17;
            this.PriorityClass9.Name = "PriorityClass9";
            this.PriorityClass9.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass9.TabIndex = 308;
            // 
            // PriorityClass24
            // 
            this.PriorityClass24.BackColor = System.Drawing.Color.White;
            this.PriorityClass24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass24.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass24.Location = new System.Drawing.Point(158, 344);
            this.PriorityClass24.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass24.MaxLength = 17;
            this.PriorityClass24.Name = "PriorityClass24";
            this.PriorityClass24.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass24.TabIndex = 307;
            // 
            // PriorityClass23
            // 
            this.PriorityClass23.BackColor = System.Drawing.Color.White;
            this.PriorityClass23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass23.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass23.Location = new System.Drawing.Point(158, 325);
            this.PriorityClass23.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass23.MaxLength = 17;
            this.PriorityClass23.Name = "PriorityClass23";
            this.PriorityClass23.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass23.TabIndex = 306;
            // 
            // PriorityClass22
            // 
            this.PriorityClass22.BackColor = System.Drawing.Color.White;
            this.PriorityClass22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass22.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass22.Location = new System.Drawing.Point(158, 306);
            this.PriorityClass22.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass22.MaxLength = 17;
            this.PriorityClass22.Name = "PriorityClass22";
            this.PriorityClass22.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass22.TabIndex = 305;
            // 
            // PriorityClass21
            // 
            this.PriorityClass21.BackColor = System.Drawing.Color.White;
            this.PriorityClass21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass21.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass21.Location = new System.Drawing.Point(158, 287);
            this.PriorityClass21.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass21.MaxLength = 17;
            this.PriorityClass21.Name = "PriorityClass21";
            this.PriorityClass21.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass21.TabIndex = 304;
            // 
            // PriorityClass20
            // 
            this.PriorityClass20.BackColor = System.Drawing.Color.White;
            this.PriorityClass20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass20.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass20.Location = new System.Drawing.Point(158, 268);
            this.PriorityClass20.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass20.MaxLength = 17;
            this.PriorityClass20.Name = "PriorityClass20";
            this.PriorityClass20.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass20.TabIndex = 303;
            // 
            // PriorityClass19
            // 
            this.PriorityClass19.BackColor = System.Drawing.Color.White;
            this.PriorityClass19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass19.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass19.Location = new System.Drawing.Point(158, 249);
            this.PriorityClass19.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass19.MaxLength = 17;
            this.PriorityClass19.Name = "PriorityClass19";
            this.PriorityClass19.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass19.TabIndex = 302;
            // 
            // PriorityClass18
            // 
            this.PriorityClass18.BackColor = System.Drawing.Color.White;
            this.PriorityClass18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass18.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass18.Location = new System.Drawing.Point(158, 230);
            this.PriorityClass18.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass18.MaxLength = 17;
            this.PriorityClass18.Name = "PriorityClass18";
            this.PriorityClass18.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass18.TabIndex = 301;
            // 
            // PriorityClass17
            // 
            this.PriorityClass17.BackColor = System.Drawing.Color.White;
            this.PriorityClass17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass17.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass17.Location = new System.Drawing.Point(158, 211);
            this.PriorityClass17.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass17.MaxLength = 17;
            this.PriorityClass17.Name = "PriorityClass17";
            this.PriorityClass17.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass17.TabIndex = 300;
            // 
            // PriorityClass8
            // 
            this.PriorityClass8.BackColor = System.Drawing.Color.White;
            this.PriorityClass8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass8.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass8.Location = new System.Drawing.Point(158, 177);
            this.PriorityClass8.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass8.MaxLength = 17;
            this.PriorityClass8.Name = "PriorityClass8";
            this.PriorityClass8.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass8.TabIndex = 299;
            // 
            // PriorityClass7
            // 
            this.PriorityClass7.BackColor = System.Drawing.Color.White;
            this.PriorityClass7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass7.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass7.Location = new System.Drawing.Point(158, 158);
            this.PriorityClass7.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass7.MaxLength = 17;
            this.PriorityClass7.Name = "PriorityClass7";
            this.PriorityClass7.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass7.TabIndex = 298;
            // 
            // PriorityClass6
            // 
            this.PriorityClass6.BackColor = System.Drawing.Color.White;
            this.PriorityClass6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass6.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass6.Location = new System.Drawing.Point(158, 139);
            this.PriorityClass6.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass6.MaxLength = 17;
            this.PriorityClass6.Name = "PriorityClass6";
            this.PriorityClass6.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass6.TabIndex = 297;
            // 
            // PriorityClass5
            // 
            this.PriorityClass5.BackColor = System.Drawing.Color.White;
            this.PriorityClass5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass5.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass5.Location = new System.Drawing.Point(158, 120);
            this.PriorityClass5.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass5.MaxLength = 17;
            this.PriorityClass5.Name = "PriorityClass5";
            this.PriorityClass5.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass5.TabIndex = 296;
            // 
            // PriorityClass4
            // 
            this.PriorityClass4.BackColor = System.Drawing.Color.White;
            this.PriorityClass4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass4.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass4.Location = new System.Drawing.Point(158, 101);
            this.PriorityClass4.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass4.MaxLength = 17;
            this.PriorityClass4.Name = "PriorityClass4";
            this.PriorityClass4.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass4.TabIndex = 295;
            // 
            // PriorityClass3
            // 
            this.PriorityClass3.BackColor = System.Drawing.Color.White;
            this.PriorityClass3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass3.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass3.Location = new System.Drawing.Point(158, 82);
            this.PriorityClass3.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass3.MaxLength = 17;
            this.PriorityClass3.Name = "PriorityClass3";
            this.PriorityClass3.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass3.TabIndex = 294;
            // 
            // PriorityClass2
            // 
            this.PriorityClass2.BackColor = System.Drawing.Color.White;
            this.PriorityClass2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass2.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass2.Location = new System.Drawing.Point(158, 63);
            this.PriorityClass2.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass2.MaxLength = 17;
            this.PriorityClass2.Name = "PriorityClass2";
            this.PriorityClass2.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass2.TabIndex = 293;
            // 
            // PriorityClass1
            // 
            this.PriorityClass1.BackColor = System.Drawing.Color.White;
            this.PriorityClass1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass1.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass1.Location = new System.Drawing.Point(158, 44);
            this.PriorityClass1.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass1.MaxLength = 17;
            this.PriorityClass1.Name = "PriorityClass1";
            this.PriorityClass1.Size = new System.Drawing.Size(86, 24);
            this.PriorityClass1.TabIndex = 292;
            // 
            // Affinity32
            // 
            this.Affinity32.BackColor = System.Drawing.Color.White;
            this.Affinity32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity32.Location = new System.Drawing.Point(508, 342);
            this.Affinity32.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity32.MaxLength = 17;
            this.Affinity32.Name = "Affinity32";
            this.Affinity32.Size = new System.Drawing.Size(48, 24);
            this.Affinity32.TabIndex = 291;
            // 
            // Affinity31
            // 
            this.Affinity31.BackColor = System.Drawing.Color.White;
            this.Affinity31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity31.Location = new System.Drawing.Point(508, 323);
            this.Affinity31.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity31.MaxLength = 17;
            this.Affinity31.Name = "Affinity31";
            this.Affinity31.Size = new System.Drawing.Size(48, 24);
            this.Affinity31.TabIndex = 290;
            // 
            // Affinity30
            // 
            this.Affinity30.BackColor = System.Drawing.Color.White;
            this.Affinity30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity30.Location = new System.Drawing.Point(508, 304);
            this.Affinity30.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity30.MaxLength = 17;
            this.Affinity30.Name = "Affinity30";
            this.Affinity30.Size = new System.Drawing.Size(48, 24);
            this.Affinity30.TabIndex = 289;
            // 
            // Affinity29
            // 
            this.Affinity29.BackColor = System.Drawing.Color.White;
            this.Affinity29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity29.Location = new System.Drawing.Point(508, 285);
            this.Affinity29.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity29.MaxLength = 17;
            this.Affinity29.Name = "Affinity29";
            this.Affinity29.Size = new System.Drawing.Size(48, 24);
            this.Affinity29.TabIndex = 288;
            // 
            // Affinity28
            // 
            this.Affinity28.BackColor = System.Drawing.Color.White;
            this.Affinity28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity28.Location = new System.Drawing.Point(508, 266);
            this.Affinity28.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity28.MaxLength = 17;
            this.Affinity28.Name = "Affinity28";
            this.Affinity28.Size = new System.Drawing.Size(48, 24);
            this.Affinity28.TabIndex = 287;
            // 
            // Affinity27
            // 
            this.Affinity27.BackColor = System.Drawing.Color.White;
            this.Affinity27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity27.Location = new System.Drawing.Point(508, 247);
            this.Affinity27.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity27.MaxLength = 17;
            this.Affinity27.Name = "Affinity27";
            this.Affinity27.Size = new System.Drawing.Size(48, 24);
            this.Affinity27.TabIndex = 286;
            // 
            // Affinity26
            // 
            this.Affinity26.BackColor = System.Drawing.Color.White;
            this.Affinity26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity26.Location = new System.Drawing.Point(508, 228);
            this.Affinity26.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity26.MaxLength = 17;
            this.Affinity26.Name = "Affinity26";
            this.Affinity26.Size = new System.Drawing.Size(48, 24);
            this.Affinity26.TabIndex = 285;
            // 
            // Affinity25
            // 
            this.Affinity25.BackColor = System.Drawing.Color.White;
            this.Affinity25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity25.Location = new System.Drawing.Point(508, 209);
            this.Affinity25.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity25.MaxLength = 17;
            this.Affinity25.Name = "Affinity25";
            this.Affinity25.Size = new System.Drawing.Size(48, 24);
            this.Affinity25.TabIndex = 284;
            // 
            // Affinity24
            // 
            this.Affinity24.BackColor = System.Drawing.Color.White;
            this.Affinity24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity24.Location = new System.Drawing.Point(106, 344);
            this.Affinity24.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity24.MaxLength = 2;
            this.Affinity24.Name = "Affinity24";
            this.Affinity24.Size = new System.Drawing.Size(48, 24);
            this.Affinity24.TabIndex = 283;
            // 
            // Affinity23
            // 
            this.Affinity23.BackColor = System.Drawing.Color.White;
            this.Affinity23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity23.Location = new System.Drawing.Point(106, 325);
            this.Affinity23.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity23.MaxLength = 2;
            this.Affinity23.Name = "Affinity23";
            this.Affinity23.Size = new System.Drawing.Size(48, 24);
            this.Affinity23.TabIndex = 282;
            // 
            // Affinity22
            // 
            this.Affinity22.BackColor = System.Drawing.Color.White;
            this.Affinity22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity22.Location = new System.Drawing.Point(106, 306);
            this.Affinity22.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity22.MaxLength = 2;
            this.Affinity22.Name = "Affinity22";
            this.Affinity22.Size = new System.Drawing.Size(48, 24);
            this.Affinity22.TabIndex = 281;
            // 
            // Affinity21
            // 
            this.Affinity21.BackColor = System.Drawing.Color.White;
            this.Affinity21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity21.Location = new System.Drawing.Point(106, 287);
            this.Affinity21.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity21.MaxLength = 2;
            this.Affinity21.Name = "Affinity21";
            this.Affinity21.Size = new System.Drawing.Size(48, 24);
            this.Affinity21.TabIndex = 280;
            // 
            // Affinity20
            // 
            this.Affinity20.BackColor = System.Drawing.Color.White;
            this.Affinity20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity20.Location = new System.Drawing.Point(106, 268);
            this.Affinity20.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity20.MaxLength = 2;
            this.Affinity20.Name = "Affinity20";
            this.Affinity20.Size = new System.Drawing.Size(48, 24);
            this.Affinity20.TabIndex = 279;
            // 
            // Affinity19
            // 
            this.Affinity19.BackColor = System.Drawing.Color.White;
            this.Affinity19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity19.Location = new System.Drawing.Point(106, 249);
            this.Affinity19.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity19.MaxLength = 2;
            this.Affinity19.Name = "Affinity19";
            this.Affinity19.Size = new System.Drawing.Size(48, 24);
            this.Affinity19.TabIndex = 278;
            // 
            // Affinity18
            // 
            this.Affinity18.BackColor = System.Drawing.Color.White;
            this.Affinity18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity18.Location = new System.Drawing.Point(106, 230);
            this.Affinity18.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity18.MaxLength = 2;
            this.Affinity18.Name = "Affinity18";
            this.Affinity18.Size = new System.Drawing.Size(48, 24);
            this.Affinity18.TabIndex = 277;
            // 
            // Affinity17
            // 
            this.Affinity17.BackColor = System.Drawing.Color.White;
            this.Affinity17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity17.Location = new System.Drawing.Point(106, 211);
            this.Affinity17.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity17.MaxLength = 2;
            this.Affinity17.Name = "Affinity17";
            this.Affinity17.Size = new System.Drawing.Size(48, 24);
            this.Affinity17.TabIndex = 276;
            // 
            // IdealProcessor32
            // 
            this.IdealProcessor32.BackColor = System.Drawing.Color.White;
            this.IdealProcessor32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor32.Location = new System.Drawing.Point(456, 342);
            this.IdealProcessor32.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor32.MaxLength = 9;
            this.IdealProcessor32.Name = "IdealProcessor32";
            this.IdealProcessor32.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor32.TabIndex = 274;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 347);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 275;
            this.label1.Text = "32 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor31
            // 
            this.IdealProcessor31.BackColor = System.Drawing.Color.White;
            this.IdealProcessor31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor31.Location = new System.Drawing.Point(456, 323);
            this.IdealProcessor31.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor31.MaxLength = 9;
            this.IdealProcessor31.Name = "IdealProcessor31";
            this.IdealProcessor31.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor31.TabIndex = 272;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 327);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 273;
            this.label3.Text = "31 :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor30
            // 
            this.IdealProcessor30.BackColor = System.Drawing.Color.White;
            this.IdealProcessor30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor30.Location = new System.Drawing.Point(456, 304);
            this.IdealProcessor30.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor30.MaxLength = 9;
            this.IdealProcessor30.Name = "IdealProcessor30";
            this.IdealProcessor30.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor30.TabIndex = 270;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 308);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 16);
            this.label4.TabIndex = 271;
            this.label4.Text = "30 :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor29
            // 
            this.IdealProcessor29.BackColor = System.Drawing.Color.White;
            this.IdealProcessor29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor29.Location = new System.Drawing.Point(456, 285);
            this.IdealProcessor29.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor29.MaxLength = 9;
            this.IdealProcessor29.Name = "IdealProcessor29";
            this.IdealProcessor29.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor29.TabIndex = 268;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(424, 289);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 269;
            this.label5.Text = "29:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor28
            // 
            this.IdealProcessor28.BackColor = System.Drawing.Color.White;
            this.IdealProcessor28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor28.Location = new System.Drawing.Point(456, 266);
            this.IdealProcessor28.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor28.MaxLength = 9;
            this.IdealProcessor28.Name = "IdealProcessor28";
            this.IdealProcessor28.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor28.TabIndex = 266;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(424, 270);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(27, 16);
            this.label30.TabIndex = 267;
            this.label30.Text = "28 :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor27
            // 
            this.IdealProcessor27.BackColor = System.Drawing.Color.White;
            this.IdealProcessor27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor27.Location = new System.Drawing.Point(456, 247);
            this.IdealProcessor27.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor27.MaxLength = 9;
            this.IdealProcessor27.Name = "IdealProcessor27";
            this.IdealProcessor27.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor27.TabIndex = 265;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(424, 251);
            this.label56.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(27, 16);
            this.label56.TabIndex = 264;
            this.label56.Text = "27 :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor26
            // 
            this.IdealProcessor26.BackColor = System.Drawing.Color.White;
            this.IdealProcessor26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor26.Location = new System.Drawing.Point(456, 228);
            this.IdealProcessor26.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor26.MaxLength = 9;
            this.IdealProcessor26.Name = "IdealProcessor26";
            this.IdealProcessor26.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor26.TabIndex = 263;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(424, 231);
            this.label60.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(27, 16);
            this.label60.TabIndex = 261;
            this.label60.Text = "26 :";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor25
            // 
            this.IdealProcessor25.BackColor = System.Drawing.Color.White;
            this.IdealProcessor25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor25.Location = new System.Drawing.Point(456, 209);
            this.IdealProcessor25.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor25.MaxLength = 9;
            this.IdealProcessor25.Name = "IdealProcessor25";
            this.IdealProcessor25.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor25.TabIndex = 262;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(425, 213);
            this.label62.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(27, 16);
            this.label62.TabIndex = 260;
            this.label62.Text = "25 :";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor24
            // 
            this.IdealProcessor24.BackColor = System.Drawing.Color.White;
            this.IdealProcessor24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor24.Location = new System.Drawing.Point(54, 344);
            this.IdealProcessor24.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor24.MaxLength = 2;
            this.IdealProcessor24.Name = "IdealProcessor24";
            this.IdealProcessor24.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor24.TabIndex = 258;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(20, 349);
            this.label63.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(27, 16);
            this.label63.TabIndex = 259;
            this.label63.Text = "24 :";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor23
            // 
            this.IdealProcessor23.BackColor = System.Drawing.Color.White;
            this.IdealProcessor23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor23.Location = new System.Drawing.Point(54, 325);
            this.IdealProcessor23.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor23.MaxLength = 2;
            this.IdealProcessor23.Name = "IdealProcessor23";
            this.IdealProcessor23.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor23.TabIndex = 256;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(20, 329);
            this.label64.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(27, 16);
            this.label64.TabIndex = 257;
            this.label64.Text = "23 :";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor22
            // 
            this.IdealProcessor22.BackColor = System.Drawing.Color.White;
            this.IdealProcessor22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor22.Location = new System.Drawing.Point(54, 306);
            this.IdealProcessor22.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor22.MaxLength = 2;
            this.IdealProcessor22.Name = "IdealProcessor22";
            this.IdealProcessor22.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor22.TabIndex = 254;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(20, 310);
            this.label65.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(27, 16);
            this.label65.TabIndex = 255;
            this.label65.Text = "22 :";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor21
            // 
            this.IdealProcessor21.BackColor = System.Drawing.Color.White;
            this.IdealProcessor21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor21.Location = new System.Drawing.Point(54, 287);
            this.IdealProcessor21.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor21.MaxLength = 2;
            this.IdealProcessor21.Name = "IdealProcessor21";
            this.IdealProcessor21.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor21.TabIndex = 252;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(20, 291);
            this.label66.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(27, 16);
            this.label66.TabIndex = 253;
            this.label66.Text = "21 :";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor20
            // 
            this.IdealProcessor20.BackColor = System.Drawing.Color.White;
            this.IdealProcessor20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor20.Location = new System.Drawing.Point(54, 268);
            this.IdealProcessor20.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor20.MaxLength = 2;
            this.IdealProcessor20.Name = "IdealProcessor20";
            this.IdealProcessor20.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor20.TabIndex = 250;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(20, 272);
            this.label67.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(27, 16);
            this.label67.TabIndex = 251;
            this.label67.Text = "20 :";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor19
            // 
            this.IdealProcessor19.BackColor = System.Drawing.Color.White;
            this.IdealProcessor19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor19.Location = new System.Drawing.Point(54, 249);
            this.IdealProcessor19.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor19.MaxLength = 2;
            this.IdealProcessor19.Name = "IdealProcessor19";
            this.IdealProcessor19.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor19.TabIndex = 249;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(20, 253);
            this.label68.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(27, 16);
            this.label68.TabIndex = 248;
            this.label68.Text = "19 :";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor18
            // 
            this.IdealProcessor18.BackColor = System.Drawing.Color.White;
            this.IdealProcessor18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor18.Location = new System.Drawing.Point(54, 230);
            this.IdealProcessor18.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor18.MaxLength = 2;
            this.IdealProcessor18.Name = "IdealProcessor18";
            this.IdealProcessor18.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor18.TabIndex = 247;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(20, 236);
            this.label69.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(27, 16);
            this.label69.TabIndex = 245;
            this.label69.Text = "18 :";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor17
            // 
            this.IdealProcessor17.BackColor = System.Drawing.Color.White;
            this.IdealProcessor17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor17.Location = new System.Drawing.Point(54, 211);
            this.IdealProcessor17.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor17.MaxLength = 2;
            this.IdealProcessor17.Name = "IdealProcessor17";
            this.IdealProcessor17.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor17.TabIndex = 246;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(20, 217);
            this.label70.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(27, 16);
            this.label70.TabIndex = 244;
            this.label70.Text = "17 :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Affinity16
            // 
            this.Affinity16.BackColor = System.Drawing.Color.White;
            this.Affinity16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity16.Location = new System.Drawing.Point(508, 175);
            this.Affinity16.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity16.MaxLength = 17;
            this.Affinity16.Name = "Affinity16";
            this.Affinity16.Size = new System.Drawing.Size(48, 24);
            this.Affinity16.TabIndex = 242;
            // 
            // Affinity15
            // 
            this.Affinity15.BackColor = System.Drawing.Color.White;
            this.Affinity15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity15.Location = new System.Drawing.Point(508, 156);
            this.Affinity15.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity15.MaxLength = 17;
            this.Affinity15.Name = "Affinity15";
            this.Affinity15.Size = new System.Drawing.Size(48, 24);
            this.Affinity15.TabIndex = 241;
            // 
            // Affinity14
            // 
            this.Affinity14.BackColor = System.Drawing.Color.White;
            this.Affinity14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity14.Location = new System.Drawing.Point(508, 137);
            this.Affinity14.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity14.MaxLength = 17;
            this.Affinity14.Name = "Affinity14";
            this.Affinity14.Size = new System.Drawing.Size(48, 24);
            this.Affinity14.TabIndex = 240;
            // 
            // Affinity13
            // 
            this.Affinity13.BackColor = System.Drawing.Color.White;
            this.Affinity13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity13.Location = new System.Drawing.Point(508, 118);
            this.Affinity13.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity13.MaxLength = 17;
            this.Affinity13.Name = "Affinity13";
            this.Affinity13.Size = new System.Drawing.Size(48, 24);
            this.Affinity13.TabIndex = 239;
            // 
            // Affinity12
            // 
            this.Affinity12.BackColor = System.Drawing.Color.White;
            this.Affinity12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity12.Location = new System.Drawing.Point(508, 99);
            this.Affinity12.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity12.MaxLength = 17;
            this.Affinity12.Name = "Affinity12";
            this.Affinity12.Size = new System.Drawing.Size(48, 24);
            this.Affinity12.TabIndex = 238;
            // 
            // Affinity11
            // 
            this.Affinity11.BackColor = System.Drawing.Color.White;
            this.Affinity11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity11.Location = new System.Drawing.Point(508, 80);
            this.Affinity11.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity11.MaxLength = 17;
            this.Affinity11.Name = "Affinity11";
            this.Affinity11.Size = new System.Drawing.Size(48, 24);
            this.Affinity11.TabIndex = 237;
            // 
            // Affinity10
            // 
            this.Affinity10.BackColor = System.Drawing.Color.White;
            this.Affinity10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity10.Location = new System.Drawing.Point(508, 61);
            this.Affinity10.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity10.MaxLength = 17;
            this.Affinity10.Name = "Affinity10";
            this.Affinity10.Size = new System.Drawing.Size(48, 24);
            this.Affinity10.TabIndex = 236;
            // 
            // Affinity9
            // 
            this.Affinity9.BackColor = System.Drawing.Color.White;
            this.Affinity9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity9.Location = new System.Drawing.Point(508, 42);
            this.Affinity9.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity9.MaxLength = 17;
            this.Affinity9.Name = "Affinity9";
            this.Affinity9.Size = new System.Drawing.Size(48, 24);
            this.Affinity9.TabIndex = 235;
            // 
            // Affinity8
            // 
            this.Affinity8.BackColor = System.Drawing.Color.White;
            this.Affinity8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity8.Location = new System.Drawing.Point(106, 177);
            this.Affinity8.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity8.MaxLength = 2;
            this.Affinity8.Name = "Affinity8";
            this.Affinity8.Size = new System.Drawing.Size(48, 24);
            this.Affinity8.TabIndex = 233;
            // 
            // Affinity7
            // 
            this.Affinity7.BackColor = System.Drawing.Color.White;
            this.Affinity7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity7.Location = new System.Drawing.Point(106, 158);
            this.Affinity7.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity7.MaxLength = 2;
            this.Affinity7.Name = "Affinity7";
            this.Affinity7.Size = new System.Drawing.Size(48, 24);
            this.Affinity7.TabIndex = 232;
            // 
            // Affinity6
            // 
            this.Affinity6.BackColor = System.Drawing.Color.White;
            this.Affinity6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity6.Location = new System.Drawing.Point(106, 139);
            this.Affinity6.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity6.MaxLength = 2;
            this.Affinity6.Name = "Affinity6";
            this.Affinity6.Size = new System.Drawing.Size(48, 24);
            this.Affinity6.TabIndex = 231;
            // 
            // Affinity5
            // 
            this.Affinity5.BackColor = System.Drawing.Color.White;
            this.Affinity5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity5.Location = new System.Drawing.Point(106, 120);
            this.Affinity5.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity5.MaxLength = 2;
            this.Affinity5.Name = "Affinity5";
            this.Affinity5.Size = new System.Drawing.Size(48, 24);
            this.Affinity5.TabIndex = 230;
            // 
            // Affinity4
            // 
            this.Affinity4.BackColor = System.Drawing.Color.White;
            this.Affinity4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity4.Location = new System.Drawing.Point(106, 101);
            this.Affinity4.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity4.MaxLength = 2;
            this.Affinity4.Name = "Affinity4";
            this.Affinity4.Size = new System.Drawing.Size(48, 24);
            this.Affinity4.TabIndex = 229;
            // 
            // Affinity3
            // 
            this.Affinity3.BackColor = System.Drawing.Color.White;
            this.Affinity3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity3.Location = new System.Drawing.Point(106, 82);
            this.Affinity3.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity3.MaxLength = 2;
            this.Affinity3.Name = "Affinity3";
            this.Affinity3.Size = new System.Drawing.Size(48, 24);
            this.Affinity3.TabIndex = 228;
            // 
            // Affinity2
            // 
            this.Affinity2.BackColor = System.Drawing.Color.White;
            this.Affinity2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity2.Location = new System.Drawing.Point(106, 63);
            this.Affinity2.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity2.MaxLength = 2;
            this.Affinity2.Name = "Affinity2";
            this.Affinity2.Size = new System.Drawing.Size(48, 24);
            this.Affinity2.TabIndex = 227;
            // 
            // Affinity_Label
            // 
            this.Affinity_Label.AutoSize = true;
            this.Affinity_Label.Location = new System.Drawing.Point(189, 7);
            this.Affinity_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Affinity_Label.Name = "Affinity_Label";
            this.Affinity_Label.Size = new System.Drawing.Size(45, 16);
            this.Affinity_Label.TabIndex = 225;
            this.Affinity_Label.Text = "Affinity";
            // 
            // Affinity1
            // 
            this.Affinity1.BackColor = System.Drawing.Color.White;
            this.Affinity1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Affinity1.Location = new System.Drawing.Point(106, 44);
            this.Affinity1.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity1.MaxLength = 2;
            this.Affinity1.Name = "Affinity1";
            this.Affinity1.Size = new System.Drawing.Size(48, 24);
            this.Affinity1.TabIndex = 226;
            // 
            // PriorityClass_Label
            // 
            this.PriorityClass_Label.AutoSize = true;
            this.PriorityClass_Label.Location = new System.Drawing.Point(241, 7);
            this.PriorityClass_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PriorityClass_Label.Name = "PriorityClass_Label";
            this.PriorityClass_Label.Size = new System.Drawing.Size(85, 16);
            this.PriorityClass_Label.TabIndex = 224;
            this.PriorityClass_Label.Text = "Priority Class";
            // 
            // IdealProcessor16
            // 
            this.IdealProcessor16.BackColor = System.Drawing.Color.White;
            this.IdealProcessor16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor16.Location = new System.Drawing.Point(456, 175);
            this.IdealProcessor16.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor16.MaxLength = 9;
            this.IdealProcessor16.Name = "IdealProcessor16";
            this.IdealProcessor16.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor16.TabIndex = 221;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(424, 180);
            this.label76.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(27, 16);
            this.label76.TabIndex = 222;
            this.label76.Text = "16 :";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor15
            // 
            this.IdealProcessor15.BackColor = System.Drawing.Color.White;
            this.IdealProcessor15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor15.Location = new System.Drawing.Point(456, 156);
            this.IdealProcessor15.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor15.MaxLength = 9;
            this.IdealProcessor15.Name = "IdealProcessor15";
            this.IdealProcessor15.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor15.TabIndex = 219;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(424, 160);
            this.label77.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(27, 16);
            this.label77.TabIndex = 220;
            this.label77.Text = "15 :";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor14
            // 
            this.IdealProcessor14.BackColor = System.Drawing.Color.White;
            this.IdealProcessor14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor14.Location = new System.Drawing.Point(456, 137);
            this.IdealProcessor14.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor14.MaxLength = 9;
            this.IdealProcessor14.Name = "IdealProcessor14";
            this.IdealProcessor14.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor14.TabIndex = 217;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(424, 141);
            this.label78.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(27, 16);
            this.label78.TabIndex = 218;
            this.label78.Text = "14 :";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor13
            // 
            this.IdealProcessor13.BackColor = System.Drawing.Color.White;
            this.IdealProcessor13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor13.Location = new System.Drawing.Point(456, 118);
            this.IdealProcessor13.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor13.MaxLength = 9;
            this.IdealProcessor13.Name = "IdealProcessor13";
            this.IdealProcessor13.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor13.TabIndex = 215;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(424, 122);
            this.label79.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(27, 16);
            this.label79.TabIndex = 216;
            this.label79.Text = "13 :";
            this.label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor12
            // 
            this.IdealProcessor12.BackColor = System.Drawing.Color.White;
            this.IdealProcessor12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor12.Location = new System.Drawing.Point(456, 99);
            this.IdealProcessor12.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor12.MaxLength = 9;
            this.IdealProcessor12.Name = "IdealProcessor12";
            this.IdealProcessor12.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor12.TabIndex = 213;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(424, 103);
            this.label80.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(27, 16);
            this.label80.TabIndex = 214;
            this.label80.Text = "12 :";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor11
            // 
            this.IdealProcessor11.BackColor = System.Drawing.Color.White;
            this.IdealProcessor11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor11.Location = new System.Drawing.Point(456, 80);
            this.IdealProcessor11.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor11.MaxLength = 9;
            this.IdealProcessor11.Name = "IdealProcessor11";
            this.IdealProcessor11.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor11.TabIndex = 212;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(424, 84);
            this.label81.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(27, 16);
            this.label81.TabIndex = 211;
            this.label81.Text = "11 :";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor10
            // 
            this.IdealProcessor10.BackColor = System.Drawing.Color.White;
            this.IdealProcessor10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor10.Location = new System.Drawing.Point(456, 61);
            this.IdealProcessor10.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor10.MaxLength = 9;
            this.IdealProcessor10.Name = "IdealProcessor10";
            this.IdealProcessor10.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor10.TabIndex = 210;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(424, 64);
            this.label82.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(27, 16);
            this.label82.TabIndex = 208;
            this.label82.Text = "10 :";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor9
            // 
            this.IdealProcessor9.BackColor = System.Drawing.Color.White;
            this.IdealProcessor9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor9.Location = new System.Drawing.Point(456, 42);
            this.IdealProcessor9.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor9.MaxLength = 9;
            this.IdealProcessor9.Name = "IdealProcessor9";
            this.IdealProcessor9.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor9.TabIndex = 209;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(425, 46);
            this.label83.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(20, 16);
            this.label83.TabIndex = 207;
            this.label83.Text = "9 :";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor8
            // 
            this.IdealProcessor8.BackColor = System.Drawing.Color.White;
            this.IdealProcessor8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor8.Location = new System.Drawing.Point(54, 177);
            this.IdealProcessor8.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor8.MaxLength = 2;
            this.IdealProcessor8.Name = "IdealProcessor8";
            this.IdealProcessor8.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor8.TabIndex = 205;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(24, 182);
            this.label84.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(20, 16);
            this.label84.TabIndex = 206;
            this.label84.Text = "8 :";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor7
            // 
            this.IdealProcessor7.BackColor = System.Drawing.Color.White;
            this.IdealProcessor7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor7.Location = new System.Drawing.Point(54, 158);
            this.IdealProcessor7.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor7.MaxLength = 2;
            this.IdealProcessor7.Name = "IdealProcessor7";
            this.IdealProcessor7.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor7.TabIndex = 203;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(24, 166);
            this.label85.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(20, 16);
            this.label85.TabIndex = 204;
            this.label85.Text = "7 :";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor6
            // 
            this.IdealProcessor6.BackColor = System.Drawing.Color.White;
            this.IdealProcessor6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor6.Location = new System.Drawing.Point(54, 139);
            this.IdealProcessor6.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor6.MaxLength = 2;
            this.IdealProcessor6.Name = "IdealProcessor6";
            this.IdealProcessor6.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor6.TabIndex = 201;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(24, 148);
            this.label86.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(20, 16);
            this.label86.TabIndex = 202;
            this.label86.Text = "6 :";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor5
            // 
            this.IdealProcessor5.BackColor = System.Drawing.Color.White;
            this.IdealProcessor5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor5.Location = new System.Drawing.Point(54, 120);
            this.IdealProcessor5.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor5.MaxLength = 2;
            this.IdealProcessor5.Name = "IdealProcessor5";
            this.IdealProcessor5.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor5.TabIndex = 199;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(24, 129);
            this.label87.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(20, 16);
            this.label87.TabIndex = 200;
            this.label87.Text = "5 :";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Instance_Label
            // 
            this.Instance_Label.AutoSize = true;
            this.Instance_Label.Location = new System.Drawing.Point(12, 7);
            this.Instance_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Instance_Label.Name = "Instance_Label";
            this.Instance_Label.Size = new System.Drawing.Size(57, 16);
            this.Instance_Label.TabIndex = 198;
            this.Instance_Label.Text = "Instance";
            // 
            // IdealProcessor4
            // 
            this.IdealProcessor4.BackColor = System.Drawing.Color.White;
            this.IdealProcessor4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor4.Location = new System.Drawing.Point(54, 101);
            this.IdealProcessor4.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor4.MaxLength = 2;
            this.IdealProcessor4.Name = "IdealProcessor4";
            this.IdealProcessor4.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor4.TabIndex = 196;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(24, 111);
            this.label89.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(20, 16);
            this.label89.TabIndex = 197;
            this.label89.Text = "4 :";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor3
            // 
            this.IdealProcessor3.BackColor = System.Drawing.Color.White;
            this.IdealProcessor3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor3.Location = new System.Drawing.Point(54, 82);
            this.IdealProcessor3.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor3.MaxLength = 2;
            this.IdealProcessor3.Name = "IdealProcessor3";
            this.IdealProcessor3.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor3.TabIndex = 195;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(24, 90);
            this.label90.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(20, 16);
            this.label90.TabIndex = 194;
            this.label90.Text = "3 :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor2
            // 
            this.IdealProcessor2.BackColor = System.Drawing.Color.White;
            this.IdealProcessor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor2.Location = new System.Drawing.Point(54, 63);
            this.IdealProcessor2.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor2.MaxLength = 2;
            this.IdealProcessor2.Name = "IdealProcessor2";
            this.IdealProcessor2.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor2.TabIndex = 193;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(24, 69);
            this.label91.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(20, 16);
            this.label91.TabIndex = 191;
            this.label91.Text = "2 :";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor_Label
            // 
            this.IdealProcessor_Label.AutoSize = true;
            this.IdealProcessor_Label.Location = new System.Drawing.Point(77, 7);
            this.IdealProcessor_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IdealProcessor_Label.Name = "IdealProcessor_Label";
            this.IdealProcessor_Label.Size = new System.Drawing.Size(102, 16);
            this.IdealProcessor_Label.TabIndex = 190;
            this.IdealProcessor_Label.Text = "Ideal Processor";
            // 
            // IdealProcessor1
            // 
            this.IdealProcessor1.BackColor = System.Drawing.Color.White;
            this.IdealProcessor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor1.Location = new System.Drawing.Point(54, 44);
            this.IdealProcessor1.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor1.MaxLength = 2;
            this.IdealProcessor1.Name = "IdealProcessor1";
            this.IdealProcessor1.Size = new System.Drawing.Size(48, 24);
            this.IdealProcessor1.TabIndex = 192;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(24, 47);
            this.label93.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(20, 16);
            this.label93.TabIndex = 189;
            this.label93.Text = "1 :";
            this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.PaleGreen;
            this.Panel2.Controls.Add(this.steamid32);
            this.Panel2.Controls.Add(this.steamid31);
            this.Panel2.Controls.Add(this.steamid30);
            this.Panel2.Controls.Add(this.steamid29);
            this.Panel2.Controls.Add(this.steamid28);
            this.Panel2.Controls.Add(this.steamid27);
            this.Panel2.Controls.Add(this.steamid26);
            this.Panel2.Controls.Add(this.steamid25);
            this.Panel2.Controls.Add(this.steamid24);
            this.Panel2.Controls.Add(this.steamid23);
            this.Panel2.Controls.Add(this.steamid22);
            this.Panel2.Controls.Add(this.steamid21);
            this.Panel2.Controls.Add(this.steamid20);
            this.Panel2.Controls.Add(this.steamid19);
            this.Panel2.Controls.Add(this.steamid18);
            this.Panel2.Controls.Add(this.steamid17);
            this.Panel2.Controls.Add(this.player32N);
            this.Panel2.Controls.Add(this.label31);
            this.Panel2.Controls.Add(this.player31N);
            this.Panel2.Controls.Add(this.label33);
            this.Panel2.Controls.Add(this.player30N);
            this.Panel2.Controls.Add(this.label34);
            this.Panel2.Controls.Add(this.player29N);
            this.Panel2.Controls.Add(this.label35);
            this.Panel2.Controls.Add(this.player28N);
            this.Panel2.Controls.Add(this.label38);
            this.Panel2.Controls.Add(this.player27N);
            this.Panel2.Controls.Add(this.label46);
            this.Panel2.Controls.Add(this.player26N);
            this.Panel2.Controls.Add(this.label50);
            this.Panel2.Controls.Add(this.player25N);
            this.Panel2.Controls.Add(this.label51);
            this.Panel2.Controls.Add(this.player24N);
            this.Panel2.Controls.Add(this.label52);
            this.Panel2.Controls.Add(this.player23N);
            this.Panel2.Controls.Add(this.label53);
            this.Panel2.Controls.Add(this.player22N);
            this.Panel2.Controls.Add(this.label54);
            this.Panel2.Controls.Add(this.player21N);
            this.Panel2.Controls.Add(this.label55);
            this.Panel2.Controls.Add(this.player20N);
            this.Panel2.Controls.Add(this.label57);
            this.Panel2.Controls.Add(this.player19N);
            this.Panel2.Controls.Add(this.label58);
            this.Panel2.Controls.Add(this.player18N);
            this.Panel2.Controls.Add(this.label59);
            this.Panel2.Controls.Add(this.player17N);
            this.Panel2.Controls.Add(this.label61);
            this.Panel2.Controls.Add(this.def_sid_textBox_container);
            this.Panel2.Controls.Add(this.steamid16);
            this.Panel2.Controls.Add(this.steamid15);
            this.Panel2.Controls.Add(this.steamid14);
            this.Panel2.Controls.Add(this.steamid13);
            this.Panel2.Controls.Add(this.steamid12);
            this.Panel2.Controls.Add(this.steamid11);
            this.Panel2.Controls.Add(this.steamid10);
            this.Panel2.Controls.Add(this.label48);
            this.Panel2.Controls.Add(this.steamid9);
            this.Panel2.Controls.Add(this.steamid8);
            this.Panel2.Controls.Add(this.steamid7);
            this.Panel2.Controls.Add(this.steamid6);
            this.Panel2.Controls.Add(this.steamid5);
            this.Panel2.Controls.Add(this.steamid4);
            this.Panel2.Controls.Add(this.steamid3);
            this.Panel2.Controls.Add(this.steamid2);
            this.Panel2.Controls.Add(this.label47);
            this.Panel2.Controls.Add(this.steamid1);
            this.Panel2.Controls.Add(this.label21);
            this.Panel2.Controls.Add(this.label25);
            this.Panel2.Controls.Add(this.player16N);
            this.Panel2.Controls.Add(this.label17);
            this.Panel2.Controls.Add(this.player15N);
            this.Panel2.Controls.Add(this.label18);
            this.Panel2.Controls.Add(this.player14N);
            this.Panel2.Controls.Add(this.label19);
            this.Panel2.Controls.Add(this.player13N);
            this.Panel2.Controls.Add(this.label20);
            this.Panel2.Controls.Add(this.player12N);
            this.Panel2.Controls.Add(this.label22);
            this.Panel2.Controls.Add(this.player11N);
            this.Panel2.Controls.Add(this.label23);
            this.Panel2.Controls.Add(this.player10N);
            this.Panel2.Controls.Add(this.label24);
            this.Panel2.Controls.Add(this.player9N);
            this.Panel2.Controls.Add(this.label26);
            this.Panel2.Controls.Add(this.player8N);
            this.Panel2.Controls.Add(this.label16);
            this.Panel2.Controls.Add(this.player7N);
            this.Panel2.Controls.Add(this.label15);
            this.Panel2.Controls.Add(this.player6N);
            this.Panel2.Controls.Add(this.label11);
            this.Panel2.Controls.Add(this.player5N);
            this.Panel2.Controls.Add(this.label9);
            this.Panel2.Controls.Add(this.label13);
            this.Panel2.Controls.Add(this.player4N);
            this.Panel2.Controls.Add(this.label14);
            this.Panel2.Controls.Add(this.player3N);
            this.Panel2.Controls.Add(this.label12);
            this.Panel2.Controls.Add(this.player2N);
            this.Panel2.Controls.Add(this.label10);
            this.Panel2.Controls.Add(this.label8);
            this.Panel2.Controls.Add(this.player1N);
            this.Panel2.Controls.Add(this.label7);
            this.Panel2.Location = new System.Drawing.Point(58, 30);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(671, 401);
            this.Panel2.TabIndex = 132;
            // 
            // steamid32
            // 
            this.steamid32.BackColor = System.Drawing.Color.White;
            this.steamid32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid32.Location = new System.Drawing.Point(505, 327);
            this.steamid32.Margin = new System.Windows.Forms.Padding(2);
            this.steamid32.MaxLength = 17;
            this.steamid32.Name = "steamid32";
            this.steamid32.Size = new System.Drawing.Size(133, 24);
            this.steamid32.TabIndex = 291;
            // 
            // steamid31
            // 
            this.steamid31.BackColor = System.Drawing.Color.White;
            this.steamid31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid31.Location = new System.Drawing.Point(505, 308);
            this.steamid31.Margin = new System.Windows.Forms.Padding(2);
            this.steamid31.MaxLength = 17;
            this.steamid31.Name = "steamid31";
            this.steamid31.Size = new System.Drawing.Size(133, 24);
            this.steamid31.TabIndex = 290;
            // 
            // steamid30
            // 
            this.steamid30.BackColor = System.Drawing.Color.White;
            this.steamid30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid30.Location = new System.Drawing.Point(505, 289);
            this.steamid30.Margin = new System.Windows.Forms.Padding(2);
            this.steamid30.MaxLength = 17;
            this.steamid30.Name = "steamid30";
            this.steamid30.Size = new System.Drawing.Size(133, 24);
            this.steamid30.TabIndex = 289;
            // 
            // steamid29
            // 
            this.steamid29.BackColor = System.Drawing.Color.White;
            this.steamid29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid29.Location = new System.Drawing.Point(505, 270);
            this.steamid29.Margin = new System.Windows.Forms.Padding(2);
            this.steamid29.MaxLength = 17;
            this.steamid29.Name = "steamid29";
            this.steamid29.Size = new System.Drawing.Size(133, 24);
            this.steamid29.TabIndex = 288;
            // 
            // steamid28
            // 
            this.steamid28.BackColor = System.Drawing.Color.White;
            this.steamid28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid28.Location = new System.Drawing.Point(505, 251);
            this.steamid28.Margin = new System.Windows.Forms.Padding(2);
            this.steamid28.MaxLength = 17;
            this.steamid28.Name = "steamid28";
            this.steamid28.Size = new System.Drawing.Size(133, 24);
            this.steamid28.TabIndex = 287;
            // 
            // steamid27
            // 
            this.steamid27.BackColor = System.Drawing.Color.White;
            this.steamid27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid27.Location = new System.Drawing.Point(505, 232);
            this.steamid27.Margin = new System.Windows.Forms.Padding(2);
            this.steamid27.MaxLength = 17;
            this.steamid27.Name = "steamid27";
            this.steamid27.Size = new System.Drawing.Size(133, 24);
            this.steamid27.TabIndex = 286;
            // 
            // steamid26
            // 
            this.steamid26.BackColor = System.Drawing.Color.White;
            this.steamid26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid26.Location = new System.Drawing.Point(505, 213);
            this.steamid26.Margin = new System.Windows.Forms.Padding(2);
            this.steamid26.MaxLength = 17;
            this.steamid26.Name = "steamid26";
            this.steamid26.Size = new System.Drawing.Size(133, 24);
            this.steamid26.TabIndex = 285;
            // 
            // steamid25
            // 
            this.steamid25.BackColor = System.Drawing.Color.White;
            this.steamid25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid25.Location = new System.Drawing.Point(505, 194);
            this.steamid25.Margin = new System.Windows.Forms.Padding(2);
            this.steamid25.MaxLength = 17;
            this.steamid25.Name = "steamid25";
            this.steamid25.Size = new System.Drawing.Size(133, 24);
            this.steamid25.TabIndex = 284;
            // 
            // steamid24
            // 
            this.steamid24.BackColor = System.Drawing.Color.White;
            this.steamid24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid24.Location = new System.Drawing.Point(164, 329);
            this.steamid24.Margin = new System.Windows.Forms.Padding(2);
            this.steamid24.MaxLength = 17;
            this.steamid24.Name = "steamid24";
            this.steamid24.Size = new System.Drawing.Size(133, 24);
            this.steamid24.TabIndex = 283;
            // 
            // steamid23
            // 
            this.steamid23.BackColor = System.Drawing.Color.White;
            this.steamid23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid23.Location = new System.Drawing.Point(164, 310);
            this.steamid23.Margin = new System.Windows.Forms.Padding(2);
            this.steamid23.MaxLength = 17;
            this.steamid23.Name = "steamid23";
            this.steamid23.Size = new System.Drawing.Size(133, 24);
            this.steamid23.TabIndex = 282;
            // 
            // steamid22
            // 
            this.steamid22.BackColor = System.Drawing.Color.White;
            this.steamid22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid22.Location = new System.Drawing.Point(164, 291);
            this.steamid22.Margin = new System.Windows.Forms.Padding(2);
            this.steamid22.MaxLength = 17;
            this.steamid22.Name = "steamid22";
            this.steamid22.Size = new System.Drawing.Size(133, 24);
            this.steamid22.TabIndex = 281;
            // 
            // steamid21
            // 
            this.steamid21.BackColor = System.Drawing.Color.White;
            this.steamid21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid21.Location = new System.Drawing.Point(164, 272);
            this.steamid21.Margin = new System.Windows.Forms.Padding(2);
            this.steamid21.MaxLength = 17;
            this.steamid21.Name = "steamid21";
            this.steamid21.Size = new System.Drawing.Size(133, 24);
            this.steamid21.TabIndex = 280;
            // 
            // steamid20
            // 
            this.steamid20.BackColor = System.Drawing.Color.White;
            this.steamid20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid20.Location = new System.Drawing.Point(164, 253);
            this.steamid20.Margin = new System.Windows.Forms.Padding(2);
            this.steamid20.MaxLength = 17;
            this.steamid20.Name = "steamid20";
            this.steamid20.Size = new System.Drawing.Size(133, 24);
            this.steamid20.TabIndex = 279;
            // 
            // steamid19
            // 
            this.steamid19.BackColor = System.Drawing.Color.White;
            this.steamid19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid19.Location = new System.Drawing.Point(164, 234);
            this.steamid19.Margin = new System.Windows.Forms.Padding(2);
            this.steamid19.MaxLength = 17;
            this.steamid19.Name = "steamid19";
            this.steamid19.Size = new System.Drawing.Size(133, 24);
            this.steamid19.TabIndex = 278;
            // 
            // steamid18
            // 
            this.steamid18.BackColor = System.Drawing.Color.White;
            this.steamid18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid18.Location = new System.Drawing.Point(164, 215);
            this.steamid18.Margin = new System.Windows.Forms.Padding(2);
            this.steamid18.MaxLength = 17;
            this.steamid18.Name = "steamid18";
            this.steamid18.Size = new System.Drawing.Size(133, 24);
            this.steamid18.TabIndex = 277;
            // 
            // steamid17
            // 
            this.steamid17.BackColor = System.Drawing.Color.White;
            this.steamid17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid17.Location = new System.Drawing.Point(164, 196);
            this.steamid17.Margin = new System.Windows.Forms.Padding(2);
            this.steamid17.MaxLength = 17;
            this.steamid17.Name = "steamid17";
            this.steamid17.Size = new System.Drawing.Size(133, 24);
            this.steamid17.TabIndex = 276;
            // 
            // player32N
            // 
            this.player32N.BackColor = System.Drawing.Color.White;
            this.player32N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player32N.Location = new System.Drawing.Point(429, 327);
            this.player32N.Margin = new System.Windows.Forms.Padding(2);
            this.player32N.MaxLength = 9;
            this.player32N.Name = "player32N";
            this.player32N.Size = new System.Drawing.Size(69, 24);
            this.player32N.TabIndex = 274;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(368, 331);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(69, 16);
            this.label31.TabIndex = 275;
            this.label31.Text = "Player 32 :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player31N
            // 
            this.player31N.BackColor = System.Drawing.Color.White;
            this.player31N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player31N.Location = new System.Drawing.Point(429, 308);
            this.player31N.Margin = new System.Windows.Forms.Padding(2);
            this.player31N.MaxLength = 9;
            this.player31N.Name = "player31N";
            this.player31N.Size = new System.Drawing.Size(69, 24);
            this.player31N.TabIndex = 272;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(368, 311);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(69, 16);
            this.label33.TabIndex = 273;
            this.label33.Text = "Player 31 :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player30N
            // 
            this.player30N.BackColor = System.Drawing.Color.White;
            this.player30N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player30N.Location = new System.Drawing.Point(429, 289);
            this.player30N.Margin = new System.Windows.Forms.Padding(2);
            this.player30N.MaxLength = 9;
            this.player30N.Name = "player30N";
            this.player30N.Size = new System.Drawing.Size(69, 24);
            this.player30N.TabIndex = 270;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(368, 292);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(69, 16);
            this.label34.TabIndex = 271;
            this.label34.Text = "Player 30 :";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player29N
            // 
            this.player29N.BackColor = System.Drawing.Color.White;
            this.player29N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player29N.Location = new System.Drawing.Point(429, 270);
            this.player29N.Margin = new System.Windows.Forms.Padding(2);
            this.player29N.MaxLength = 9;
            this.player29N.Name = "player29N";
            this.player29N.Size = new System.Drawing.Size(69, 24);
            this.player29N.TabIndex = 268;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(368, 273);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 16);
            this.label35.TabIndex = 269;
            this.label35.Text = "Player 29:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player28N
            // 
            this.player28N.BackColor = System.Drawing.Color.White;
            this.player28N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player28N.Location = new System.Drawing.Point(429, 251);
            this.player28N.Margin = new System.Windows.Forms.Padding(2);
            this.player28N.MaxLength = 9;
            this.player28N.Name = "player28N";
            this.player28N.Size = new System.Drawing.Size(69, 24);
            this.player28N.TabIndex = 266;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(368, 254);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 16);
            this.label38.TabIndex = 267;
            this.label38.Text = "Player 28 :";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player27N
            // 
            this.player27N.BackColor = System.Drawing.Color.White;
            this.player27N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player27N.Location = new System.Drawing.Point(429, 232);
            this.player27N.Margin = new System.Windows.Forms.Padding(2);
            this.player27N.MaxLength = 9;
            this.player27N.Name = "player27N";
            this.player27N.Size = new System.Drawing.Size(69, 24);
            this.player27N.TabIndex = 265;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(368, 235);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(69, 16);
            this.label46.TabIndex = 264;
            this.label46.Text = "Player 27 :";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player26N
            // 
            this.player26N.BackColor = System.Drawing.Color.White;
            this.player26N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player26N.Location = new System.Drawing.Point(429, 213);
            this.player26N.Margin = new System.Windows.Forms.Padding(2);
            this.player26N.MaxLength = 9;
            this.player26N.Name = "player26N";
            this.player26N.Size = new System.Drawing.Size(69, 24);
            this.player26N.TabIndex = 263;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(368, 215);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(66, 16);
            this.label50.TabIndex = 261;
            this.label50.Text = "Player 26:";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player25N
            // 
            this.player25N.BackColor = System.Drawing.Color.White;
            this.player25N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player25N.Location = new System.Drawing.Point(429, 194);
            this.player25N.Margin = new System.Windows.Forms.Padding(2);
            this.player25N.MaxLength = 9;
            this.player25N.Name = "player25N";
            this.player25N.Size = new System.Drawing.Size(69, 24);
            this.player25N.TabIndex = 262;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(369, 197);
            this.label51.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(69, 16);
            this.label51.TabIndex = 260;
            this.label51.Text = "Player 25 :";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player24N
            // 
            this.player24N.BackColor = System.Drawing.Color.White;
            this.player24N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player24N.Location = new System.Drawing.Point(88, 329);
            this.player24N.Margin = new System.Windows.Forms.Padding(2);
            this.player24N.MaxLength = 9;
            this.player24N.Name = "player24N";
            this.player24N.Size = new System.Drawing.Size(69, 24);
            this.player24N.TabIndex = 258;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(34, 333);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(66, 16);
            this.label52.TabIndex = 259;
            this.label52.Text = "Player 24:";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player23N
            // 
            this.player23N.BackColor = System.Drawing.Color.White;
            this.player23N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player23N.Location = new System.Drawing.Point(88, 310);
            this.player23N.Margin = new System.Windows.Forms.Padding(2);
            this.player23N.MaxLength = 9;
            this.player23N.Name = "player23N";
            this.player23N.Size = new System.Drawing.Size(69, 24);
            this.player23N.TabIndex = 256;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(34, 313);
            this.label53.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(69, 16);
            this.label53.TabIndex = 257;
            this.label53.Text = "Player 23 :";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player22N
            // 
            this.player22N.BackColor = System.Drawing.Color.White;
            this.player22N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player22N.Location = new System.Drawing.Point(88, 291);
            this.player22N.Margin = new System.Windows.Forms.Padding(2);
            this.player22N.MaxLength = 9;
            this.player22N.Name = "player22N";
            this.player22N.Size = new System.Drawing.Size(69, 24);
            this.player22N.TabIndex = 254;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(34, 294);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(69, 16);
            this.label54.TabIndex = 255;
            this.label54.Text = "Player 22 :";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player21N
            // 
            this.player21N.BackColor = System.Drawing.Color.White;
            this.player21N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player21N.Location = new System.Drawing.Point(88, 272);
            this.player21N.Margin = new System.Windows.Forms.Padding(2);
            this.player21N.MaxLength = 9;
            this.player21N.Name = "player21N";
            this.player21N.Size = new System.Drawing.Size(69, 24);
            this.player21N.TabIndex = 252;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(34, 275);
            this.label55.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(69, 16);
            this.label55.TabIndex = 253;
            this.label55.Text = "Player 21 :";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player20N
            // 
            this.player20N.BackColor = System.Drawing.Color.White;
            this.player20N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player20N.Location = new System.Drawing.Point(88, 253);
            this.player20N.Margin = new System.Windows.Forms.Padding(2);
            this.player20N.MaxLength = 9;
            this.player20N.Name = "player20N";
            this.player20N.Size = new System.Drawing.Size(69, 24);
            this.player20N.TabIndex = 250;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(34, 256);
            this.label57.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(69, 16);
            this.label57.TabIndex = 251;
            this.label57.Text = "Player 20 :";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player19N
            // 
            this.player19N.BackColor = System.Drawing.Color.White;
            this.player19N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player19N.Location = new System.Drawing.Point(88, 234);
            this.player19N.Margin = new System.Windows.Forms.Padding(2);
            this.player19N.MaxLength = 9;
            this.player19N.Name = "player19N";
            this.player19N.Size = new System.Drawing.Size(69, 24);
            this.player19N.TabIndex = 249;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(34, 237);
            this.label58.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(69, 16);
            this.label58.TabIndex = 248;
            this.label58.Text = "Player 19 :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player18N
            // 
            this.player18N.BackColor = System.Drawing.Color.White;
            this.player18N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player18N.Location = new System.Drawing.Point(88, 215);
            this.player18N.Margin = new System.Windows.Forms.Padding(2);
            this.player18N.MaxLength = 9;
            this.player18N.Name = "player18N";
            this.player18N.Size = new System.Drawing.Size(69, 24);
            this.player18N.TabIndex = 247;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(34, 217);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(69, 16);
            this.label59.TabIndex = 245;
            this.label59.Text = "Player 18 :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player17N
            // 
            this.player17N.BackColor = System.Drawing.Color.White;
            this.player17N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player17N.Location = new System.Drawing.Point(88, 196);
            this.player17N.Margin = new System.Windows.Forms.Padding(2);
            this.player17N.MaxLength = 9;
            this.player17N.Name = "player17N";
            this.player17N.Size = new System.Drawing.Size(69, 24);
            this.player17N.TabIndex = 246;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(34, 198);
            this.label61.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(66, 16);
            this.label61.TabIndex = 244;
            this.label61.Text = "Player 17:";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // def_sid_textBox_container
            // 
            this.def_sid_textBox_container.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.def_sid_textBox_container.Controls.Add(this.default_sid_list_label);
            this.def_sid_textBox_container.Controls.Add(this.def_sid_comboBox);
            this.def_sid_textBox_container.Location = new System.Drawing.Point(198, 362);
            this.def_sid_textBox_container.Name = "def_sid_textBox_container";
            this.def_sid_textBox_container.Size = new System.Drawing.Size(300, 31);
            this.def_sid_textBox_container.TabIndex = 243;
            // 
            // default_sid_list_label
            // 
            this.default_sid_list_label.AutoSize = true;
            this.default_sid_list_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.default_sid_list_label.Location = new System.Drawing.Point(2, 9);
            this.default_sid_list_label.Margin = new System.Windows.Forms.Padding(0);
            this.default_sid_list_label.Name = "default_sid_list_label";
            this.default_sid_list_label.Size = new System.Drawing.Size(120, 16);
            this.default_sid_list_label.TabIndex = 131;
            this.default_sid_list_label.Text = "Default Steam IDs :";
            this.default_sid_list_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // def_sid_comboBox
            // 
            this.def_sid_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.def_sid_comboBox.BackColor = System.Drawing.Color.Black;
            this.def_sid_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.def_sid_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.def_sid_comboBox.ForeColor = System.Drawing.Color.White;
            this.def_sid_comboBox.ItemHeight = 13;
            this.def_sid_comboBox.Items.AddRange(new object[] {
            "Player 1 : 76561199023125438",
            "Player 2 : 76561199023125439",
            "Player 3 : 76561199023125440",
            "Player 4 : 76561199023125441",
            "Player 5 : 76561199023125442",
            "Player 6 : 76561199023125443",
            "Player 7 : 76561199023125444",
            "Player 8 : 76561199023125445",
            "Player 9 : 76561199023125446",
            "Player 10: 76561199023125447",
            "Player 11: 76561199023125448",
            "Player 12: 76561199023125449",
            "Player 13: 76561199023125450",
            "Player 14: 76561199023125451",
            "Player 15: 76561199023125452",
            "Player 16: 76561199023125453"});
            this.def_sid_comboBox.Location = new System.Drawing.Point(125, 6);
            this.def_sid_comboBox.Margin = new System.Windows.Forms.Padding(0);
            this.def_sid_comboBox.MaxLength = 30;
            this.def_sid_comboBox.Name = "def_sid_comboBox";
            this.def_sid_comboBox.Size = new System.Drawing.Size(175, 21);
            this.def_sid_comboBox.TabIndex = 135;
            // 
            // steamid16
            // 
            this.steamid16.BackColor = System.Drawing.Color.White;
            this.steamid16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid16.Location = new System.Drawing.Point(505, 160);
            this.steamid16.Margin = new System.Windows.Forms.Padding(2);
            this.steamid16.MaxLength = 17;
            this.steamid16.Name = "steamid16";
            this.steamid16.Size = new System.Drawing.Size(133, 24);
            this.steamid16.TabIndex = 242;
            // 
            // steamid15
            // 
            this.steamid15.BackColor = System.Drawing.Color.White;
            this.steamid15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid15.Location = new System.Drawing.Point(505, 141);
            this.steamid15.Margin = new System.Windows.Forms.Padding(2);
            this.steamid15.MaxLength = 17;
            this.steamid15.Name = "steamid15";
            this.steamid15.Size = new System.Drawing.Size(133, 24);
            this.steamid15.TabIndex = 241;
            // 
            // steamid14
            // 
            this.steamid14.BackColor = System.Drawing.Color.White;
            this.steamid14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid14.Location = new System.Drawing.Point(505, 122);
            this.steamid14.Margin = new System.Windows.Forms.Padding(2);
            this.steamid14.MaxLength = 17;
            this.steamid14.Name = "steamid14";
            this.steamid14.Size = new System.Drawing.Size(133, 24);
            this.steamid14.TabIndex = 240;
            // 
            // steamid13
            // 
            this.steamid13.BackColor = System.Drawing.Color.White;
            this.steamid13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid13.Location = new System.Drawing.Point(505, 103);
            this.steamid13.Margin = new System.Windows.Forms.Padding(2);
            this.steamid13.MaxLength = 17;
            this.steamid13.Name = "steamid13";
            this.steamid13.Size = new System.Drawing.Size(133, 24);
            this.steamid13.TabIndex = 239;
            // 
            // steamid12
            // 
            this.steamid12.BackColor = System.Drawing.Color.White;
            this.steamid12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid12.Location = new System.Drawing.Point(505, 84);
            this.steamid12.Margin = new System.Windows.Forms.Padding(2);
            this.steamid12.MaxLength = 17;
            this.steamid12.Name = "steamid12";
            this.steamid12.Size = new System.Drawing.Size(133, 24);
            this.steamid12.TabIndex = 238;
            // 
            // steamid11
            // 
            this.steamid11.BackColor = System.Drawing.Color.White;
            this.steamid11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid11.Location = new System.Drawing.Point(505, 65);
            this.steamid11.Margin = new System.Windows.Forms.Padding(2);
            this.steamid11.MaxLength = 17;
            this.steamid11.Name = "steamid11";
            this.steamid11.Size = new System.Drawing.Size(133, 24);
            this.steamid11.TabIndex = 237;
            // 
            // steamid10
            // 
            this.steamid10.BackColor = System.Drawing.Color.White;
            this.steamid10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid10.Location = new System.Drawing.Point(505, 46);
            this.steamid10.Margin = new System.Windows.Forms.Padding(2);
            this.steamid10.MaxLength = 17;
            this.steamid10.Name = "steamid10";
            this.steamid10.Size = new System.Drawing.Size(133, 24);
            this.steamid10.TabIndex = 236;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(507, 6);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(104, 16);
            this.label48.TabIndex = 234;
            this.label48.Text = "Player Steam ID";
            // 
            // steamid9
            // 
            this.steamid9.BackColor = System.Drawing.Color.White;
            this.steamid9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid9.Location = new System.Drawing.Point(505, 27);
            this.steamid9.Margin = new System.Windows.Forms.Padding(2);
            this.steamid9.MaxLength = 17;
            this.steamid9.Name = "steamid9";
            this.steamid9.Size = new System.Drawing.Size(133, 24);
            this.steamid9.TabIndex = 235;
            // 
            // steamid8
            // 
            this.steamid8.BackColor = System.Drawing.Color.White;
            this.steamid8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid8.Location = new System.Drawing.Point(164, 162);
            this.steamid8.Margin = new System.Windows.Forms.Padding(2);
            this.steamid8.MaxLength = 17;
            this.steamid8.Name = "steamid8";
            this.steamid8.Size = new System.Drawing.Size(133, 24);
            this.steamid8.TabIndex = 233;
            // 
            // steamid7
            // 
            this.steamid7.BackColor = System.Drawing.Color.White;
            this.steamid7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid7.Location = new System.Drawing.Point(164, 143);
            this.steamid7.Margin = new System.Windows.Forms.Padding(2);
            this.steamid7.MaxLength = 17;
            this.steamid7.Name = "steamid7";
            this.steamid7.Size = new System.Drawing.Size(133, 24);
            this.steamid7.TabIndex = 232;
            // 
            // steamid6
            // 
            this.steamid6.BackColor = System.Drawing.Color.White;
            this.steamid6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid6.Location = new System.Drawing.Point(164, 124);
            this.steamid6.Margin = new System.Windows.Forms.Padding(2);
            this.steamid6.MaxLength = 17;
            this.steamid6.Name = "steamid6";
            this.steamid6.Size = new System.Drawing.Size(133, 24);
            this.steamid6.TabIndex = 231;
            // 
            // steamid5
            // 
            this.steamid5.BackColor = System.Drawing.Color.White;
            this.steamid5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid5.Location = new System.Drawing.Point(164, 105);
            this.steamid5.Margin = new System.Windows.Forms.Padding(2);
            this.steamid5.MaxLength = 17;
            this.steamid5.Name = "steamid5";
            this.steamid5.Size = new System.Drawing.Size(133, 24);
            this.steamid5.TabIndex = 230;
            // 
            // steamid4
            // 
            this.steamid4.BackColor = System.Drawing.Color.White;
            this.steamid4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid4.Location = new System.Drawing.Point(164, 86);
            this.steamid4.Margin = new System.Windows.Forms.Padding(2);
            this.steamid4.MaxLength = 17;
            this.steamid4.Name = "steamid4";
            this.steamid4.Size = new System.Drawing.Size(133, 24);
            this.steamid4.TabIndex = 229;
            // 
            // steamid3
            // 
            this.steamid3.BackColor = System.Drawing.Color.White;
            this.steamid3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid3.Location = new System.Drawing.Point(164, 67);
            this.steamid3.Margin = new System.Windows.Forms.Padding(2);
            this.steamid3.MaxLength = 17;
            this.steamid3.Name = "steamid3";
            this.steamid3.Size = new System.Drawing.Size(133, 24);
            this.steamid3.TabIndex = 228;
            // 
            // steamid2
            // 
            this.steamid2.BackColor = System.Drawing.Color.White;
            this.steamid2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid2.Location = new System.Drawing.Point(164, 48);
            this.steamid2.Margin = new System.Windows.Forms.Padding(2);
            this.steamid2.MaxLength = 17;
            this.steamid2.Name = "steamid2";
            this.steamid2.Size = new System.Drawing.Size(133, 24);
            this.steamid2.TabIndex = 227;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(166, 8);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(104, 16);
            this.label47.TabIndex = 225;
            this.label47.Text = "Player Steam ID";
            // 
            // steamid1
            // 
            this.steamid1.BackColor = System.Drawing.Color.White;
            this.steamid1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid1.Location = new System.Drawing.Point(164, 29);
            this.steamid1.Margin = new System.Windows.Forms.Padding(2);
            this.steamid1.MaxLength = 17;
            this.steamid1.Name = "steamid1";
            this.steamid1.Size = new System.Drawing.Size(133, 24);
            this.steamid1.TabIndex = 226;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(368, 6);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 16);
            this.label21.TabIndex = 224;
            this.label21.Text = "Player ID";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(431, 6);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 16);
            this.label25.TabIndex = 223;
            this.label25.Text = "Nickname";
            // 
            // player16N
            // 
            this.player16N.BackColor = System.Drawing.Color.White;
            this.player16N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player16N.Location = new System.Drawing.Point(429, 160);
            this.player16N.Margin = new System.Windows.Forms.Padding(2);
            this.player16N.MaxLength = 9;
            this.player16N.Name = "player16N";
            this.player16N.Size = new System.Drawing.Size(69, 24);
            this.player16N.TabIndex = 221;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(368, 164);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 16);
            this.label17.TabIndex = 222;
            this.label17.Text = "Player 16 :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player15N
            // 
            this.player15N.BackColor = System.Drawing.Color.White;
            this.player15N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player15N.Location = new System.Drawing.Point(429, 141);
            this.player15N.Margin = new System.Windows.Forms.Padding(2);
            this.player15N.MaxLength = 9;
            this.player15N.Name = "player15N";
            this.player15N.Size = new System.Drawing.Size(69, 24);
            this.player15N.TabIndex = 219;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(368, 144);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 16);
            this.label18.TabIndex = 220;
            this.label18.Text = "Player 15 :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player14N
            // 
            this.player14N.BackColor = System.Drawing.Color.White;
            this.player14N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player14N.Location = new System.Drawing.Point(429, 122);
            this.player14N.Margin = new System.Windows.Forms.Padding(2);
            this.player14N.MaxLength = 9;
            this.player14N.Name = "player14N";
            this.player14N.Size = new System.Drawing.Size(69, 24);
            this.player14N.TabIndex = 217;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(368, 125);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 16);
            this.label19.TabIndex = 218;
            this.label19.Text = "Player 14 :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player13N
            // 
            this.player13N.BackColor = System.Drawing.Color.White;
            this.player13N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player13N.Location = new System.Drawing.Point(429, 103);
            this.player13N.Margin = new System.Windows.Forms.Padding(2);
            this.player13N.MaxLength = 9;
            this.player13N.Name = "player13N";
            this.player13N.Size = new System.Drawing.Size(69, 24);
            this.player13N.TabIndex = 215;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(368, 106);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 16);
            this.label20.TabIndex = 216;
            this.label20.Text = "Player 13 :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player12N
            // 
            this.player12N.BackColor = System.Drawing.Color.White;
            this.player12N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player12N.Location = new System.Drawing.Point(429, 84);
            this.player12N.Margin = new System.Windows.Forms.Padding(2);
            this.player12N.MaxLength = 9;
            this.player12N.Name = "player12N";
            this.player12N.Size = new System.Drawing.Size(69, 24);
            this.player12N.TabIndex = 213;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(368, 87);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(69, 16);
            this.label22.TabIndex = 214;
            this.label22.Text = "Player 12 :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player11N
            // 
            this.player11N.BackColor = System.Drawing.Color.White;
            this.player11N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player11N.Location = new System.Drawing.Point(429, 65);
            this.player11N.Margin = new System.Windows.Forms.Padding(2);
            this.player11N.MaxLength = 9;
            this.player11N.Name = "player11N";
            this.player11N.Size = new System.Drawing.Size(69, 24);
            this.player11N.TabIndex = 212;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(368, 68);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(69, 16);
            this.label23.TabIndex = 211;
            this.label23.Text = "Player 11 :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player10N
            // 
            this.player10N.BackColor = System.Drawing.Color.White;
            this.player10N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player10N.Location = new System.Drawing.Point(429, 46);
            this.player10N.Margin = new System.Windows.Forms.Padding(2);
            this.player10N.MaxLength = 9;
            this.player10N.Name = "player10N";
            this.player10N.Size = new System.Drawing.Size(69, 24);
            this.player10N.TabIndex = 210;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(368, 48);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(69, 16);
            this.label24.TabIndex = 208;
            this.label24.Text = "Player 10 :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player9N
            // 
            this.player9N.BackColor = System.Drawing.Color.White;
            this.player9N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player9N.Location = new System.Drawing.Point(429, 27);
            this.player9N.Margin = new System.Windows.Forms.Padding(2);
            this.player9N.MaxLength = 9;
            this.player9N.Name = "player9N";
            this.player9N.Size = new System.Drawing.Size(69, 24);
            this.player9N.TabIndex = 209;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(369, 30);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(62, 16);
            this.label26.TabIndex = 207;
            this.label26.Text = "Player 9 :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player8N
            // 
            this.player8N.BackColor = System.Drawing.Color.White;
            this.player8N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player8N.Location = new System.Drawing.Point(88, 162);
            this.player8N.Margin = new System.Windows.Forms.Padding(2);
            this.player8N.MaxLength = 9;
            this.player8N.Name = "player8N";
            this.player8N.Size = new System.Drawing.Size(69, 24);
            this.player8N.TabIndex = 205;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 166);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 16);
            this.label16.TabIndex = 206;
            this.label16.Text = "Player 8 :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player7N
            // 
            this.player7N.BackColor = System.Drawing.Color.White;
            this.player7N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player7N.Location = new System.Drawing.Point(88, 143);
            this.player7N.Margin = new System.Windows.Forms.Padding(2);
            this.player7N.MaxLength = 9;
            this.player7N.Name = "player7N";
            this.player7N.Size = new System.Drawing.Size(69, 24);
            this.player7N.TabIndex = 203;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(34, 146);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 16);
            this.label15.TabIndex = 204;
            this.label15.Text = "Player 7 :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player6N
            // 
            this.player6N.BackColor = System.Drawing.Color.White;
            this.player6N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player6N.Location = new System.Drawing.Point(88, 124);
            this.player6N.Margin = new System.Windows.Forms.Padding(2);
            this.player6N.MaxLength = 9;
            this.player6N.Name = "player6N";
            this.player6N.Size = new System.Drawing.Size(69, 24);
            this.player6N.TabIndex = 201;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 127);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 202;
            this.label11.Text = "Player 6 :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player5N
            // 
            this.player5N.BackColor = System.Drawing.Color.White;
            this.player5N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player5N.Location = new System.Drawing.Point(88, 105);
            this.player5N.Margin = new System.Windows.Forms.Padding(2);
            this.player5N.MaxLength = 9;
            this.player5N.Name = "player5N";
            this.player5N.Size = new System.Drawing.Size(69, 24);
            this.player5N.TabIndex = 199;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 108);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 16);
            this.label9.TabIndex = 200;
            this.label9.Text = "Player 5 :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 8);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 16);
            this.label13.TabIndex = 198;
            this.label13.Text = "Player ID";
            // 
            // player4N
            // 
            this.player4N.BackColor = System.Drawing.Color.White;
            this.player4N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player4N.Location = new System.Drawing.Point(88, 86);
            this.player4N.Margin = new System.Windows.Forms.Padding(2);
            this.player4N.MaxLength = 9;
            this.player4N.Name = "player4N";
            this.player4N.Size = new System.Drawing.Size(69, 24);
            this.player4N.TabIndex = 196;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 89);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 16);
            this.label14.TabIndex = 197;
            this.label14.Text = "Player 4 :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player3N
            // 
            this.player3N.BackColor = System.Drawing.Color.White;
            this.player3N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player3N.Location = new System.Drawing.Point(88, 67);
            this.player3N.Margin = new System.Windows.Forms.Padding(2);
            this.player3N.MaxLength = 9;
            this.player3N.Name = "player3N";
            this.player3N.Size = new System.Drawing.Size(69, 24);
            this.player3N.TabIndex = 195;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 70);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 16);
            this.label12.TabIndex = 194;
            this.label12.Text = "Player 3 :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player2N
            // 
            this.player2N.BackColor = System.Drawing.Color.White;
            this.player2N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player2N.Location = new System.Drawing.Point(88, 48);
            this.player2N.Margin = new System.Windows.Forms.Padding(2);
            this.player2N.MaxLength = 9;
            this.player2N.Name = "player2N";
            this.player2N.Size = new System.Drawing.Size(69, 24);
            this.player2N.TabIndex = 193;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 16);
            this.label10.TabIndex = 191;
            this.label10.Text = "Player 2 :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(90, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 16);
            this.label8.TabIndex = 190;
            this.label8.Text = "Nickname";
            // 
            // player1N
            // 
            this.player1N.BackColor = System.Drawing.Color.White;
            this.player1N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player1N.Location = new System.Drawing.Point(88, 29);
            this.player1N.Margin = new System.Windows.Forms.Padding(2);
            this.player1N.MaxLength = 9;
            this.player1N.Name = "player1N";
            this.player1N.Size = new System.Drawing.Size(69, 24);
            this.player1N.TabIndex = 192;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 189;
            this.label7.Text = "Player 1 :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Panel1.Controls.Add(this.SplitColors);
            this.Panel1.Controls.Add(this.cmb_Network);
            this.Panel1.Controls.Add(this.autoPlay);
            this.Panel1.Controls.Add(this.numMaxPlyrs);
            this.Panel1.Controls.Add(this.numUpDownHor);
            this.Panel1.Controls.Add(this.numUpDownVer);
            this.Panel1.Controls.Add(this.SplitDiv);
            this.Panel1.Controls.Add(this.label49);
            this.Panel1.Controls.Add(this.slitWarning_Label);
            this.Panel1.Controls.Add(this.layoutSizer);
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.label27);
            this.Panel1.Controls.Add(this.label28);
            this.Panel1.Controls.Add(this.KeepSymLinkCheckBox);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.WIndowsSetupTiming_Label);
            this.Panel1.Controls.Add(this.WindowsSetupTiming_TextBox);
            this.Panel1.Controls.Add(this.pauseBetweenInstanceLaunch_TxtBox);
            this.Panel1.Controls.Add(this.pauseBetweenInstanceLauch_Label);
            this.Panel1.Controls.Add(this.scaleOptionCbx);
            this.Panel1.Controls.Add(this.keepAccountsCheck);
            this.Panel1.Controls.Add(this.label32);
            this.Panel1.Controls.Add(this.useNicksCheck);
            this.Panel1.Location = new System.Drawing.Point(3, 116);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(671, 401);
            this.Panel1.TabIndex = 42;
            // 
            // SplitColors
            // 
            this.SplitColors.BackColor = System.Drawing.Color.Transparent;
            this.SplitColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SplitColors.Location = new System.Drawing.Point(14, 342);
            this.SplitColors.Margin = new System.Windows.Forms.Padding(0);
            this.SplitColors.Name = "SplitColors";
            this.SplitColors.ScrollOffset = 2;
            this.SplitColors.SelectedItem = null;
            this.SplitColors.Size = new System.Drawing.Size(119, 19);
            this.SplitColors.TabIndex = 137;
            // 
            // cmb_Network
            // 
            this.cmb_Network.BackColor = System.Drawing.Color.Transparent;
            this.cmb_Network.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cmb_Network.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Network.Location = new System.Drawing.Point(14, 156);
            this.cmb_Network.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmb_Network.Name = "cmb_Network";
            this.cmb_Network.ScrollOffset = 2;
            this.cmb_Network.SelectedItem = null;
            this.cmb_Network.Size = new System.Drawing.Size(198, 19);
            this.cmb_Network.TabIndex = 136;
            // 
            // autoPlay
            // 
            this.autoPlay.AutoSize = true;
            this.autoPlay.Location = new System.Drawing.Point(14, 108);
            this.autoPlay.Margin = new System.Windows.Forms.Padding(2);
            this.autoPlay.Name = "autoPlay";
            this.autoPlay.Size = new System.Drawing.Size(203, 20);
            this.autoPlay.TabIndex = 135;
            this.autoPlay.Text = "Auto Play On Profile Selection";
            this.autoPlay.UseVisualStyleBackColor = true;
            // 
            // numMaxPlyrs
            // 
            this.numMaxPlyrs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxPlyrs.BackColor = System.Drawing.Color.Transparent;
            this.numMaxPlyrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMaxPlyrs.Location = new System.Drawing.Point(294, 128);
            this.numMaxPlyrs.Margin = new System.Windows.Forms.Padding(0);
            this.numMaxPlyrs.Name = "numMaxPlyrs";
            this.numMaxPlyrs.Size = new System.Drawing.Size(48, 20);
            this.numMaxPlyrs.TabIndex = 134;
            this.numMaxPlyrs.Value = 0;
            // 
            // numUpDownHor
            // 
            this.numUpDownHor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownHor.BackColor = System.Drawing.Color.Transparent;
            this.numUpDownHor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownHor.Location = new System.Drawing.Point(294, 77);
            this.numUpDownHor.Margin = new System.Windows.Forms.Padding(0);
            this.numUpDownHor.Name = "numUpDownHor";
            this.numUpDownHor.Size = new System.Drawing.Size(48, 20);
            this.numUpDownHor.TabIndex = 133;
            this.numUpDownHor.Value = 0;
            // 
            // numUpDownVer
            // 
            this.numUpDownVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownVer.BackColor = System.Drawing.Color.Transparent;
            this.numUpDownVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownVer.Location = new System.Drawing.Point(294, 30);
            this.numUpDownVer.Margin = new System.Windows.Forms.Padding(0);
            this.numUpDownVer.Name = "numUpDownVer";
            this.numUpDownVer.Size = new System.Drawing.Size(48, 20);
            this.numUpDownVer.TabIndex = 132;
            this.numUpDownVer.Value = 0;
            // 
            // SplitDiv
            // 
            this.SplitDiv.AutoSize = true;
            this.SplitDiv.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitDiv.Location = new System.Drawing.Point(15, 294);
            this.SplitDiv.Margin = new System.Windows.Forms.Padding(0);
            this.SplitDiv.Name = "SplitDiv";
            this.SplitDiv.Size = new System.Drawing.Size(142, 20);
            this.SplitDiv.TabIndex = 131;
            this.SplitDiv.Text = "Splitscreen division";
            this.SplitDiv.UseVisualStyleBackColor = true;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(15, 320);
            this.label49.Margin = new System.Windows.Forms.Padding(0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(103, 15);
            this.label49.TabIndex = 130;
            this.label49.Text = "Background color";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slitWarning_Label
            // 
            this.slitWarning_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.slitWarning_Label.AutoSize = true;
            this.slitWarning_Label.BackColor = System.Drawing.Color.Transparent;
            this.slitWarning_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.slitWarning_Label.ForeColor = System.Drawing.Color.Red;
            this.slitWarning_Label.Location = new System.Drawing.Point(195, 357);
            this.slitWarning_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.slitWarning_Label.Name = "slitWarning_Label";
            this.slitWarning_Label.Size = new System.Drawing.Size(287, 16);
            this.slitWarning_Label.TabIndex = 128;
            this.slitWarning_Label.Text = "Splitscreen division may not work for all games.";
            this.slitWarning_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.slitWarning_Label.UseMnemonic = false;
            // 
            // layoutSizer
            // 
            this.layoutSizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutSizer.BackColor = System.Drawing.Color.Transparent;
            this.layoutSizer.Location = new System.Drawing.Point(416, 6);
            this.layoutSizer.Margin = new System.Windows.Forms.Padding(0);
            this.layoutSizer.MaximumSize = new System.Drawing.Size(245, 170);
            this.layoutSizer.MinimumSize = new System.Drawing.Size(245, 170);
            this.layoutSizer.Name = "layoutSizer";
            this.layoutSizer.Size = new System.Drawing.Size(245, 170);
            this.layoutSizer.TabIndex = 127;
            this.layoutSizer.Paint += new System.Windows.Forms.PaintEventHandler(this.layoutSizer_Paint);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label29.Location = new System.Drawing.Point(287, 106);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(81, 16);
            this.label29.TabIndex = 122;
            this.label29.Text = "Max Players";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label27.Location = new System.Drawing.Point(287, 58);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(125, 16);
            this.label27.TabIndex = 120;
            this.label27.Text = "Horizontal Divisions";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label28.Location = new System.Drawing.Point(287, 12);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(113, 16);
            this.label28.TabIndex = 121;
            this.label28.Text = " Vertical Divisions";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // KeepSymLinkCheckBox
            // 
            this.KeepSymLinkCheckBox.AutoSize = true;
            this.KeepSymLinkCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.KeepSymLinkCheckBox.Location = new System.Drawing.Point(15, 58);
            this.KeepSymLinkCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.KeepSymLinkCheckBox.Name = "KeepSymLinkCheckBox";
            this.KeepSymLinkCheckBox.Size = new System.Drawing.Size(156, 20);
            this.KeepSymLinkCheckBox.TabIndex = 119;
            this.KeepSymLinkCheckBox.Text = "Keep SymLink On Exit";
            this.KeepSymLinkCheckBox.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 264);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 16);
            this.label6.TabIndex = 118;
            this.label6.Text = "(milliseconds)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 214);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 117;
            this.label2.Text = "(seconds)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WIndowsSetupTiming_Label
            // 
            this.WIndowsSetupTiming_Label.AutoSize = true;
            this.WIndowsSetupTiming_Label.Location = new System.Drawing.Point(15, 240);
            this.WIndowsSetupTiming_Label.Margin = new System.Windows.Forms.Padding(0);
            this.WIndowsSetupTiming_Label.Name = "WIndowsSetupTiming_Label";
            this.WIndowsSetupTiming_Label.Size = new System.Drawing.Size(147, 16);
            this.WIndowsSetupTiming_Label.TabIndex = 116;
            this.WIndowsSetupTiming_Label.Text = "Windows Setup Timing ";
            this.WIndowsSetupTiming_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WindowsSetupTiming_TextBox
            // 
            this.WindowsSetupTiming_TextBox.Location = new System.Drawing.Point(15, 261);
            this.WindowsSetupTiming_TextBox.Margin = new System.Windows.Forms.Padding(0);
            this.WindowsSetupTiming_TextBox.MaxLength = 5;
            this.WindowsSetupTiming_TextBox.Name = "WindowsSetupTiming_TextBox";
            this.WindowsSetupTiming_TextBox.Size = new System.Drawing.Size(57, 22);
            this.WindowsSetupTiming_TextBox.TabIndex = 115;
            this.WindowsSetupTiming_TextBox.Text = "0";
            this.WindowsSetupTiming_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pauseBetweenInstanceLaunch_TxtBox
            // 
            this.pauseBetweenInstanceLaunch_TxtBox.Location = new System.Drawing.Point(15, 211);
            this.pauseBetweenInstanceLaunch_TxtBox.Margin = new System.Windows.Forms.Padding(0);
            this.pauseBetweenInstanceLaunch_TxtBox.MaxLength = 3;
            this.pauseBetweenInstanceLaunch_TxtBox.Name = "pauseBetweenInstanceLaunch_TxtBox";
            this.pauseBetweenInstanceLaunch_TxtBox.Size = new System.Drawing.Size(37, 22);
            this.pauseBetweenInstanceLaunch_TxtBox.TabIndex = 113;
            this.pauseBetweenInstanceLaunch_TxtBox.Text = "0";
            this.pauseBetweenInstanceLaunch_TxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pauseBetweenInstanceLauch_Label
            // 
            this.pauseBetweenInstanceLauch_Label.AutoSize = true;
            this.pauseBetweenInstanceLauch_Label.Location = new System.Drawing.Point(15, 190);
            this.pauseBetweenInstanceLauch_Label.Margin = new System.Windows.Forms.Padding(0);
            this.pauseBetweenInstanceLauch_Label.Name = "pauseBetweenInstanceLauch_Label";
            this.pauseBetweenInstanceLauch_Label.Size = new System.Drawing.Size(189, 16);
            this.pauseBetweenInstanceLauch_Label.TabIndex = 114;
            this.pauseBetweenInstanceLauch_Label.Text = "Pause Between Game Startup ";
            this.pauseBetweenInstanceLauch_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scaleOptionCbx
            // 
            this.scaleOptionCbx.AutoSize = true;
            this.scaleOptionCbx.Checked = true;
            this.scaleOptionCbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleOptionCbx.Location = new System.Drawing.Point(15, 83);
            this.scaleOptionCbx.Margin = new System.Windows.Forms.Padding(2);
            this.scaleOptionCbx.Name = "scaleOptionCbx";
            this.scaleOptionCbx.Size = new System.Drawing.Size(212, 20);
            this.scaleOptionCbx.TabIndex = 112;
            this.scaleOptionCbx.Text = "Auto set desktop scale to 100%";
            this.scaleOptionCbx.UseVisualStyleBackColor = true;
            // 
            // keepAccountsCheck
            // 
            this.keepAccountsCheck.AutoSize = true;
            this.keepAccountsCheck.Location = new System.Drawing.Point(15, 35);
            this.keepAccountsCheck.Margin = new System.Windows.Forms.Padding(2);
            this.keepAccountsCheck.Name = "keepAccountsCheck";
            this.keepAccountsCheck.Size = new System.Drawing.Size(200, 20);
            this.keepAccountsCheck.TabIndex = 111;
            this.keepAccountsCheck.Text = "Keep Nucleus User Accounts";
            this.keepAccountsCheck.UseVisualStyleBackColor = true;
            this.keepAccountsCheck.Click += new System.EventHandler(this.keepAccountsCheck_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(15, 134);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(56, 16);
            this.label32.TabIndex = 109;
            this.label32.Text = "Network";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useNicksCheck
            // 
            this.useNicksCheck.AutoSize = true;
            this.useNicksCheck.BackColor = System.Drawing.Color.Transparent;
            this.useNicksCheck.Location = new System.Drawing.Point(15, 11);
            this.useNicksCheck.Margin = new System.Windows.Forms.Padding(2);
            this.useNicksCheck.Name = "useNicksCheck";
            this.useNicksCheck.Size = new System.Drawing.Size(176, 20);
            this.useNicksCheck.TabIndex = 108;
            this.useNicksCheck.Text = "Use Nicknames In-Game";
            this.useNicksCheck.UseVisualStyleBackColor = false;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.RosyBrown;
            this.Panel3.Controls.Add(this.label39);
            this.Panel3.Controls.Add(this.audioDefaultDevice);
            this.Panel3.Controls.Add(this.audioCustomSettingsBox);
            this.Panel3.Controls.Add(this.audioCustomSettingsRadio);
            this.Panel3.Controls.Add(this.audioDefaultSettingsRadio);
            this.Panel3.Controls.Add(this.audioRefresh);
            this.Panel3.Location = new System.Drawing.Point(149, 30);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(671, 401);
            this.Panel3.TabIndex = 292;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(290, 40);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(288, 12);
            this.label39.TabIndex = 22;
            this.label39.Text = "This feature may not work for all games";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label39.UseMnemonic = false;
            // 
            // audioDefaultDevice
            // 
            this.audioDefaultDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.audioDefaultDevice.BackColor = System.Drawing.Color.Transparent;
            this.audioDefaultDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioDefaultDevice.ForeColor = System.Drawing.Color.White;
            this.audioDefaultDevice.Location = new System.Drawing.Point(290, 15);
            this.audioDefaultDevice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.audioDefaultDevice.Name = "audioDefaultDevice";
            this.audioDefaultDevice.Size = new System.Drawing.Size(290, 17);
            this.audioDefaultDevice.TabIndex = 21;
            this.audioDefaultDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // audioCustomSettingsBox
            // 
            this.audioCustomSettingsBox.BackColor = System.Drawing.Color.Transparent;
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance8);
            this.audioCustomSettingsBox.Controls.Add(this.label45);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance7);
            this.audioCustomSettingsBox.Controls.Add(this.label44);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance6);
            this.audioCustomSettingsBox.Controls.Add(this.label43);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance5);
            this.audioCustomSettingsBox.Controls.Add(this.label42);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance4);
            this.audioCustomSettingsBox.Controls.Add(this.label41);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance3);
            this.audioCustomSettingsBox.Controls.Add(this.label40);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance2);
            this.audioCustomSettingsBox.Controls.Add(this.AudioInstance1);
            this.audioCustomSettingsBox.Controls.Add(this.label37);
            this.audioCustomSettingsBox.Controls.Add(this.label36);
            this.audioCustomSettingsBox.Location = new System.Drawing.Point(75, 69);
            this.audioCustomSettingsBox.Margin = new System.Windows.Forms.Padding(2);
            this.audioCustomSettingsBox.Name = "audioCustomSettingsBox";
            this.audioCustomSettingsBox.Padding = new System.Windows.Forms.Padding(2);
            this.audioCustomSettingsBox.Size = new System.Drawing.Size(503, 121);
            this.audioCustomSettingsBox.TabIndex = 20;
            this.audioCustomSettingsBox.TabStop = false;
            // 
            // AudioInstance8
            // 
            this.AudioInstance8.BackColor = System.Drawing.Color.White;
            this.AudioInstance8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance8.FormattingEnabled = true;
            this.AudioInstance8.Location = new System.Drawing.Point(321, 84);
            this.AudioInstance8.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance8.Name = "AudioInstance8";
            this.AudioInstance8.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance8.TabIndex = 29;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(257, 85);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(70, 16);
            this.label45.TabIndex = 28;
            this.label45.Text = "Instance 8:";
            // 
            // AudioInstance7
            // 
            this.AudioInstance7.BackColor = System.Drawing.Color.White;
            this.AudioInstance7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance7.FormattingEnabled = true;
            this.AudioInstance7.Location = new System.Drawing.Point(321, 62);
            this.AudioInstance7.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance7.Name = "AudioInstance7";
            this.AudioInstance7.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance7.TabIndex = 27;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(257, 64);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(70, 16);
            this.label44.TabIndex = 26;
            this.label44.Text = "Instance 7:";
            // 
            // AudioInstance6
            // 
            this.AudioInstance6.BackColor = System.Drawing.Color.White;
            this.AudioInstance6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance6.FormattingEnabled = true;
            this.AudioInstance6.Location = new System.Drawing.Point(321, 40);
            this.AudioInstance6.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance6.Name = "AudioInstance6";
            this.AudioInstance6.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance6.TabIndex = 25;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(257, 42);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(70, 16);
            this.label43.TabIndex = 24;
            this.label43.Text = "Instance 6:";
            // 
            // AudioInstance5
            // 
            this.AudioInstance5.BackColor = System.Drawing.Color.White;
            this.AudioInstance5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance5.FormattingEnabled = true;
            this.AudioInstance5.Location = new System.Drawing.Point(321, 18);
            this.AudioInstance5.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance5.Name = "AudioInstance5";
            this.AudioInstance5.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance5.TabIndex = 23;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(257, 20);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(70, 16);
            this.label42.TabIndex = 22;
            this.label42.Text = "Instance 5:";
            // 
            // AudioInstance4
            // 
            this.AudioInstance4.BackColor = System.Drawing.Color.White;
            this.AudioInstance4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance4.FormattingEnabled = true;
            this.AudioInstance4.Location = new System.Drawing.Point(70, 84);
            this.AudioInstance4.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance4.Name = "AudioInstance4";
            this.AudioInstance4.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance4.TabIndex = 21;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 86);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(70, 16);
            this.label41.TabIndex = 20;
            this.label41.Text = "Instance 4:";
            // 
            // AudioInstance3
            // 
            this.AudioInstance3.BackColor = System.Drawing.Color.White;
            this.AudioInstance3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance3.FormattingEnabled = true;
            this.AudioInstance3.Location = new System.Drawing.Point(70, 62);
            this.AudioInstance3.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance3.Name = "AudioInstance3";
            this.AudioInstance3.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance3.TabIndex = 19;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 65);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(70, 16);
            this.label40.TabIndex = 18;
            this.label40.Text = "Instance 3:";
            // 
            // AudioInstance2
            // 
            this.AudioInstance2.BackColor = System.Drawing.Color.White;
            this.AudioInstance2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance2.FormattingEnabled = true;
            this.AudioInstance2.Location = new System.Drawing.Point(70, 40);
            this.AudioInstance2.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance2.Name = "AudioInstance2";
            this.AudioInstance2.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance2.TabIndex = 6;
            // 
            // AudioInstance1
            // 
            this.AudioInstance1.BackColor = System.Drawing.Color.White;
            this.AudioInstance1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance1.FormattingEnabled = true;
            this.AudioInstance1.Items.AddRange(new object[] {
            "Default"});
            this.AudioInstance1.Location = new System.Drawing.Point(70, 18);
            this.AudioInstance1.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance1.Name = "AudioInstance1";
            this.AudioInstance1.Size = new System.Drawing.Size(170, 24);
            this.AudioInstance1.TabIndex = 1;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 43);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 16);
            this.label37.TabIndex = 5;
            this.label37.Text = "Instance 2:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 21);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(70, 16);
            this.label36.TabIndex = 0;
            this.label36.Text = "Instance 1:";
            // 
            // audioCustomSettingsRadio
            // 
            this.audioCustomSettingsRadio.AutoSize = true;
            this.audioCustomSettingsRadio.BackColor = System.Drawing.Color.Transparent;
            this.audioCustomSettingsRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.audioCustomSettingsRadio.Location = new System.Drawing.Point(15, 35);
            this.audioCustomSettingsRadio.Margin = new System.Windows.Forms.Padding(2);
            this.audioCustomSettingsRadio.Name = "audioCustomSettingsRadio";
            this.audioCustomSettingsRadio.Size = new System.Drawing.Size(119, 20);
            this.audioCustomSettingsRadio.TabIndex = 19;
            this.audioCustomSettingsRadio.Text = "Custom settings";
            this.audioCustomSettingsRadio.UseVisualStyleBackColor = false;
            this.audioCustomSettingsRadio.CheckedChanged += new System.EventHandler(this.audioCustomSettingsRadio_CheckedChanged);
            // 
            // audioDefaultSettingsRadio
            // 
            this.audioDefaultSettingsRadio.AutoSize = true;
            this.audioDefaultSettingsRadio.BackColor = System.Drawing.Color.Transparent;
            this.audioDefaultSettingsRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.audioDefaultSettingsRadio.Checked = true;
            this.audioDefaultSettingsRadio.Location = new System.Drawing.Point(15, 15);
            this.audioDefaultSettingsRadio.Margin = new System.Windows.Forms.Padding(2);
            this.audioDefaultSettingsRadio.Name = "audioDefaultSettingsRadio";
            this.audioDefaultSettingsRadio.Size = new System.Drawing.Size(268, 20);
            this.audioDefaultSettingsRadio.TabIndex = 18;
            this.audioDefaultSettingsRadio.TabStop = true;
            this.audioDefaultSettingsRadio.Text = "Use default audio playback device for all";
            this.audioDefaultSettingsRadio.UseVisualStyleBackColor = false;
            // 
            // audioRefresh
            // 
            this.audioRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.audioRefresh.BackColor = System.Drawing.Color.Orange;
            this.audioRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.audioRefresh.FlatAppearance.BorderSize = 0;
            this.audioRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.audioRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.audioRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioRefresh.Location = new System.Drawing.Point(318, 349);
            this.audioRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.audioRefresh.Name = "audioRefresh";
            this.audioRefresh.Size = new System.Drawing.Size(23, 23);
            this.audioRefresh.TabIndex = 23;
            this.audioRefresh.UseVisualStyleBackColor = false;
            this.audioRefresh.Click += new System.EventHandler(this.audioRefresh_Click);
            // 
            // ProfileSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.priorities);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.modeLabel);
            this.Controls.Add(this.closeBtnPicture);
            this.Controls.Add(this.prioritiesBtnPicture);
            this.Controls.Add(this.sharedBtnPicture);
            this.Controls.Add(this.playersBtnPicture);
            this.Controls.Add(this.audioBtnPicture);
            this.Controls.Add(this.prioritiesBtn);
            this.Controls.Add(this.audioBtn);
            this.Controls.Add(this.playersBtn);
            this.Controls.Add(this.sharedBtn);
            this.Controls.Add(this.saveBtnPicture);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.closeBtn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ProfileSettings";
            this.Size = new System.Drawing.Size(674, 430);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProfileSettings_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.saveBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.audioBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playersBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prioritiesBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtnPicture)).EndInit();
            this.priorities.ResumeLayout(false);
            this.priorities.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.def_sid_textBox_container.ResumeLayout(false);
            this.def_sid_textBox_container.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.audioCustomSettingsBox.ResumeLayout(false);
            this.audioCustomSettingsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button closeBtn;
        private Button sharedBtn;
        private Button playersBtn;
        private Button audioBtn;
        private Button prioritiesBtn;
        private ComboBox steamid32;
        private ComboBox steamid31;
        private ComboBox steamid30;
        private ComboBox steamid29;
        private ComboBox steamid28;
        private ComboBox steamid27;
        private ComboBox steamid26;
        private ComboBox steamid25;
        private ComboBox steamid24;
        private ComboBox steamid23;
        private ComboBox steamid22;
        private ComboBox steamid21;
        private ComboBox steamid20;
        private ComboBox steamid19;
        private ComboBox steamid18;
        private ComboBox steamid17;
        private ComboBox player32N;
        private Label label31;
        private ComboBox player31N;
        private Label label33;
        private ComboBox player30N;
        private Label label34;
        private ComboBox player29N;
        private Label label35;
        private ComboBox player28N;
        private Label label38;
        private ComboBox player27N;
        private Label label46;
        private ComboBox player26N;
        private Label label50;
        private ComboBox player25N;
        private Label label51;
        private ComboBox player24N;
        private Label label52;
        private ComboBox player23N;
        private Label label53;
        private ComboBox player22N;
        private Label label54;
        private ComboBox player21N;
        private Label label55;
        private ComboBox player20N;
        private Label label57;
        private ComboBox player19N;
        private Label label58;
        private ComboBox player18N;
        private Label label59;
        private ComboBox player17N;
        private Label label61;
        private Panel def_sid_textBox_container;
        private Label default_sid_list_label;
        private ComboBox def_sid_comboBox;
        private ComboBox steamid16;
        private ComboBox steamid15;
        private ComboBox steamid14;
        private ComboBox steamid13;
        private ComboBox steamid12;
        private ComboBox steamid11;
        private ComboBox steamid10;
        private Label label48;
        private ComboBox steamid9;
        private ComboBox steamid8;
        private ComboBox steamid7;
        private ComboBox steamid6;
        private ComboBox steamid5;
        private ComboBox steamid4;
        private ComboBox steamid3;
        private ComboBox steamid2;
        private Label label47;
        private ComboBox steamid1;
        private Label label21;
        private Label label25;
        private ComboBox player16N;
        private Label label17;
        private ComboBox player15N;
        private Label label18;
        private ComboBox player14N;
        private Label label19;
        private ComboBox player13N;
        private Label label20;
        private ComboBox player12N;
        private Label label22;
        private ComboBox player11N;
        private Label label23;
        private ComboBox player10N;
        private Label label24;
        private ComboBox player9N;
        private Label label26;
        private ComboBox player8N;
        private Label label16;
        private ComboBox player7N;
        private Label label15;
        private ComboBox player6N;
        private Label label11;
        private ComboBox player5N;
        private Label label9;
        private Label label13;
        private ComboBox player4N;
        private Label label14;
        private ComboBox player3N;
        private Label label12;
        private ComboBox player2N;
        private Label label10;
        private Label label8;
        private ComboBox player1N;
        private Label label7;
        private BufferedClientAreaPanel Panel3;
        private Label label39;
        private Label audioDefaultDevice;
        private GroupBox audioCustomSettingsBox;
        private ComboBox AudioInstance8;
        private Label label45;
        private ComboBox AudioInstance7;
        private Label label44;
        private ComboBox AudioInstance6;
        private Label label43;
        private ComboBox AudioInstance5;
        private Label label42;
        private ComboBox AudioInstance4;
        private Label label41;
        private ComboBox AudioInstance3;
        private Label label40;
        private ComboBox AudioInstance2;
        private ComboBox AudioInstance1;
        private Label label37;
        private Label label36;
        private RadioButton audioCustomSettingsRadio;
        private RadioButton audioDefaultSettingsRadio;
        private Button audioRefresh;
        private PictureBox saveBtnPicture;
        private PictureBox audioBtnPicture;
        private PictureBox playersBtnPicture;
        private PictureBox sharedBtnPicture;
        private PictureBox prioritiesBtnPicture;
        private BufferedClientAreaPanel Panel1;
        private CheckBox SplitDiv;
        private Label label49;
        private Label slitWarning_Label;
        public Panel layoutSizer;
        private Label label29;
        private Label label27;
        private Label label28;
        private CheckBox KeepSymLinkCheckBox;
        private Label label6;
        private Label label2;
        private Label WIndowsSetupTiming_Label;
        private TextBox WindowsSetupTiming_TextBox;
        private TextBox pauseBetweenInstanceLaunch_TxtBox;
        private Label pauseBetweenInstanceLauch_Label;
        private CheckBox scaleOptionCbx;
        private CheckBox keepAccountsCheck;
        private Label label32;
        private CheckBox useNicksCheck;
        private BufferedClientAreaPanel Panel2;
        private PictureBox closeBtnPicture;
        private Gaming.Controls.CustomNumericUpDown numMaxPlyrs;
        private Gaming.Controls.CustomNumericUpDown numUpDownHor;
        private Gaming.Controls.CustomNumericUpDown numUpDownVer;
        private Label modeLabel;
        private CheckBox autoPlay;
        private Gaming.Controls.CustomComboBox cmb_Network;
        private Gaming.Controls.CustomComboBox SplitColors;
        private BufferedClientAreaPanel priorities;
        private ComboBox PriorityClass32;
        private ComboBox PriorityClass31;
        private ComboBox PriorityClass30;
        private ComboBox PriorityClass29;
        private ComboBox PriorityClass28;
        private ComboBox PriorityClass27;
        private ComboBox PriorityClass26;
        private ComboBox PriorityClass25;
        private ComboBox PriorityClass16;
        private ComboBox PriorityClass15;
        private ComboBox PriorityClass14;
        private ComboBox PriorityClass13;
        private ComboBox PriorityClass12;
        private ComboBox PriorityClass11;
        private ComboBox PriorityClass10;
        private ComboBox PriorityClass9;
        private ComboBox PriorityClass24;
        private ComboBox PriorityClass23;
        private ComboBox PriorityClass22;
        private ComboBox PriorityClass21;
        private ComboBox PriorityClass20;
        private ComboBox PriorityClass19;
        private ComboBox PriorityClass18;
        private ComboBox PriorityClass17;
        private ComboBox PriorityClass8;
        private ComboBox PriorityClass7;
        private ComboBox PriorityClass6;
        private ComboBox PriorityClass5;
        private ComboBox PriorityClass4;
        private ComboBox PriorityClass3;
        private ComboBox PriorityClass2;
        private ComboBox PriorityClass1;
        private ComboBox Affinity32;
        private ComboBox Affinity31;
        private ComboBox Affinity30;
        private ComboBox Affinity29;
        private ComboBox Affinity28;
        private ComboBox Affinity27;
        private ComboBox Affinity26;
        private ComboBox Affinity25;
        private ComboBox Affinity24;
        private ComboBox Affinity23;
        private ComboBox Affinity22;
        private ComboBox Affinity21;
        private ComboBox Affinity20;
        private ComboBox Affinity19;
        private ComboBox Affinity18;
        private ComboBox Affinity17;
        private ComboBox IdealProcessor32;
        private Label label1;
        private ComboBox IdealProcessor31;
        private Label label3;
        private ComboBox IdealProcessor30;
        private Label label4;
        private ComboBox IdealProcessor29;
        private Label label5;
        private ComboBox IdealProcessor28;
        private Label label30;
        private ComboBox IdealProcessor27;
        private Label label56;
        private ComboBox IdealProcessor26;
        private Label label60;
        private ComboBox IdealProcessor25;
        private Label label62;
        private ComboBox IdealProcessor24;
        private Label label63;
        private ComboBox IdealProcessor23;
        private Label label64;
        private ComboBox IdealProcessor22;
        private Label label65;
        private ComboBox IdealProcessor21;
        private Label label66;
        private ComboBox IdealProcessor20;
        private Label label67;
        private ComboBox IdealProcessor19;
        private Label label68;
        private ComboBox IdealProcessor18;
        private Label label69;
        private ComboBox IdealProcessor17;
        private Label label70;
        private ComboBox Affinity16;
        private ComboBox Affinity15;
        private ComboBox Affinity14;
        private ComboBox Affinity13;
        private ComboBox Affinity12;
        private ComboBox Affinity11;
        private ComboBox Affinity10;
        private ComboBox Affinity9;
        private ComboBox Affinity8;
        private ComboBox Affinity7;
        private ComboBox Affinity6;
        private ComboBox Affinity5;
        private ComboBox Affinity4;
        private ComboBox Affinity3;
        private ComboBox Affinity2;
        private Label Affinity_Label;
        private ComboBox Affinity1;
        private Label PriorityClass_Label;
        private ComboBox IdealProcessor16;
        private Label label76;
        private ComboBox IdealProcessor15;
        private Label label77;
        private ComboBox IdealProcessor14;
        private Label label78;
        private ComboBox IdealProcessor13;
        private Label label79;
        private ComboBox IdealProcessor12;
        private Label label80;
        private ComboBox IdealProcessor11;
        private Label label81;
        private ComboBox IdealProcessor10;
        private Label label82;
        private ComboBox IdealProcessor9;
        private Label label83;
        private ComboBox IdealProcessor8;
        private Label label84;
        private ComboBox IdealProcessor7;
        private Label label85;
        private ComboBox IdealProcessor6;
        private Label label86;
        private ComboBox IdealProcessor5;
        private Label label87;
        private Label Instance_Label;
        private ComboBox IdealProcessor4;
        private Label label89;
        private ComboBox IdealProcessor3;
        private Label label90;
        private ComboBox IdealProcessor2;
        private Label label91;
        private Label IdealProcessor_Label;
        private ComboBox IdealProcessor1;
        private Label label93;
    }
}