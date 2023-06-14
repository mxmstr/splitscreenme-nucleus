namespace Nucleus.Coop.Forms
{
    partial class ScriptDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDownloader));
            this.mainContainer = new BufferedClientAreaPanel();
            this.btn_Maximize = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Info = new System.Windows.Forms.Button();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Sort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.cmb_NumResults = new System.Windows.Forms.ComboBox();
            this.btn_ViewAll = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.chkBox_Verified = new System.Windows.Forms.CheckBox();
            this.list_Games = new System.Windows.Forms.ListView();
            this.gameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ownerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.verified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.downloadCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stars = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createdAt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updatedAt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContainer.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer.Controls.Add(this.btn_Maximize);
            this.mainContainer.Controls.Add(this.btn_Download);
            this.mainContainer.Controls.Add(this.btn_Close);
            this.mainContainer.Controls.Add(this.btn_Search);
            this.mainContainer.Controls.Add(this.txt_Search);
            this.mainContainer.Controls.Add(this.btn_Info);
            this.mainContainer.Controls.Add(this.btn_Extract);
            this.mainContainer.Controls.Add(this.label2);
            this.mainContainer.Controls.Add(this.cmb_Sort);
            this.mainContainer.Controls.Add(this.label1);
            this.mainContainer.Controls.Add(this.btn_Next);
            this.mainContainer.Controls.Add(this.btn_Prev);
            this.mainContainer.Controls.Add(this.cmb_NumResults);
            this.mainContainer.Controls.Add(this.btn_ViewAll);
            this.mainContainer.Controls.Add(this.lbl_Status);
            this.mainContainer.Controls.Add(this.chkBox_Verified);
            this.mainContainer.Controls.Add(this.list_Games);
            this.mainContainer.Controls.Add(this.label4);
            this.mainContainer.Location = new System.Drawing.Point(10, 9);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(852, 496);
            this.mainContainer.TabIndex = 0;
            this.mainContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainContainer_MouseDown);
            // 
            // btn_Maximize
            // 
            this.btn_Maximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Maximize.BackColor = System.Drawing.Color.Silver;
            this.btn_Maximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Maximize.FlatAppearance.BorderSize = 0;
            this.btn_Maximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Maximize.Location = new System.Drawing.Point(810, 0);
            this.btn_Maximize.Name = "btn_Maximize";
            this.btn_Maximize.Size = new System.Drawing.Size(20, 20);
            this.btn_Maximize.TabIndex = 61;
            this.btn_Maximize.UseVisualStyleBackColor = false;
            this.btn_Maximize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_Maximize_MouseClick);
            // 
            // btn_Download
            // 
            this.btn_Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Download.BackColor = System.Drawing.Color.Silver;
            this.btn_Download.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Download.FlatAppearance.BorderSize = 0;
            this.btn_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Download.Location = new System.Drawing.Point(784, 69);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(20, 20);
            this.btn_Download.TabIndex = 46;
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.Visible = false;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Silver;
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Location = new System.Drawing.Point(832, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(20, 20);
            this.btn_Close.TabIndex = 47;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.Silver;
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Search.FlatAppearance.BorderSize = 0;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Location = new System.Drawing.Point(382, 20);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(20, 20);
            this.btn_Search.TabIndex = 48;
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(50, 20);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(326, 20);
            this.txt_Search.TabIndex = 45;
            this.txt_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Search_KeyDown);
            // 
            // btn_Info
            // 
            this.btn_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Info.BackColor = System.Drawing.Color.Silver;
            this.btn_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Info.FlatAppearance.BorderSize = 0;
            this.btn_Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Info.Location = new System.Drawing.Point(810, 69);
            this.btn_Info.Name = "btn_Info";
            this.btn_Info.Size = new System.Drawing.Size(20, 20);
            this.btn_Info.TabIndex = 50;
            this.btn_Info.UseVisualStyleBackColor = false;
            this.btn_Info.Visible = false;
            this.btn_Info.Click += new System.EventHandler(this.btn_Info_Click);
            // 
            // btn_Extract
            // 
            this.btn_Extract.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Extract.BackColor = System.Drawing.Color.IndianRed;
            this.btn_Extract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Extract.Enabled = false;
            this.btn_Extract.FlatAppearance.BorderSize = 0;
            this.btn_Extract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Extract.Location = new System.Drawing.Point(617, -1);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(20, 20);
            this.btn_Extract.TabIndex = 60;
            this.btn_Extract.UseVisualStyleBackColor = false;
            this.btn_Extract.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Sort by:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_Sort
            // 
            this.cmb_Sort.BackColor = System.Drawing.Color.White;
            this.cmb_Sort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Sort.FormattingEnabled = true;
            this.cmb_Sort.Items.AddRange(new object[] {
            "Alphabetical",
            "Most Downloaded",
            "Most Likes",
            "Most Recently Uploaded",
            "Most Recently Updated"});
            this.cmb_Sort.Location = new System.Drawing.Point(56, 60);
            this.cmb_Sort.Margin = new System.Windows.Forms.Padding(1);
            this.cmb_Sort.Name = "cmb_Sort";
            this.cmb_Sort.Size = new System.Drawing.Size(167, 21);
            this.cmb_Sort.TabIndex = 58;
            this.cmb_Sort.SelectedIndexChanged += new System.EventHandler(this.cmb_Sort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(233, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Results Per Page:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Next
            // 
            this.btn_Next.BackColor = System.Drawing.Color.Silver;
            this.btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Next.Enabled = false;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Location = new System.Drawing.Point(441, 59);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(20, 20);
            this.btn_Next.TabIndex = 55;
            this.btn_Next.UseVisualStyleBackColor = false;
            this.btn_Next.Visible = false;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.BackColor = System.Drawing.Color.Silver;
            this.btn_Prev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Prev.Enabled = false;
            this.btn_Prev.FlatAppearance.BorderSize = 0;
            this.btn_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Prev.Location = new System.Drawing.Point(415, 59);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(20, 20);
            this.btn_Prev.TabIndex = 54;
            this.btn_Prev.UseVisualStyleBackColor = false;
            this.btn_Prev.Visible = false;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // cmb_NumResults
            // 
            this.cmb_NumResults.BackColor = System.Drawing.Color.White;
            this.cmb_NumResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_NumResults.FormattingEnabled = true;
            this.cmb_NumResults.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "250",
            "All"});
            this.cmb_NumResults.Location = new System.Drawing.Point(327, 59);
            this.cmb_NumResults.Margin = new System.Windows.Forms.Padding(1);
            this.cmb_NumResults.Name = "cmb_NumResults";
            this.cmb_NumResults.Size = new System.Drawing.Size(49, 21);
            this.cmb_NumResults.TabIndex = 56;
            this.cmb_NumResults.SelectedIndexChanged += new System.EventHandler(this.cmb_NumResults_SelectedIndexChanged);
            // 
            // btn_ViewAll
            // 
            this.btn_ViewAll.BackColor = System.Drawing.Color.Silver;
            this.btn_ViewAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ViewAll.FlatAppearance.BorderSize = 0;
            this.btn_ViewAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ViewAll.Location = new System.Drawing.Point(382, 59);
            this.btn_ViewAll.Name = "btn_ViewAll";
            this.btn_ViewAll.Size = new System.Drawing.Size(20, 20);
            this.btn_ViewAll.TabIndex = 53;
            this.btn_ViewAll.UseVisualStyleBackColor = false;
            this.btn_ViewAll.Click += new System.EventHandler(this.btn_ViewAll_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Status.Location = new System.Drawing.Point(467, 62);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(0, 13);
            this.lbl_Status.TabIndex = 52;
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkBox_Verified
            // 
            this.chkBox_Verified.AutoSize = true;
            this.chkBox_Verified.BackColor = System.Drawing.Color.IndianRed;
            this.chkBox_Verified.Checked = true;
            this.chkBox_Verified.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBox_Verified.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBox_Verified.Location = new System.Drawing.Point(653, 19);
            this.chkBox_Verified.Name = "chkBox_Verified";
            this.chkBox_Verified.Size = new System.Drawing.Size(82, 17);
            this.chkBox_Verified.TabIndex = 51;
            this.chkBox_Verified.Text = "Only Verified";
            this.chkBox_Verified.UseVisualStyleBackColor = false;
            this.chkBox_Verified.Visible = false;
            // 
            // list_Games
            // 
            this.list_Games.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Games.AutoArrange = false;
            this.list_Games.BackColor = System.Drawing.Color.Black;
            this.list_Games.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.gameName,
            this.ownerName,
            this.verified,
            this.downloadCount,
            this.stars,
            this.createdAt,
            this.updatedAt,
            this.description,
            this.id});
            this.list_Games.ForeColor = System.Drawing.Color.White;
            this.list_Games.FullRowSelect = true;
            this.list_Games.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.list_Games.HideSelection = false;
            this.list_Games.Location = new System.Drawing.Point(12, 95);
            this.list_Games.MultiSelect = false;
            this.list_Games.Name = "list_Games";
            this.list_Games.Size = new System.Drawing.Size(828, 387);
            this.list_Games.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.list_Games.TabIndex = 49;
            this.list_Games.TileSize = new System.Drawing.Size(1, 1);
            this.list_Games.UseCompatibleStateImageBehavior = false;
            this.list_Games.View = System.Windows.Forms.View.Details;
            this.list_Games.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.list_Games_ColumnClick);
            this.list_Games.SelectedIndexChanged += new System.EventHandler(this.list_Games_SelectedIndexChanged);
            // 
            // gameName
            // 
            this.gameName.Text = "Game";
            this.gameName.Width = 177;
            // 
            // ownerName
            // 
            this.ownerName.Text = "Uploader";
            this.ownerName.Width = 120;
            // 
            // verified
            // 
            this.verified.Text = "Verified";
            this.verified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verified.Width = 76;
            // 
            // downloadCount
            // 
            this.downloadCount.Text = "Downloads";
            this.downloadCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.downloadCount.Width = 77;
            // 
            // stars
            // 
            this.stars.Text = "Likes";
            this.stars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // createdAt
            // 
            this.createdAt.Text = "Uploaded";
            this.createdAt.Width = 110;
            // 
            // updatedAt
            // 
            this.updatedAt.Text = "Last Updated";
            this.updatedAt.Width = 138;
            // 
            // description
            // 
            this.description.Text = "Uploader Description";
            this.description.Width = 144;
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(11, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Game:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScriptDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(872, 516);
            this.Controls.Add(this.mainContainer);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(862, 472);
            this.Name = "ScriptDownloader";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Download Game Handlers";
            this.ResizeBegin += new System.EventHandler(this.ScriptDownloader_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ScriptDownloader_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.ScriptDownloader_ClientSizeChanged);
            this.Resize += new System.EventHandler(this.ScriptDownloader_Resize);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BufferedClientAreaPanel mainContainer;
        private System.Windows.Forms.Button btn_Maximize;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Info;
        private System.Windows.Forms.Button btn_Extract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Sort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.ComboBox cmb_NumResults;
        private System.Windows.Forms.Button btn_ViewAll;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.CheckBox chkBox_Verified;
        private System.Windows.Forms.ListView list_Games;
        private System.Windows.Forms.ColumnHeader gameName;
        private System.Windows.Forms.ColumnHeader ownerName;
        private System.Windows.Forms.ColumnHeader verified;
        private System.Windows.Forms.ColumnHeader downloadCount;
        private System.Windows.Forms.ColumnHeader stars;
        private System.Windows.Forms.ColumnHeader createdAt;
        private System.Windows.Forms.ColumnHeader updatedAt;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.Label label4;
    }
}