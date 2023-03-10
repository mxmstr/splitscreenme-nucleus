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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileSettings));
            this.sharedTabBtn = new System.Windows.Forms.Button();
            this.playersTabBtn = new System.Windows.Forms.Button();
            this.audioTabBtn = new System.Windows.Forms.Button();
            this.processorTabBtn = new System.Windows.Forms.Button();
            this.audioBtnPicture = new System.Windows.Forms.PictureBox();
            this.playersBtnPicture = new System.Windows.Forms.PictureBox();
            this.sharedBtnPicture = new System.Windows.Forms.PictureBox();
            this.processorBtnPicture = new System.Windows.Forms.PictureBox();
            this.closeBtnPicture = new System.Windows.Forms.PictureBox();
            this.modeLabel = new System.Windows.Forms.Label();
            this.profile_info_btn = new System.Windows.Forms.PictureBox();
            this.profileInfo = new System.Windows.Forms.TextBox();
            this.layoutBtnPicture = new System.Windows.Forms.PictureBox();
            this.layoutTabBtn = new System.Windows.Forms.Button();
            this.processorTab = new BufferedClientAreaPanel();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.coreCountLabel = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.btnProcessorNext = new System.Windows.Forms.Button();
            this.label99 = new System.Windows.Forms.Label();
            this.processorPage2 = new System.Windows.Forms.Panel();
            this.PriorityClass32 = new System.Windows.Forms.ComboBox();
            this.PriorityClass31 = new System.Windows.Forms.ComboBox();
            this.PriorityClass30 = new System.Windows.Forms.ComboBox();
            this.PriorityClass29 = new System.Windows.Forms.ComboBox();
            this.PriorityClass28 = new System.Windows.Forms.ComboBox();
            this.PriorityClass27 = new System.Windows.Forms.ComboBox();
            this.PriorityClass26 = new System.Windows.Forms.ComboBox();
            this.PriorityClass25 = new System.Windows.Forms.ComboBox();
            this.PriorityClass24 = new System.Windows.Forms.ComboBox();
            this.PriorityClass23 = new System.Windows.Forms.ComboBox();
            this.PriorityClass22 = new System.Windows.Forms.ComboBox();
            this.PriorityClass21 = new System.Windows.Forms.ComboBox();
            this.PriorityClass20 = new System.Windows.Forms.ComboBox();
            this.PriorityClass19 = new System.Windows.Forms.ComboBox();
            this.PriorityClass18 = new System.Windows.Forms.ComboBox();
            this.PriorityClass17 = new System.Windows.Forms.ComboBox();
            this.Affinity32 = new System.Windows.Forms.TextBox();
            this.Affinity31 = new System.Windows.Forms.TextBox();
            this.Affinity30 = new System.Windows.Forms.TextBox();
            this.Affinity29 = new System.Windows.Forms.TextBox();
            this.Affinity28 = new System.Windows.Forms.TextBox();
            this.Affinity27 = new System.Windows.Forms.TextBox();
            this.Affinity26 = new System.Windows.Forms.TextBox();
            this.Affinity25 = new System.Windows.Forms.TextBox();
            this.Affinity24 = new System.Windows.Forms.TextBox();
            this.Affinity23 = new System.Windows.Forms.TextBox();
            this.Affinity22 = new System.Windows.Forms.TextBox();
            this.Affinity21 = new System.Windows.Forms.TextBox();
            this.Affinity20 = new System.Windows.Forms.TextBox();
            this.Affinity19 = new System.Windows.Forms.TextBox();
            this.Affinity18 = new System.Windows.Forms.TextBox();
            this.Affinity17 = new System.Windows.Forms.TextBox();
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
            this.processorPage1 = new System.Windows.Forms.Panel();
            this.Affinity4 = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.IdealProcessor1 = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.IdealProcessor2 = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.IdealProcessor3 = new System.Windows.Forms.ComboBox();
            this.label89 = new System.Windows.Forms.Label();
            this.IdealProcessor4 = new System.Windows.Forms.ComboBox();
            this.label87 = new System.Windows.Forms.Label();
            this.PriorityClass16 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor5 = new System.Windows.Forms.ComboBox();
            this.PriorityClass15 = new System.Windows.Forms.ComboBox();
            this.label86 = new System.Windows.Forms.Label();
            this.PriorityClass14 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor6 = new System.Windows.Forms.ComboBox();
            this.PriorityClass13 = new System.Windows.Forms.ComboBox();
            this.label85 = new System.Windows.Forms.Label();
            this.PriorityClass12 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor7 = new System.Windows.Forms.ComboBox();
            this.PriorityClass11 = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.PriorityClass10 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor8 = new System.Windows.Forms.ComboBox();
            this.PriorityClass9 = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.IdealProcessor9 = new System.Windows.Forms.ComboBox();
            this.label82 = new System.Windows.Forms.Label();
            this.IdealProcessor10 = new System.Windows.Forms.ComboBox();
            this.label81 = new System.Windows.Forms.Label();
            this.IdealProcessor11 = new System.Windows.Forms.ComboBox();
            this.label80 = new System.Windows.Forms.Label();
            this.IdealProcessor12 = new System.Windows.Forms.ComboBox();
            this.label79 = new System.Windows.Forms.Label();
            this.PriorityClass8 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor13 = new System.Windows.Forms.ComboBox();
            this.PriorityClass7 = new System.Windows.Forms.ComboBox();
            this.label78 = new System.Windows.Forms.Label();
            this.PriorityClass6 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor14 = new System.Windows.Forms.ComboBox();
            this.PriorityClass5 = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.PriorityClass4 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor15 = new System.Windows.Forms.ComboBox();
            this.PriorityClass3 = new System.Windows.Forms.ComboBox();
            this.label76 = new System.Windows.Forms.Label();
            this.PriorityClass2 = new System.Windows.Forms.ComboBox();
            this.IdealProcessor16 = new System.Windows.Forms.ComboBox();
            this.PriorityClass1 = new System.Windows.Forms.ComboBox();
            this.Affinity1 = new System.Windows.Forms.TextBox();
            this.Affinity2 = new System.Windows.Forms.TextBox();
            this.Affinity3 = new System.Windows.Forms.TextBox();
            this.Affinity5 = new System.Windows.Forms.TextBox();
            this.Affinity6 = new System.Windows.Forms.TextBox();
            this.Affinity7 = new System.Windows.Forms.TextBox();
            this.Affinity8 = new System.Windows.Forms.TextBox();
            this.Affinity9 = new System.Windows.Forms.TextBox();
            this.Affinity10 = new System.Windows.Forms.TextBox();
            this.Affinity11 = new System.Windows.Forms.TextBox();
            this.Affinity12 = new System.Windows.Forms.TextBox();
            this.Affinity13 = new System.Windows.Forms.TextBox();
            this.Affinity14 = new System.Windows.Forms.TextBox();
            this.Affinity15 = new System.Windows.Forms.TextBox();
            this.Affinity16 = new System.Windows.Forms.TextBox();
            this.audioTab = new BufferedClientAreaPanel();
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
            this.playersTab = new BufferedClientAreaPanel();
            this.btnNext = new System.Windows.Forms.Button();
            this.def_sid_comboBox = new System.Windows.Forms.ComboBox();
            this.default_sid_list_label = new System.Windows.Forms.Label();
            this.page1 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.steamid16 = new System.Windows.Forms.ComboBox();
            this.steamid15 = new System.Windows.Forms.ComboBox();
            this.steamid14 = new System.Windows.Forms.ComboBox();
            this.steamid13 = new System.Windows.Forms.ComboBox();
            this.steamid12 = new System.Windows.Forms.ComboBox();
            this.steamid11 = new System.Windows.Forms.ComboBox();
            this.steamid10 = new System.Windows.Forms.ComboBox();
            this.steamid9 = new System.Windows.Forms.ComboBox();
            this.steamid8 = new System.Windows.Forms.ComboBox();
            this.steamid7 = new System.Windows.Forms.ComboBox();
            this.steamid6 = new System.Windows.Forms.ComboBox();
            this.steamid5 = new System.Windows.Forms.ComboBox();
            this.steamid4 = new System.Windows.Forms.ComboBox();
            this.steamid3 = new System.Windows.Forms.ComboBox();
            this.steamid2 = new System.Windows.Forms.ComboBox();
            this.steamid1 = new System.Windows.Forms.ComboBox();
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
            this.player4N = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.player3N = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.player2N = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.player1N = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.page2 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.steamid32 = new System.Windows.Forms.ComboBox();
            this.steamid17 = new System.Windows.Forms.ComboBox();
            this.steamid31 = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.steamid30 = new System.Windows.Forms.ComboBox();
            this.player17N = new System.Windows.Forms.ComboBox();
            this.steamid29 = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.steamid28 = new System.Windows.Forms.ComboBox();
            this.player18N = new System.Windows.Forms.ComboBox();
            this.steamid27 = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.steamid26 = new System.Windows.Forms.ComboBox();
            this.player19N = new System.Windows.Forms.ComboBox();
            this.steamid25 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.steamid24 = new System.Windows.Forms.ComboBox();
            this.player20N = new System.Windows.Forms.ComboBox();
            this.steamid23 = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.steamid22 = new System.Windows.Forms.ComboBox();
            this.player21N = new System.Windows.Forms.ComboBox();
            this.steamid21 = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.steamid20 = new System.Windows.Forms.ComboBox();
            this.player22N = new System.Windows.Forms.ComboBox();
            this.steamid19 = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.steamid18 = new System.Windows.Forms.ComboBox();
            this.player23N = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.player32N = new System.Windows.Forms.ComboBox();
            this.player24N = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.player31N = new System.Windows.Forms.ComboBox();
            this.player25N = new System.Windows.Forms.ComboBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.player30N = new System.Windows.Forms.ComboBox();
            this.player26N = new System.Windows.Forms.ComboBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.player29N = new System.Windows.Forms.ComboBox();
            this.player27N = new System.Windows.Forms.ComboBox();
            this.label92 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.player28N = new System.Windows.Forms.ComboBox();
            this.layoutTab = new BufferedClientAreaPanel();
            this.label71 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cts_unfocus = new System.Windows.Forms.CheckBox();
            this.cts_kar = new System.Windows.Forms.CheckBox();
            this.cts_Mute = new System.Windows.Forms.CheckBox();
            this.SplitColors = new System.Windows.Forms.ComboBox();
            this.numMaxPlyrs = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.numUpDownVer = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.SplitDiv = new System.Windows.Forms.CheckBox();
            this.label49 = new System.Windows.Forms.Label();
            this.layoutSizer = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.numUpDownHor = new Nucleus.Gaming.Controls.CustomNumericUpDown();
            this.sharedTab = new BufferedClientAreaPanel();
            this.scaleOptionCbx = new System.Windows.Forms.CheckBox();
            this.useNicksCheck = new System.Windows.Forms.CheckBox();
            this.label72 = new System.Windows.Forms.Label();
            this.notes_text = new System.Windows.Forms.TextBox();
            this.cmb_Network = new System.Windows.Forms.ComboBox();
            this.autoPlay = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WIndowsSetupTiming_Label = new System.Windows.Forms.Label();
            this.WindowsSetupTiming_TextBox = new System.Windows.Forms.TextBox();
            this.pauseBetweenInstanceLaunch_TxtBox = new System.Windows.Forms.TextBox();
            this.pauseBetweenInstanceLauch_Label = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.audioBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playersBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processorBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtnPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profile_info_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnPicture)).BeginInit();
            this.processorTab.SuspendLayout();
            this.processorPage2.SuspendLayout();
            this.processorPage1.SuspendLayout();
            this.audioTab.SuspendLayout();
            this.audioCustomSettingsBox.SuspendLayout();
            this.playersTab.SuspendLayout();
            this.page1.SuspendLayout();
            this.page2.SuspendLayout();
            this.layoutTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.sharedTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // sharedTabBtn
            // 
            this.sharedTabBtn.BackColor = System.Drawing.Color.Transparent;
            this.sharedTabBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sharedTabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sharedTabBtn.Location = new System.Drawing.Point(1, 2);
            this.sharedTabBtn.Margin = new System.Windows.Forms.Padding(0);
            this.sharedTabBtn.Name = "sharedTabBtn";
            this.sharedTabBtn.Size = new System.Drawing.Size(80, 23);
            this.sharedTabBtn.TabIndex = 38;
            this.sharedTabBtn.Text = "Shared";
            this.sharedTabBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sharedTabBtn.UseVisualStyleBackColor = false;
            this.sharedTabBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // playersTabBtn
            // 
            this.playersTabBtn.BackColor = System.Drawing.Color.Transparent;
            this.playersTabBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.playersTabBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.playersTabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playersTabBtn.Location = new System.Drawing.Point(102, 2);
            this.playersTabBtn.Margin = new System.Windows.Forms.Padding(0);
            this.playersTabBtn.Name = "playersTabBtn";
            this.playersTabBtn.Size = new System.Drawing.Size(80, 23);
            this.playersTabBtn.TabIndex = 43;
            this.playersTabBtn.Text = "Players";
            this.playersTabBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.playersTabBtn.UseVisualStyleBackColor = false;
            this.playersTabBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // audioTabBtn
            // 
            this.audioTabBtn.BackColor = System.Drawing.Color.Transparent;
            this.audioTabBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.audioTabBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.audioTabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioTabBtn.Location = new System.Drawing.Point(203, 2);
            this.audioTabBtn.Margin = new System.Windows.Forms.Padding(0);
            this.audioTabBtn.Name = "audioTabBtn";
            this.audioTabBtn.Size = new System.Drawing.Size(80, 23);
            this.audioTabBtn.TabIndex = 44;
            this.audioTabBtn.Text = "Audio";
            this.audioTabBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.audioTabBtn.UseVisualStyleBackColor = false;
            this.audioTabBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // processorTabBtn
            // 
            this.processorTabBtn.BackColor = System.Drawing.Color.Transparent;
            this.processorTabBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.processorTabBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.processorTabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processorTabBtn.Location = new System.Drawing.Point(304, 2);
            this.processorTabBtn.Margin = new System.Windows.Forms.Padding(0);
            this.processorTabBtn.Name = "processorTabBtn";
            this.processorTabBtn.Size = new System.Drawing.Size(80, 23);
            this.processorTabBtn.TabIndex = 45;
            this.processorTabBtn.Text = "Processor";
            this.processorTabBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.processorTabBtn.UseVisualStyleBackColor = false;
            this.processorTabBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // audioBtnPicture
            // 
            this.audioBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.audioBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.audioBtnPicture.Location = new System.Drawing.Point(284, 4);
            this.audioBtnPicture.Name = "audioBtnPicture";
            this.audioBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.audioBtnPicture.TabIndex = 294;
            this.audioBtnPicture.TabStop = false;
            // 
            // playersBtnPicture
            // 
            this.playersBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.playersBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playersBtnPicture.Location = new System.Drawing.Point(183, 4);
            this.playersBtnPicture.Name = "playersBtnPicture";
            this.playersBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.playersBtnPicture.TabIndex = 295;
            this.playersBtnPicture.TabStop = false;
            // 
            // sharedBtnPicture
            // 
            this.sharedBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.sharedBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sharedBtnPicture.Location = new System.Drawing.Point(82, 4);
            this.sharedBtnPicture.Name = "sharedBtnPicture";
            this.sharedBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.sharedBtnPicture.TabIndex = 296;
            this.sharedBtnPicture.TabStop = false;
            // 
            // processorBtnPicture
            // 
            this.processorBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.processorBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.processorBtnPicture.Location = new System.Drawing.Point(385, 4);
            this.processorBtnPicture.Name = "processorBtnPicture";
            this.processorBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.processorBtnPicture.TabIndex = 297;
            this.processorBtnPicture.TabStop = false;
            // 
            // closeBtnPicture
            // 
            this.closeBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.closeBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtnPicture.Location = new System.Drawing.Point(653, 2);
            this.closeBtnPicture.Name = "closeBtnPicture";
            this.closeBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.closeBtnPicture.TabIndex = 298;
            this.closeBtnPicture.TabStop = false;
            this.closeBtnPicture.Click += new System.EventHandler(this.closeBtnPicture_Click);
            this.closeBtnPicture.MouseEnter += new System.EventHandler(this.closeBtnPicture_MouseEnter);
            this.closeBtnPicture.MouseLeave += new System.EventHandler(this.closeBtnPicture_MouseLeave);
            // 
            // modeLabel
            // 
            this.modeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modeLabel.BackColor = System.Drawing.Color.Transparent;
            this.modeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modeLabel.ForeColor = System.Drawing.Color.Orange;
            this.modeLabel.Location = new System.Drawing.Point(539, 5);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(95, 16);
            this.modeLabel.TabIndex = 299;
            this.modeLabel.Text = "New Profile";
            this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // profile_info_btn
            // 
            this.profile_info_btn.BackColor = System.Drawing.Color.Transparent;
            this.profile_info_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.profile_info_btn.Location = new System.Drawing.Point(634, 2);
            this.profile_info_btn.Name = "profile_info_btn";
            this.profile_info_btn.Size = new System.Drawing.Size(19, 19);
            this.profile_info_btn.TabIndex = 301;
            this.profile_info_btn.TabStop = false;
            this.profile_info_btn.Click += new System.EventHandler(this.profile_info_btn_Click);
            this.profile_info_btn.MouseEnter += new System.EventHandler(this.profile_info_btn_MouseEnter);
            this.profile_info_btn.MouseLeave += new System.EventHandler(this.profile_info_btn_MouseLeave);
            // 
            // profileInfo
            // 
            this.profileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profileInfo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.profileInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileInfo.ForeColor = System.Drawing.Color.White;
            this.profileInfo.Location = new System.Drawing.Point(152, 96);
            this.profileInfo.Multiline = true;
            this.profileInfo.Name = "profileInfo";
            this.profileInfo.ReadOnly = true;
            this.profileInfo.Size = new System.Drawing.Size(485, 126);
            this.profileInfo.TabIndex = 0;
            this.profileInfo.Text = resources.GetString("profileInfo.Text");
            this.profileInfo.Visible = false;
            // 
            // layoutBtnPicture
            // 
            this.layoutBtnPicture.BackColor = System.Drawing.Color.Transparent;
            this.layoutBtnPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.layoutBtnPicture.Location = new System.Drawing.Point(486, 4);
            this.layoutBtnPicture.Name = "layoutBtnPicture";
            this.layoutBtnPicture.Size = new System.Drawing.Size(19, 19);
            this.layoutBtnPicture.TabIndex = 303;
            this.layoutBtnPicture.TabStop = false;
            // 
            // layoutTabBtn
            // 
            this.layoutTabBtn.BackColor = System.Drawing.Color.Transparent;
            this.layoutTabBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.layoutTabBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.layoutTabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.layoutTabBtn.Location = new System.Drawing.Point(405, 2);
            this.layoutTabBtn.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTabBtn.Name = "layoutTabBtn";
            this.layoutTabBtn.Size = new System.Drawing.Size(80, 23);
            this.layoutTabBtn.TabIndex = 302;
            this.layoutTabBtn.Text = "Layout";
            this.layoutTabBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutTabBtn.UseVisualStyleBackColor = false;
            this.layoutTabBtn.Click += new System.EventHandler(this.button5_Click);
            // 
            // processorTab
            // 
            this.processorTab.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.processorTab.Controls.Add(this.label96);
            this.processorTab.Controls.Add(this.label97);
            this.processorTab.Controls.Add(this.coreCountLabel);
            this.processorTab.Controls.Add(this.label98);
            this.processorTab.Controls.Add(this.btnProcessorNext);
            this.processorTab.Controls.Add(this.label99);
            this.processorTab.Controls.Add(this.processorPage1);
            this.processorTab.Controls.Add(this.processorPage2);
            this.processorTab.Location = new System.Drawing.Point(0, 26);
            this.processorTab.Name = "processorTab";
            this.processorTab.Size = new System.Drawing.Size(671, 401);
            this.processorTab.TabIndex = 300;
            this.processorTab.Visible = false;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label96.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(126, 7);
            this.label96.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(80, 13);
            this.label96.TabIndex = 388;
            this.label96.Text = "Ideal Processor";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(4, 7);
            this.label97.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(48, 13);
            this.label97.TabIndex = 389;
            this.label97.Text = "Instance";
            // 
            // coreCountLabel
            // 
            this.coreCountLabel.AutoSize = true;
            this.coreCountLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coreCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coreCountLabel.Location = new System.Drawing.Point(511, 7);
            this.coreCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.coreCountLabel.Name = "coreCountLabel";
            this.coreCountLabel.Size = new System.Drawing.Size(149, 13);
            this.coreCountLabel.TabIndex = 392;
            this.coreCountLabel.Text = "Cores/Threads     (Max Value)";
            this.coreCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label98.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(207, 7);
            this.label98.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(104, 13);
            this.label98.TabIndex = 391;
            this.label98.Text = "Affinity  (e.g. 1,2,3,4)";
            // 
            // btnProcessorNext
            // 
            this.btnProcessorNext.BackColor = System.Drawing.Color.Transparent;
            this.btnProcessorNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessorNext.Location = new System.Drawing.Point(595, 109);
            this.btnProcessorNext.Name = "btnProcessorNext";
            this.btnProcessorNext.Size = new System.Drawing.Size(75, 23);
            this.btnProcessorNext.TabIndex = 327;
            this.btnProcessorNext.Text = "Next";
            this.btnProcessorNext.UseVisualStyleBackColor = false;
            this.btnProcessorNext.Click += new System.EventHandler(this.btnProcessorNext_Click_1);
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(56, 7);
            this.label99.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(66, 13);
            this.label99.TabIndex = 390;
            this.label99.Text = "Priority Class";
            // 
            // processorPage2
            // 
            this.processorPage2.Controls.Add(this.PriorityClass32);
            this.processorPage2.Controls.Add(this.PriorityClass31);
            this.processorPage2.Controls.Add(this.PriorityClass30);
            this.processorPage2.Controls.Add(this.PriorityClass29);
            this.processorPage2.Controls.Add(this.PriorityClass28);
            this.processorPage2.Controls.Add(this.PriorityClass27);
            this.processorPage2.Controls.Add(this.PriorityClass26);
            this.processorPage2.Controls.Add(this.PriorityClass25);
            this.processorPage2.Controls.Add(this.PriorityClass24);
            this.processorPage2.Controls.Add(this.PriorityClass23);
            this.processorPage2.Controls.Add(this.PriorityClass22);
            this.processorPage2.Controls.Add(this.PriorityClass21);
            this.processorPage2.Controls.Add(this.PriorityClass20);
            this.processorPage2.Controls.Add(this.PriorityClass19);
            this.processorPage2.Controls.Add(this.PriorityClass18);
            this.processorPage2.Controls.Add(this.PriorityClass17);
            this.processorPage2.Controls.Add(this.Affinity32);
            this.processorPage2.Controls.Add(this.Affinity31);
            this.processorPage2.Controls.Add(this.Affinity30);
            this.processorPage2.Controls.Add(this.Affinity29);
            this.processorPage2.Controls.Add(this.Affinity28);
            this.processorPage2.Controls.Add(this.Affinity27);
            this.processorPage2.Controls.Add(this.Affinity26);
            this.processorPage2.Controls.Add(this.Affinity25);
            this.processorPage2.Controls.Add(this.Affinity24);
            this.processorPage2.Controls.Add(this.Affinity23);
            this.processorPage2.Controls.Add(this.Affinity22);
            this.processorPage2.Controls.Add(this.Affinity21);
            this.processorPage2.Controls.Add(this.Affinity20);
            this.processorPage2.Controls.Add(this.Affinity19);
            this.processorPage2.Controls.Add(this.Affinity18);
            this.processorPage2.Controls.Add(this.Affinity17);
            this.processorPage2.Controls.Add(this.IdealProcessor32);
            this.processorPage2.Controls.Add(this.label1);
            this.processorPage2.Controls.Add(this.IdealProcessor31);
            this.processorPage2.Controls.Add(this.label3);
            this.processorPage2.Controls.Add(this.IdealProcessor30);
            this.processorPage2.Controls.Add(this.label4);
            this.processorPage2.Controls.Add(this.IdealProcessor29);
            this.processorPage2.Controls.Add(this.label5);
            this.processorPage2.Controls.Add(this.IdealProcessor28);
            this.processorPage2.Controls.Add(this.label30);
            this.processorPage2.Controls.Add(this.IdealProcessor27);
            this.processorPage2.Controls.Add(this.label56);
            this.processorPage2.Controls.Add(this.IdealProcessor26);
            this.processorPage2.Controls.Add(this.label60);
            this.processorPage2.Controls.Add(this.IdealProcessor25);
            this.processorPage2.Controls.Add(this.label62);
            this.processorPage2.Controls.Add(this.IdealProcessor24);
            this.processorPage2.Controls.Add(this.label63);
            this.processorPage2.Controls.Add(this.IdealProcessor23);
            this.processorPage2.Controls.Add(this.label64);
            this.processorPage2.Controls.Add(this.IdealProcessor22);
            this.processorPage2.Controls.Add(this.label65);
            this.processorPage2.Controls.Add(this.IdealProcessor21);
            this.processorPage2.Controls.Add(this.label66);
            this.processorPage2.Controls.Add(this.IdealProcessor20);
            this.processorPage2.Controls.Add(this.label67);
            this.processorPage2.Controls.Add(this.IdealProcessor19);
            this.processorPage2.Controls.Add(this.label68);
            this.processorPage2.Controls.Add(this.IdealProcessor18);
            this.processorPage2.Controls.Add(this.label69);
            this.processorPage2.Controls.Add(this.IdealProcessor17);
            this.processorPage2.Controls.Add(this.label70);
            this.processorPage2.Location = new System.Drawing.Point(1, 134);
            this.processorPage2.Name = "processorPage2";
            this.processorPage2.Size = new System.Drawing.Size(671, 210);
            this.processorPage2.TabIndex = 326;
            // 
            // PriorityClass32
            // 
            this.PriorityClass32.BackColor = System.Drawing.Color.White;
            this.PriorityClass32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass32.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass32.Location = new System.Drawing.Point(405, 178);
            this.PriorityClass32.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass32.MaxLength = 17;
            this.PriorityClass32.Name = "PriorityClass32";
            this.PriorityClass32.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass32.TabIndex = 387;
            // 
            // PriorityClass31
            // 
            this.PriorityClass31.BackColor = System.Drawing.Color.White;
            this.PriorityClass31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass31.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass31.Location = new System.Drawing.Point(405, 156);
            this.PriorityClass31.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass31.MaxLength = 17;
            this.PriorityClass31.Name = "PriorityClass31";
            this.PriorityClass31.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass31.TabIndex = 386;
            // 
            // PriorityClass30
            // 
            this.PriorityClass30.BackColor = System.Drawing.Color.White;
            this.PriorityClass30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass30.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass30.Location = new System.Drawing.Point(405, 134);
            this.PriorityClass30.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass30.MaxLength = 17;
            this.PriorityClass30.Name = "PriorityClass30";
            this.PriorityClass30.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass30.TabIndex = 385;
            // 
            // PriorityClass29
            // 
            this.PriorityClass29.BackColor = System.Drawing.Color.White;
            this.PriorityClass29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass29.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass29.Location = new System.Drawing.Point(405, 112);
            this.PriorityClass29.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass29.MaxLength = 17;
            this.PriorityClass29.Name = "PriorityClass29";
            this.PriorityClass29.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass29.TabIndex = 384;
            // 
            // PriorityClass28
            // 
            this.PriorityClass28.BackColor = System.Drawing.Color.White;
            this.PriorityClass28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass28.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass28.Location = new System.Drawing.Point(405, 90);
            this.PriorityClass28.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass28.MaxLength = 17;
            this.PriorityClass28.Name = "PriorityClass28";
            this.PriorityClass28.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass28.TabIndex = 383;
            // 
            // PriorityClass27
            // 
            this.PriorityClass27.BackColor = System.Drawing.Color.White;
            this.PriorityClass27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass27.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass27.Location = new System.Drawing.Point(405, 68);
            this.PriorityClass27.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass27.MaxLength = 17;
            this.PriorityClass27.Name = "PriorityClass27";
            this.PriorityClass27.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass27.TabIndex = 382;
            // 
            // PriorityClass26
            // 
            this.PriorityClass26.BackColor = System.Drawing.Color.White;
            this.PriorityClass26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass26.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass26.Location = new System.Drawing.Point(405, 46);
            this.PriorityClass26.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass26.MaxLength = 17;
            this.PriorityClass26.Name = "PriorityClass26";
            this.PriorityClass26.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass26.TabIndex = 381;
            // 
            // PriorityClass25
            // 
            this.PriorityClass25.BackColor = System.Drawing.Color.White;
            this.PriorityClass25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass25.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass25.Location = new System.Drawing.Point(405, 24);
            this.PriorityClass25.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass25.MaxLength = 17;
            this.PriorityClass25.Name = "PriorityClass25";
            this.PriorityClass25.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass25.TabIndex = 380;
            // 
            // PriorityClass24
            // 
            this.PriorityClass24.BackColor = System.Drawing.Color.White;
            this.PriorityClass24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass24.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass24.Location = new System.Drawing.Point(41, 178);
            this.PriorityClass24.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass24.MaxLength = 17;
            this.PriorityClass24.Name = "PriorityClass24";
            this.PriorityClass24.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass24.TabIndex = 379;
            // 
            // PriorityClass23
            // 
            this.PriorityClass23.BackColor = System.Drawing.Color.White;
            this.PriorityClass23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass23.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass23.Location = new System.Drawing.Point(41, 156);
            this.PriorityClass23.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass23.MaxLength = 17;
            this.PriorityClass23.Name = "PriorityClass23";
            this.PriorityClass23.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass23.TabIndex = 378;
            // 
            // PriorityClass22
            // 
            this.PriorityClass22.BackColor = System.Drawing.Color.White;
            this.PriorityClass22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass22.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass22.Location = new System.Drawing.Point(41, 134);
            this.PriorityClass22.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass22.MaxLength = 17;
            this.PriorityClass22.Name = "PriorityClass22";
            this.PriorityClass22.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass22.TabIndex = 377;
            // 
            // PriorityClass21
            // 
            this.PriorityClass21.BackColor = System.Drawing.Color.White;
            this.PriorityClass21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass21.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass21.Location = new System.Drawing.Point(41, 112);
            this.PriorityClass21.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass21.MaxLength = 17;
            this.PriorityClass21.Name = "PriorityClass21";
            this.PriorityClass21.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass21.TabIndex = 376;
            // 
            // PriorityClass20
            // 
            this.PriorityClass20.BackColor = System.Drawing.Color.White;
            this.PriorityClass20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass20.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass20.Location = new System.Drawing.Point(41, 90);
            this.PriorityClass20.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass20.MaxLength = 17;
            this.PriorityClass20.Name = "PriorityClass20";
            this.PriorityClass20.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass20.TabIndex = 375;
            // 
            // PriorityClass19
            // 
            this.PriorityClass19.BackColor = System.Drawing.Color.White;
            this.PriorityClass19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass19.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass19.Location = new System.Drawing.Point(41, 68);
            this.PriorityClass19.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass19.MaxLength = 17;
            this.PriorityClass19.Name = "PriorityClass19";
            this.PriorityClass19.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass19.TabIndex = 374;
            // 
            // PriorityClass18
            // 
            this.PriorityClass18.BackColor = System.Drawing.Color.White;
            this.PriorityClass18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass18.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass18.Location = new System.Drawing.Point(41, 46);
            this.PriorityClass18.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass18.MaxLength = 17;
            this.PriorityClass18.Name = "PriorityClass18";
            this.PriorityClass18.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass18.TabIndex = 373;
            // 
            // PriorityClass17
            // 
            this.PriorityClass17.BackColor = System.Drawing.Color.White;
            this.PriorityClass17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass17.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass17.Location = new System.Drawing.Point(41, 24);
            this.PriorityClass17.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass17.MaxLength = 17;
            this.PriorityClass17.Name = "PriorityClass17";
            this.PriorityClass17.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass17.TabIndex = 372;
            // 
            // Affinity32
            // 
            this.Affinity32.BackColor = System.Drawing.Color.White;
            this.Affinity32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity32.Location = new System.Drawing.Point(555, 179);
            this.Affinity32.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity32.MaxLength = 100;
            this.Affinity32.Name = "Affinity32";
            this.Affinity32.Size = new System.Drawing.Size(86, 20);
            this.Affinity32.TabIndex = 371;
            // 
            // Affinity31
            // 
            this.Affinity31.BackColor = System.Drawing.Color.White;
            this.Affinity31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity31.Location = new System.Drawing.Point(555, 157);
            this.Affinity31.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity31.MaxLength = 100;
            this.Affinity31.Name = "Affinity31";
            this.Affinity31.Size = new System.Drawing.Size(86, 20);
            this.Affinity31.TabIndex = 370;
            // 
            // Affinity30
            // 
            this.Affinity30.BackColor = System.Drawing.Color.White;
            this.Affinity30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity30.Location = new System.Drawing.Point(555, 135);
            this.Affinity30.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity30.MaxLength = 100;
            this.Affinity30.Name = "Affinity30";
            this.Affinity30.Size = new System.Drawing.Size(86, 20);
            this.Affinity30.TabIndex = 369;
            // 
            // Affinity29
            // 
            this.Affinity29.BackColor = System.Drawing.Color.White;
            this.Affinity29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity29.Location = new System.Drawing.Point(555, 113);
            this.Affinity29.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity29.MaxLength = 100;
            this.Affinity29.Name = "Affinity29";
            this.Affinity29.Size = new System.Drawing.Size(86, 20);
            this.Affinity29.TabIndex = 368;
            // 
            // Affinity28
            // 
            this.Affinity28.BackColor = System.Drawing.Color.White;
            this.Affinity28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity28.Location = new System.Drawing.Point(555, 91);
            this.Affinity28.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity28.MaxLength = 100;
            this.Affinity28.Name = "Affinity28";
            this.Affinity28.Size = new System.Drawing.Size(86, 20);
            this.Affinity28.TabIndex = 367;
            // 
            // Affinity27
            // 
            this.Affinity27.BackColor = System.Drawing.Color.White;
            this.Affinity27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity27.Location = new System.Drawing.Point(555, 69);
            this.Affinity27.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity27.MaxLength = 100;
            this.Affinity27.Name = "Affinity27";
            this.Affinity27.Size = new System.Drawing.Size(86, 20);
            this.Affinity27.TabIndex = 366;
            // 
            // Affinity26
            // 
            this.Affinity26.BackColor = System.Drawing.Color.White;
            this.Affinity26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity26.Location = new System.Drawing.Point(555, 47);
            this.Affinity26.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity26.MaxLength = 100;
            this.Affinity26.Name = "Affinity26";
            this.Affinity26.Size = new System.Drawing.Size(86, 20);
            this.Affinity26.TabIndex = 365;
            // 
            // Affinity25
            // 
            this.Affinity25.BackColor = System.Drawing.Color.White;
            this.Affinity25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity25.Location = new System.Drawing.Point(555, 25);
            this.Affinity25.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity25.MaxLength = 100;
            this.Affinity25.Name = "Affinity25";
            this.Affinity25.Size = new System.Drawing.Size(86, 20);
            this.Affinity25.TabIndex = 364;
            // 
            // Affinity24
            // 
            this.Affinity24.BackColor = System.Drawing.Color.White;
            this.Affinity24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity24.Location = new System.Drawing.Point(192, 179);
            this.Affinity24.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity24.MaxLength = 100;
            this.Affinity24.Name = "Affinity24";
            this.Affinity24.Size = new System.Drawing.Size(86, 20);
            this.Affinity24.TabIndex = 363;
            // 
            // Affinity23
            // 
            this.Affinity23.BackColor = System.Drawing.Color.White;
            this.Affinity23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity23.Location = new System.Drawing.Point(192, 157);
            this.Affinity23.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity23.MaxLength = 100;
            this.Affinity23.Name = "Affinity23";
            this.Affinity23.Size = new System.Drawing.Size(86, 20);
            this.Affinity23.TabIndex = 362;
            // 
            // Affinity22
            // 
            this.Affinity22.BackColor = System.Drawing.Color.White;
            this.Affinity22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity22.Location = new System.Drawing.Point(192, 135);
            this.Affinity22.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity22.MaxLength = 100;
            this.Affinity22.Name = "Affinity22";
            this.Affinity22.Size = new System.Drawing.Size(86, 20);
            this.Affinity22.TabIndex = 361;
            // 
            // Affinity21
            // 
            this.Affinity21.BackColor = System.Drawing.Color.White;
            this.Affinity21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity21.Location = new System.Drawing.Point(192, 113);
            this.Affinity21.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity21.MaxLength = 100;
            this.Affinity21.Name = "Affinity21";
            this.Affinity21.Size = new System.Drawing.Size(86, 20);
            this.Affinity21.TabIndex = 360;
            // 
            // Affinity20
            // 
            this.Affinity20.BackColor = System.Drawing.Color.White;
            this.Affinity20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity20.Location = new System.Drawing.Point(192, 91);
            this.Affinity20.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity20.MaxLength = 100;
            this.Affinity20.Name = "Affinity20";
            this.Affinity20.Size = new System.Drawing.Size(86, 20);
            this.Affinity20.TabIndex = 359;
            // 
            // Affinity19
            // 
            this.Affinity19.BackColor = System.Drawing.Color.White;
            this.Affinity19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity19.Location = new System.Drawing.Point(192, 69);
            this.Affinity19.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity19.MaxLength = 100;
            this.Affinity19.Name = "Affinity19";
            this.Affinity19.Size = new System.Drawing.Size(86, 20);
            this.Affinity19.TabIndex = 358;
            // 
            // Affinity18
            // 
            this.Affinity18.BackColor = System.Drawing.Color.White;
            this.Affinity18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity18.Location = new System.Drawing.Point(192, 47);
            this.Affinity18.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity18.MaxLength = 100;
            this.Affinity18.Name = "Affinity18";
            this.Affinity18.Size = new System.Drawing.Size(86, 20);
            this.Affinity18.TabIndex = 357;
            // 
            // Affinity17
            // 
            this.Affinity17.BackColor = System.Drawing.Color.White;
            this.Affinity17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity17.Location = new System.Drawing.Point(192, 25);
            this.Affinity17.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity17.MaxLength = 100;
            this.Affinity17.Name = "Affinity17";
            this.Affinity17.Size = new System.Drawing.Size(86, 20);
            this.Affinity17.TabIndex = 356;
            // 
            // IdealProcessor32
            // 
            this.IdealProcessor32.BackColor = System.Drawing.Color.White;
            this.IdealProcessor32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor32.Location = new System.Drawing.Point(500, 178);
            this.IdealProcessor32.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor32.MaxLength = 9;
            this.IdealProcessor32.Name = "IdealProcessor32";
            this.IdealProcessor32.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor32.TabIndex = 354;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(376, 181);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 355;
            this.label1.Text = "32 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor31
            // 
            this.IdealProcessor31.BackColor = System.Drawing.Color.White;
            this.IdealProcessor31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor31.Location = new System.Drawing.Point(500, 156);
            this.IdealProcessor31.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor31.MaxLength = 9;
            this.IdealProcessor31.Name = "IdealProcessor31";
            this.IdealProcessor31.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor31.TabIndex = 352;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(376, 159);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 353;
            this.label3.Text = "31 :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor30
            // 
            this.IdealProcessor30.BackColor = System.Drawing.Color.White;
            this.IdealProcessor30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor30.Location = new System.Drawing.Point(500, 134);
            this.IdealProcessor30.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor30.MaxLength = 9;
            this.IdealProcessor30.Name = "IdealProcessor30";
            this.IdealProcessor30.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor30.TabIndex = 350;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(376, 137);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 351;
            this.label4.Text = "30 :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor29
            // 
            this.IdealProcessor29.BackColor = System.Drawing.Color.White;
            this.IdealProcessor29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor29.Location = new System.Drawing.Point(500, 112);
            this.IdealProcessor29.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor29.MaxLength = 9;
            this.IdealProcessor29.Name = "IdealProcessor29";
            this.IdealProcessor29.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor29.TabIndex = 348;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(376, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 349;
            this.label5.Text = "29 :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor28
            // 
            this.IdealProcessor28.BackColor = System.Drawing.Color.White;
            this.IdealProcessor28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor28.Location = new System.Drawing.Point(500, 90);
            this.IdealProcessor28.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor28.MaxLength = 9;
            this.IdealProcessor28.Name = "IdealProcessor28";
            this.IdealProcessor28.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor28.TabIndex = 346;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(376, 93);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(25, 13);
            this.label30.TabIndex = 347;
            this.label30.Text = "28 :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor27
            // 
            this.IdealProcessor27.BackColor = System.Drawing.Color.White;
            this.IdealProcessor27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor27.Location = new System.Drawing.Point(500, 68);
            this.IdealProcessor27.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor27.MaxLength = 9;
            this.IdealProcessor27.Name = "IdealProcessor27";
            this.IdealProcessor27.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor27.TabIndex = 345;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(376, 71);
            this.label56.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(25, 13);
            this.label56.TabIndex = 344;
            this.label56.Text = "27 :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor26
            // 
            this.IdealProcessor26.BackColor = System.Drawing.Color.White;
            this.IdealProcessor26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor26.Location = new System.Drawing.Point(500, 46);
            this.IdealProcessor26.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor26.MaxLength = 9;
            this.IdealProcessor26.Name = "IdealProcessor26";
            this.IdealProcessor26.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor26.TabIndex = 343;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(376, 49);
            this.label60.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(25, 13);
            this.label60.TabIndex = 341;
            this.label60.Text = "26 :";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor25
            // 
            this.IdealProcessor25.BackColor = System.Drawing.Color.White;
            this.IdealProcessor25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor25.Location = new System.Drawing.Point(500, 24);
            this.IdealProcessor25.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor25.MaxLength = 9;
            this.IdealProcessor25.Name = "IdealProcessor25";
            this.IdealProcessor25.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor25.TabIndex = 342;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(376, 28);
            this.label62.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(25, 13);
            this.label62.TabIndex = 340;
            this.label62.Text = "25 :";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor24
            // 
            this.IdealProcessor24.BackColor = System.Drawing.Color.White;
            this.IdealProcessor24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor24.Location = new System.Drawing.Point(136, 178);
            this.IdealProcessor24.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor24.MaxLength = 2;
            this.IdealProcessor24.Name = "IdealProcessor24";
            this.IdealProcessor24.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor24.TabIndex = 338;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(12, 181);
            this.label63.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(25, 13);
            this.label63.TabIndex = 339;
            this.label63.Text = "24 :";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor23
            // 
            this.IdealProcessor23.BackColor = System.Drawing.Color.White;
            this.IdealProcessor23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor23.Location = new System.Drawing.Point(136, 156);
            this.IdealProcessor23.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor23.MaxLength = 2;
            this.IdealProcessor23.Name = "IdealProcessor23";
            this.IdealProcessor23.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor23.TabIndex = 336;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(12, 159);
            this.label64.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(25, 13);
            this.label64.TabIndex = 337;
            this.label64.Text = "23 :";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor22
            // 
            this.IdealProcessor22.BackColor = System.Drawing.Color.White;
            this.IdealProcessor22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor22.Location = new System.Drawing.Point(136, 134);
            this.IdealProcessor22.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor22.MaxLength = 2;
            this.IdealProcessor22.Name = "IdealProcessor22";
            this.IdealProcessor22.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor22.TabIndex = 334;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(12, 137);
            this.label65.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(25, 13);
            this.label65.TabIndex = 335;
            this.label65.Text = "22 :";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor21
            // 
            this.IdealProcessor21.BackColor = System.Drawing.Color.White;
            this.IdealProcessor21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor21.Location = new System.Drawing.Point(136, 112);
            this.IdealProcessor21.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor21.MaxLength = 2;
            this.IdealProcessor21.Name = "IdealProcessor21";
            this.IdealProcessor21.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor21.TabIndex = 332;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(12, 115);
            this.label66.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(25, 13);
            this.label66.TabIndex = 333;
            this.label66.Text = "21 :";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor20
            // 
            this.IdealProcessor20.BackColor = System.Drawing.Color.White;
            this.IdealProcessor20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor20.Location = new System.Drawing.Point(136, 90);
            this.IdealProcessor20.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor20.MaxLength = 2;
            this.IdealProcessor20.Name = "IdealProcessor20";
            this.IdealProcessor20.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor20.TabIndex = 330;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(12, 93);
            this.label67.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(25, 13);
            this.label67.TabIndex = 331;
            this.label67.Text = "20 :";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor19
            // 
            this.IdealProcessor19.BackColor = System.Drawing.Color.White;
            this.IdealProcessor19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor19.Location = new System.Drawing.Point(136, 68);
            this.IdealProcessor19.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor19.MaxLength = 2;
            this.IdealProcessor19.Name = "IdealProcessor19";
            this.IdealProcessor19.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor19.TabIndex = 329;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(12, 71);
            this.label68.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(25, 13);
            this.label68.TabIndex = 328;
            this.label68.Text = "19 :";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor18
            // 
            this.IdealProcessor18.BackColor = System.Drawing.Color.White;
            this.IdealProcessor18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor18.Location = new System.Drawing.Point(136, 46);
            this.IdealProcessor18.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor18.MaxLength = 2;
            this.IdealProcessor18.Name = "IdealProcessor18";
            this.IdealProcessor18.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor18.TabIndex = 327;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(12, 49);
            this.label69.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(25, 13);
            this.label69.TabIndex = 325;
            this.label69.Text = "18 :";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor17
            // 
            this.IdealProcessor17.BackColor = System.Drawing.Color.White;
            this.IdealProcessor17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor17.Location = new System.Drawing.Point(136, 24);
            this.IdealProcessor17.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor17.MaxLength = 2;
            this.IdealProcessor17.Name = "IdealProcessor17";
            this.IdealProcessor17.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor17.TabIndex = 326;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(12, 28);
            this.label70.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(25, 13);
            this.label70.TabIndex = 324;
            this.label70.Text = "17 :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // processorPage1
            // 
            this.processorPage1.Controls.Add(this.Affinity4);
            this.processorPage1.Controls.Add(this.label93);
            this.processorPage1.Controls.Add(this.IdealProcessor1);
            this.processorPage1.Controls.Add(this.label91);
            this.processorPage1.Controls.Add(this.IdealProcessor2);
            this.processorPage1.Controls.Add(this.label90);
            this.processorPage1.Controls.Add(this.IdealProcessor3);
            this.processorPage1.Controls.Add(this.label89);
            this.processorPage1.Controls.Add(this.IdealProcessor4);
            this.processorPage1.Controls.Add(this.label87);
            this.processorPage1.Controls.Add(this.PriorityClass16);
            this.processorPage1.Controls.Add(this.IdealProcessor5);
            this.processorPage1.Controls.Add(this.PriorityClass15);
            this.processorPage1.Controls.Add(this.label86);
            this.processorPage1.Controls.Add(this.PriorityClass14);
            this.processorPage1.Controls.Add(this.IdealProcessor6);
            this.processorPage1.Controls.Add(this.PriorityClass13);
            this.processorPage1.Controls.Add(this.label85);
            this.processorPage1.Controls.Add(this.PriorityClass12);
            this.processorPage1.Controls.Add(this.IdealProcessor7);
            this.processorPage1.Controls.Add(this.PriorityClass11);
            this.processorPage1.Controls.Add(this.label84);
            this.processorPage1.Controls.Add(this.PriorityClass10);
            this.processorPage1.Controls.Add(this.IdealProcessor8);
            this.processorPage1.Controls.Add(this.PriorityClass9);
            this.processorPage1.Controls.Add(this.label83);
            this.processorPage1.Controls.Add(this.IdealProcessor9);
            this.processorPage1.Controls.Add(this.label82);
            this.processorPage1.Controls.Add(this.IdealProcessor10);
            this.processorPage1.Controls.Add(this.label81);
            this.processorPage1.Controls.Add(this.IdealProcessor11);
            this.processorPage1.Controls.Add(this.label80);
            this.processorPage1.Controls.Add(this.IdealProcessor12);
            this.processorPage1.Controls.Add(this.label79);
            this.processorPage1.Controls.Add(this.PriorityClass8);
            this.processorPage1.Controls.Add(this.IdealProcessor13);
            this.processorPage1.Controls.Add(this.PriorityClass7);
            this.processorPage1.Controls.Add(this.label78);
            this.processorPage1.Controls.Add(this.PriorityClass6);
            this.processorPage1.Controls.Add(this.IdealProcessor14);
            this.processorPage1.Controls.Add(this.PriorityClass5);
            this.processorPage1.Controls.Add(this.label77);
            this.processorPage1.Controls.Add(this.PriorityClass4);
            this.processorPage1.Controls.Add(this.IdealProcessor15);
            this.processorPage1.Controls.Add(this.PriorityClass3);
            this.processorPage1.Controls.Add(this.label76);
            this.processorPage1.Controls.Add(this.PriorityClass2);
            this.processorPage1.Controls.Add(this.IdealProcessor16);
            this.processorPage1.Controls.Add(this.PriorityClass1);
            this.processorPage1.Controls.Add(this.Affinity1);
            this.processorPage1.Controls.Add(this.Affinity2);
            this.processorPage1.Controls.Add(this.Affinity3);
            this.processorPage1.Controls.Add(this.Affinity5);
            this.processorPage1.Controls.Add(this.Affinity6);
            this.processorPage1.Controls.Add(this.Affinity7);
            this.processorPage1.Controls.Add(this.Affinity8);
            this.processorPage1.Controls.Add(this.Affinity9);
            this.processorPage1.Controls.Add(this.Affinity10);
            this.processorPage1.Controls.Add(this.Affinity11);
            this.processorPage1.Controls.Add(this.Affinity12);
            this.processorPage1.Controls.Add(this.Affinity13);
            this.processorPage1.Controls.Add(this.Affinity14);
            this.processorPage1.Controls.Add(this.Affinity15);
            this.processorPage1.Controls.Add(this.Affinity16);
            this.processorPage1.Location = new System.Drawing.Point(3, 133);
            this.processorPage1.Name = "processorPage1";
            this.processorPage1.Size = new System.Drawing.Size(671, 210);
            this.processorPage1.TabIndex = 325;
            // 
            // Affinity4
            // 
            this.Affinity4.BackColor = System.Drawing.Color.White;
            this.Affinity4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity4.Location = new System.Drawing.Point(192, 91);
            this.Affinity4.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity4.MaxLength = 100;
            this.Affinity4.Name = "Affinity4";
            this.Affinity4.Size = new System.Drawing.Size(86, 20);
            this.Affinity4.TabIndex = 229;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(18, 28);
            this.label93.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(19, 13);
            this.label93.TabIndex = 189;
            this.label93.Text = "1 :";
            this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor1
            // 
            this.IdealProcessor1.BackColor = System.Drawing.Color.White;
            this.IdealProcessor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor1.Location = new System.Drawing.Point(136, 24);
            this.IdealProcessor1.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor1.MaxLength = 2;
            this.IdealProcessor1.Name = "IdealProcessor1";
            this.IdealProcessor1.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor1.TabIndex = 192;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(18, 50);
            this.label91.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(19, 13);
            this.label91.TabIndex = 191;
            this.label91.Text = "2 :";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor2
            // 
            this.IdealProcessor2.BackColor = System.Drawing.Color.White;
            this.IdealProcessor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor2.Location = new System.Drawing.Point(136, 46);
            this.IdealProcessor2.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor2.MaxLength = 2;
            this.IdealProcessor2.Name = "IdealProcessor2";
            this.IdealProcessor2.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor2.TabIndex = 193;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(18, 72);
            this.label90.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(19, 13);
            this.label90.TabIndex = 194;
            this.label90.Text = "3 :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor3
            // 
            this.IdealProcessor3.BackColor = System.Drawing.Color.White;
            this.IdealProcessor3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor3.Location = new System.Drawing.Point(136, 68);
            this.IdealProcessor3.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor3.MaxLength = 2;
            this.IdealProcessor3.Name = "IdealProcessor3";
            this.IdealProcessor3.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor3.TabIndex = 195;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(18, 94);
            this.label89.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(19, 13);
            this.label89.TabIndex = 197;
            this.label89.Text = "4 :";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor4
            // 
            this.IdealProcessor4.BackColor = System.Drawing.Color.White;
            this.IdealProcessor4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor4.Location = new System.Drawing.Point(136, 90);
            this.IdealProcessor4.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor4.MaxLength = 2;
            this.IdealProcessor4.Name = "IdealProcessor4";
            this.IdealProcessor4.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor4.TabIndex = 196;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(18, 116);
            this.label87.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(19, 13);
            this.label87.TabIndex = 200;
            this.label87.Text = "5 :";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass16
            // 
            this.PriorityClass16.BackColor = System.Drawing.Color.White;
            this.PriorityClass16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass16.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass16.Location = new System.Drawing.Point(405, 178);
            this.PriorityClass16.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass16.MaxLength = 17;
            this.PriorityClass16.Name = "PriorityClass16";
            this.PriorityClass16.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass16.TabIndex = 315;
            // 
            // IdealProcessor5
            // 
            this.IdealProcessor5.BackColor = System.Drawing.Color.White;
            this.IdealProcessor5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor5.Location = new System.Drawing.Point(136, 112);
            this.IdealProcessor5.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor5.MaxLength = 2;
            this.IdealProcessor5.Name = "IdealProcessor5";
            this.IdealProcessor5.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor5.TabIndex = 199;
            // 
            // PriorityClass15
            // 
            this.PriorityClass15.BackColor = System.Drawing.Color.White;
            this.PriorityClass15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass15.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass15.Location = new System.Drawing.Point(405, 156);
            this.PriorityClass15.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass15.MaxLength = 17;
            this.PriorityClass15.Name = "PriorityClass15";
            this.PriorityClass15.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass15.TabIndex = 314;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(18, 138);
            this.label86.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(19, 13);
            this.label86.TabIndex = 202;
            this.label86.Text = "6 :";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass14
            // 
            this.PriorityClass14.BackColor = System.Drawing.Color.White;
            this.PriorityClass14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass14.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass14.Location = new System.Drawing.Point(405, 134);
            this.PriorityClass14.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass14.MaxLength = 17;
            this.PriorityClass14.Name = "PriorityClass14";
            this.PriorityClass14.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass14.TabIndex = 313;
            // 
            // IdealProcessor6
            // 
            this.IdealProcessor6.BackColor = System.Drawing.Color.White;
            this.IdealProcessor6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor6.Location = new System.Drawing.Point(136, 134);
            this.IdealProcessor6.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor6.MaxLength = 2;
            this.IdealProcessor6.Name = "IdealProcessor6";
            this.IdealProcessor6.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor6.TabIndex = 201;
            // 
            // PriorityClass13
            // 
            this.PriorityClass13.BackColor = System.Drawing.Color.White;
            this.PriorityClass13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass13.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass13.Location = new System.Drawing.Point(405, 112);
            this.PriorityClass13.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass13.MaxLength = 17;
            this.PriorityClass13.Name = "PriorityClass13";
            this.PriorityClass13.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass13.TabIndex = 312;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(18, 159);
            this.label85.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(19, 13);
            this.label85.TabIndex = 204;
            this.label85.Text = "7 :";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass12
            // 
            this.PriorityClass12.BackColor = System.Drawing.Color.White;
            this.PriorityClass12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass12.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass12.Location = new System.Drawing.Point(405, 90);
            this.PriorityClass12.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass12.MaxLength = 17;
            this.PriorityClass12.Name = "PriorityClass12";
            this.PriorityClass12.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass12.TabIndex = 311;
            // 
            // IdealProcessor7
            // 
            this.IdealProcessor7.BackColor = System.Drawing.Color.White;
            this.IdealProcessor7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor7.Location = new System.Drawing.Point(136, 156);
            this.IdealProcessor7.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor7.MaxLength = 2;
            this.IdealProcessor7.Name = "IdealProcessor7";
            this.IdealProcessor7.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor7.TabIndex = 203;
            // 
            // PriorityClass11
            // 
            this.PriorityClass11.BackColor = System.Drawing.Color.White;
            this.PriorityClass11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass11.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass11.Location = new System.Drawing.Point(405, 68);
            this.PriorityClass11.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass11.MaxLength = 17;
            this.PriorityClass11.Name = "PriorityClass11";
            this.PriorityClass11.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass11.TabIndex = 310;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(18, 181);
            this.label84.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(19, 13);
            this.label84.TabIndex = 206;
            this.label84.Text = "8 :";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass10
            // 
            this.PriorityClass10.BackColor = System.Drawing.Color.White;
            this.PriorityClass10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass10.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass10.Location = new System.Drawing.Point(405, 46);
            this.PriorityClass10.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass10.MaxLength = 17;
            this.PriorityClass10.Name = "PriorityClass10";
            this.PriorityClass10.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass10.TabIndex = 309;
            // 
            // IdealProcessor8
            // 
            this.IdealProcessor8.BackColor = System.Drawing.Color.White;
            this.IdealProcessor8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor8.Location = new System.Drawing.Point(136, 178);
            this.IdealProcessor8.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor8.MaxLength = 2;
            this.IdealProcessor8.Name = "IdealProcessor8";
            this.IdealProcessor8.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor8.TabIndex = 205;
            // 
            // PriorityClass9
            // 
            this.PriorityClass9.BackColor = System.Drawing.Color.White;
            this.PriorityClass9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass9.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass9.Location = new System.Drawing.Point(405, 24);
            this.PriorityClass9.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass9.MaxLength = 17;
            this.PriorityClass9.Name = "PriorityClass9";
            this.PriorityClass9.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass9.TabIndex = 308;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(382, 28);
            this.label83.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(19, 13);
            this.label83.TabIndex = 207;
            this.label83.Text = "9 :";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor9
            // 
            this.IdealProcessor9.BackColor = System.Drawing.Color.White;
            this.IdealProcessor9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor9.Location = new System.Drawing.Point(500, 24);
            this.IdealProcessor9.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor9.MaxLength = 9;
            this.IdealProcessor9.Name = "IdealProcessor9";
            this.IdealProcessor9.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor9.TabIndex = 209;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(376, 50);
            this.label82.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(25, 13);
            this.label82.TabIndex = 208;
            this.label82.Text = "10 :";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor10
            // 
            this.IdealProcessor10.BackColor = System.Drawing.Color.White;
            this.IdealProcessor10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor10.Location = new System.Drawing.Point(500, 46);
            this.IdealProcessor10.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor10.MaxLength = 9;
            this.IdealProcessor10.Name = "IdealProcessor10";
            this.IdealProcessor10.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor10.TabIndex = 210;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(376, 72);
            this.label81.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(25, 13);
            this.label81.TabIndex = 211;
            this.label81.Text = "11 :";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor11
            // 
            this.IdealProcessor11.BackColor = System.Drawing.Color.White;
            this.IdealProcessor11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor11.Location = new System.Drawing.Point(500, 68);
            this.IdealProcessor11.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor11.MaxLength = 9;
            this.IdealProcessor11.Name = "IdealProcessor11";
            this.IdealProcessor11.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor11.TabIndex = 212;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(376, 94);
            this.label80.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(25, 13);
            this.label80.TabIndex = 214;
            this.label80.Text = "12 :";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IdealProcessor12
            // 
            this.IdealProcessor12.BackColor = System.Drawing.Color.White;
            this.IdealProcessor12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor12.Location = new System.Drawing.Point(500, 90);
            this.IdealProcessor12.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor12.MaxLength = 9;
            this.IdealProcessor12.Name = "IdealProcessor12";
            this.IdealProcessor12.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor12.TabIndex = 213;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(376, 116);
            this.label79.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(25, 13);
            this.label79.TabIndex = 216;
            this.label79.Text = "13 :";
            this.label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass8
            // 
            this.PriorityClass8.BackColor = System.Drawing.Color.White;
            this.PriorityClass8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass8.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass8.Location = new System.Drawing.Point(41, 178);
            this.PriorityClass8.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass8.MaxLength = 17;
            this.PriorityClass8.Name = "PriorityClass8";
            this.PriorityClass8.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass8.TabIndex = 299;
            // 
            // IdealProcessor13
            // 
            this.IdealProcessor13.BackColor = System.Drawing.Color.White;
            this.IdealProcessor13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor13.Location = new System.Drawing.Point(500, 112);
            this.IdealProcessor13.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor13.MaxLength = 9;
            this.IdealProcessor13.Name = "IdealProcessor13";
            this.IdealProcessor13.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor13.TabIndex = 215;
            // 
            // PriorityClass7
            // 
            this.PriorityClass7.BackColor = System.Drawing.Color.White;
            this.PriorityClass7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass7.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass7.Location = new System.Drawing.Point(41, 156);
            this.PriorityClass7.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass7.MaxLength = 17;
            this.PriorityClass7.Name = "PriorityClass7";
            this.PriorityClass7.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass7.TabIndex = 298;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(376, 138);
            this.label78.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(25, 13);
            this.label78.TabIndex = 218;
            this.label78.Text = "14 :";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass6
            // 
            this.PriorityClass6.BackColor = System.Drawing.Color.White;
            this.PriorityClass6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass6.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass6.Location = new System.Drawing.Point(41, 134);
            this.PriorityClass6.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass6.MaxLength = 17;
            this.PriorityClass6.Name = "PriorityClass6";
            this.PriorityClass6.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass6.TabIndex = 297;
            // 
            // IdealProcessor14
            // 
            this.IdealProcessor14.BackColor = System.Drawing.Color.White;
            this.IdealProcessor14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor14.Location = new System.Drawing.Point(500, 134);
            this.IdealProcessor14.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor14.MaxLength = 9;
            this.IdealProcessor14.Name = "IdealProcessor14";
            this.IdealProcessor14.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor14.TabIndex = 217;
            // 
            // PriorityClass5
            // 
            this.PriorityClass5.BackColor = System.Drawing.Color.White;
            this.PriorityClass5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass5.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass5.Location = new System.Drawing.Point(41, 112);
            this.PriorityClass5.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass5.MaxLength = 17;
            this.PriorityClass5.Name = "PriorityClass5";
            this.PriorityClass5.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass5.TabIndex = 296;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(376, 160);
            this.label77.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(25, 13);
            this.label77.TabIndex = 220;
            this.label77.Text = "15 :";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass4
            // 
            this.PriorityClass4.BackColor = System.Drawing.Color.White;
            this.PriorityClass4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass4.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass4.Location = new System.Drawing.Point(41, 90);
            this.PriorityClass4.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass4.MaxLength = 17;
            this.PriorityClass4.Name = "PriorityClass4";
            this.PriorityClass4.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass4.TabIndex = 295;
            // 
            // IdealProcessor15
            // 
            this.IdealProcessor15.BackColor = System.Drawing.Color.White;
            this.IdealProcessor15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor15.Location = new System.Drawing.Point(500, 156);
            this.IdealProcessor15.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor15.MaxLength = 9;
            this.IdealProcessor15.Name = "IdealProcessor15";
            this.IdealProcessor15.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor15.TabIndex = 219;
            // 
            // PriorityClass3
            // 
            this.PriorityClass3.BackColor = System.Drawing.Color.White;
            this.PriorityClass3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass3.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass3.Location = new System.Drawing.Point(41, 68);
            this.PriorityClass3.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass3.MaxLength = 17;
            this.PriorityClass3.Name = "PriorityClass3";
            this.PriorityClass3.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass3.TabIndex = 294;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(376, 182);
            this.label76.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(25, 13);
            this.label76.TabIndex = 222;
            this.label76.Text = "16 :";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PriorityClass2
            // 
            this.PriorityClass2.BackColor = System.Drawing.Color.White;
            this.PriorityClass2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass2.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass2.Location = new System.Drawing.Point(41, 46);
            this.PriorityClass2.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass2.MaxLength = 17;
            this.PriorityClass2.Name = "PriorityClass2";
            this.PriorityClass2.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass2.TabIndex = 293;
            // 
            // IdealProcessor16
            // 
            this.IdealProcessor16.BackColor = System.Drawing.Color.White;
            this.IdealProcessor16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdealProcessor16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdealProcessor16.Location = new System.Drawing.Point(500, 178);
            this.IdealProcessor16.Margin = new System.Windows.Forms.Padding(2);
            this.IdealProcessor16.MaxLength = 9;
            this.IdealProcessor16.Name = "IdealProcessor16";
            this.IdealProcessor16.Size = new System.Drawing.Size(48, 21);
            this.IdealProcessor16.TabIndex = 221;
            // 
            // PriorityClass1
            // 
            this.PriorityClass1.BackColor = System.Drawing.Color.White;
            this.PriorityClass1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriorityClass1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriorityClass1.Items.AddRange(new object[] {
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.PriorityClass1.Location = new System.Drawing.Point(41, 24);
            this.PriorityClass1.Margin = new System.Windows.Forms.Padding(2);
            this.PriorityClass1.MaxLength = 17;
            this.PriorityClass1.Name = "PriorityClass1";
            this.PriorityClass1.Size = new System.Drawing.Size(86, 21);
            this.PriorityClass1.TabIndex = 292;
            // 
            // Affinity1
            // 
            this.Affinity1.BackColor = System.Drawing.Color.White;
            this.Affinity1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity1.Location = new System.Drawing.Point(192, 25);
            this.Affinity1.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity1.MaxLength = 100;
            this.Affinity1.Name = "Affinity1";
            this.Affinity1.Size = new System.Drawing.Size(86, 20);
            this.Affinity1.TabIndex = 226;
            // 
            // Affinity2
            // 
            this.Affinity2.BackColor = System.Drawing.Color.White;
            this.Affinity2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity2.Location = new System.Drawing.Point(192, 47);
            this.Affinity2.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity2.MaxLength = 100;
            this.Affinity2.Name = "Affinity2";
            this.Affinity2.Size = new System.Drawing.Size(86, 20);
            this.Affinity2.TabIndex = 227;
            // 
            // Affinity3
            // 
            this.Affinity3.BackColor = System.Drawing.Color.White;
            this.Affinity3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity3.Location = new System.Drawing.Point(192, 69);
            this.Affinity3.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity3.MaxLength = 100;
            this.Affinity3.Name = "Affinity3";
            this.Affinity3.Size = new System.Drawing.Size(86, 20);
            this.Affinity3.TabIndex = 228;
            // 
            // Affinity5
            // 
            this.Affinity5.BackColor = System.Drawing.Color.White;
            this.Affinity5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity5.Location = new System.Drawing.Point(192, 113);
            this.Affinity5.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity5.MaxLength = 100;
            this.Affinity5.Name = "Affinity5";
            this.Affinity5.Size = new System.Drawing.Size(86, 20);
            this.Affinity5.TabIndex = 230;
            // 
            // Affinity6
            // 
            this.Affinity6.BackColor = System.Drawing.Color.White;
            this.Affinity6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity6.Location = new System.Drawing.Point(192, 135);
            this.Affinity6.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity6.MaxLength = 100;
            this.Affinity6.Name = "Affinity6";
            this.Affinity6.Size = new System.Drawing.Size(86, 20);
            this.Affinity6.TabIndex = 231;
            // 
            // Affinity7
            // 
            this.Affinity7.BackColor = System.Drawing.Color.White;
            this.Affinity7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity7.Location = new System.Drawing.Point(192, 157);
            this.Affinity7.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity7.MaxLength = 100;
            this.Affinity7.Name = "Affinity7";
            this.Affinity7.Size = new System.Drawing.Size(86, 20);
            this.Affinity7.TabIndex = 232;
            // 
            // Affinity8
            // 
            this.Affinity8.BackColor = System.Drawing.Color.White;
            this.Affinity8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Affinity8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity8.Location = new System.Drawing.Point(192, 179);
            this.Affinity8.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity8.MaxLength = 100;
            this.Affinity8.Name = "Affinity8";
            this.Affinity8.Size = new System.Drawing.Size(86, 20);
            this.Affinity8.TabIndex = 233;
            // 
            // Affinity9
            // 
            this.Affinity9.BackColor = System.Drawing.Color.White;
            this.Affinity9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity9.Location = new System.Drawing.Point(555, 25);
            this.Affinity9.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity9.MaxLength = 100;
            this.Affinity9.Name = "Affinity9";
            this.Affinity9.Size = new System.Drawing.Size(86, 20);
            this.Affinity9.TabIndex = 235;
            // 
            // Affinity10
            // 
            this.Affinity10.BackColor = System.Drawing.Color.White;
            this.Affinity10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity10.Location = new System.Drawing.Point(555, 47);
            this.Affinity10.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity10.MaxLength = 100;
            this.Affinity10.Name = "Affinity10";
            this.Affinity10.Size = new System.Drawing.Size(86, 20);
            this.Affinity10.TabIndex = 236;
            // 
            // Affinity11
            // 
            this.Affinity11.BackColor = System.Drawing.Color.White;
            this.Affinity11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity11.Location = new System.Drawing.Point(555, 69);
            this.Affinity11.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity11.MaxLength = 100;
            this.Affinity11.Name = "Affinity11";
            this.Affinity11.Size = new System.Drawing.Size(86, 20);
            this.Affinity11.TabIndex = 237;
            // 
            // Affinity12
            // 
            this.Affinity12.BackColor = System.Drawing.Color.White;
            this.Affinity12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity12.Location = new System.Drawing.Point(555, 91);
            this.Affinity12.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity12.MaxLength = 100;
            this.Affinity12.Name = "Affinity12";
            this.Affinity12.Size = new System.Drawing.Size(86, 20);
            this.Affinity12.TabIndex = 238;
            // 
            // Affinity13
            // 
            this.Affinity13.BackColor = System.Drawing.Color.White;
            this.Affinity13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity13.Location = new System.Drawing.Point(555, 113);
            this.Affinity13.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity13.MaxLength = 100;
            this.Affinity13.Name = "Affinity13";
            this.Affinity13.Size = new System.Drawing.Size(86, 20);
            this.Affinity13.TabIndex = 239;
            // 
            // Affinity14
            // 
            this.Affinity14.BackColor = System.Drawing.Color.White;
            this.Affinity14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity14.Location = new System.Drawing.Point(555, 135);
            this.Affinity14.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity14.MaxLength = 100;
            this.Affinity14.Name = "Affinity14";
            this.Affinity14.Size = new System.Drawing.Size(86, 20);
            this.Affinity14.TabIndex = 240;
            // 
            // Affinity15
            // 
            this.Affinity15.BackColor = System.Drawing.Color.White;
            this.Affinity15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity15.Location = new System.Drawing.Point(555, 157);
            this.Affinity15.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity15.MaxLength = 100;
            this.Affinity15.Name = "Affinity15";
            this.Affinity15.Size = new System.Drawing.Size(86, 20);
            this.Affinity15.TabIndex = 241;
            // 
            // Affinity16
            // 
            this.Affinity16.BackColor = System.Drawing.Color.White;
            this.Affinity16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affinity16.Location = new System.Drawing.Point(555, 179);
            this.Affinity16.Margin = new System.Windows.Forms.Padding(2);
            this.Affinity16.MaxLength = 100;
            this.Affinity16.Name = "Affinity16";
            this.Affinity16.Size = new System.Drawing.Size(86, 20);
            this.Affinity16.TabIndex = 242;
            // 
            // audioTab
            // 
            this.audioTab.BackColor = System.Drawing.Color.RosyBrown;
            this.audioTab.Controls.Add(this.label39);
            this.audioTab.Controls.Add(this.audioDefaultDevice);
            this.audioTab.Controls.Add(this.audioCustomSettingsBox);
            this.audioTab.Controls.Add(this.audioCustomSettingsRadio);
            this.audioTab.Controls.Add(this.audioDefaultSettingsRadio);
            this.audioTab.Controls.Add(this.audioRefresh);
            this.audioTab.Location = new System.Drawing.Point(1, 30);
            this.audioTab.Name = "audioTab";
            this.audioTab.Size = new System.Drawing.Size(671, 401);
            this.audioTab.TabIndex = 292;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(299, 99);
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
            this.audioDefaultDevice.Location = new System.Drawing.Point(299, 74);
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
            this.audioCustomSettingsBox.Location = new System.Drawing.Point(84, 128);
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
            this.AudioInstance8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance8.FormattingEnabled = true;
            this.AudioInstance8.Location = new System.Drawing.Point(321, 84);
            this.AudioInstance8.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance8.Name = "AudioInstance8";
            this.AudioInstance8.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance8.TabIndex = 29;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(257, 85);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(60, 13);
            this.label45.TabIndex = 28;
            this.label45.Text = "Instance 8:";
            // 
            // AudioInstance7
            // 
            this.AudioInstance7.BackColor = System.Drawing.Color.White;
            this.AudioInstance7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance7.FormattingEnabled = true;
            this.AudioInstance7.Location = new System.Drawing.Point(321, 62);
            this.AudioInstance7.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance7.Name = "AudioInstance7";
            this.AudioInstance7.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance7.TabIndex = 27;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(257, 64);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(60, 13);
            this.label44.TabIndex = 26;
            this.label44.Text = "Instance 7:";
            // 
            // AudioInstance6
            // 
            this.AudioInstance6.BackColor = System.Drawing.Color.White;
            this.AudioInstance6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance6.FormattingEnabled = true;
            this.AudioInstance6.Location = new System.Drawing.Point(321, 40);
            this.AudioInstance6.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance6.Name = "AudioInstance6";
            this.AudioInstance6.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance6.TabIndex = 25;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(257, 42);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(60, 13);
            this.label43.TabIndex = 24;
            this.label43.Text = "Instance 6:";
            // 
            // AudioInstance5
            // 
            this.AudioInstance5.BackColor = System.Drawing.Color.White;
            this.AudioInstance5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance5.FormattingEnabled = true;
            this.AudioInstance5.Location = new System.Drawing.Point(321, 18);
            this.AudioInstance5.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance5.Name = "AudioInstance5";
            this.AudioInstance5.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance5.TabIndex = 23;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(257, 20);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(60, 13);
            this.label42.TabIndex = 22;
            this.label42.Text = "Instance 5:";
            // 
            // AudioInstance4
            // 
            this.AudioInstance4.BackColor = System.Drawing.Color.White;
            this.AudioInstance4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance4.FormattingEnabled = true;
            this.AudioInstance4.Location = new System.Drawing.Point(70, 84);
            this.AudioInstance4.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance4.Name = "AudioInstance4";
            this.AudioInstance4.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance4.TabIndex = 21;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(6, 86);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(60, 13);
            this.label41.TabIndex = 20;
            this.label41.Text = "Instance 4:";
            // 
            // AudioInstance3
            // 
            this.AudioInstance3.BackColor = System.Drawing.Color.White;
            this.AudioInstance3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance3.FormattingEnabled = true;
            this.AudioInstance3.Location = new System.Drawing.Point(70, 62);
            this.AudioInstance3.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance3.Name = "AudioInstance3";
            this.AudioInstance3.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance3.TabIndex = 19;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(6, 65);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(60, 13);
            this.label40.TabIndex = 18;
            this.label40.Text = "Instance 3:";
            // 
            // AudioInstance2
            // 
            this.AudioInstance2.BackColor = System.Drawing.Color.White;
            this.AudioInstance2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance2.FormattingEnabled = true;
            this.AudioInstance2.Location = new System.Drawing.Point(70, 40);
            this.AudioInstance2.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance2.Name = "AudioInstance2";
            this.AudioInstance2.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance2.TabIndex = 6;
            // 
            // AudioInstance1
            // 
            this.AudioInstance1.BackColor = System.Drawing.Color.White;
            this.AudioInstance1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInstance1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioInstance1.FormattingEnabled = true;
            this.AudioInstance1.Items.AddRange(new object[] {
            "Default"});
            this.AudioInstance1.Location = new System.Drawing.Point(70, 18);
            this.AudioInstance1.Margin = new System.Windows.Forms.Padding(2);
            this.AudioInstance1.Name = "AudioInstance1";
            this.AudioInstance1.Size = new System.Drawing.Size(170, 23);
            this.AudioInstance1.TabIndex = 1;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(6, 43);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(60, 13);
            this.label37.TabIndex = 5;
            this.label37.Text = "Instance 2:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(6, 21);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(60, 13);
            this.label36.TabIndex = 0;
            this.label36.Text = "Instance 1:";
            // 
            // audioCustomSettingsRadio
            // 
            this.audioCustomSettingsRadio.AutoSize = true;
            this.audioCustomSettingsRadio.BackColor = System.Drawing.Color.Transparent;
            this.audioCustomSettingsRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.audioCustomSettingsRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioCustomSettingsRadio.Location = new System.Drawing.Point(24, 94);
            this.audioCustomSettingsRadio.Margin = new System.Windows.Forms.Padding(2);
            this.audioCustomSettingsRadio.Name = "audioCustomSettingsRadio";
            this.audioCustomSettingsRadio.Size = new System.Drawing.Size(99, 17);
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
            this.audioDefaultSettingsRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioDefaultSettingsRadio.Location = new System.Drawing.Point(24, 74);
            this.audioDefaultSettingsRadio.Margin = new System.Windows.Forms.Padding(2);
            this.audioDefaultSettingsRadio.Name = "audioDefaultSettingsRadio";
            this.audioDefaultSettingsRadio.Size = new System.Drawing.Size(217, 17);
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
            // playersTab
            // 
            this.playersTab.BackColor = System.Drawing.Color.PaleGreen;
            this.playersTab.Controls.Add(this.btnNext);
            this.playersTab.Controls.Add(this.def_sid_comboBox);
            this.playersTab.Controls.Add(this.default_sid_list_label);
            this.playersTab.Controls.Add(this.page1);
            this.playersTab.Controls.Add(this.page2);
            this.playersTab.Location = new System.Drawing.Point(-1, 26);
            this.playersTab.Name = "playersTab";
            this.playersTab.Size = new System.Drawing.Size(671, 401);
            this.playersTab.TabIndex = 132;
            this.playersTab.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(18, 51);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 299;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // def_sid_comboBox
            // 
            this.def_sid_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.def_sid_comboBox.BackColor = System.Drawing.Color.White;
            this.def_sid_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.def_sid_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.def_sid_comboBox.ForeColor = System.Drawing.Color.Black;
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
            this.def_sid_comboBox.Location = new System.Drawing.Point(310, 328);
            this.def_sid_comboBox.Margin = new System.Windows.Forms.Padding(0);
            this.def_sid_comboBox.MaxLength = 30;
            this.def_sid_comboBox.Name = "def_sid_comboBox";
            this.def_sid_comboBox.Size = new System.Drawing.Size(175, 21);
            this.def_sid_comboBox.TabIndex = 296;
            // 
            // default_sid_list_label
            // 
            this.default_sid_list_label.AutoSize = true;
            this.default_sid_list_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.default_sid_list_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.default_sid_list_label.Location = new System.Drawing.Point(201, 330);
            this.default_sid_list_label.Margin = new System.Windows.Forms.Padding(0);
            this.default_sid_list_label.Name = "default_sid_list_label";
            this.default_sid_list_label.Size = new System.Drawing.Size(110, 15);
            this.default_sid_list_label.TabIndex = 295;
            this.default_sid_list_label.Text = "Default Steam Ids :";
            this.default_sid_list_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // page1
            // 
            this.page1.Controls.Add(this.label48);
            this.page1.Controls.Add(this.label47);
            this.page1.Controls.Add(this.label21);
            this.page1.Controls.Add(this.label25);
            this.page1.Controls.Add(this.label13);
            this.page1.Controls.Add(this.label8);
            this.page1.Controls.Add(this.steamid16);
            this.page1.Controls.Add(this.steamid15);
            this.page1.Controls.Add(this.steamid14);
            this.page1.Controls.Add(this.steamid13);
            this.page1.Controls.Add(this.steamid12);
            this.page1.Controls.Add(this.steamid11);
            this.page1.Controls.Add(this.steamid10);
            this.page1.Controls.Add(this.steamid9);
            this.page1.Controls.Add(this.steamid8);
            this.page1.Controls.Add(this.steamid7);
            this.page1.Controls.Add(this.steamid6);
            this.page1.Controls.Add(this.steamid5);
            this.page1.Controls.Add(this.steamid4);
            this.page1.Controls.Add(this.steamid3);
            this.page1.Controls.Add(this.steamid2);
            this.page1.Controls.Add(this.steamid1);
            this.page1.Controls.Add(this.player16N);
            this.page1.Controls.Add(this.label17);
            this.page1.Controls.Add(this.player15N);
            this.page1.Controls.Add(this.label18);
            this.page1.Controls.Add(this.player14N);
            this.page1.Controls.Add(this.label19);
            this.page1.Controls.Add(this.player13N);
            this.page1.Controls.Add(this.label20);
            this.page1.Controls.Add(this.player12N);
            this.page1.Controls.Add(this.label22);
            this.page1.Controls.Add(this.player11N);
            this.page1.Controls.Add(this.label23);
            this.page1.Controls.Add(this.player10N);
            this.page1.Controls.Add(this.label24);
            this.page1.Controls.Add(this.player9N);
            this.page1.Controls.Add(this.label26);
            this.page1.Controls.Add(this.player8N);
            this.page1.Controls.Add(this.label16);
            this.page1.Controls.Add(this.player7N);
            this.page1.Controls.Add(this.label15);
            this.page1.Controls.Add(this.player6N);
            this.page1.Controls.Add(this.label11);
            this.page1.Controls.Add(this.player5N);
            this.page1.Controls.Add(this.label9);
            this.page1.Controls.Add(this.player4N);
            this.page1.Controls.Add(this.label14);
            this.page1.Controls.Add(this.player3N);
            this.page1.Controls.Add(this.label12);
            this.page1.Controls.Add(this.player2N);
            this.page1.Controls.Add(this.label10);
            this.page1.Controls.Add(this.player1N);
            this.page1.Controls.Add(this.label7);
            this.page1.Location = new System.Drawing.Point(1, 81);
            this.page1.Name = "page1";
            this.page1.Size = new System.Drawing.Size(671, 210);
            this.page1.TabIndex = 298;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(536, 9);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(49, 13);
            this.label48.TabIndex = 296;
            this.label48.Text = "Steam Id";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(176, 9);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(49, 13);
            this.label47.TabIndex = 295;
            this.label47.Text = "Steam Id";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(366, 9);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 13);
            this.label21.TabIndex = 294;
            this.label21.Text = "Player Id";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(428, 9);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 13);
            this.label25.TabIndex = 293;
            this.label25.Text = "Nickname";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 292;
            this.label13.Text = " Player Id";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(68, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 291;
            this.label8.Text = "Nickname";
            // 
            // steamid16
            // 
            this.steamid16.BackColor = System.Drawing.Color.White;
            this.steamid16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid16.ForeColor = System.Drawing.Color.Black;
            this.steamid16.Location = new System.Drawing.Point(495, 186);
            this.steamid16.Margin = new System.Windows.Forms.Padding(0);
            this.steamid16.MaxLength = 17;
            this.steamid16.Name = "steamid16";
            this.steamid16.Size = new System.Drawing.Size(133, 21);
            this.steamid16.TabIndex = 290;
            // 
            // steamid15
            // 
            this.steamid15.BackColor = System.Drawing.Color.White;
            this.steamid15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid15.ForeColor = System.Drawing.Color.Black;
            this.steamid15.Location = new System.Drawing.Point(495, 164);
            this.steamid15.Margin = new System.Windows.Forms.Padding(0);
            this.steamid15.MaxLength = 17;
            this.steamid15.Name = "steamid15";
            this.steamid15.Size = new System.Drawing.Size(133, 21);
            this.steamid15.TabIndex = 289;
            // 
            // steamid14
            // 
            this.steamid14.BackColor = System.Drawing.Color.White;
            this.steamid14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid14.ForeColor = System.Drawing.Color.Black;
            this.steamid14.Location = new System.Drawing.Point(495, 142);
            this.steamid14.Margin = new System.Windows.Forms.Padding(0);
            this.steamid14.MaxLength = 17;
            this.steamid14.Name = "steamid14";
            this.steamid14.Size = new System.Drawing.Size(133, 21);
            this.steamid14.TabIndex = 288;
            // 
            // steamid13
            // 
            this.steamid13.BackColor = System.Drawing.Color.White;
            this.steamid13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid13.ForeColor = System.Drawing.Color.Black;
            this.steamid13.Location = new System.Drawing.Point(495, 120);
            this.steamid13.Margin = new System.Windows.Forms.Padding(0);
            this.steamid13.MaxLength = 17;
            this.steamid13.Name = "steamid13";
            this.steamid13.Size = new System.Drawing.Size(133, 21);
            this.steamid13.TabIndex = 287;
            // 
            // steamid12
            // 
            this.steamid12.BackColor = System.Drawing.Color.White;
            this.steamid12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid12.ForeColor = System.Drawing.Color.Black;
            this.steamid12.Location = new System.Drawing.Point(495, 98);
            this.steamid12.Margin = new System.Windows.Forms.Padding(0);
            this.steamid12.MaxLength = 17;
            this.steamid12.Name = "steamid12";
            this.steamid12.Size = new System.Drawing.Size(133, 21);
            this.steamid12.TabIndex = 286;
            // 
            // steamid11
            // 
            this.steamid11.BackColor = System.Drawing.Color.White;
            this.steamid11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid11.ForeColor = System.Drawing.Color.Black;
            this.steamid11.Location = new System.Drawing.Point(495, 76);
            this.steamid11.Margin = new System.Windows.Forms.Padding(0);
            this.steamid11.MaxLength = 17;
            this.steamid11.Name = "steamid11";
            this.steamid11.Size = new System.Drawing.Size(133, 21);
            this.steamid11.TabIndex = 285;
            // 
            // steamid10
            // 
            this.steamid10.BackColor = System.Drawing.Color.White;
            this.steamid10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid10.ForeColor = System.Drawing.Color.Black;
            this.steamid10.Location = new System.Drawing.Point(495, 54);
            this.steamid10.Margin = new System.Windows.Forms.Padding(0);
            this.steamid10.MaxLength = 17;
            this.steamid10.Name = "steamid10";
            this.steamid10.Size = new System.Drawing.Size(133, 21);
            this.steamid10.TabIndex = 284;
            // 
            // steamid9
            // 
            this.steamid9.BackColor = System.Drawing.Color.White;
            this.steamid9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid9.ForeColor = System.Drawing.Color.Black;
            this.steamid9.Location = new System.Drawing.Point(495, 32);
            this.steamid9.Margin = new System.Windows.Forms.Padding(0);
            this.steamid9.MaxLength = 17;
            this.steamid9.Name = "steamid9";
            this.steamid9.Size = new System.Drawing.Size(133, 21);
            this.steamid9.TabIndex = 283;
            // 
            // steamid8
            // 
            this.steamid8.BackColor = System.Drawing.Color.White;
            this.steamid8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid8.ForeColor = System.Drawing.Color.Black;
            this.steamid8.Location = new System.Drawing.Point(135, 186);
            this.steamid8.Margin = new System.Windows.Forms.Padding(0);
            this.steamid8.MaxLength = 17;
            this.steamid8.Name = "steamid8";
            this.steamid8.Size = new System.Drawing.Size(133, 21);
            this.steamid8.TabIndex = 282;
            // 
            // steamid7
            // 
            this.steamid7.BackColor = System.Drawing.Color.White;
            this.steamid7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid7.ForeColor = System.Drawing.Color.Black;
            this.steamid7.Location = new System.Drawing.Point(135, 164);
            this.steamid7.Margin = new System.Windows.Forms.Padding(0);
            this.steamid7.MaxLength = 17;
            this.steamid7.Name = "steamid7";
            this.steamid7.Size = new System.Drawing.Size(133, 21);
            this.steamid7.TabIndex = 281;
            // 
            // steamid6
            // 
            this.steamid6.BackColor = System.Drawing.Color.White;
            this.steamid6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid6.ForeColor = System.Drawing.Color.Black;
            this.steamid6.Location = new System.Drawing.Point(135, 142);
            this.steamid6.Margin = new System.Windows.Forms.Padding(0);
            this.steamid6.MaxLength = 17;
            this.steamid6.Name = "steamid6";
            this.steamid6.Size = new System.Drawing.Size(133, 21);
            this.steamid6.TabIndex = 280;
            // 
            // steamid5
            // 
            this.steamid5.BackColor = System.Drawing.Color.White;
            this.steamid5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid5.ForeColor = System.Drawing.Color.Black;
            this.steamid5.Location = new System.Drawing.Point(135, 120);
            this.steamid5.Margin = new System.Windows.Forms.Padding(0);
            this.steamid5.MaxLength = 17;
            this.steamid5.Name = "steamid5";
            this.steamid5.Size = new System.Drawing.Size(133, 21);
            this.steamid5.TabIndex = 279;
            // 
            // steamid4
            // 
            this.steamid4.BackColor = System.Drawing.Color.White;
            this.steamid4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid4.ForeColor = System.Drawing.Color.Black;
            this.steamid4.Location = new System.Drawing.Point(135, 98);
            this.steamid4.Margin = new System.Windows.Forms.Padding(0);
            this.steamid4.MaxLength = 17;
            this.steamid4.Name = "steamid4";
            this.steamid4.Size = new System.Drawing.Size(133, 21);
            this.steamid4.TabIndex = 278;
            // 
            // steamid3
            // 
            this.steamid3.BackColor = System.Drawing.Color.White;
            this.steamid3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid3.ForeColor = System.Drawing.Color.Black;
            this.steamid3.Location = new System.Drawing.Point(135, 76);
            this.steamid3.Margin = new System.Windows.Forms.Padding(0);
            this.steamid3.MaxLength = 17;
            this.steamid3.Name = "steamid3";
            this.steamid3.Size = new System.Drawing.Size(133, 21);
            this.steamid3.TabIndex = 277;
            // 
            // steamid2
            // 
            this.steamid2.BackColor = System.Drawing.Color.White;
            this.steamid2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid2.ForeColor = System.Drawing.Color.Black;
            this.steamid2.Location = new System.Drawing.Point(135, 54);
            this.steamid2.Margin = new System.Windows.Forms.Padding(0);
            this.steamid2.MaxLength = 17;
            this.steamid2.Name = "steamid2";
            this.steamid2.Size = new System.Drawing.Size(133, 21);
            this.steamid2.TabIndex = 276;
            // 
            // steamid1
            // 
            this.steamid1.BackColor = System.Drawing.Color.White;
            this.steamid1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid1.ForeColor = System.Drawing.Color.Black;
            this.steamid1.Location = new System.Drawing.Point(135, 32);
            this.steamid1.Margin = new System.Windows.Forms.Padding(0);
            this.steamid1.MaxLength = 17;
            this.steamid1.Name = "steamid1";
            this.steamid1.Size = new System.Drawing.Size(133, 21);
            this.steamid1.TabIndex = 275;
            // 
            // player16N
            // 
            this.player16N.BackColor = System.Drawing.Color.White;
            this.player16N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player16N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player16N.ForeColor = System.Drawing.Color.Black;
            this.player16N.Location = new System.Drawing.Point(419, 186);
            this.player16N.Margin = new System.Windows.Forms.Padding(0);
            this.player16N.MaxLength = 9;
            this.player16N.Name = "player16N";
            this.player16N.Size = new System.Drawing.Size(69, 21);
            this.player16N.TabIndex = 273;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(361, 189);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 274;
            this.label17.Text = "Player 16:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player15N
            // 
            this.player15N.BackColor = System.Drawing.Color.White;
            this.player15N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player15N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player15N.ForeColor = System.Drawing.Color.Black;
            this.player15N.Location = new System.Drawing.Point(419, 164);
            this.player15N.Margin = new System.Windows.Forms.Padding(0);
            this.player15N.MaxLength = 9;
            this.player15N.Name = "player15N";
            this.player15N.Size = new System.Drawing.Size(69, 21);
            this.player15N.TabIndex = 271;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(361, 168);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 272;
            this.label18.Text = "Player 15:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player14N
            // 
            this.player14N.BackColor = System.Drawing.Color.White;
            this.player14N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player14N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player14N.ForeColor = System.Drawing.Color.Black;
            this.player14N.Location = new System.Drawing.Point(419, 142);
            this.player14N.Margin = new System.Windows.Forms.Padding(0);
            this.player14N.MaxLength = 9;
            this.player14N.Name = "player14N";
            this.player14N.Size = new System.Drawing.Size(69, 21);
            this.player14N.TabIndex = 269;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(361, 145);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 13);
            this.label19.TabIndex = 270;
            this.label19.Text = "Player 14:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player13N
            // 
            this.player13N.BackColor = System.Drawing.Color.White;
            this.player13N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player13N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player13N.ForeColor = System.Drawing.Color.Black;
            this.player13N.Location = new System.Drawing.Point(419, 120);
            this.player13N.Margin = new System.Windows.Forms.Padding(0);
            this.player13N.MaxLength = 9;
            this.player13N.Name = "player13N";
            this.player13N.Size = new System.Drawing.Size(69, 21);
            this.player13N.TabIndex = 267;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(361, 124);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 13);
            this.label20.TabIndex = 268;
            this.label20.Text = "Player 13:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player12N
            // 
            this.player12N.BackColor = System.Drawing.Color.White;
            this.player12N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player12N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player12N.ForeColor = System.Drawing.Color.Black;
            this.player12N.Location = new System.Drawing.Point(419, 98);
            this.player12N.Margin = new System.Windows.Forms.Padding(0);
            this.player12N.MaxLength = 9;
            this.player12N.Name = "player12N";
            this.player12N.Size = new System.Drawing.Size(69, 21);
            this.player12N.TabIndex = 265;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(361, 101);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 266;
            this.label22.Text = "Player 12:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player11N
            // 
            this.player11N.BackColor = System.Drawing.Color.White;
            this.player11N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player11N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player11N.ForeColor = System.Drawing.Color.Black;
            this.player11N.Location = new System.Drawing.Point(419, 76);
            this.player11N.Margin = new System.Windows.Forms.Padding(0);
            this.player11N.MaxLength = 9;
            this.player11N.Name = "player11N";
            this.player11N.Size = new System.Drawing.Size(69, 21);
            this.player11N.TabIndex = 264;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(361, 79);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 13);
            this.label23.TabIndex = 263;
            this.label23.Text = "Player 11:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player10N
            // 
            this.player10N.BackColor = System.Drawing.Color.White;
            this.player10N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player10N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player10N.ForeColor = System.Drawing.Color.Black;
            this.player10N.Location = new System.Drawing.Point(419, 54);
            this.player10N.Margin = new System.Windows.Forms.Padding(0);
            this.player10N.MaxLength = 9;
            this.player10N.Name = "player10N";
            this.player10N.Size = new System.Drawing.Size(69, 21);
            this.player10N.TabIndex = 262;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(361, 57);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(54, 13);
            this.label24.TabIndex = 260;
            this.label24.Text = "Player 10:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player9N
            // 
            this.player9N.BackColor = System.Drawing.Color.White;
            this.player9N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player9N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player9N.ForeColor = System.Drawing.Color.Black;
            this.player9N.Location = new System.Drawing.Point(419, 32);
            this.player9N.Margin = new System.Windows.Forms.Padding(0);
            this.player9N.MaxLength = 9;
            this.player9N.Name = "player9N";
            this.player9N.Size = new System.Drawing.Size(69, 21);
            this.player9N.TabIndex = 261;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(367, 35);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 259;
            this.label26.Text = "Player 9:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player8N
            // 
            this.player8N.BackColor = System.Drawing.Color.White;
            this.player8N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player8N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player8N.ForeColor = System.Drawing.Color.Black;
            this.player8N.Location = new System.Drawing.Point(59, 186);
            this.player8N.Margin = new System.Windows.Forms.Padding(0);
            this.player8N.MaxLength = 9;
            this.player8N.Name = "player8N";
            this.player8N.Size = new System.Drawing.Size(69, 21);
            this.player8N.TabIndex = 257;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(7, 188);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 258;
            this.label16.Text = "Player 8:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player7N
            // 
            this.player7N.BackColor = System.Drawing.Color.White;
            this.player7N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player7N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player7N.ForeColor = System.Drawing.Color.Black;
            this.player7N.Location = new System.Drawing.Point(59, 164);
            this.player7N.Margin = new System.Windows.Forms.Padding(0);
            this.player7N.MaxLength = 9;
            this.player7N.Name = "player7N";
            this.player7N.Size = new System.Drawing.Size(69, 21);
            this.player7N.TabIndex = 255;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 167);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 256;
            this.label15.Text = "Player 7:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player6N
            // 
            this.player6N.BackColor = System.Drawing.Color.White;
            this.player6N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player6N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player6N.ForeColor = System.Drawing.Color.Black;
            this.player6N.Location = new System.Drawing.Point(59, 142);
            this.player6N.Margin = new System.Windows.Forms.Padding(0);
            this.player6N.MaxLength = 9;
            this.player6N.Name = "player6N";
            this.player6N.Size = new System.Drawing.Size(69, 21);
            this.player6N.TabIndex = 253;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 145);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 254;
            this.label11.Text = "Player 6:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player5N
            // 
            this.player5N.BackColor = System.Drawing.Color.White;
            this.player5N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player5N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player5N.ForeColor = System.Drawing.Color.Black;
            this.player5N.Location = new System.Drawing.Point(59, 120);
            this.player5N.Margin = new System.Windows.Forms.Padding(0);
            this.player5N.MaxLength = 9;
            this.player5N.Name = "player5N";
            this.player5N.Size = new System.Drawing.Size(69, 21);
            this.player5N.TabIndex = 251;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 252;
            this.label9.Text = "Player 5:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player4N
            // 
            this.player4N.BackColor = System.Drawing.Color.White;
            this.player4N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player4N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player4N.ForeColor = System.Drawing.Color.Black;
            this.player4N.Location = new System.Drawing.Point(59, 98);
            this.player4N.Margin = new System.Windows.Forms.Padding(0);
            this.player4N.MaxLength = 9;
            this.player4N.Name = "player4N";
            this.player4N.Size = new System.Drawing.Size(69, 21);
            this.player4N.TabIndex = 249;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 101);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 250;
            this.label14.Text = "Player 4:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player3N
            // 
            this.player3N.BackColor = System.Drawing.Color.White;
            this.player3N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player3N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player3N.ForeColor = System.Drawing.Color.Black;
            this.player3N.Location = new System.Drawing.Point(59, 76);
            this.player3N.Margin = new System.Windows.Forms.Padding(0);
            this.player3N.MaxLength = 9;
            this.player3N.Name = "player3N";
            this.player3N.Size = new System.Drawing.Size(69, 21);
            this.player3N.TabIndex = 248;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 79);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 247;
            this.label12.Text = "Player 3:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player2N
            // 
            this.player2N.BackColor = System.Drawing.Color.White;
            this.player2N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player2N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2N.ForeColor = System.Drawing.Color.Black;
            this.player2N.Location = new System.Drawing.Point(59, 54);
            this.player2N.Margin = new System.Windows.Forms.Padding(0);
            this.player2N.MaxLength = 9;
            this.player2N.Name = "player2N";
            this.player2N.Size = new System.Drawing.Size(69, 21);
            this.player2N.TabIndex = 246;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 57);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 244;
            this.label10.Text = "Player 2:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player1N
            // 
            this.player1N.BackColor = System.Drawing.Color.White;
            this.player1N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player1N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1N.ForeColor = System.Drawing.Color.Black;
            this.player1N.Location = new System.Drawing.Point(59, 32);
            this.player1N.Margin = new System.Windows.Forms.Padding(0);
            this.player1N.MaxLength = 9;
            this.player1N.Name = "player1N";
            this.player1N.Size = new System.Drawing.Size(69, 21);
            this.player1N.TabIndex = 245;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 35);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 243;
            this.label7.Text = "Player 1:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // page2
            // 
            this.page2.Controls.Add(this.label31);
            this.page2.Controls.Add(this.label33);
            this.page2.Controls.Add(this.label34);
            this.page2.Controls.Add(this.label35);
            this.page2.Controls.Add(this.label38);
            this.page2.Controls.Add(this.label46);
            this.page2.Controls.Add(this.steamid32);
            this.page2.Controls.Add(this.steamid17);
            this.page2.Controls.Add(this.steamid31);
            this.page2.Controls.Add(this.label61);
            this.page2.Controls.Add(this.steamid30);
            this.page2.Controls.Add(this.player17N);
            this.page2.Controls.Add(this.steamid29);
            this.page2.Controls.Add(this.label59);
            this.page2.Controls.Add(this.steamid28);
            this.page2.Controls.Add(this.player18N);
            this.page2.Controls.Add(this.steamid27);
            this.page2.Controls.Add(this.label58);
            this.page2.Controls.Add(this.steamid26);
            this.page2.Controls.Add(this.player19N);
            this.page2.Controls.Add(this.steamid25);
            this.page2.Controls.Add(this.label57);
            this.page2.Controls.Add(this.steamid24);
            this.page2.Controls.Add(this.player20N);
            this.page2.Controls.Add(this.steamid23);
            this.page2.Controls.Add(this.label55);
            this.page2.Controls.Add(this.steamid22);
            this.page2.Controls.Add(this.player21N);
            this.page2.Controls.Add(this.steamid21);
            this.page2.Controls.Add(this.label54);
            this.page2.Controls.Add(this.steamid20);
            this.page2.Controls.Add(this.player22N);
            this.page2.Controls.Add(this.steamid19);
            this.page2.Controls.Add(this.label53);
            this.page2.Controls.Add(this.steamid18);
            this.page2.Controls.Add(this.player23N);
            this.page2.Controls.Add(this.label52);
            this.page2.Controls.Add(this.player32N);
            this.page2.Controls.Add(this.player24N);
            this.page2.Controls.Add(this.label50);
            this.page2.Controls.Add(this.label51);
            this.page2.Controls.Add(this.player31N);
            this.page2.Controls.Add(this.player25N);
            this.page2.Controls.Add(this.label73);
            this.page2.Controls.Add(this.label74);
            this.page2.Controls.Add(this.player30N);
            this.page2.Controls.Add(this.player26N);
            this.page2.Controls.Add(this.label75);
            this.page2.Controls.Add(this.label88);
            this.page2.Controls.Add(this.player29N);
            this.page2.Controls.Add(this.player27N);
            this.page2.Controls.Add(this.label92);
            this.page2.Controls.Add(this.label94);
            this.page2.Controls.Add(this.player28N);
            this.page2.Location = new System.Drawing.Point(1, 80);
            this.page2.Name = "page2";
            this.page2.Size = new System.Drawing.Size(671, 210);
            this.page2.TabIndex = 297;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(536, 9);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(49, 13);
            this.label31.TabIndex = 355;
            this.label31.Text = "Steam Id";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(176, 9);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(49, 13);
            this.label33.TabIndex = 354;
            this.label33.Text = "Steam Id";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(366, 9);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(48, 13);
            this.label34.TabIndex = 353;
            this.label34.Text = "Player Id";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(428, 9);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(55, 13);
            this.label35.TabIndex = 352;
            this.label35.Text = "Nickname";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(4, 9);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(51, 13);
            this.label38.TabIndex = 351;
            this.label38.Text = " Player Id";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(68, 9);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(55, 13);
            this.label46.TabIndex = 350;
            this.label46.Text = "Nickname";
            // 
            // steamid32
            // 
            this.steamid32.BackColor = System.Drawing.Color.White;
            this.steamid32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid32.ForeColor = System.Drawing.Color.Black;
            this.steamid32.Location = new System.Drawing.Point(495, 186);
            this.steamid32.Margin = new System.Windows.Forms.Padding(0);
            this.steamid32.MaxLength = 17;
            this.steamid32.Name = "steamid32";
            this.steamid32.Size = new System.Drawing.Size(133, 21);
            this.steamid32.TabIndex = 349;
            // 
            // steamid17
            // 
            this.steamid17.BackColor = System.Drawing.Color.White;
            this.steamid17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid17.ForeColor = System.Drawing.Color.Black;
            this.steamid17.Location = new System.Drawing.Point(135, 32);
            this.steamid17.Margin = new System.Windows.Forms.Padding(0);
            this.steamid17.MaxLength = 17;
            this.steamid17.Name = "steamid17";
            this.steamid17.Size = new System.Drawing.Size(133, 21);
            this.steamid17.TabIndex = 334;
            // 
            // steamid31
            // 
            this.steamid31.BackColor = System.Drawing.Color.White;
            this.steamid31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid31.ForeColor = System.Drawing.Color.Black;
            this.steamid31.Location = new System.Drawing.Point(495, 164);
            this.steamid31.Margin = new System.Windows.Forms.Padding(0);
            this.steamid31.MaxLength = 17;
            this.steamid31.Name = "steamid31";
            this.steamid31.Size = new System.Drawing.Size(133, 21);
            this.steamid31.TabIndex = 348;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(1, 35);
            this.label61.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(54, 13);
            this.label61.TabIndex = 302;
            this.label61.Text = "Player 17:";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid30
            // 
            this.steamid30.BackColor = System.Drawing.Color.White;
            this.steamid30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid30.ForeColor = System.Drawing.Color.Black;
            this.steamid30.Location = new System.Drawing.Point(495, 142);
            this.steamid30.Margin = new System.Windows.Forms.Padding(0);
            this.steamid30.MaxLength = 17;
            this.steamid30.Name = "steamid30";
            this.steamid30.Size = new System.Drawing.Size(133, 21);
            this.steamid30.TabIndex = 347;
            // 
            // player17N
            // 
            this.player17N.BackColor = System.Drawing.Color.White;
            this.player17N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player17N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player17N.ForeColor = System.Drawing.Color.Black;
            this.player17N.Location = new System.Drawing.Point(59, 32);
            this.player17N.Margin = new System.Windows.Forms.Padding(0);
            this.player17N.MaxLength = 9;
            this.player17N.Name = "player17N";
            this.player17N.Size = new System.Drawing.Size(69, 21);
            this.player17N.TabIndex = 304;
            // 
            // steamid29
            // 
            this.steamid29.BackColor = System.Drawing.Color.White;
            this.steamid29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid29.ForeColor = System.Drawing.Color.Black;
            this.steamid29.Location = new System.Drawing.Point(495, 120);
            this.steamid29.Margin = new System.Windows.Forms.Padding(0);
            this.steamid29.MaxLength = 17;
            this.steamid29.Name = "steamid29";
            this.steamid29.Size = new System.Drawing.Size(133, 21);
            this.steamid29.TabIndex = 346;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(1, 57);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(54, 13);
            this.label59.TabIndex = 303;
            this.label59.Text = "Player 18:";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid28
            // 
            this.steamid28.BackColor = System.Drawing.Color.White;
            this.steamid28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid28.ForeColor = System.Drawing.Color.Black;
            this.steamid28.Location = new System.Drawing.Point(495, 98);
            this.steamid28.Margin = new System.Windows.Forms.Padding(0);
            this.steamid28.MaxLength = 17;
            this.steamid28.Name = "steamid28";
            this.steamid28.Size = new System.Drawing.Size(133, 21);
            this.steamid28.TabIndex = 345;
            // 
            // player18N
            // 
            this.player18N.BackColor = System.Drawing.Color.White;
            this.player18N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player18N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player18N.ForeColor = System.Drawing.Color.Black;
            this.player18N.Location = new System.Drawing.Point(59, 54);
            this.player18N.Margin = new System.Windows.Forms.Padding(0);
            this.player18N.MaxLength = 9;
            this.player18N.Name = "player18N";
            this.player18N.Size = new System.Drawing.Size(69, 21);
            this.player18N.TabIndex = 305;
            // 
            // steamid27
            // 
            this.steamid27.BackColor = System.Drawing.Color.White;
            this.steamid27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid27.ForeColor = System.Drawing.Color.Black;
            this.steamid27.Location = new System.Drawing.Point(495, 76);
            this.steamid27.Margin = new System.Windows.Forms.Padding(0);
            this.steamid27.MaxLength = 17;
            this.steamid27.Name = "steamid27";
            this.steamid27.Size = new System.Drawing.Size(133, 21);
            this.steamid27.TabIndex = 344;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(1, 79);
            this.label58.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(54, 13);
            this.label58.TabIndex = 306;
            this.label58.Text = "Player 19:";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid26
            // 
            this.steamid26.BackColor = System.Drawing.Color.White;
            this.steamid26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid26.ForeColor = System.Drawing.Color.Black;
            this.steamid26.Location = new System.Drawing.Point(495, 54);
            this.steamid26.Margin = new System.Windows.Forms.Padding(0);
            this.steamid26.MaxLength = 17;
            this.steamid26.Name = "steamid26";
            this.steamid26.Size = new System.Drawing.Size(133, 21);
            this.steamid26.TabIndex = 343;
            // 
            // player19N
            // 
            this.player19N.BackColor = System.Drawing.Color.White;
            this.player19N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player19N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player19N.ForeColor = System.Drawing.Color.Black;
            this.player19N.Location = new System.Drawing.Point(59, 76);
            this.player19N.Margin = new System.Windows.Forms.Padding(0);
            this.player19N.MaxLength = 9;
            this.player19N.Name = "player19N";
            this.player19N.Size = new System.Drawing.Size(69, 21);
            this.player19N.TabIndex = 307;
            // 
            // steamid25
            // 
            this.steamid25.BackColor = System.Drawing.Color.White;
            this.steamid25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid25.ForeColor = System.Drawing.Color.Black;
            this.steamid25.Location = new System.Drawing.Point(495, 32);
            this.steamid25.Margin = new System.Windows.Forms.Padding(0);
            this.steamid25.MaxLength = 17;
            this.steamid25.Name = "steamid25";
            this.steamid25.Size = new System.Drawing.Size(133, 21);
            this.steamid25.TabIndex = 342;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(1, 101);
            this.label57.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(54, 13);
            this.label57.TabIndex = 309;
            this.label57.Text = "Player 20:";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid24
            // 
            this.steamid24.BackColor = System.Drawing.Color.White;
            this.steamid24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid24.ForeColor = System.Drawing.Color.Black;
            this.steamid24.Location = new System.Drawing.Point(135, 186);
            this.steamid24.Margin = new System.Windows.Forms.Padding(0);
            this.steamid24.MaxLength = 17;
            this.steamid24.Name = "steamid24";
            this.steamid24.Size = new System.Drawing.Size(133, 21);
            this.steamid24.TabIndex = 341;
            // 
            // player20N
            // 
            this.player20N.BackColor = System.Drawing.Color.White;
            this.player20N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player20N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player20N.ForeColor = System.Drawing.Color.Black;
            this.player20N.Location = new System.Drawing.Point(59, 98);
            this.player20N.Margin = new System.Windows.Forms.Padding(0);
            this.player20N.MaxLength = 9;
            this.player20N.Name = "player20N";
            this.player20N.Size = new System.Drawing.Size(69, 21);
            this.player20N.TabIndex = 308;
            // 
            // steamid23
            // 
            this.steamid23.BackColor = System.Drawing.Color.White;
            this.steamid23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid23.ForeColor = System.Drawing.Color.Black;
            this.steamid23.Location = new System.Drawing.Point(135, 164);
            this.steamid23.Margin = new System.Windows.Forms.Padding(0);
            this.steamid23.MaxLength = 17;
            this.steamid23.Name = "steamid23";
            this.steamid23.Size = new System.Drawing.Size(133, 21);
            this.steamid23.TabIndex = 340;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(1, 122);
            this.label55.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(54, 13);
            this.label55.TabIndex = 311;
            this.label55.Text = "Player 21:";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid22
            // 
            this.steamid22.BackColor = System.Drawing.Color.White;
            this.steamid22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid22.ForeColor = System.Drawing.Color.Black;
            this.steamid22.Location = new System.Drawing.Point(135, 142);
            this.steamid22.Margin = new System.Windows.Forms.Padding(0);
            this.steamid22.MaxLength = 17;
            this.steamid22.Name = "steamid22";
            this.steamid22.Size = new System.Drawing.Size(133, 21);
            this.steamid22.TabIndex = 339;
            // 
            // player21N
            // 
            this.player21N.BackColor = System.Drawing.Color.White;
            this.player21N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player21N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player21N.ForeColor = System.Drawing.Color.Black;
            this.player21N.Location = new System.Drawing.Point(59, 120);
            this.player21N.Margin = new System.Windows.Forms.Padding(0);
            this.player21N.MaxLength = 9;
            this.player21N.Name = "player21N";
            this.player21N.Size = new System.Drawing.Size(69, 21);
            this.player21N.TabIndex = 310;
            // 
            // steamid21
            // 
            this.steamid21.BackColor = System.Drawing.Color.White;
            this.steamid21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid21.ForeColor = System.Drawing.Color.Black;
            this.steamid21.Location = new System.Drawing.Point(135, 120);
            this.steamid21.Margin = new System.Windows.Forms.Padding(0);
            this.steamid21.MaxLength = 17;
            this.steamid21.Name = "steamid21";
            this.steamid21.Size = new System.Drawing.Size(133, 21);
            this.steamid21.TabIndex = 338;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(1, 145);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(54, 13);
            this.label54.TabIndex = 313;
            this.label54.Text = "Player 22:";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid20
            // 
            this.steamid20.BackColor = System.Drawing.Color.White;
            this.steamid20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid20.ForeColor = System.Drawing.Color.Black;
            this.steamid20.Location = new System.Drawing.Point(135, 98);
            this.steamid20.Margin = new System.Windows.Forms.Padding(0);
            this.steamid20.MaxLength = 17;
            this.steamid20.Name = "steamid20";
            this.steamid20.Size = new System.Drawing.Size(133, 21);
            this.steamid20.TabIndex = 337;
            // 
            // player22N
            // 
            this.player22N.BackColor = System.Drawing.Color.White;
            this.player22N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player22N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player22N.ForeColor = System.Drawing.Color.Black;
            this.player22N.Location = new System.Drawing.Point(59, 142);
            this.player22N.Margin = new System.Windows.Forms.Padding(0);
            this.player22N.MaxLength = 9;
            this.player22N.Name = "player22N";
            this.player22N.Size = new System.Drawing.Size(69, 21);
            this.player22N.TabIndex = 312;
            // 
            // steamid19
            // 
            this.steamid19.BackColor = System.Drawing.Color.White;
            this.steamid19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid19.ForeColor = System.Drawing.Color.Black;
            this.steamid19.Location = new System.Drawing.Point(135, 76);
            this.steamid19.Margin = new System.Windows.Forms.Padding(0);
            this.steamid19.MaxLength = 17;
            this.steamid19.Name = "steamid19";
            this.steamid19.Size = new System.Drawing.Size(133, 21);
            this.steamid19.TabIndex = 336;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(1, 166);
            this.label53.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(54, 13);
            this.label53.TabIndex = 315;
            this.label53.Text = "Player 23:";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamid18
            // 
            this.steamid18.BackColor = System.Drawing.Color.White;
            this.steamid18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.steamid18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamid18.ForeColor = System.Drawing.Color.Black;
            this.steamid18.Location = new System.Drawing.Point(135, 54);
            this.steamid18.Margin = new System.Windows.Forms.Padding(0);
            this.steamid18.MaxLength = 17;
            this.steamid18.Name = "steamid18";
            this.steamid18.Size = new System.Drawing.Size(133, 21);
            this.steamid18.TabIndex = 335;
            // 
            // player23N
            // 
            this.player23N.BackColor = System.Drawing.Color.White;
            this.player23N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player23N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player23N.ForeColor = System.Drawing.Color.Black;
            this.player23N.Location = new System.Drawing.Point(59, 164);
            this.player23N.Margin = new System.Windows.Forms.Padding(0);
            this.player23N.MaxLength = 9;
            this.player23N.Name = "player23N";
            this.player23N.Size = new System.Drawing.Size(69, 21);
            this.player23N.TabIndex = 314;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(1, 187);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(54, 13);
            this.label52.TabIndex = 317;
            this.label52.Text = "Player 24:";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player32N
            // 
            this.player32N.BackColor = System.Drawing.Color.White;
            this.player32N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player32N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player32N.ForeColor = System.Drawing.Color.Black;
            this.player32N.Location = new System.Drawing.Point(419, 186);
            this.player32N.Margin = new System.Windows.Forms.Padding(0);
            this.player32N.MaxLength = 9;
            this.player32N.Name = "player32N";
            this.player32N.Size = new System.Drawing.Size(69, 21);
            this.player32N.TabIndex = 332;
            // 
            // player24N
            // 
            this.player24N.BackColor = System.Drawing.Color.White;
            this.player24N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player24N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player24N.ForeColor = System.Drawing.Color.Black;
            this.player24N.Location = new System.Drawing.Point(59, 186);
            this.player24N.Margin = new System.Windows.Forms.Padding(0);
            this.player24N.MaxLength = 9;
            this.player24N.Name = "player24N";
            this.player24N.Size = new System.Drawing.Size(69, 21);
            this.player24N.TabIndex = 316;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(361, 187);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(54, 13);
            this.label50.TabIndex = 333;
            this.label50.Text = "Player 32:";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(361, 35);
            this.label51.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(54, 13);
            this.label51.TabIndex = 318;
            this.label51.Text = "Player 25:";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player31N
            // 
            this.player31N.BackColor = System.Drawing.Color.White;
            this.player31N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player31N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player31N.ForeColor = System.Drawing.Color.Black;
            this.player31N.Location = new System.Drawing.Point(419, 164);
            this.player31N.Margin = new System.Windows.Forms.Padding(0);
            this.player31N.MaxLength = 9;
            this.player31N.Name = "player31N";
            this.player31N.Size = new System.Drawing.Size(69, 21);
            this.player31N.TabIndex = 330;
            // 
            // player25N
            // 
            this.player25N.BackColor = System.Drawing.Color.White;
            this.player25N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player25N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player25N.ForeColor = System.Drawing.Color.Black;
            this.player25N.Location = new System.Drawing.Point(419, 32);
            this.player25N.Margin = new System.Windows.Forms.Padding(0);
            this.player25N.MaxLength = 9;
            this.player25N.Name = "player25N";
            this.player25N.Size = new System.Drawing.Size(69, 21);
            this.player25N.TabIndex = 320;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(361, 166);
            this.label73.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(54, 13);
            this.label73.TabIndex = 331;
            this.label73.Text = "Player 31:";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(361, 57);
            this.label74.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(54, 13);
            this.label74.TabIndex = 319;
            this.label74.Text = "Player 26:";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player30N
            // 
            this.player30N.BackColor = System.Drawing.Color.White;
            this.player30N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player30N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player30N.ForeColor = System.Drawing.Color.Black;
            this.player30N.Location = new System.Drawing.Point(419, 142);
            this.player30N.Margin = new System.Windows.Forms.Padding(0);
            this.player30N.MaxLength = 9;
            this.player30N.Name = "player30N";
            this.player30N.Size = new System.Drawing.Size(69, 21);
            this.player30N.TabIndex = 328;
            // 
            // player26N
            // 
            this.player26N.BackColor = System.Drawing.Color.White;
            this.player26N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player26N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player26N.ForeColor = System.Drawing.Color.Black;
            this.player26N.Location = new System.Drawing.Point(419, 54);
            this.player26N.Margin = new System.Windows.Forms.Padding(0);
            this.player26N.MaxLength = 9;
            this.player26N.Name = "player26N";
            this.player26N.Size = new System.Drawing.Size(69, 21);
            this.player26N.TabIndex = 321;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(361, 145);
            this.label75.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(54, 13);
            this.label75.TabIndex = 329;
            this.label75.Text = "Player 30:";
            this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(361, 79);
            this.label88.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(54, 13);
            this.label88.TabIndex = 322;
            this.label88.Text = "Player 27:";
            this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player29N
            // 
            this.player29N.BackColor = System.Drawing.Color.White;
            this.player29N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player29N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player29N.ForeColor = System.Drawing.Color.Black;
            this.player29N.Location = new System.Drawing.Point(419, 120);
            this.player29N.Margin = new System.Windows.Forms.Padding(0);
            this.player29N.MaxLength = 9;
            this.player29N.Name = "player29N";
            this.player29N.Size = new System.Drawing.Size(69, 21);
            this.player29N.TabIndex = 326;
            // 
            // player27N
            // 
            this.player27N.BackColor = System.Drawing.Color.White;
            this.player27N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player27N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player27N.ForeColor = System.Drawing.Color.Black;
            this.player27N.Location = new System.Drawing.Point(419, 76);
            this.player27N.Margin = new System.Windows.Forms.Padding(0);
            this.player27N.MaxLength = 9;
            this.player27N.Name = "player27N";
            this.player27N.Size = new System.Drawing.Size(69, 21);
            this.player27N.TabIndex = 323;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(361, 123);
            this.label92.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(54, 13);
            this.label92.TabIndex = 327;
            this.label92.Text = "Player 29:";
            this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(361, 101);
            this.label94.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(54, 13);
            this.label94.TabIndex = 325;
            this.label94.Text = "Player 28:";
            this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // player28N
            // 
            this.player28N.BackColor = System.Drawing.Color.White;
            this.player28N.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player28N.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player28N.ForeColor = System.Drawing.Color.Black;
            this.player28N.Location = new System.Drawing.Point(419, 98);
            this.player28N.Margin = new System.Windows.Forms.Padding(0);
            this.player28N.MaxLength = 9;
            this.player28N.Name = "player28N";
            this.player28N.Size = new System.Drawing.Size(69, 21);
            this.player28N.TabIndex = 324;
            // 
            // layoutTab
            // 
            this.layoutTab.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.layoutTab.Controls.Add(this.label71);
            this.layoutTab.Controls.Add(this.panel1);
            this.layoutTab.Controls.Add(this.SplitColors);
            this.layoutTab.Controls.Add(this.numMaxPlyrs);
            this.layoutTab.Controls.Add(this.numUpDownVer);
            this.layoutTab.Controls.Add(this.SplitDiv);
            this.layoutTab.Controls.Add(this.label49);
            this.layoutTab.Controls.Add(this.layoutSizer);
            this.layoutTab.Controls.Add(this.label29);
            this.layoutTab.Controls.Add(this.label27);
            this.layoutTab.Controls.Add(this.label28);
            this.layoutTab.Controls.Add(this.numUpDownHor);
            this.layoutTab.Location = new System.Drawing.Point(2, 28);
            this.layoutTab.Name = "layoutTab";
            this.layoutTab.Size = new System.Drawing.Size(671, 401);
            this.layoutTab.TabIndex = 304;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(13, 193);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(128, 13);
            this.label71.TabIndex = 155;
            this.label71.Text = "Cutscenes Mode Settings";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cts_unfocus);
            this.panel1.Controls.Add(this.cts_kar);
            this.panel1.Controls.Add(this.cts_Mute);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 82);
            this.panel1.TabIndex = 154;
            // 
            // cts_unfocus
            // 
            this.cts_unfocus.AutoSize = true;
            this.cts_unfocus.Location = new System.Drawing.Point(6, 54);
            this.cts_unfocus.Name = "cts_unfocus";
            this.cts_unfocus.Size = new System.Drawing.Size(138, 17);
            this.cts_unfocus.TabIndex = 142;
            this.cts_unfocus.Text = "Auto Unfocus Windows";
            this.cts_unfocus.UseVisualStyleBackColor = true;
            // 
            // cts_kar
            // 
            this.cts_kar.AutoSize = true;
            this.cts_kar.Location = new System.Drawing.Point(6, 29);
            this.cts_kar.Name = "cts_kar";
            this.cts_kar.Size = new System.Drawing.Size(154, 17);
            this.cts_kar.TabIndex = 141;
            this.cts_kar.Text = "Keep Original Window Size";
            this.cts_kar.UseVisualStyleBackColor = true;
            // 
            // cts_Mute
            // 
            this.cts_Mute.AutoSize = true;
            this.cts_Mute.Location = new System.Drawing.Point(6, 4);
            this.cts_Mute.Name = "cts_Mute";
            this.cts_Mute.Size = new System.Drawing.Size(104, 17);
            this.cts_Mute.TabIndex = 140;
            this.cts_Mute.Text = "Mute Audio Only";
            this.cts_Mute.UseVisualStyleBackColor = true;
            this.cts_Mute.CheckedChanged += new System.EventHandler(this.cts_Mute_CheckedChanged);
            // 
            // SplitColors
            // 
            this.SplitColors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SplitColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SplitColors.Location = new System.Drawing.Point(15, 155);
            this.SplitColors.Margin = new System.Windows.Forms.Padding(0);
            this.SplitColors.Name = "SplitColors";
            this.SplitColors.Size = new System.Drawing.Size(119, 24);
            this.SplitColors.TabIndex = 153;
            // 
            // numMaxPlyrs
            // 
            this.numMaxPlyrs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxPlyrs.BackColor = System.Drawing.Color.Transparent;
            this.numMaxPlyrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMaxPlyrs.Location = new System.Drawing.Point(320, 245);
            this.numMaxPlyrs.Margin = new System.Windows.Forms.Padding(0);
            this.numMaxPlyrs.Name = "numMaxPlyrs";
            this.numMaxPlyrs.Size = new System.Drawing.Size(48, 20);
            this.numMaxPlyrs.TabIndex = 152;
            this.numMaxPlyrs.Value = 0;
            // 
            // numUpDownVer
            // 
            this.numUpDownVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownVer.BackColor = System.Drawing.Color.Transparent;
            this.numUpDownVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownVer.Location = new System.Drawing.Point(318, 147);
            this.numUpDownVer.Margin = new System.Windows.Forms.Padding(0);
            this.numUpDownVer.Name = "numUpDownVer";
            this.numUpDownVer.Size = new System.Drawing.Size(48, 20);
            this.numUpDownVer.TabIndex = 150;
            this.numUpDownVer.Value = 0;
            // 
            // SplitDiv
            // 
            this.SplitDiv.AutoSize = true;
            this.SplitDiv.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SplitDiv.Location = new System.Drawing.Point(16, 107);
            this.SplitDiv.Margin = new System.Windows.Forms.Padding(0);
            this.SplitDiv.Name = "SplitDiv";
            this.SplitDiv.Size = new System.Drawing.Size(118, 17);
            this.SplitDiv.TabIndex = 149;
            this.SplitDiv.Text = "Splitscreen Division";
            this.SplitDiv.UseVisualStyleBackColor = true;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(13, 133);
            this.label49.Margin = new System.Windows.Forms.Padding(0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(105, 15);
            this.label49.TabIndex = 148;
            this.label49.Text = "Background Color";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layoutSizer
            // 
            this.layoutSizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutSizer.BackColor = System.Drawing.Color.Transparent;
            this.layoutSizer.Location = new System.Drawing.Point(413, 113);
            this.layoutSizer.Margin = new System.Windows.Forms.Padding(0);
            this.layoutSizer.MaximumSize = new System.Drawing.Size(245, 170);
            this.layoutSizer.MinimumSize = new System.Drawing.Size(245, 170);
            this.layoutSizer.Name = "layoutSizer";
            this.layoutSizer.Size = new System.Drawing.Size(245, 170);
            this.layoutSizer.TabIndex = 147;
            this.layoutSizer.Paint += new System.Windows.Forms.PaintEventHandler(this.layoutSizer_Paint);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label29.Location = new System.Drawing.Point(310, 222);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(64, 13);
            this.label29.TabIndex = 146;
            this.label29.Text = "Max Players";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label27.Location = new System.Drawing.Point(310, 175);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(99, 13);
            this.label27.TabIndex = 144;
            this.label27.Text = "Horizontal Divisions";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label28.Location = new System.Drawing.Point(307, 128);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(90, 13);
            this.label28.TabIndex = 145;
            this.label28.Text = " Vertical Divisions";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numUpDownHor
            // 
            this.numUpDownHor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDownHor.BackColor = System.Drawing.Color.Transparent;
            this.numUpDownHor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownHor.Location = new System.Drawing.Point(319, 194);
            this.numUpDownHor.Margin = new System.Windows.Forms.Padding(0);
            this.numUpDownHor.Name = "numUpDownHor";
            this.numUpDownHor.Size = new System.Drawing.Size(48, 20);
            this.numUpDownHor.TabIndex = 151;
            this.numUpDownHor.Value = 0;
            // 
            // sharedTab
            // 
            this.sharedTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sharedTab.Controls.Add(this.scaleOptionCbx);
            this.sharedTab.Controls.Add(this.useNicksCheck);
            this.sharedTab.Controls.Add(this.label72);
            this.sharedTab.Controls.Add(this.notes_text);
            this.sharedTab.Controls.Add(this.cmb_Network);
            this.sharedTab.Controls.Add(this.autoPlay);
            this.sharedTab.Controls.Add(this.label6);
            this.sharedTab.Controls.Add(this.label2);
            this.sharedTab.Controls.Add(this.WIndowsSetupTiming_Label);
            this.sharedTab.Controls.Add(this.WindowsSetupTiming_TextBox);
            this.sharedTab.Controls.Add(this.pauseBetweenInstanceLaunch_TxtBox);
            this.sharedTab.Controls.Add(this.pauseBetweenInstanceLauch_Label);
            this.sharedTab.Controls.Add(this.label32);
            this.sharedTab.Location = new System.Drawing.Point(1, 33);
            this.sharedTab.Name = "sharedTab";
            this.sharedTab.Size = new System.Drawing.Size(671, 401);
            this.sharedTab.TabIndex = 42;
            // 
            // scaleOptionCbx
            // 
            this.scaleOptionCbx.AutoSize = true;
            this.scaleOptionCbx.BackColor = System.Drawing.Color.Transparent;
            this.scaleOptionCbx.Checked = true;
            this.scaleOptionCbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleOptionCbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleOptionCbx.Location = new System.Drawing.Point(38, 104);
            this.scaleOptionCbx.Margin = new System.Windows.Forms.Padding(2);
            this.scaleOptionCbx.Name = "scaleOptionCbx";
            this.scaleOptionCbx.Size = new System.Drawing.Size(175, 17);
            this.scaleOptionCbx.TabIndex = 148;
            this.scaleOptionCbx.Text = "Auto set desktop scale to 100%";
            this.scaleOptionCbx.UseVisualStyleBackColor = false;
            // 
            // useNicksCheck
            // 
            this.useNicksCheck.AutoSize = true;
            this.useNicksCheck.BackColor = System.Drawing.Color.Transparent;
            this.useNicksCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useNicksCheck.Location = new System.Drawing.Point(38, 80);
            this.useNicksCheck.Margin = new System.Windows.Forms.Padding(2);
            this.useNicksCheck.Name = "useNicksCheck";
            this.useNicksCheck.Size = new System.Drawing.Size(144, 17);
            this.useNicksCheck.TabIndex = 146;
            this.useNicksCheck.Text = "Use Nicknames In-Game";
            this.useNicksCheck.UseVisualStyleBackColor = false;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(316, 83);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(60, 13);
            this.label72.TabIndex = 145;
            this.label72.Text = "User Notes";
            // 
            // notes_text
            // 
            this.notes_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.notes_text.BackColor = System.Drawing.SystemColors.InfoText;
            this.notes_text.ForeColor = System.Drawing.Color.White;
            this.notes_text.Location = new System.Drawing.Point(318, 100);
            this.notes_text.MaximumSize = new System.Drawing.Size(341, 81);
            this.notes_text.Multiline = true;
            this.notes_text.Name = "notes_text";
            this.notes_text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notes_text.Size = new System.Drawing.Size(341, 81);
            this.notes_text.TabIndex = 144;
            // 
            // cmb_Network
            // 
            this.cmb_Network.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Network.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Network.Location = new System.Drawing.Point(37, 175);
            this.cmb_Network.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmb_Network.Name = "cmb_Network";
            this.cmb_Network.Size = new System.Drawing.Size(198, 21);
            this.cmb_Network.TabIndex = 136;
            // 
            // autoPlay
            // 
            this.autoPlay.AutoSize = true;
            this.autoPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoPlay.Location = new System.Drawing.Point(38, 128);
            this.autoPlay.Margin = new System.Windows.Forms.Padding(2);
            this.autoPlay.Name = "autoPlay";
            this.autoPlay.Size = new System.Drawing.Size(167, 17);
            this.autoPlay.TabIndex = 135;
            this.autoPlay.Text = "Auto Play On Profile Selection";
            this.autoPlay.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(98, 278);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 118;
            this.label6.Text = "(milliseconds)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "(seconds)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WIndowsSetupTiming_Label
            // 
            this.WIndowsSetupTiming_Label.AutoSize = true;
            this.WIndowsSetupTiming_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WIndowsSetupTiming_Label.Location = new System.Drawing.Point(35, 254);
            this.WIndowsSetupTiming_Label.Margin = new System.Windows.Forms.Padding(0);
            this.WIndowsSetupTiming_Label.Name = "WIndowsSetupTiming_Label";
            this.WIndowsSetupTiming_Label.Size = new System.Drawing.Size(119, 13);
            this.WIndowsSetupTiming_Label.TabIndex = 116;
            this.WIndowsSetupTiming_Label.Text = "Windows Setup Timing ";
            this.WIndowsSetupTiming_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WindowsSetupTiming_TextBox
            // 
            this.WindowsSetupTiming_TextBox.Location = new System.Drawing.Point(38, 275);
            this.WindowsSetupTiming_TextBox.Margin = new System.Windows.Forms.Padding(0);
            this.WindowsSetupTiming_TextBox.MaxLength = 5;
            this.WindowsSetupTiming_TextBox.Name = "WindowsSetupTiming_TextBox";
            this.WindowsSetupTiming_TextBox.Size = new System.Drawing.Size(57, 21);
            this.WindowsSetupTiming_TextBox.TabIndex = 115;
            this.WindowsSetupTiming_TextBox.Text = "0";
            this.WindowsSetupTiming_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pauseBetweenInstanceLaunch_TxtBox
            // 
            this.pauseBetweenInstanceLaunch_TxtBox.Location = new System.Drawing.Point(38, 225);
            this.pauseBetweenInstanceLaunch_TxtBox.Margin = new System.Windows.Forms.Padding(0);
            this.pauseBetweenInstanceLaunch_TxtBox.MaxLength = 3;
            this.pauseBetweenInstanceLaunch_TxtBox.Name = "pauseBetweenInstanceLaunch_TxtBox";
            this.pauseBetweenInstanceLaunch_TxtBox.Size = new System.Drawing.Size(37, 21);
            this.pauseBetweenInstanceLaunch_TxtBox.TabIndex = 113;
            this.pauseBetweenInstanceLaunch_TxtBox.Text = "0";
            this.pauseBetweenInstanceLaunch_TxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pauseBetweenInstanceLauch_Label
            // 
            this.pauseBetweenInstanceLauch_Label.AutoSize = true;
            this.pauseBetweenInstanceLauch_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseBetweenInstanceLauch_Label.Location = new System.Drawing.Point(35, 204);
            this.pauseBetweenInstanceLauch_Label.Margin = new System.Windows.Forms.Padding(0);
            this.pauseBetweenInstanceLauch_Label.Name = "pauseBetweenInstanceLauch_Label";
            this.pauseBetweenInstanceLauch_Label.Size = new System.Drawing.Size(150, 13);
            this.pauseBetweenInstanceLauch_Label.TabIndex = 114;
            this.pauseBetweenInstanceLauch_Label.Text = "Pause Between Game Startup";
            this.pauseBetweenInstanceLauch_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(36, 153);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 13);
            this.label32.TabIndex = 109;
            this.label32.Text = "Network";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProfileSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.layoutBtnPicture);
            this.Controls.Add(this.layoutTabBtn);
            this.Controls.Add(this.profile_info_btn);
            this.Controls.Add(this.closeBtnPicture);
            this.Controls.Add(this.processorBtnPicture);
            this.Controls.Add(this.sharedBtnPicture);
            this.Controls.Add(this.playersBtnPicture);
            this.Controls.Add(this.audioBtnPicture);
            this.Controls.Add(this.processorTabBtn);
            this.Controls.Add(this.audioTabBtn);
            this.Controls.Add(this.playersTabBtn);
            this.Controls.Add(this.sharedTabBtn);
            this.Controls.Add(this.modeLabel);
            this.Controls.Add(this.processorTab);
            this.Controls.Add(this.audioTab);
            this.Controls.Add(this.playersTab);
            this.Controls.Add(this.layoutTab);
            this.Controls.Add(this.sharedTab);
            this.Controls.Add(this.profileInfo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ProfileSettings";
            this.Size = new System.Drawing.Size(674, 430);
            this.Click += new System.EventHandler(this.ProfileSettings_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProfileSettings_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.audioBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playersBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processorBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeBtnPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profile_info_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnPicture)).EndInit();
            this.processorTab.ResumeLayout(false);
            this.processorTab.PerformLayout();
            this.processorPage2.ResumeLayout(false);
            this.processorPage2.PerformLayout();
            this.processorPage1.ResumeLayout(false);
            this.processorPage1.PerformLayout();
            this.audioTab.ResumeLayout(false);
            this.audioTab.PerformLayout();
            this.audioCustomSettingsBox.ResumeLayout(false);
            this.audioCustomSettingsBox.PerformLayout();
            this.playersTab.ResumeLayout(false);
            this.playersTab.PerformLayout();
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            this.page2.ResumeLayout(false);
            this.page2.PerformLayout();
            this.layoutTab.ResumeLayout(false);
            this.layoutTab.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.sharedTab.ResumeLayout(false);
            this.sharedTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button sharedTabBtn;
        private Button playersTabBtn;
        private Button audioTabBtn;
        private Button processorTabBtn;
        private BufferedClientAreaPanel audioTab;
        private Label label39;
        private Label audioDefaultDevice;
        private Label label45;
        private Label label44;
        private Label label43;
        private Label label42;
        private Label label41;
        private Label label40;
        private Label label37;
        private Label label36;
        private RadioButton audioCustomSettingsRadio;
        private RadioButton audioDefaultSettingsRadio;
        private Button audioRefresh;
        private PictureBox audioBtnPicture;
        private PictureBox playersBtnPicture;
        private PictureBox sharedBtnPicture;
        private PictureBox processorBtnPicture;
        private BufferedClientAreaPanel sharedTab;
        private Label label6;
        private Label label2;
        private Label WIndowsSetupTiming_Label;
        private TextBox WindowsSetupTiming_TextBox;
        private TextBox pauseBetweenInstanceLaunch_TxtBox;
        private Label pauseBetweenInstanceLauch_Label;
        private Label label32;
        private BufferedClientAreaPanel playersTab;
        private PictureBox closeBtnPicture;
        private Label modeLabel;
        private CheckBox autoPlay;
        private ComboBox cmb_Network;
        private BufferedClientAreaPanel processorTab;
        private ComboBox PriorityClass16;
        private ComboBox PriorityClass15;
        private ComboBox PriorityClass14;
        private ComboBox PriorityClass13;
        private ComboBox PriorityClass12;
        private ComboBox PriorityClass11;
        private ComboBox PriorityClass10;
        private ComboBox PriorityClass9;
        private ComboBox PriorityClass8;
        private ComboBox PriorityClass7;
        private ComboBox PriorityClass6;
        private ComboBox PriorityClass5;
        private ComboBox PriorityClass4;
        private ComboBox PriorityClass3;
        private ComboBox PriorityClass2;
        private ComboBox PriorityClass1;
        private TextBox Affinity16;
        private TextBox Affinity15;
        private TextBox Affinity14;
        private TextBox Affinity13;
        private TextBox Affinity12;
        private TextBox Affinity11;
        private TextBox Affinity10;
        private TextBox Affinity9;
        private TextBox Affinity8;
        private TextBox Affinity7;
        private TextBox Affinity6;
        private TextBox Affinity5;
        private TextBox Affinity4;
        private TextBox Affinity3;
        private TextBox Affinity2;
        private TextBox Affinity1;
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
        private ComboBox IdealProcessor4;
        private Label label89;
        private ComboBox IdealProcessor3;
        private Label label90;
        private ComboBox IdealProcessor2;
        private Label label91;
        private ComboBox IdealProcessor1;
        private Label label93;
        private Label label72;
        private TextBox notes_text;
        private PictureBox profile_info_btn;
        private TextBox profileInfo;
        private CheckBox useNicksCheck;
        private CheckBox scaleOptionCbx;
        private GroupBox audioCustomSettingsBox;
        private ComboBox AudioInstance8;
        private ComboBox AudioInstance7;
        private ComboBox AudioInstance6;
        private ComboBox AudioInstance5;
        private ComboBox AudioInstance4;
        private ComboBox AudioInstance3;
        private ComboBox AudioInstance2;
        private ComboBox AudioInstance1;
        private PictureBox layoutBtnPicture;
        private Button layoutTabBtn;
        private BufferedClientAreaPanel layoutTab;
        private Label label71;
        private Panel panel1;
        private CheckBox cts_unfocus;
        private CheckBox cts_kar;
        private CheckBox cts_Mute;
        private ComboBox SplitColors;
        private Gaming.Controls.CustomNumericUpDown numMaxPlyrs;
        private Gaming.Controls.CustomNumericUpDown numUpDownVer;
        private CheckBox SplitDiv;
        private Label label49;
        public Panel layoutSizer;
        private Label label29;
        private Label label27;
        private Label label28;
        private Gaming.Controls.CustomNumericUpDown numUpDownHor;
        private Button btnNext;
        private Panel page1;
        private Label label48;
        private Label label47;
        private Label label21;
        private Label label25;
        private Label label13;
        private Label label8;
        private ComboBox steamid16;
        private ComboBox steamid15;
        private ComboBox steamid14;
        private ComboBox steamid13;
        private ComboBox steamid12;
        private ComboBox steamid11;
        private ComboBox steamid10;
        private ComboBox steamid9;
        private ComboBox steamid8;
        private ComboBox steamid7;
        private ComboBox steamid6;
        private ComboBox steamid5;
        private ComboBox steamid4;
        private ComboBox steamid3;
        private ComboBox steamid2;
        private ComboBox steamid1;
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
        private ComboBox player4N;
        private Label label14;
        private ComboBox player3N;
        private Label label12;
        private ComboBox player2N;
        private Label label10;
        private ComboBox player1N;
        private Label label7;
        private Panel page2;
        private Label label31;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label38;
        private Label label46;
        private ComboBox steamid32;
        private ComboBox steamid17;
        private ComboBox steamid31;
        private Label label61;
        private ComboBox steamid30;
        private ComboBox player17N;
        private ComboBox steamid29;
        private Label label59;
        private ComboBox steamid28;
        private ComboBox player18N;
        private ComboBox steamid27;
        private Label label58;
        private ComboBox steamid26;
        private ComboBox player19N;
        private ComboBox steamid25;
        private Label label57;
        private ComboBox steamid24;
        private ComboBox player20N;
        private ComboBox steamid23;
        private Label label55;
        private ComboBox steamid22;
        private ComboBox player21N;
        private ComboBox steamid21;
        private Label label54;
        private ComboBox steamid20;
        private ComboBox player22N;
        private ComboBox steamid19;
        private Label label53;
        private ComboBox steamid18;
        private ComboBox player23N;
        private Label label52;
        private ComboBox player32N;
        private ComboBox player24N;
        private Label label50;
        private Label label51;
        private ComboBox player31N;
        private ComboBox player25N;
        private Label label73;
        private Label label74;
        private ComboBox player30N;
        private ComboBox player26N;
        private Label label75;
        private Label label88;
        private ComboBox player29N;
        private ComboBox player27N;
        private Label label92;
        private Label label94;
        private ComboBox player28N;
        private ComboBox def_sid_comboBox;
        private Label default_sid_list_label;
        private Panel processorPage2;
        private Label coreCountLabel;
        private Label label96;
        private Label label97;
        private Label label98;
        private Label label99;
        private ComboBox PriorityClass32;
        private ComboBox PriorityClass31;
        private ComboBox PriorityClass30;
        private ComboBox PriorityClass29;
        private ComboBox PriorityClass28;
        private ComboBox PriorityClass27;
        private ComboBox PriorityClass26;
        private ComboBox PriorityClass25;
        private ComboBox PriorityClass24;
        private ComboBox PriorityClass23;
        private ComboBox PriorityClass22;
        private ComboBox PriorityClass21;
        private ComboBox PriorityClass20;
        private ComboBox PriorityClass19;
        private ComboBox PriorityClass18;
        private ComboBox PriorityClass17;
        private TextBox Affinity32;
        private TextBox Affinity31;
        private TextBox Affinity30;
        private TextBox Affinity29;
        private TextBox Affinity28;
        private TextBox Affinity27;
        private TextBox Affinity26;
        private TextBox Affinity25;
        private TextBox Affinity24;
        private TextBox Affinity23;
        private TextBox Affinity22;
        private TextBox Affinity21;
        private TextBox Affinity20;
        private TextBox Affinity19;
        private TextBox Affinity18;
        private TextBox Affinity17;
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
        private Panel processorPage1;
        private Button btnProcessorNext;
    }
}