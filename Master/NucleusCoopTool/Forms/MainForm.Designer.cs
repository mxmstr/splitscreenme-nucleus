using System.Drawing;
using System.Windows.Forms;


namespace Nucleus.Coop
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel4 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.btn_thirdPartytools = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.btn_SplitCalculator = new System.Windows.Forms.Button();
            this.btn_reddit = new System.Windows.Forms.Button();
            this.btn_faq = new System.Windows.Forms.Button();
            this.btn_downloadAssets = new System.Windows.Forms.Button();
            this.btn_Discord = new System.Windows.Forms.Button();
            this.gameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOrigExePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserProfileConfigPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openUserProfileSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserProfileSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentConfigPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDocumentConfigPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDocumentSavePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.changeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteContentFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepInstancesFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateHandlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Links = new System.Windows.Forms.Button();
            this.third_party_tools_container = new System.Windows.Forms.Panel();
            this.clientAreaPanel = new BufferedClientAreaPanel();
            this.StepPanel = new BufferedClientAreaPanel();
            this.game_listSizer = new BufferedClientAreaPanel();
            this.list_Games = new Nucleus.Gaming.ControlListBox();
            this.rightFrame = new BufferedClientAreaPanel();
            this.btn_Steam = new System.Windows.Forms.Button();
            this.icons_Container = new BufferedFlowLayoutPanel();
            this.btn_gameOptions = new System.Windows.Forms.Button();
            this.scriptAuthorTxtSizer = new BufferedClientAreaPanel();
            this.btn_textSwitcher = new System.Windows.Forms.PictureBox();
            this.scriptAuthorTxt = new System.Windows.Forms.RichTextBox();
            this.btn_magnifier = new System.Windows.Forms.PictureBox();
            this.HandlerNoteTitle = new System.Windows.Forms.Label();
            this.cover = new BufferedClientAreaPanel();
            this.coverFrame = new BufferedClientAreaPanel();
            this.button_UpdateAvailable = new System.Windows.Forms.Button();
            this.btn_Play = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.mainButtonFrame = new BufferedClientAreaPanel();
            this.btn_debuglog = new System.Windows.Forms.Button();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.linksPanel = new BufferedClientAreaPanel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.txt_version = new System.Windows.Forms.Label();
            this.label_StepTitle = new System.Windows.Forms.Label();
            this.stepPanelPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.gameContextMenuStrip.SuspendLayout();
            this.third_party_tools_container.SuspendLayout();
            this.clientAreaPanel.SuspendLayout();
            this.game_listSizer.SuspendLayout();
            this.rightFrame.SuspendLayout();
            this.scriptAuthorTxtSizer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_textSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_magnifier)).BeginInit();
            this.cover.SuspendLayout();
            this.coverFrame.SuspendLayout();
            this.mainButtonFrame.SuspendLayout();
            this.linksPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepPanelPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel4
            // 
            this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.ForeColor = System.Drawing.Color.Aqua;
            this.linkLabel4.Location = new System.Drawing.Point(218, 4);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(67, 15);
            this.linkLabel4.TabIndex = 3;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "ScpToolkit ";
            this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel4.Click += new System.EventHandler(this.LinkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.ForeColor = System.Drawing.Color.Aqua;
            this.linkLabel3.Location = new System.Drawing.Point(154, 4);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(59, 15);
            this.linkLabel3.TabIndex = 2;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "HidHide";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel3.Click += new System.EventHandler(this.LinkLabel3_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.ForeColor = System.Drawing.Color.Aqua;
            this.linkLabel2.Location = new System.Drawing.Point(67, 4);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(81, 15);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "DS4Windows";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.Click += new System.EventHandler(this.LinkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.Aqua;
            this.linkLabel1.Location = new System.Drawing.Point(3, 4);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(59, 15);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "XOutput";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Click += new System.EventHandler(this.LinkLabel1_LinkClicked);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logo.Location = new System.Drawing.Point(4, 14);
            this.logo.Margin = new System.Windows.Forms.Padding(0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(150, 26);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 24;
            this.logo.TabStop = false;
            this.logo.DoubleClick += new System.EventHandler(this.Logo_Click);
            // 
            // btn_thirdPartytools
            // 
            this.btn_thirdPartytools.BackColor = System.Drawing.Color.Transparent;
            this.btn_thirdPartytools.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_thirdPartytools.BackgroundImage")));
            this.btn_thirdPartytools.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_thirdPartytools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_thirdPartytools.FlatAppearance.BorderSize = 0;
            this.btn_thirdPartytools.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_thirdPartytools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_thirdPartytools.ForeColor = System.Drawing.Color.White;
            this.btn_thirdPartytools.Location = new System.Drawing.Point(124, 4);
            this.btn_thirdPartytools.Name = "btn_thirdPartytools";
            this.btn_thirdPartytools.Size = new System.Drawing.Size(20, 20);
            this.btn_thirdPartytools.TabIndex = 37;
            this.btn_thirdPartytools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_thirdPartytools.UseVisualStyleBackColor = false;
            this.btn_thirdPartytools.Click += new System.EventHandler(this.Btn_thirdPartytools_Click);
            this.btn_thirdPartytools.MouseEnter += new System.EventHandler(this.Btn_thirdPartytools_MouseEnter);
            this.btn_thirdPartytools.MouseLeave += new System.EventHandler(this.Btn_thirdPartytools_MouseLeave);
            // 
            // btn_settings
            // 
            this.btn_settings.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_settings.BackColor = System.Drawing.Color.Transparent;
            this.btn_settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_settings.BackgroundImage")));
            this.btn_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_settings.FlatAppearance.BorderSize = 0;
            this.btn_settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_settings.Location = new System.Drawing.Point(588, 6);
            this.btn_settings.Margin = new System.Windows.Forms.Padding(2);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(30, 30);
            this.btn_settings.TabIndex = 16;
            this.btn_settings.UseVisualStyleBackColor = false;
            this.btn_settings.Click += new System.EventHandler(this.SettingsBtn_Click);
            this.btn_settings.MouseEnter += new System.EventHandler(this.Btn_settings_MouseEnter);
            this.btn_settings.MouseLeave += new System.EventHandler(this.Btn_settings_MouseLeave);
            // 
            // btn_SplitCalculator
            // 
            this.btn_SplitCalculator.BackColor = System.Drawing.Color.Transparent;
            this.btn_SplitCalculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SplitCalculator.BackgroundImage")));
            this.btn_SplitCalculator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SplitCalculator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SplitCalculator.FlatAppearance.BorderSize = 0;
            this.btn_SplitCalculator.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_SplitCalculator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SplitCalculator.ForeColor = System.Drawing.Color.White;
            this.btn_SplitCalculator.Location = new System.Drawing.Point(98, 4);
            this.btn_SplitCalculator.Name = "btn_SplitCalculator";
            this.btn_SplitCalculator.Size = new System.Drawing.Size(20, 20);
            this.btn_SplitCalculator.TabIndex = 38;
            this.btn_SplitCalculator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SplitCalculator.UseVisualStyleBackColor = false;
            this.btn_SplitCalculator.Click += new System.EventHandler(this.Btn_SplitCalculator_Click);
            this.btn_SplitCalculator.MouseEnter += new System.EventHandler(this.Btn_SplitCalculator_MouseEnter);
            this.btn_SplitCalculator.MouseLeave += new System.EventHandler(this.Btn_SplitCalculator_MouseLeave);
            // 
            // btn_reddit
            // 
            this.btn_reddit.BackColor = System.Drawing.Color.Transparent;
            this.btn_reddit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_reddit.BackgroundImage")));
            this.btn_reddit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_reddit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reddit.FlatAppearance.BorderSize = 0;
            this.btn_reddit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_reddit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reddit.ForeColor = System.Drawing.Color.White;
            this.btn_reddit.Location = new System.Drawing.Point(35, 2);
            this.btn_reddit.Name = "btn_reddit";
            this.btn_reddit.Size = new System.Drawing.Size(25, 22);
            this.btn_reddit.TabIndex = 40;
            this.btn_reddit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_reddit.UseVisualStyleBackColor = false;
            this.btn_reddit.Click += new System.EventHandler(this.Button2_Click);
            this.btn_reddit.MouseEnter += new System.EventHandler(this.Btn_reddit_MouseEnter);
            this.btn_reddit.MouseLeave += new System.EventHandler(this.Btn_reddit_MouseLeave);
            // 
            // btn_faq
            // 
            this.btn_faq.BackColor = System.Drawing.Color.Transparent;
            this.btn_faq.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_faq.BackgroundImage")));
            this.btn_faq.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_faq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_faq.FlatAppearance.BorderSize = 0;
            this.btn_faq.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_faq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_faq.ForeColor = System.Drawing.Color.White;
            this.btn_faq.Location = new System.Drawing.Point(4, 2);
            this.btn_faq.Name = "btn_faq";
            this.btn_faq.Size = new System.Drawing.Size(25, 23);
            this.btn_faq.TabIndex = 36;
            this.btn_faq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_faq.UseVisualStyleBackColor = false;
            this.btn_faq.Click += new System.EventHandler(this.Link_faq_Click);
            this.btn_faq.MouseEnter += new System.EventHandler(this.Btn_faq_MouseEnter);
            this.btn_faq.MouseLeave += new System.EventHandler(this.Btn_faq_MouseLeave);
            // 
            // btn_downloadAssets
            // 
            this.btn_downloadAssets.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_downloadAssets.BackColor = System.Drawing.Color.Transparent;
            this.btn_downloadAssets.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_downloadAssets.BackgroundImage")));
            this.btn_downloadAssets.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_downloadAssets.FlatAppearance.BorderSize = 0;
            this.btn_downloadAssets.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_downloadAssets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_downloadAssets.Location = new System.Drawing.Point(554, 6);
            this.btn_downloadAssets.Margin = new System.Windows.Forms.Padding(2);
            this.btn_downloadAssets.Name = "btn_downloadAssets";
            this.btn_downloadAssets.Size = new System.Drawing.Size(30, 30);
            this.btn_downloadAssets.TabIndex = 23;
            this.btn_downloadAssets.Text = " ";
            this.btn_downloadAssets.UseVisualStyleBackColor = false;
            this.btn_downloadAssets.Click += new System.EventHandler(this.Btn_downloadAssets_Click);
            this.btn_downloadAssets.MouseEnter += new System.EventHandler(this.Btn_downloadAssets_MouseEnter);
            this.btn_downloadAssets.MouseLeave += new System.EventHandler(this.Btn_downloadAssets_MouseLeave);
            // 
            // btn_Discord
            // 
            this.btn_Discord.BackColor = System.Drawing.Color.Transparent;
            this.btn_Discord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Discord.BackgroundImage")));
            this.btn_Discord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Discord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Discord.FlatAppearance.BorderSize = 0;
            this.btn_Discord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Discord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Discord.ForeColor = System.Drawing.Color.White;
            this.btn_Discord.Location = new System.Drawing.Point(67, 3);
            this.btn_Discord.Name = "btn_Discord";
            this.btn_Discord.Size = new System.Drawing.Size(25, 20);
            this.btn_Discord.TabIndex = 39;
            this.btn_Discord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Discord.UseVisualStyleBackColor = false;
            this.btn_Discord.Click += new System.EventHandler(this.Button1_Click);
            this.btn_Discord.MouseEnter += new System.EventHandler(this.Btn_Discord_MouseEnter);
            this.btn_Discord.MouseLeave += new System.EventHandler(this.Btn_Discord_MouseLeave);
            // 
            // gameContextMenuStrip
            // 
            this.gameContextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gameContextMenuStrip.DropShadowEnabled = false;
            this.gameContextMenuStrip.ImageScalingSize = new System.Drawing.Size(15, 15);
            this.gameContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nullToolStripMenuItem,
            this.scriptNotesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.detailsToolStripMenuItem,
            this.openScriptToolStripMenuItem,
            this.openDataFolderToolStripMenuItem,
            this.openOrigExePathToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openToolStripMenuItem,
            this.deleteUserProfileConfigPathToolStripMenuItem,
            this.openUserProfileSavePathToolStripMenuItem,
            this.deleteUserProfileSavePathToolStripMenuItem,
            this.openDocumentConfigPathToolStripMenuItem,
            this.deleteDocumentConfigPathToolStripMenuItem,
            this.openDocumentSavePathToolStripMenuItem,
            this.deleteDocumentSavePathToolStripMenuItem,
            this.toolStripMenuItem3,
            this.changeIconToolStripMenuItem,
            this.deleteContentFolderToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.keepInstancesFolderToolStripMenuItem,
            this.updateHandlerToolStripMenuItem});
            this.gameContextMenuStrip.Name = "gameContextMenuStrip";
            this.gameContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.gameContextMenuStrip.Size = new System.Drawing.Size(236, 440);
            this.gameContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.GameContextMenuStrip_Opening);
            this.gameContextMenuStrip.Opened += new System.EventHandler(this.GameContextMenuStrip_Opened);
            // 
            // nullToolStripMenuItem
            // 
            this.nullToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.nullToolStripMenuItem.Name = "nullToolStripMenuItem";
            this.nullToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.nullToolStripMenuItem.Text = "null";
            // 
            // scriptNotesToolStripMenuItem
            // 
            this.scriptNotesToolStripMenuItem.Name = "scriptNotesToolStripMenuItem";
            this.scriptNotesToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.scriptNotesToolStripMenuItem.Text = "Handler Author\'s Notes";
            this.scriptNotesToolStripMenuItem.Visible = false;
            this.scriptNotesToolStripMenuItem.Click += new System.EventHandler(this.ScriptNotesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.detailsToolStripMenuItem.Text = "Nucleus Game Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.DetailsToolStripMenuItem_Click);
            // 
            // openScriptToolStripMenuItem
            // 
            this.openScriptToolStripMenuItem.Name = "openScriptToolStripMenuItem";
            this.openScriptToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openScriptToolStripMenuItem.Text = "Open Game Handler";
            this.openScriptToolStripMenuItem.Click += new System.EventHandler(this.OpenScriptToolStripMenuItem_Click);
            // 
            // openDataFolderToolStripMenuItem
            // 
            this.openDataFolderToolStripMenuItem.Name = "openDataFolderToolStripMenuItem";
            this.openDataFolderToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openDataFolderToolStripMenuItem.Text = "Open Nucleus Content Folder";
            this.openDataFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenDataFolderToolStripMenuItem_Click);
            // 
            // openOrigExePathToolStripMenuItem
            // 
            this.openOrigExePathToolStripMenuItem.Name = "openOrigExePathToolStripMenuItem";
            this.openOrigExePathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openOrigExePathToolStripMenuItem.Text = "Open Original Exe Path";
            this.openOrigExePathToolStripMenuItem.Click += new System.EventHandler(this.OpenOrigExePathToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(232, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openToolStripMenuItem.Text = "Open UserProfile Config Path";
            this.openToolStripMenuItem.Visible = false;
            // 
            // deleteUserProfileConfigPathToolStripMenuItem
            // 
            this.deleteUserProfileConfigPathToolStripMenuItem.Name = "deleteUserProfileConfigPathToolStripMenuItem";
            this.deleteUserProfileConfigPathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteUserProfileConfigPathToolStripMenuItem.Text = "Delete UserProfile Config Path";
            this.deleteUserProfileConfigPathToolStripMenuItem.Visible = false;
            // 
            // openUserProfileSavePathToolStripMenuItem
            // 
            this.openUserProfileSavePathToolStripMenuItem.Name = "openUserProfileSavePathToolStripMenuItem";
            this.openUserProfileSavePathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openUserProfileSavePathToolStripMenuItem.Text = "Open UserProfile Save Path";
            this.openUserProfileSavePathToolStripMenuItem.Visible = false;
            // 
            // deleteUserProfileSavePathToolStripMenuItem
            // 
            this.deleteUserProfileSavePathToolStripMenuItem.Name = "deleteUserProfileSavePathToolStripMenuItem";
            this.deleteUserProfileSavePathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteUserProfileSavePathToolStripMenuItem.Text = "Delete UserProfile Save Path";
            this.deleteUserProfileSavePathToolStripMenuItem.Visible = false;
            // 
            // openDocumentConfigPathToolStripMenuItem
            // 
            this.openDocumentConfigPathToolStripMenuItem.Name = "openDocumentConfigPathToolStripMenuItem";
            this.openDocumentConfigPathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openDocumentConfigPathToolStripMenuItem.Text = "Open Document Config Path";
            this.openDocumentConfigPathToolStripMenuItem.Visible = false;
            // 
            // deleteDocumentConfigPathToolStripMenuItem
            // 
            this.deleteDocumentConfigPathToolStripMenuItem.Name = "deleteDocumentConfigPathToolStripMenuItem";
            this.deleteDocumentConfigPathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteDocumentConfigPathToolStripMenuItem.Text = "Delete Document Config Path";
            this.deleteDocumentConfigPathToolStripMenuItem.Visible = false;
            // 
            // openDocumentSavePathToolStripMenuItem
            // 
            this.openDocumentSavePathToolStripMenuItem.Name = "openDocumentSavePathToolStripMenuItem";
            this.openDocumentSavePathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openDocumentSavePathToolStripMenuItem.Text = "Open Document Save Path";
            this.openDocumentSavePathToolStripMenuItem.Visible = false;
            // 
            // deleteDocumentSavePathToolStripMenuItem
            // 
            this.deleteDocumentSavePathToolStripMenuItem.Name = "deleteDocumentSavePathToolStripMenuItem";
            this.deleteDocumentSavePathToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteDocumentSavePathToolStripMenuItem.Text = "Delete Document Save Path";
            this.deleteDocumentSavePathToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(232, 6);
            // 
            // changeIconToolStripMenuItem
            // 
            this.changeIconToolStripMenuItem.Name = "changeIconToolStripMenuItem";
            this.changeIconToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.changeIconToolStripMenuItem.Text = "Change Game Icon";
            this.changeIconToolStripMenuItem.Click += new System.EventHandler(this.ChangeIconToolStripMenuItem_Click);
            // 
            // deleteContentFolderToolStripMenuItem
            // 
            this.deleteContentFolderToolStripMenuItem.Name = "deleteContentFolderToolStripMenuItem";
            this.deleteContentFolderToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteContentFolderToolStripMenuItem.Text = "Delete Nucleus Content Folder";
            this.deleteContentFolderToolStripMenuItem.Click += new System.EventHandler(this.DeleteContentFolderToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.deleteToolStripMenuItem.Text = "Remove Game from List";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // keepInstancesFolderToolStripMenuItem
            // 
            this.keepInstancesFolderToolStripMenuItem.Name = "keepInstancesFolderToolStripMenuItem";
            this.keepInstancesFolderToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.keepInstancesFolderToolStripMenuItem.Text = "Keep instances content folder";
            this.keepInstancesFolderToolStripMenuItem.Click += new System.EventHandler(this.KeepInstancesFolderToolStripMenuItem_Click);
            // 
            // updateHandlerToolStripMenuItem
            // 
            this.updateHandlerToolStripMenuItem.BackColor = System.Drawing.Color.PaleGreen;
            this.updateHandlerToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.updateHandlerToolStripMenuItem.Name = "updateHandlerToolStripMenuItem";
            this.updateHandlerToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.updateHandlerToolStripMenuItem.Text = "Update Handler";
            this.updateHandlerToolStripMenuItem.Click += new System.EventHandler(this.UpdateHandlerToolStripMenuItem_Click);
            // 
            // btn_Links
            // 
            this.btn_Links.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Links.BackColor = System.Drawing.Color.Transparent;
            this.btn_Links.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Links.BackgroundImage")));
            this.btn_Links.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Links.FlatAppearance.BorderSize = 0;
            this.btn_Links.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Links.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Links.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Links.Location = new System.Drawing.Point(1012, 6);
            this.btn_Links.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Links.Name = "btn_Links";
            this.btn_Links.Size = new System.Drawing.Size(20, 20);
            this.btn_Links.TabIndex = 42;
            this.btn_Links.UseVisualStyleBackColor = false;
            this.btn_Links.Click += new System.EventHandler(this.Btn_Links_Click);
            // 
            // third_party_tools_container
            // 
            this.third_party_tools_container.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.third_party_tools_container.BackColor = System.Drawing.Color.Black;
            this.third_party_tools_container.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.third_party_tools_container.Controls.Add(this.linkLabel4);
            this.third_party_tools_container.Controls.Add(this.linkLabel3);
            this.third_party_tools_container.Controls.Add(this.linkLabel2);
            this.third_party_tools_container.Controls.Add(this.linkLabel1);
            this.third_party_tools_container.Location = new System.Drawing.Point(824, 33);
            this.third_party_tools_container.Name = "third_party_tools_container";
            this.third_party_tools_container.Size = new System.Drawing.Size(286, 19);
            this.third_party_tools_container.TabIndex = 0;
            this.third_party_tools_container.Visible = false;
            // 
            // clientAreaPanel
            // 
            this.clientAreaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientAreaPanel.BackColor = System.Drawing.Color.Black;
            this.clientAreaPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clientAreaPanel.Controls.Add(this.StepPanel);
            this.clientAreaPanel.Controls.Add(this.game_listSizer);
            this.clientAreaPanel.Controls.Add(this.rightFrame);
            this.clientAreaPanel.Controls.Add(this.mainButtonFrame);
            this.clientAreaPanel.Controls.Add(this.stepPanelPictureBox);
            this.clientAreaPanel.Location = new System.Drawing.Point(5, 5);
            this.clientAreaPanel.Margin = new System.Windows.Forms.Padding(0);
            this.clientAreaPanel.Name = "clientAreaPanel";
            this.clientAreaPanel.Size = new System.Drawing.Size(1110, 631);
            this.clientAreaPanel.TabIndex = 34;
            // 
            // StepPanel
            // 
            this.StepPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepPanel.AutoScroll = true;
            this.StepPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StepPanel.BackColor = System.Drawing.Color.Transparent;
            this.StepPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StepPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.StepPanel.Location = new System.Drawing.Point(209, 59);
            this.StepPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StepPanel.Name = "StepPanel";
            this.StepPanel.Size = new System.Drawing.Size(715, 572);
            this.StepPanel.TabIndex = 0;
            this.StepPanel.Visible = false;
            // 
            // game_listSizer
            // 
            this.game_listSizer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.game_listSizer.BackColor = System.Drawing.Color.Transparent;
            this.game_listSizer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.game_listSizer.Controls.Add(this.list_Games);
            this.game_listSizer.Location = new System.Drawing.Point(0, 59);
            this.game_listSizer.Margin = new System.Windows.Forms.Padding(0);
            this.game_listSizer.Name = "game_listSizer";
            this.game_listSizer.Size = new System.Drawing.Size(209, 572);
            this.game_listSizer.TabIndex = 35;
            // 
            // list_Games
            // 
            this.list_Games.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Games.AutoScroll = true;
            this.list_Games.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.list_Games.BackColor = System.Drawing.Color.Transparent;
            this.list_Games.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.list_Games.Border = 0;
            this.list_Games.ContextMenuStrip = this.gameContextMenuStrip;
            this.list_Games.Location = new System.Drawing.Point(0, 0);
            this.list_Games.Margin = new System.Windows.Forms.Padding(0);
            this.list_Games.Name = "list_Games";
            this.list_Games.Offset = new System.Drawing.Size(0, 0);
            this.list_Games.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.list_Games.Size = new System.Drawing.Size(230, 572);
            this.list_Games.TabIndex = 2;
            this.list_Games.SelectedChanged += new System.Action<object, System.Windows.Forms.Control>(this.List_Games_SelectedChanged);
            // 
            // rightFrame
            // 
            this.rightFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightFrame.BackColor = System.Drawing.Color.Transparent;
            this.rightFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightFrame.Controls.Add(this.btn_Steam);
            this.rightFrame.Controls.Add(this.icons_Container);
            this.rightFrame.Controls.Add(this.btn_gameOptions);
            this.rightFrame.Controls.Add(this.scriptAuthorTxtSizer);
            this.rightFrame.Controls.Add(this.cover);
            this.rightFrame.Controls.Add(this.btn_Play);
            this.rightFrame.Controls.Add(this.btn_Next);
            this.rightFrame.Controls.Add(this.btn_Prev);
            this.rightFrame.Location = new System.Drawing.Point(924, 59);
            this.rightFrame.Margin = new System.Windows.Forms.Padding(0);
            this.rightFrame.Name = "rightFrame";
            this.rightFrame.Size = new System.Drawing.Size(186, 575);
            this.rightFrame.TabIndex = 34;
            this.rightFrame.Visible = false;
            // 
            // btn_Steam
            // 
            this.btn_Steam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Steam.FlatAppearance.BorderSize = 0;
            this.btn_Steam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Steam.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Steam.Location = new System.Drawing.Point(8, 527);
            this.btn_Steam.Name = "btn_Steam";
            this.btn_Steam.Size = new System.Drawing.Size(170, 25);
            this.btn_Steam.TabIndex = 102;
            this.btn_Steam.Text = "Start Steam Client";
            this.btn_Steam.UseVisualStyleBackColor = true;
            this.btn_Steam.Visible = false;
            this.btn_Steam.Click += new System.EventHandler(this.Btn_Steam_Click);
            // 
            // icons_Container
            // 
            this.icons_Container.AutoSize = true;
            this.icons_Container.Location = new System.Drawing.Point(6, 41);
            this.icons_Container.Name = "icons_Container";
            this.icons_Container.Size = new System.Drawing.Size(44, 19);
            this.icons_Container.TabIndex = 32;
            // 
            // btn_gameOptions
            // 
            this.btn_gameOptions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_gameOptions.BackColor = System.Drawing.Color.Transparent;
            this.btn_gameOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gameOptions.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_gameOptions.FlatAppearance.BorderSize = 0;
            this.btn_gameOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gameOptions.Location = new System.Drawing.Point(157, 44);
            this.btn_gameOptions.Margin = new System.Windows.Forms.Padding(2);
            this.btn_gameOptions.Name = "btn_gameOptions";
            this.btn_gameOptions.Size = new System.Drawing.Size(20, 20);
            this.btn_gameOptions.TabIndex = 21;
            this.btn_gameOptions.UseVisualStyleBackColor = false;
            this.btn_gameOptions.Click += new System.EventHandler(this.GameOptions_Click);
            // 
            // scriptAuthorTxtSizer
            // 
            this.scriptAuthorTxtSizer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptAuthorTxtSizer.BackColor = System.Drawing.Color.Transparent;
            this.scriptAuthorTxtSizer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scriptAuthorTxtSizer.Controls.Add(this.btn_textSwitcher);
            this.scriptAuthorTxtSizer.Controls.Add(this.scriptAuthorTxt);
            this.scriptAuthorTxtSizer.Controls.Add(this.btn_magnifier);
            this.scriptAuthorTxtSizer.Controls.Add(this.HandlerNoteTitle);
            this.scriptAuthorTxtSizer.Location = new System.Drawing.Point(8, 305);
            this.scriptAuthorTxtSizer.Margin = new System.Windows.Forms.Padding(5);
            this.scriptAuthorTxtSizer.MaximumSize = new System.Drawing.Size(172, 232);
            this.scriptAuthorTxtSizer.Name = "scriptAuthorTxtSizer";
            this.scriptAuthorTxtSizer.Size = new System.Drawing.Size(171, 218);
            this.scriptAuthorTxtSizer.TabIndex = 31;
            // 
            // btn_textSwitcher
            // 
            this.btn_textSwitcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_textSwitcher.BackColor = System.Drawing.Color.Transparent;
            this.btn_textSwitcher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_textSwitcher.Location = new System.Drawing.Point(149, 1);
            this.btn_textSwitcher.Name = "btn_textSwitcher";
            this.btn_textSwitcher.Size = new System.Drawing.Size(20, 20);
            this.btn_textSwitcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_textSwitcher.TabIndex = 44;
            this.btn_textSwitcher.TabStop = false;
            this.btn_textSwitcher.Visible = false;
            this.btn_textSwitcher.Click += new System.EventHandler(this.Btn_textSwitcher_Click);
            // 
            // scriptAuthorTxt
            // 
            this.scriptAuthorTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptAuthorTxt.BackColor = System.Drawing.Color.Black;
            this.scriptAuthorTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scriptAuthorTxt.BulletIndent = 1;
            this.scriptAuthorTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.scriptAuthorTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.scriptAuthorTxt.ForeColor = System.Drawing.Color.White;
            this.scriptAuthorTxt.Location = new System.Drawing.Point(0, 23);
            this.scriptAuthorTxt.Margin = new System.Windows.Forms.Padding(0);
            this.scriptAuthorTxt.MaximumSize = new System.Drawing.Size(189, 191);
            this.scriptAuthorTxt.Name = "scriptAuthorTxt";
            this.scriptAuthorTxt.ReadOnly = true;
            this.scriptAuthorTxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.scriptAuthorTxt.Size = new System.Drawing.Size(188, 175);
            this.scriptAuthorTxt.TabIndex = 13;
            this.scriptAuthorTxt.Text = "";
            this.scriptAuthorTxt.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ScriptAuthorTxt_LinkClicked);
            // 
            // btn_magnifier
            // 
            this.btn_magnifier.BackColor = System.Drawing.Color.Transparent;
            this.btn_magnifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_magnifier.Location = new System.Drawing.Point(2, 1);
            this.btn_magnifier.Name = "btn_magnifier";
            this.btn_magnifier.Size = new System.Drawing.Size(20, 20);
            this.btn_magnifier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_magnifier.TabIndex = 43;
            this.btn_magnifier.TabStop = false;
            this.btn_magnifier.Click += new System.EventHandler(this.Btn_magnifier_Click);
            // 
            // HandlerNoteTitle
            // 
            this.HandlerNoteTitle.BackColor = System.Drawing.Color.Transparent;
            this.HandlerNoteTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.HandlerNoteTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HandlerNoteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HandlerNoteTitle.Location = new System.Drawing.Point(0, 0);
            this.HandlerNoteTitle.Name = "HandlerNoteTitle";
            this.HandlerNoteTitle.Size = new System.Drawing.Size(171, 20);
            this.HandlerNoteTitle.TabIndex = 33;
            this.HandlerNoteTitle.Text = "Handler Notes";
            this.HandlerNoteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cover
            // 
            this.cover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cover.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cover.BackColor = System.Drawing.Color.Black;
            this.cover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cover.Controls.Add(this.coverFrame);
            this.cover.Location = new System.Drawing.Point(7, 69);
            this.cover.Name = "cover";
            this.cover.Size = new System.Drawing.Size(172, 229);
            this.cover.TabIndex = 27;
            // 
            // coverFrame
            // 
            this.coverFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coverFrame.BackColor = System.Drawing.Color.Transparent;
            this.coverFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.coverFrame.Controls.Add(this.button_UpdateAvailable);
            this.coverFrame.Location = new System.Drawing.Point(0, 0);
            this.coverFrame.Margin = new System.Windows.Forms.Padding(0);
            this.coverFrame.Name = "coverFrame";
            this.coverFrame.Size = new System.Drawing.Size(170, 227);
            this.coverFrame.TabIndex = 26;
            // 
            // button_UpdateAvailable
            // 
            this.button_UpdateAvailable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button_UpdateAvailable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_UpdateAvailable.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_UpdateAvailable.FlatAppearance.BorderSize = 0;
            this.button_UpdateAvailable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_UpdateAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_UpdateAvailable.ForeColor = System.Drawing.Color.Yellow;
            this.button_UpdateAvailable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_UpdateAvailable.Location = new System.Drawing.Point(0, 204);
            this.button_UpdateAvailable.Margin = new System.Windows.Forms.Padding(2);
            this.button_UpdateAvailable.Name = "button_UpdateAvailable";
            this.button_UpdateAvailable.Size = new System.Drawing.Size(172, 25);
            this.button_UpdateAvailable.TabIndex = 23;
            this.button_UpdateAvailable.Text = "New Handler Available!";
            this.button_UpdateAvailable.UseVisualStyleBackColor = false;
            this.button_UpdateAvailable.Visible = false;
            this.button_UpdateAvailable.Click += new System.EventHandler(this.Button_UpdateAvailable_Click);
            // 
            // btn_Play
            // 
            this.btn_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Play.BackColor = System.Drawing.Color.Transparent;
            this.btn_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Play.Enabled = false;
            this.btn_Play.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Play.FlatAppearance.BorderSize = 0;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Play.ForeColor = System.Drawing.Color.Lime;
            this.btn_Play.Location = new System.Drawing.Point(45, 2);
            this.btn_Play.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(100, 25);
            this.btn_Play.TabIndex = 4;
            this.btn_Play.Text = "PLAY";
            this.btn_Play.UseVisualStyleBackColor = false;
            this.btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Next.BackColor = System.Drawing.Color.Transparent;
            this.btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Next.Enabled = false;
            this.btn_Next.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Location = new System.Drawing.Point(154, 2);
            this.btn_Next.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(25, 25);
            this.btn_Next.TabIndex = 11;
            this.btn_Next.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_Next.UseVisualStyleBackColor = false;
            this.btn_Next.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Prev.BackColor = System.Drawing.Color.Transparent;
            this.btn_Prev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Prev.Enabled = false;
            this.btn_Prev.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Prev.FlatAppearance.BorderSize = 0;
            this.btn_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Prev.Location = new System.Drawing.Point(8, 2);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(25, 25);
            this.btn_Prev.TabIndex = 9;
            this.btn_Prev.UseVisualStyleBackColor = false;
            this.btn_Prev.Click += new System.EventHandler(this.Btn_Prev_Click);
            // 
            // mainButtonFrame
            // 
            this.mainButtonFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainButtonFrame.BackColor = System.Drawing.Color.Transparent;
            this.mainButtonFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainButtonFrame.Controls.Add(this.btn_debuglog);
            this.mainButtonFrame.Controls.Add(this.btn_settings);
            this.mainButtonFrame.Controls.Add(this.btn_Extract);
            this.mainButtonFrame.Controls.Add(this.btn_downloadAssets);
            this.mainButtonFrame.Controls.Add(this.btnSearch);
            this.mainButtonFrame.Controls.Add(this.third_party_tools_container);
            this.mainButtonFrame.Controls.Add(this.linksPanel);
            this.mainButtonFrame.Controls.Add(this.closeBtn);
            this.mainButtonFrame.Controls.Add(this.btn_Links);
            this.mainButtonFrame.Controls.Add(this.maximizeBtn);
            this.mainButtonFrame.Controls.Add(this.minimizeBtn);
            this.mainButtonFrame.Controls.Add(this.txt_version);
            this.mainButtonFrame.Controls.Add(this.label_StepTitle);
            this.mainButtonFrame.Controls.Add(this.logo);
            this.mainButtonFrame.Location = new System.Drawing.Point(0, 0);
            this.mainButtonFrame.Margin = new System.Windows.Forms.Padding(0);
            this.mainButtonFrame.Name = "mainButtonFrame";
            this.mainButtonFrame.Size = new System.Drawing.Size(1110, 59);
            this.mainButtonFrame.TabIndex = 0;
            this.mainButtonFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.MainButtonFrame_Paint);
            this.mainButtonFrame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainButtonFrame_MouseDown);
            // 
            // btn_debuglog
            // 
            this.btn_debuglog.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_debuglog.BackgroundImage = global::Nucleus.Coop.Properties.Resources.log1;
            this.btn_debuglog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_debuglog.FlatAppearance.BorderSize = 0;
            this.btn_debuglog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_debuglog.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_debuglog.Location = new System.Drawing.Point(450, 8);
            this.btn_debuglog.Margin = new System.Windows.Forms.Padding(2);
            this.btn_debuglog.Name = "btn_debuglog";
            this.btn_debuglog.Size = new System.Drawing.Size(30, 30);
            this.btn_debuglog.TabIndex = 101;
            this.btn_debuglog.UseVisualStyleBackColor = false;
            this.btn_debuglog.Visible = false;
            this.btn_debuglog.VisibleChanged += new System.EventHandler(this.Btn_debuglog_VisibleChanged);
            this.btn_debuglog.Click += new System.EventHandler(this.Btn_debuglog_Click);
            this.btn_debuglog.MouseEnter += new System.EventHandler(this.Btn_debuglog_MouseEnter);
            this.btn_debuglog.MouseLeave += new System.EventHandler(this.Btn_debuglog_MouseLeave);
            // 
            // btn_Extract
            // 
            this.btn_Extract.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_Extract.BackColor = System.Drawing.Color.Transparent;
            this.btn_Extract.BackgroundImage = global::Nucleus.Coop.Properties.Resources.extract_nc;
            this.btn_Extract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Extract.FlatAppearance.BorderSize = 0;
            this.btn_Extract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Extract.ForeColor = System.Drawing.Color.White;
            this.btn_Extract.Location = new System.Drawing.Point(485, 8);
            this.btn_Extract.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(30, 30);
            this.btn_Extract.TabIndex = 100;
            this.btn_Extract.UseVisualStyleBackColor = false;
            this.btn_Extract.Click += new System.EventHandler(this.Btn_Extract_Click);
            this.btn_Extract.MouseEnter += new System.EventHandler(this.Btn_Extract_MouseEnter);
            this.btn_Extract.MouseLeave += new System.EventHandler(this.Btn_Extract_MouseLeave);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::Nucleus.Coop.Properties.Resources.search_game;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(520, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 30);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.MouseEnter += new System.EventHandler(this.BtnSearch_MouseEnter);
            this.btnSearch.MouseLeave += new System.EventHandler(this.BtnSearch_MouseLeave);
            // 
            // linksPanel
            // 
            this.linksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linksPanel.BackColor = System.Drawing.Color.Black;
            this.linksPanel.Controls.Add(this.btn_faq);
            this.linksPanel.Controls.Add(this.btn_reddit);
            this.linksPanel.Controls.Add(this.btn_Discord);
            this.linksPanel.Controls.Add(this.btn_SplitCalculator);
            this.linksPanel.Controls.Add(this.btn_thirdPartytools);
            this.linksPanel.Location = new System.Drawing.Point(858, 4);
            this.linksPanel.Name = "linksPanel";
            this.linksPanel.Size = new System.Drawing.Size(150, 30);
            this.linksPanel.TabIndex = 0;
            this.linksPanel.Visible = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeBtn.BackgroundImage")));
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(1084, 6);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(20, 20);
            this.closeBtn.TabIndex = 16;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.MouseEnter += new System.EventHandler(this.CloseBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.CloseBtn_MouseLeave);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.maximizeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("maximizeBtn.BackgroundImage")));
            this.maximizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.Location = new System.Drawing.Point(1060, 6);
            this.maximizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(20, 20);
            this.maximizeBtn.TabIndex = 16;
            this.maximizeBtn.UseVisualStyleBackColor = false;
            this.maximizeBtn.MouseEnter += new System.EventHandler(this.MaximizeBtn_MouseEnter);
            this.maximizeBtn.MouseLeave += new System.EventHandler(this.MaximizeBtn_MouseLeave);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("minimizeBtn.BackgroundImage")));
            this.minimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Location = new System.Drawing.Point(1036, 6);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(20, 20);
            this.minimizeBtn.TabIndex = 16;
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.MouseEnter += new System.EventHandler(this.MinimizeBtn_MouseEnter);
            this.minimizeBtn.MouseLeave += new System.EventHandler(this.MinimizeBtn_MouseLeave);
            // 
            // txt_version
            // 
            this.txt_version.AutoSize = true;
            this.txt_version.BackColor = System.Drawing.Color.Transparent;
            this.txt_version.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txt_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_version.ForeColor = System.Drawing.Color.White;
            this.txt_version.Location = new System.Drawing.Point(152, 21);
            this.txt_version.Margin = new System.Windows.Forms.Padding(0);
            this.txt_version.Name = "txt_version";
            this.txt_version.Size = new System.Drawing.Size(28, 13);
            this.txt_version.TabIndex = 35;
            this.txt_version.Text = "vxxx";
            this.txt_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_StepTitle
            // 
            this.label_StepTitle.AutoSize = true;
            this.label_StepTitle.BackColor = System.Drawing.Color.Transparent;
            this.label_StepTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.label_StepTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_StepTitle.Location = new System.Drawing.Point(11, 37);
            this.label_StepTitle.Margin = new System.Windows.Forms.Padding(0);
            this.label_StepTitle.Name = "label_StepTitle";
            this.label_StepTitle.Size = new System.Drawing.Size(86, 15);
            this.label_StepTitle.TabIndex = 3;
            this.label_StepTitle.Text = "Select a game";
            this.label_StepTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_StepTitle.Visible = false;
            // 
            // stepPanelPictureBox
            // 
            this.stepPanelPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stepPanelPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.stepPanelPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stepPanelPictureBox.Location = new System.Drawing.Point(392, 175);
            this.stepPanelPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.stepPanelPictureBox.Name = "stepPanelPictureBox";
            this.stepPanelPictureBox.Size = new System.Drawing.Size(532, 306);
            this.stepPanelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stepPanelPictureBox.TabIndex = 15;
            this.stepPanelPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.ControlBox = false;
            this.Controls.Add(this.clientAreaPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1120, 640);
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nucleus Co-op";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.gameContextMenuStrip.ResumeLayout(false);
            this.third_party_tools_container.ResumeLayout(false);
            this.clientAreaPanel.ResumeLayout(false);
            this.game_listSizer.ResumeLayout(false);
            this.rightFrame.ResumeLayout(false);
            this.rightFrame.PerformLayout();
            this.scriptAuthorTxtSizer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_textSwitcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_magnifier)).EndInit();
            this.cover.ResumeLayout(false);
            this.coverFrame.ResumeLayout(false);
            this.mainButtonFrame.ResumeLayout(false);
            this.mainButtonFrame.PerformLayout();
            this.linksPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stepPanelPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Gaming.ControlListBox list_Games;
        private System.Windows.Forms.Label label_StepTitle;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ContextMenuStrip gameContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem nullToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem changeIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOrigExePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteContentFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserProfileConfigPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openUserProfileSavePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserProfileSavePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem openDocumentConfigPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDocumentConfigPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDocumentSavePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDocumentSavePathToolStripMenuItem;
		private System.Windows.Forms.PictureBox stepPanelPictureBox;
        private Button minimizeBtn;
        private Button maximizeBtn;
        private PictureBox logo;
        private BufferedClientAreaPanel scriptAuthorTxtSizer;
        private BufferedClientAreaPanel rightFrame;
        private Label txt_version;
        private Button btn_faq;
        private Button btn_thirdPartytools;
        private Panel third_party_tools_container;
        private Label linkLabel3;
        private Label linkLabel2;
        private Label linkLabel1;
        private Button btn_Extract;
        private Button btn_SplitCalculator;
        private Label linkLabel4;
        private Button btn_Discord;
        private Button btn_reddit;
        public BufferedClientAreaPanel clientAreaPanel;
        private BufferedClientAreaPanel linksPanel;
        private Button btn_Links;
        private Label HandlerNoteTitle;
        private PictureBox btn_magnifier;
        private ToolStripMenuItem updateHandlerToolStripMenuItem;
        public BufferedFlowLayoutPanel icons_Container;
        private Button closeBtn;
        public Button btn_settings;
        private ToolStripMenuItem keepInstancesFolderToolStripMenuItem;
        public PictureBox btn_textSwitcher;
        public BufferedClientAreaPanel coverFrame;
        public BufferedClientAreaPanel cover;
        public BufferedClientAreaPanel StepPanel;
        public Button btn_gameOptions;
        public Button button_UpdateAvailable;
        public BufferedClientAreaPanel game_listSizer;
        public BufferedClientAreaPanel mainButtonFrame;
        public Button btn_downloadAssets;
        public RichTextBox scriptAuthorTxt;
        public Button btn_debuglog;
        public Button btn_Steam;
    }
}