using Nucleus.Coop.Controls;
using Nucleus.Gaming.Controls;
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
            this.gameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gameNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.notesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHandlerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOrigExePathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openUserProfConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserProfConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openUserProfSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserProfSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentConfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDocumentConfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDocumentSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBackupFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBackupFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.keepInstancesFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableProfilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableHandlerUpdateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteContentFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.changeIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameAssetsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenshotsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.socialLinksMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fAQMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitCalculatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.thirdPartyToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dS4WindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scpToolkitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientAreaPanel = new BufferedClientAreaPanel();
            this.StepPanel = new BufferedClientAreaPanel();
            this.game_listSizer = new BufferedClientAreaPanel();
            this.list_Games = new Nucleus.Gaming.ControlListBox();
            this.btn_debuglog = new System.Windows.Forms.Button();
            this.btn_downloadAssets = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.rightFrame = new BufferedClientAreaPanel();
            this.stepButtonsPanel = new BufferedClientAreaPanel();
            this.btn_Play = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_expandNotes = new System.Windows.Forms.PictureBox();
            this.infoPanel = new BufferedClientAreaPanel();
            this.playTimeValue = new System.Windows.Forms.Label();
            this.lastPlayedAtValue = new System.Windows.Forms.Label();
            this.lastPlayedAt = new System.Windows.Forms.Label();
            this.playTime = new System.Windows.Forms.Label();
            this.icons_Container = new BufferedFlowLayoutPanel();
            this.scriptAuthorTxtSizer = new BufferedClientAreaPanel();
            this.scriptAuthorTxt = new Nucleus.Gaming.Controls.TransparentRichTextBox();
            this.HandlerNoteTitle = new System.Windows.Forms.Label();
            this.cover = new BufferedClientAreaPanel();
            this.coverFrame = new BufferedClientAreaPanel();
            this.mainButtonFrame = new BufferedClientAreaPanel();
            this.InputsTextLabel = new System.Windows.Forms.Label();
            this.setupButtonsPanel = new BufferedClientAreaPanel();
            this.button_UpdateAvailable = new System.Windows.Forms.Button();
            this.saveProfileRadioBtn = new Nucleus.Coop.Controls.CustomRadioButton();
            this.btn_gameOptions = new System.Windows.Forms.Button();
            this.profileSettings_btn = new System.Windows.Forms.Button();
            this.profilesList_btn = new System.Windows.Forms.Button();
            this.instruction_btn = new System.Windows.Forms.Button();
            this.donationBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.btn_Links = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.txt_version = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.stepPanelPictureBox = new System.Windows.Forms.PictureBox();
            this.gameContextMenuStrip.SuspendLayout();
            this.socialLinksMenu.SuspendLayout();
            this.clientAreaPanel.SuspendLayout();
            this.game_listSizer.SuspendLayout();
            this.rightFrame.SuspendLayout();
            this.stepButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_expandNotes)).BeginInit();
            this.infoPanel.SuspendLayout();
            this.scriptAuthorTxtSizer.SuspendLayout();
            this.cover.SuspendLayout();
            this.mainButtonFrame.SuspendLayout();
            this.setupButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepPanelPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameContextMenuStrip
            // 
            this.gameContextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gameContextMenuStrip.DropShadowEnabled = false;
            this.gameContextMenuStrip.ImageScalingSize = new System.Drawing.Size(15, 15);
            this.gameContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameNameMenuItem,
            this.menuSeparator1,
            this.notesMenuItem,
            this.detailsMenuItem,
            this.openHandlerMenuItem,
            this.openDataFolderMenuItem,
            this.openOrigExePathMenuItem,
            this.menuSeparator2,
            this.openUserProfConfigMenuItem,
            this.deleteUserProfConfigMenuItem,
            this.openUserProfSaveMenuItem,
            this.deleteUserProfSaveMenuItem,
            this.openDocumentConfMenuItem,
            this.deleteDocumentConfMenuItem,
            this.openDocumentSaveMenuItem,
            this.deleteDocumentSaveMenuItem,
            this.openBackupFolderMenuItem,
            this.deleteBackupFolderMenuItem,
            this.toolStripSeparator3,
            this.keepInstancesFolderMenuItem,
            this.disableProfilesMenuItem,
            this.disableHandlerUpdateMenuItem,
            this.toolStripSeparator4,
            this.removeGameMenuItem,
            this.deleteContentFolderMenuItem,
            this.menuSeparator3,
            this.changeIconMenuItem,
            this.gameAssetsMenuItem});
            this.gameContextMenuStrip.Name = "gameContextMenuStrip";
            this.gameContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.gameContextMenuStrip.Size = new System.Drawing.Size(235, 540);
            this.gameContextMenuStrip.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.GameContextMenuStrip_Closing);
            this.gameContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.GameContextMenuStrip_Opening);
            this.gameContextMenuStrip.Opened += new System.EventHandler(this.GameContextMenuStrip_Opened);
            // 
            // gameNameMenuItem
            // 
            this.gameNameMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.gameNameMenuItem.Name = "gameNameMenuItem";
            this.gameNameMenuItem.Size = new System.Drawing.Size(234, 22);
            this.gameNameMenuItem.Text = "null";
            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Name = "menuSeparator1";
            this.menuSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // notesMenuItem
            // 
            this.notesMenuItem.Name = "notesMenuItem";
            this.notesMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.notesMenuItem.Size = new System.Drawing.Size(234, 22);
            this.notesMenuItem.Text = "Handler Author\'s Notes";
            this.notesMenuItem.Visible = false;
            this.notesMenuItem.Click += new System.EventHandler(this.NotesMenuItem_Click);
            // 
            // detailsMenuItem
            // 
            this.detailsMenuItem.Name = "detailsMenuItem";
            this.detailsMenuItem.Size = new System.Drawing.Size(234, 22);
            this.detailsMenuItem.Text = "Nucleus Game Details";
            this.detailsMenuItem.Click += new System.EventHandler(this.DetailsToolStripMenuItem_Click);
            // 
            // openHandlerMenuItem
            // 
            this.openHandlerMenuItem.Name = "openHandlerMenuItem";
            this.openHandlerMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openHandlerMenuItem.Text = "Open Game Handler";
            this.openHandlerMenuItem.Click += new System.EventHandler(this.OpenHandlerMenuItem_Click);
            // 
            // openDataFolderMenuItem
            // 
            this.openDataFolderMenuItem.Name = "openDataFolderMenuItem";
            this.openDataFolderMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openDataFolderMenuItem.Text = "Open Nucleus Content Folder";
            this.openDataFolderMenuItem.Click += new System.EventHandler(this.OpenDataFolderMenuItem_Click);
            // 
            // openOrigExePathMenuItem
            // 
            this.openOrigExePathMenuItem.Name = "openOrigExePathMenuItem";
            this.openOrigExePathMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openOrigExePathMenuItem.Text = "Open Original Exe Path";
            this.openOrigExePathMenuItem.Click += new System.EventHandler(this.OpenOrigExePathMenuItem_Click);
            // 
            // menuSeparator2
            // 
            this.menuSeparator2.Name = "menuSeparator2";
            this.menuSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // openUserProfConfigMenuItem
            // 
            this.openUserProfConfigMenuItem.Name = "openUserProfConfigMenuItem";
            this.openUserProfConfigMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openUserProfConfigMenuItem.Text = "Open UserProfile Config Path";
            this.openUserProfConfigMenuItem.Visible = false;
            // 
            // deleteUserProfConfigMenuItem
            // 
            this.deleteUserProfConfigMenuItem.Name = "deleteUserProfConfigMenuItem";
            this.deleteUserProfConfigMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteUserProfConfigMenuItem.Text = "Delete UserProfile Config Path";
            this.deleteUserProfConfigMenuItem.Visible = false;
            // 
            // openUserProfSaveMenuItem
            // 
            this.openUserProfSaveMenuItem.Name = "openUserProfSaveMenuItem";
            this.openUserProfSaveMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openUserProfSaveMenuItem.Text = "Open UserProfile Save Path";
            this.openUserProfSaveMenuItem.Visible = false;
            // 
            // deleteUserProfSaveMenuItem
            // 
            this.deleteUserProfSaveMenuItem.Name = "deleteUserProfSaveMenuItem";
            this.deleteUserProfSaveMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteUserProfSaveMenuItem.Text = "Delete UserProfile Save Path";
            this.deleteUserProfSaveMenuItem.Visible = false;
            // 
            // openDocumentConfMenuItem
            // 
            this.openDocumentConfMenuItem.Name = "openDocumentConfMenuItem";
            this.openDocumentConfMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openDocumentConfMenuItem.Text = "Open Document Config Path";
            this.openDocumentConfMenuItem.Visible = false;
            // 
            // deleteDocumentConfMenuItem
            // 
            this.deleteDocumentConfMenuItem.Name = "deleteDocumentConfMenuItem";
            this.deleteDocumentConfMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteDocumentConfMenuItem.Text = "Delete Document Config Path";
            this.deleteDocumentConfMenuItem.Visible = false;
            // 
            // openDocumentSaveMenuItem
            // 
            this.openDocumentSaveMenuItem.Name = "openDocumentSaveMenuItem";
            this.openDocumentSaveMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openDocumentSaveMenuItem.Text = "Open Document Save Path";
            this.openDocumentSaveMenuItem.Visible = false;
            // 
            // deleteDocumentSaveMenuItem
            // 
            this.deleteDocumentSaveMenuItem.Name = "deleteDocumentSaveMenuItem";
            this.deleteDocumentSaveMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteDocumentSaveMenuItem.Text = "Delete Document Save Path";
            this.deleteDocumentSaveMenuItem.Visible = false;
            // 
            // openBackupFolderMenuItem
            // 
            this.openBackupFolderMenuItem.Name = "openBackupFolderMenuItem";
            this.openBackupFolderMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openBackupFolderMenuItem.Text = "Open Backup Folder";
            // 
            // deleteBackupFolderMenuItem
            // 
            this.deleteBackupFolderMenuItem.Name = "deleteBackupFolderMenuItem";
            this.deleteBackupFolderMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteBackupFolderMenuItem.Text = "Delete Backup Folder";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // keepInstancesFolderMenuItem
            // 
            this.keepInstancesFolderMenuItem.Name = "keepInstancesFolderMenuItem";
            this.keepInstancesFolderMenuItem.Size = new System.Drawing.Size(234, 22);
            this.keepInstancesFolderMenuItem.Text = "Keep Instances Content Folder";
            this.keepInstancesFolderMenuItem.Click += new System.EventHandler(this.KeepInstancesFolderMenuItem_Click);
            // 
            // disableProfilesMenuItem
            // 
            this.disableProfilesMenuItem.Name = "disableProfilesMenuItem";
            this.disableProfilesMenuItem.Size = new System.Drawing.Size(234, 22);
            this.disableProfilesMenuItem.Text = "Disable Profile";
            this.disableProfilesMenuItem.Click += new System.EventHandler(this.DisableProfilesMenuItem_Click);
            // 
            // disableHandlerUpdateMenuItem
            // 
            this.disableHandlerUpdateMenuItem.Name = "disableHandlerUpdateMenuItem";
            this.disableHandlerUpdateMenuItem.Size = new System.Drawing.Size(234, 22);
            this.disableHandlerUpdateMenuItem.Text = "Disable Handler Update";
            this.disableHandlerUpdateMenuItem.Click += new System.EventHandler(this.DisableHandlerUpdateMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(231, 6);
            // 
            // removeGameMenuItem
            // 
            this.removeGameMenuItem.Name = "removeGameMenuItem";
            this.removeGameMenuItem.Size = new System.Drawing.Size(234, 22);
            this.removeGameMenuItem.Text = "Remove Game From List";
            this.removeGameMenuItem.Click += new System.EventHandler(this.RemoveGameMenuItem_Click);
            // 
            // deleteContentFolderMenuItem
            // 
            this.deleteContentFolderMenuItem.Name = "deleteContentFolderMenuItem";
            this.deleteContentFolderMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteContentFolderMenuItem.Text = "Delete Game Content Folder";
            this.deleteContentFolderMenuItem.Click += new System.EventHandler(this.DeleteContentFolderMenuItem_Click);
            // 
            // menuSeparator3
            // 
            this.menuSeparator3.Name = "menuSeparator3";
            this.menuSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // changeIconMenuItem
            // 
            this.changeIconMenuItem.Name = "changeIconMenuItem";
            this.changeIconMenuItem.Size = new System.Drawing.Size(234, 22);
            this.changeIconMenuItem.Text = "Change Game Icon";
            this.changeIconMenuItem.Click += new System.EventHandler(this.ChangeIconMenuItem_Click);
            // 
            // gameAssetsMenuItem
            // 
            this.gameAssetsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coverMenuItem,
            this.screenshotsMenuItem});
            this.gameAssetsMenuItem.Name = "gameAssetsMenuItem";
            this.gameAssetsMenuItem.Size = new System.Drawing.Size(234, 22);
            this.gameAssetsMenuItem.Text = "Game Assets";
            // 
            // coverMenuItem
            // 
            this.coverMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.coverMenuItem.Name = "coverMenuItem";
            this.coverMenuItem.Size = new System.Drawing.Size(205, 22);
            this.coverMenuItem.Text = "Open Cover Folder";
            this.coverMenuItem.Click += new System.EventHandler(this.CoverMenuItem_Click);
            // 
            // screenshotsMenuItem
            // 
            this.screenshotsMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.screenshotsMenuItem.Name = "screenshotsMenuItem";
            this.screenshotsMenuItem.Size = new System.Drawing.Size(205, 22);
            this.screenshotsMenuItem.Text = "Open Screenshots Folder";
            this.screenshotsMenuItem.Click += new System.EventHandler(this.ScreenshotsMenuItem_Click);
            // 
            // socialLinksMenu
            // 
            this.socialLinksMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.socialLinksMenu.DropShadowEnabled = false;
            this.socialLinksMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fAQMenuItem,
            this.discordMenuItem,
            this.redditMenuItem,
            this.toolStripSeparator1,
            this.splitCalculatorMenuItem,
            this.toolStripSeparator2,
            this.thirdPartyToolsToolStripMenuItem});
            this.socialLinksMenu.Name = "socialLinksMenu";
            this.socialLinksMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.socialLinksMenu.ShowImageMargin = false;
            this.socialLinksMenu.Size = new System.Drawing.Size(137, 126);
            this.socialLinksMenu.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.SocialLinksMenu_Closing);
            this.socialLinksMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SocialLinksMenu_Opening);
            this.socialLinksMenu.Opened += new System.EventHandler(this.SocialLinksMenu_Opened_1);
            // 
            // fAQMenuItem
            // 
            this.fAQMenuItem.Name = "fAQMenuItem";
            this.fAQMenuItem.Size = new System.Drawing.Size(136, 22);
            this.fAQMenuItem.Text = "FAQ";
            this.fAQMenuItem.Click += new System.EventHandler(this.FAQToolStripMenuItem_Click);
            // 
            // discordMenuItem
            // 
            this.discordMenuItem.Name = "discordMenuItem";
            this.discordMenuItem.Size = new System.Drawing.Size(136, 22);
            this.discordMenuItem.Text = "Discord";
            this.discordMenuItem.Click += new System.EventHandler(this.DiscordToolStripMenuItem_Click);
            // 
            // redditMenuItem
            // 
            this.redditMenuItem.Name = "redditMenuItem";
            this.redditMenuItem.Size = new System.Drawing.Size(136, 22);
            this.redditMenuItem.Text = "Reddit";
            this.redditMenuItem.Click += new System.EventHandler(this.RedditToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // splitCalculatorMenuItem
            // 
            this.splitCalculatorMenuItem.Name = "splitCalculatorMenuItem";
            this.splitCalculatorMenuItem.Size = new System.Drawing.Size(136, 22);
            this.splitCalculatorMenuItem.Text = "SplitCalculator";
            this.splitCalculatorMenuItem.Click += new System.EventHandler(this.SplitCalculatorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // thirdPartyToolsToolStripMenuItem
            // 
            this.thirdPartyToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xOutputToolStripMenuItem,
            this.dS4WindowsToolStripMenuItem,
            this.hidHideToolStripMenuItem,
            this.scpToolkitToolStripMenuItem});
            this.thirdPartyToolsToolStripMenuItem.Name = "thirdPartyToolsToolStripMenuItem";
            this.thirdPartyToolsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.thirdPartyToolsToolStripMenuItem.Text = "Third Party Tools";
            // 
            // xOutputToolStripMenuItem
            // 
            this.xOutputToolStripMenuItem.Name = "xOutputToolStripMenuItem";
            this.xOutputToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.xOutputToolStripMenuItem.Text = "XOutput";
            this.xOutputToolStripMenuItem.Click += new System.EventHandler(this.XOutputToolStripMenuItem_Click);
            // 
            // dS4WindowsToolStripMenuItem
            // 
            this.dS4WindowsToolStripMenuItem.Name = "dS4WindowsToolStripMenuItem";
            this.dS4WindowsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.dS4WindowsToolStripMenuItem.Text = "DS4Windows";
            this.dS4WindowsToolStripMenuItem.Click += new System.EventHandler(this.DS4WindowsToolStripMenuItem_Click);
            // 
            // hidHideToolStripMenuItem
            // 
            this.hidHideToolStripMenuItem.Name = "hidHideToolStripMenuItem";
            this.hidHideToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.hidHideToolStripMenuItem.Text = "HidHide";
            this.hidHideToolStripMenuItem.Click += new System.EventHandler(this.HidHideToolStripMenuItem_Click);
            // 
            // scpToolkitToolStripMenuItem
            // 
            this.scpToolkitToolStripMenuItem.Name = "scpToolkitToolStripMenuItem";
            this.scpToolkitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.scpToolkitToolStripMenuItem.Text = "ScpToolkit";
            this.scpToolkitToolStripMenuItem.Click += new System.EventHandler(this.ScpToolkitToolStripMenuItem_Click);
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
            this.clientAreaPanel.Size = new System.Drawing.Size(1166, 655);
            this.clientAreaPanel.TabIndex = 34;
            this.clientAreaPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ClientAreaPanel_Paint);
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
            this.StepPanel.Location = new System.Drawing.Point(209, 58);
            this.StepPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StepPanel.Name = "StepPanel";
            this.StepPanel.Size = new System.Drawing.Size(771, 597);
            this.StepPanel.TabIndex = 0;
            this.StepPanel.Visible = false;
            this.StepPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.StepPanel_Paint);
            // 
            // game_listSizer
            // 
            this.game_listSizer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.game_listSizer.BackColor = System.Drawing.Color.Transparent;
            this.game_listSizer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.game_listSizer.Controls.Add(this.list_Games);
            this.game_listSizer.Controls.Add(this.btn_debuglog);
            this.game_listSizer.Controls.Add(this.btn_downloadAssets);
            this.game_listSizer.Controls.Add(this.btn_settings);
            this.game_listSizer.Controls.Add(this.btnSearch);
            this.game_listSizer.Controls.Add(this.btn_Extract);
            this.game_listSizer.Location = new System.Drawing.Point(0, 58);
            this.game_listSizer.Margin = new System.Windows.Forms.Padding(0);
            this.game_listSizer.Name = "game_listSizer";
            this.game_listSizer.Size = new System.Drawing.Size(209, 597);
            this.game_listSizer.TabIndex = 35;
            this.game_listSizer.Paint += new System.Windows.Forms.PaintEventHandler(this.Game_listSizer_Paint);
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
            this.list_Games.Location = new System.Drawing.Point(0, 2);
            this.list_Games.Margin = new System.Windows.Forms.Padding(0);
            this.list_Games.Name = "list_Games";
            this.list_Games.Offset = new System.Drawing.Size(0, 0);
            this.list_Games.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.list_Games.Size = new System.Drawing.Size(230, 561);
            this.list_Games.TabIndex = 2;
            this.list_Games.SelectedChanged += new System.Action<object, System.Windows.Forms.Control>(this.List_Games_SelectedChanged);
            // 
            // btn_debuglog
            // 
            this.btn_debuglog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_debuglog.BackgroundImage = global::Nucleus.Coop.Properties.Resources.log1;
            this.btn_debuglog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_debuglog.FlatAppearance.BorderSize = 0;
            this.btn_debuglog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_debuglog.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_debuglog.Location = new System.Drawing.Point(123, 565);
            this.btn_debuglog.Margin = new System.Windows.Forms.Padding(2);
            this.btn_debuglog.Name = "btn_debuglog";
            this.btn_debuglog.Size = new System.Drawing.Size(30, 30);
            this.btn_debuglog.TabIndex = 101;
            this.btn_debuglog.UseVisualStyleBackColor = false;
            this.btn_debuglog.Click += new System.EventHandler(this.Btn_debuglog_Click);
            this.btn_debuglog.MouseEnter += new System.EventHandler(this.Btn_debuglog_MouseEnter);
            this.btn_debuglog.MouseLeave += new System.EventHandler(this.Btn_debuglog_MouseLeave);
            // 
            // btn_downloadAssets
            // 
            this.btn_downloadAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_downloadAssets.BackColor = System.Drawing.Color.Transparent;
            this.btn_downloadAssets.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_downloadAssets.BackgroundImage")));
            this.btn_downloadAssets.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_downloadAssets.FlatAppearance.BorderSize = 0;
            this.btn_downloadAssets.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_downloadAssets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_downloadAssets.Location = new System.Drawing.Point(89, 565);
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
            // btn_settings
            // 
            this.btn_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_settings.BackColor = System.Drawing.Color.Transparent;
            this.btn_settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_settings.BackgroundImage")));
            this.btn_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_settings.FlatAppearance.BorderSize = 0;
            this.btn_settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_settings.Location = new System.Drawing.Point(157, 565);
            this.btn_settings.Margin = new System.Windows.Forms.Padding(2);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(30, 30);
            this.btn_settings.TabIndex = 16;
            this.btn_settings.UseVisualStyleBackColor = false;
            this.btn_settings.Click += new System.EventHandler(this.SettingsBtn_Click);
            this.btn_settings.MouseEnter += new System.EventHandler(this.Btn_settings_MouseEnter);
            this.btn_settings.MouseLeave += new System.EventHandler(this.Btn_settings_MouseLeave);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::Nucleus.Coop.Properties.Resources.search_game;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(55, 565);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 30);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.MouseEnter += new System.EventHandler(this.BtnSearch_MouseEnter);
            this.btnSearch.MouseLeave += new System.EventHandler(this.BtnSearch_MouseLeave);
            // 
            // btn_Extract
            // 
            this.btn_Extract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Extract.BackColor = System.Drawing.Color.Transparent;
            this.btn_Extract.BackgroundImage = global::Nucleus.Coop.Properties.Resources.extract_nc;
            this.btn_Extract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Extract.FlatAppearance.BorderSize = 0;
            this.btn_Extract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Extract.ForeColor = System.Drawing.Color.White;
            this.btn_Extract.Location = new System.Drawing.Point(20, 565);
            this.btn_Extract.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(30, 30);
            this.btn_Extract.TabIndex = 100;
            this.btn_Extract.UseVisualStyleBackColor = false;
            this.btn_Extract.Click += new System.EventHandler(this.Btn_Extract_Click);
            this.btn_Extract.MouseEnter += new System.EventHandler(this.Btn_Extract_MouseEnter);
            this.btn_Extract.MouseLeave += new System.EventHandler(this.Btn_Extract_MouseLeave);
            // 
            // rightFrame
            // 
            this.rightFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightFrame.BackColor = System.Drawing.Color.Transparent;
            this.rightFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightFrame.Controls.Add(this.stepButtonsPanel);
            this.rightFrame.Controls.Add(this.btn_expandNotes);
            this.rightFrame.Controls.Add(this.infoPanel);
            this.rightFrame.Controls.Add(this.icons_Container);
            this.rightFrame.Controls.Add(this.scriptAuthorTxtSizer);
            this.rightFrame.Controls.Add(this.cover);
            this.rightFrame.Location = new System.Drawing.Point(980, 58);
            this.rightFrame.Margin = new System.Windows.Forms.Padding(0);
            this.rightFrame.Name = "rightFrame";
            this.rightFrame.Size = new System.Drawing.Size(186, 597);
            this.rightFrame.TabIndex = 34;
            this.rightFrame.Visible = false;
            this.rightFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.RightFrame_Paint);
            // 
            // stepButtonsPanel
            // 
            this.stepButtonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.stepButtonsPanel.Controls.Add(this.btn_Play);
            this.stepButtonsPanel.Controls.Add(this.btn_Prev);
            this.stepButtonsPanel.Controls.Add(this.btn_Next);
            this.stepButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.stepButtonsPanel.Name = "stepButtonsPanel";
            this.stepButtonsPanel.Size = new System.Drawing.Size(186, 26);
            this.stepButtonsPanel.TabIndex = 40;
            // 
            // btn_Play
            // 
            this.btn_Play.BackColor = System.Drawing.Color.Transparent;
            this.btn_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Play.Enabled = false;
            this.btn_Play.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Play.FlatAppearance.BorderSize = 0;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Play.ForeColor = System.Drawing.Color.Lime;
            this.btn_Play.Location = new System.Drawing.Point(43, 0);
            this.btn_Play.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(99, 25);
            this.btn_Play.TabIndex = 4;
            this.btn_Play.Text = "START";
            this.btn_Play.UseVisualStyleBackColor = false;
            this.btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.BackColor = System.Drawing.Color.Transparent;
            this.btn_Prev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Prev.Enabled = false;
            this.btn_Prev.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Prev.FlatAppearance.BorderSize = 0;
            this.btn_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Prev.Location = new System.Drawing.Point(2, 0);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(25, 25);
            this.btn_Prev.TabIndex = 9;
            this.btn_Prev.UseVisualStyleBackColor = false;
            this.btn_Prev.Click += new System.EventHandler(this.Btn_Prev_Click);
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
            this.btn_Next.Location = new System.Drawing.Point(159, 0);
            this.btn_Next.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(25, 25);
            this.btn_Next.TabIndex = 11;
            this.btn_Next.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_Next.UseVisualStyleBackColor = false;
            this.btn_Next.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btn_expandNotes
            // 
            this.btn_expandNotes.BackColor = System.Drawing.Color.Transparent;
            this.btn_expandNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_expandNotes.Location = new System.Drawing.Point(157, 347);
            this.btn_expandNotes.Name = "btn_expandNotes";
            this.btn_expandNotes.Size = new System.Drawing.Size(20, 20);
            this.btn_expandNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_expandNotes.TabIndex = 43;
            this.btn_expandNotes.TabStop = false;
            this.btn_expandNotes.Click += new System.EventHandler(this.Btn_magnifier_Click);
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.playTimeValue);
            this.infoPanel.Controls.Add(this.lastPlayedAtValue);
            this.infoPanel.Controls.Add(this.lastPlayedAt);
            this.infoPanel.Controls.Add(this.playTime);
            this.infoPanel.Location = new System.Drawing.Point(1, 64);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(172, 44);
            this.infoPanel.TabIndex = 39;
            // 
            // playTimeValue
            // 
            this.playTimeValue.AutoSize = true;
            this.playTimeValue.BackColor = System.Drawing.Color.Transparent;
            this.playTimeValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playTimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playTimeValue.ForeColor = System.Drawing.Color.Silver;
            this.playTimeValue.Location = new System.Drawing.Point(83, 19);
            this.playTimeValue.Name = "playTimeValue";
            this.playTimeValue.Size = new System.Drawing.Size(0, 16);
            this.playTimeValue.TabIndex = 40;
            this.playTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lastPlayedAtValue
            // 
            this.lastPlayedAtValue.AutoSize = true;
            this.lastPlayedAtValue.BackColor = System.Drawing.Color.Transparent;
            this.lastPlayedAtValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastPlayedAtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPlayedAtValue.ForeColor = System.Drawing.Color.Silver;
            this.lastPlayedAtValue.Location = new System.Drawing.Point(93, 3);
            this.lastPlayedAtValue.Name = "lastPlayedAtValue";
            this.lastPlayedAtValue.Size = new System.Drawing.Size(0, 16);
            this.lastPlayedAtValue.TabIndex = 39;
            this.lastPlayedAtValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lastPlayedAt
            // 
            this.lastPlayedAt.AutoSize = true;
            this.lastPlayedAt.BackColor = System.Drawing.Color.Transparent;
            this.lastPlayedAt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastPlayedAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPlayedAt.ForeColor = System.Drawing.Color.White;
            this.lastPlayedAt.Location = new System.Drawing.Point(3, 3);
            this.lastPlayedAt.Name = "lastPlayedAt";
            this.lastPlayedAt.Size = new System.Drawing.Size(81, 16);
            this.lastPlayedAt.TabIndex = 37;
            this.lastPlayedAt.Text = "Last Played:";
            this.lastPlayedAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playTime
            // 
            this.playTime.AutoSize = true;
            this.playTime.BackColor = System.Drawing.Color.Transparent;
            this.playTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playTime.ForeColor = System.Drawing.Color.White;
            this.playTime.Location = new System.Drawing.Point(3, 23);
            this.playTime.Name = "playTime";
            this.playTime.Size = new System.Drawing.Size(71, 16);
            this.playTime.TabIndex = 38;
            this.playTime.Text = "Play Time:";
            this.playTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // icons_Container
            // 
            this.icons_Container.AutoSize = true;
            this.icons_Container.Location = new System.Drawing.Point(3, 36);
            this.icons_Container.Name = "icons_Container";
            this.icons_Container.Size = new System.Drawing.Size(44, 19);
            this.icons_Container.TabIndex = 32;
            // 
            // scriptAuthorTxtSizer
            // 
            this.scriptAuthorTxtSizer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptAuthorTxtSizer.BackColor = System.Drawing.Color.Transparent;
            this.scriptAuthorTxtSizer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scriptAuthorTxtSizer.Controls.Add(this.scriptAuthorTxt);
            this.scriptAuthorTxtSizer.Controls.Add(this.HandlerNoteTitle);
            this.scriptAuthorTxtSizer.Location = new System.Drawing.Point(7, 347);
            this.scriptAuthorTxtSizer.Margin = new System.Windows.Forms.Padding(5);
            this.scriptAuthorTxtSizer.Name = "scriptAuthorTxtSizer";
            this.scriptAuthorTxtSizer.Size = new System.Drawing.Size(171, 236);
            this.scriptAuthorTxtSizer.TabIndex = 31;
            // 
            // scriptAuthorTxt
            // 
            this.scriptAuthorTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptAuthorTxt.BackColor = System.Drawing.Color.Black;
            this.scriptAuthorTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scriptAuthorTxt.BulletIndent = 1;
            this.scriptAuthorTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.scriptAuthorTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.scriptAuthorTxt.ForeColor = System.Drawing.Color.White;
            this.scriptAuthorTxt.Location = new System.Drawing.Point(0, 22);
            this.scriptAuthorTxt.Margin = new System.Windows.Forms.Padding(0);
            this.scriptAuthorTxt.MinimumSize = new System.Drawing.Size(188, 192);
            this.scriptAuthorTxt.Name = "scriptAuthorTxt";
            this.scriptAuthorTxt.ReadOnly = true;
            this.scriptAuthorTxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.scriptAuthorTxt.Size = new System.Drawing.Size(188, 214);
            this.scriptAuthorTxt.TabIndex = 13;
            this.scriptAuthorTxt.Text = "";
            this.scriptAuthorTxt.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ScriptAuthorTxt_LinkClicked);
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
            this.cover.Location = new System.Drawing.Point(7, 113);
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
            this.coverFrame.Location = new System.Drawing.Point(0, 0);
            this.coverFrame.Margin = new System.Windows.Forms.Padding(0);
            this.coverFrame.Name = "coverFrame";
            this.coverFrame.Size = new System.Drawing.Size(172, 227);
            this.coverFrame.TabIndex = 26;
            // 
            // mainButtonFrame
            // 
            this.mainButtonFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainButtonFrame.BackColor = System.Drawing.Color.Transparent;
            this.mainButtonFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainButtonFrame.Controls.Add(this.InputsTextLabel);
            this.mainButtonFrame.Controls.Add(this.setupButtonsPanel);
            this.mainButtonFrame.Controls.Add(this.donationBtn);
            this.mainButtonFrame.Controls.Add(this.closeBtn);
            this.mainButtonFrame.Controls.Add(this.btn_Links);
            this.mainButtonFrame.Controls.Add(this.maximizeBtn);
            this.mainButtonFrame.Controls.Add(this.minimizeBtn);
            this.mainButtonFrame.Controls.Add(this.txt_version);
            this.mainButtonFrame.Controls.Add(this.logo);
            this.mainButtonFrame.Location = new System.Drawing.Point(0, 0);
            this.mainButtonFrame.Margin = new System.Windows.Forms.Padding(0);
            this.mainButtonFrame.Name = "mainButtonFrame";
            this.mainButtonFrame.Size = new System.Drawing.Size(1166, 58);
            this.mainButtonFrame.TabIndex = 0;
            this.mainButtonFrame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainButtonFrame_MouseDown);
            // 
            // InputsTextLabel
            // 
            this.InputsTextLabel.AutoSize = true;
            this.InputsTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputsTextLabel.Location = new System.Drawing.Point(209, 35);
            this.InputsTextLabel.Name = "InputsTextLabel";
            this.InputsTextLabel.Size = new System.Drawing.Size(0, 16);
            this.InputsTextLabel.TabIndex = 104;
            // 
            // setupButtonsPanel
            // 
            this.setupButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setupButtonsPanel.Controls.Add(this.button_UpdateAvailable);
            this.setupButtonsPanel.Controls.Add(this.saveProfileRadioBtn);
            this.setupButtonsPanel.Controls.Add(this.btn_gameOptions);
            this.setupButtonsPanel.Controls.Add(this.profileSettings_btn);
            this.setupButtonsPanel.Controls.Add(this.profilesList_btn);
            this.setupButtonsPanel.Controls.Add(this.instruction_btn);
            this.setupButtonsPanel.Location = new System.Drawing.Point(683, 29);
            this.setupButtonsPanel.Name = "setupButtonsPanel";
            this.setupButtonsPanel.Size = new System.Drawing.Size(297, 28);
            this.setupButtonsPanel.TabIndex = 103;
            this.setupButtonsPanel.Visible = false;
            // 
            // button_UpdateAvailable
            // 
            this.button_UpdateAvailable.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_UpdateAvailable.BackColor = System.Drawing.Color.Transparent;
            this.button_UpdateAvailable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_UpdateAvailable.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_UpdateAvailable.FlatAppearance.BorderSize = 0;
            this.button_UpdateAvailable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_UpdateAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_UpdateAvailable.ForeColor = System.Drawing.Color.Yellow;
            this.button_UpdateAvailable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_UpdateAvailable.Location = new System.Drawing.Point(176, 3);
            this.button_UpdateAvailable.Margin = new System.Windows.Forms.Padding(5);
            this.button_UpdateAvailable.Name = "button_UpdateAvailable";
            this.button_UpdateAvailable.Size = new System.Drawing.Size(21, 21);
            this.button_UpdateAvailable.TabIndex = 23;
            this.button_UpdateAvailable.UseVisualStyleBackColor = false;
            this.button_UpdateAvailable.Visible = false;
            this.button_UpdateAvailable.Click += new System.EventHandler(this.Button_UpdateAvailable_Click);
            // 
            // saveProfileRadioBtn
            // 
            this.saveProfileRadioBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProfileRadioBtn.AutoSize = true;
            this.saveProfileRadioBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveProfileRadioBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveProfileRadioBtn.Location = new System.Drawing.Point(1, 6);
            this.saveProfileRadioBtn.Margin = new System.Windows.Forms.Padding(1);
            this.saveProfileRadioBtn.Name = "saveProfileRadioBtn";
            this.saveProfileRadioBtn.RadioBackColor = System.Drawing.Color.Transparent;
            this.saveProfileRadioBtn.RadioChecked = true;
            this.saveProfileRadioBtn.RadioText = "Save Profile";
            this.saveProfileRadioBtn.RadioTooltipText = " If turned off the current setup will not be saved to a new profile.";
            this.saveProfileRadioBtn.Size = new System.Drawing.Size(109, 16);
            this.saveProfileRadioBtn.TabIndex = 105;
            this.saveProfileRadioBtn.TextColor = System.Drawing.Color.White;
            this.saveProfileRadioBtn.Click += new System.EventHandler(this.SaveProfileRadioBtn_Click);
            // 
            // btn_gameOptions
            // 
            this.btn_gameOptions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_gameOptions.BackColor = System.Drawing.Color.Transparent;
            this.btn_gameOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gameOptions.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_gameOptions.FlatAppearance.BorderSize = 0;
            this.btn_gameOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gameOptions.Location = new System.Drawing.Point(249, 3);
            this.btn_gameOptions.Margin = new System.Windows.Forms.Padding(1);
            this.btn_gameOptions.Name = "btn_gameOptions";
            this.btn_gameOptions.Size = new System.Drawing.Size(21, 21);
            this.btn_gameOptions.TabIndex = 21;
            this.btn_gameOptions.UseVisualStyleBackColor = false;
            this.btn_gameOptions.Click += new System.EventHandler(this.GameOptions_Click);
            // 
            // profileSettings_btn
            // 
            this.profileSettings_btn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.profileSettings_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.profileSettings_btn.FlatAppearance.BorderSize = 0;
            this.profileSettings_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.profileSettings_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.profileSettings_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileSettings_btn.ForeColor = System.Drawing.Color.White;
            this.profileSettings_btn.Location = new System.Drawing.Point(226, 3);
            this.profileSettings_btn.Margin = new System.Windows.Forms.Padding(1);
            this.profileSettings_btn.Name = "profileSettings_btn";
            this.profileSettings_btn.Size = new System.Drawing.Size(21, 21);
            this.profileSettings_btn.TabIndex = 1;
            this.profileSettings_btn.UseVisualStyleBackColor = true;
            // 
            // profilesList_btn
            // 
            this.profilesList_btn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.profilesList_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.profilesList_btn.FlatAppearance.BorderSize = 0;
            this.profilesList_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.profilesList_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.profilesList_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profilesList_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilesList_btn.Location = new System.Drawing.Point(203, 3);
            this.profilesList_btn.Margin = new System.Windows.Forms.Padding(1);
            this.profilesList_btn.Name = "profilesList_btn";
            this.profilesList_btn.Size = new System.Drawing.Size(21, 21);
            this.profilesList_btn.TabIndex = 3;
            this.profilesList_btn.UseVisualStyleBackColor = true;
            // 
            // instruction_btn
            // 
            this.instruction_btn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.instruction_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.instruction_btn.FlatAppearance.BorderSize = 0;
            this.instruction_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.instruction_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.instruction_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.instruction_btn.ForeColor = System.Drawing.Color.White;
            this.instruction_btn.Location = new System.Drawing.Point(272, 3);
            this.instruction_btn.Margin = new System.Windows.Forms.Padding(1);
            this.instruction_btn.Name = "instruction_btn";
            this.instruction_btn.Size = new System.Drawing.Size(21, 21);
            this.instruction_btn.TabIndex = 2;
            this.instruction_btn.UseVisualStyleBackColor = true;
            this.instruction_btn.Click += new System.EventHandler(this.Instruction_btn_Click);
            // 
            // donationBtn
            // 
            this.donationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.donationBtn.BackColor = System.Drawing.Color.Transparent;
            this.donationBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("donationBtn.BackgroundImage")));
            this.donationBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.donationBtn.FlatAppearance.BorderSize = 0;
            this.donationBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.donationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.donationBtn.Location = new System.Drawing.Point(1068, 6);
            this.donationBtn.Margin = new System.Windows.Forms.Padding(2);
            this.donationBtn.Name = "donationBtn";
            this.donationBtn.Size = new System.Drawing.Size(20, 20);
            this.donationBtn.TabIndex = 102;
            this.donationBtn.UseVisualStyleBackColor = false;
            this.donationBtn.Click += new System.EventHandler(this.DonationBtn_Click);
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
            this.closeBtn.Location = new System.Drawing.Point(1140, 6);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(20, 20);
            this.closeBtn.TabIndex = 16;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.MouseEnter += new System.EventHandler(this.CloseBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.CloseBtn_MouseLeave);
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
            this.btn_Links.Location = new System.Drawing.Point(1040, 8);
            this.btn_Links.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Links.Name = "btn_Links";
            this.btn_Links.Size = new System.Drawing.Size(20, 20);
            this.btn_Links.TabIndex = 42;
            this.btn_Links.UseVisualStyleBackColor = false;
            this.btn_Links.Click += new System.EventHandler(this.Btn_Links_Click);
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
            this.maximizeBtn.Location = new System.Drawing.Point(1116, 6);
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
            this.minimizeBtn.Location = new System.Drawing.Point(1092, 6);
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
            this.txt_version.Location = new System.Drawing.Point(157, 23);
            this.txt_version.Margin = new System.Windows.Forms.Padding(0);
            this.txt_version.Name = "txt_version";
            this.txt_version.Size = new System.Drawing.Size(28, 13);
            this.txt_version.TabIndex = 35;
            this.txt_version.Text = "vxxx";
            this.txt_version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logo.Location = new System.Drawing.Point(10, 17);
            this.logo.Margin = new System.Windows.Forms.Padding(0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(145, 25);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 24;
            this.logo.TabStop = false;
            this.logo.DoubleClick += new System.EventHandler(this.Logo_Click);
            // 
            // stepPanelPictureBox
            // 
            this.stepPanelPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stepPanelPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.stepPanelPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stepPanelPictureBox.Location = new System.Drawing.Point(420, 187);
            this.stepPanelPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.stepPanelPictureBox.Name = "stepPanelPictureBox";
            this.stepPanelPictureBox.Size = new System.Drawing.Size(532, 306);
            this.stepPanelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stepPanelPictureBox.TabIndex = 15;
            this.stepPanelPictureBox.TabStop = false;
            this.stepPanelPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.StepPanelPictureBox_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1176, 664);
            this.ControlBox = false;
            this.Controls.Add(this.clientAreaPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1176, 664);
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Nucleus Co-op";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
            this.gameContextMenuStrip.ResumeLayout(false);
            this.socialLinksMenu.ResumeLayout(false);
            this.clientAreaPanel.ResumeLayout(false);
            this.game_listSizer.ResumeLayout(false);
            this.rightFrame.ResumeLayout(false);
            this.rightFrame.PerformLayout();
            this.stepButtonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_expandNotes)).EndInit();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.scriptAuthorTxtSizer.ResumeLayout(false);
            this.cover.ResumeLayout(false);
            this.mainButtonFrame.ResumeLayout(false);
            this.mainButtonFrame.PerformLayout();
            this.setupButtonsPanel.ResumeLayout(false);
            this.setupButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepPanelPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Gaming.ControlListBox list_Games;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ContextMenuStrip gameContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem gameNameMenuItem;
        private System.Windows.Forms.ToolStripSeparator menuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem detailsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openHandlerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFolderMenuItem;
        private System.Windows.Forms.ToolStripSeparator menuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem changeIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOrigExePathMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteContentFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openUserProfConfigMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserProfConfigMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openUserProfSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserProfSaveMenuItem;
        private System.Windows.Forms.ToolStripSeparator menuSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openDocumentConfMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDocumentConfMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDocumentSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDocumentSaveMenuItem;
		private System.Windows.Forms.PictureBox stepPanelPictureBox;
        private Button minimizeBtn;
        private Button maximizeBtn;
        private PictureBox logo;
        private BufferedClientAreaPanel scriptAuthorTxtSizer;
        private BufferedClientAreaPanel rightFrame;
        private Label txt_version;
        private Button btn_Extract;
        public BufferedClientAreaPanel clientAreaPanel;
        private Button btn_Links;
        private Label HandlerNoteTitle;
        private PictureBox btn_expandNotes;
        public BufferedFlowLayoutPanel icons_Container;
        private Button closeBtn;
        public Button btn_settings;
        private ToolStripMenuItem keepInstancesFolderMenuItem;
        public BufferedClientAreaPanel coverFrame;
        public BufferedClientAreaPanel cover;
        public BufferedClientAreaPanel StepPanel;
        public Button btn_gameOptions;
        public Button button_UpdateAvailable;
        public BufferedClientAreaPanel game_listSizer;
        public BufferedClientAreaPanel mainButtonFrame;
        public Button btn_downloadAssets;
        public TransparentRichTextBox scriptAuthorTxt;
        public Button btn_debuglog;
        private Button donationBtn;
        private Label playTime;
        private Label lastPlayedAt;
        private BufferedClientAreaPanel infoPanel;
        private BufferedClientAreaPanel stepButtonsPanel;
        private ToolStripMenuItem disableProfilesMenuItem;
        private ToolStripMenuItem openBackupFolderMenuItem;
        private ToolStripMenuItem deleteBackupFolderMenuItem;
        private Label lastPlayedAtValue;
        private Label playTimeValue;
        private ContextMenuStrip socialLinksMenu;
        private ToolStripMenuItem fAQMenuItem;
        private ToolStripMenuItem redditMenuItem;
        private ToolStripMenuItem discordMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem splitCalculatorMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem thirdPartyToolsToolStripMenuItem;
        private ToolStripMenuItem xOutputToolStripMenuItem;
        private ToolStripMenuItem dS4WindowsToolStripMenuItem;
        private ToolStripMenuItem hidHideToolStripMenuItem;
        private ToolStripMenuItem scpToolkitToolStripMenuItem;
        private BufferedClientAreaPanel setupButtonsPanel;
        private Button instruction_btn;
        private Button profileSettings_btn;
        private Button profilesList_btn;
        private CustomRadioButton saveProfileRadioBtn;
        private Label InputsTextLabel;
        private ToolStripMenuItem gameAssetsMenuItem;
        private ToolStripMenuItem coverMenuItem;
        private ToolStripMenuItem screenshotsMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem disableHandlerUpdateMenuItem;
    }
}