namespace Nucleus.Coop.Forms
{
    partial class HubWebView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HubWebView));
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.closeBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.modal = new BufferedClientAreaPanel();
            this.modal_text = new System.Windows.Forms.Label();
            this.modal_no = new System.Windows.Forms.Button();
            this.modal_yes = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.home = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.modal.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Location = new System.Drawing.Point(4, 28);
            this.webView.MinimumSize = new System.Drawing.Size(250, 250);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1111, 608);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            this.webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.WebView_CoreWebView2InitializationCompleted);
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
            this.closeBtn.Location = new System.Drawing.Point(1095, 3);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(20, 20);
            this.closeBtn.TabIndex = 17;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
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
            this.maximizeBtn.ForeColor = System.Drawing.Color.Black;
            this.maximizeBtn.Location = new System.Drawing.Point(1071, 3);
            this.maximizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(20, 20);
            this.maximizeBtn.TabIndex = 18;
            this.maximizeBtn.UseVisualStyleBackColor = false;
            this.maximizeBtn.Click += new System.EventHandler(this.MaximizeBtn_Click);
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
            this.minimizeBtn.Location = new System.Drawing.Point(1047, 3);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(20, 20);
            this.minimizeBtn.TabIndex = 19;
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            this.minimizeBtn.MouseEnter += new System.EventHandler(this.MinimizeBtn_MouseEnter);
            this.minimizeBtn.MouseLeave += new System.EventHandler(this.MinimizeBtn_MouseLeave);
            // 
            // modal
            // 
            this.modal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modal.Controls.Add(this.modal_text);
            this.modal.Controls.Add(this.modal_no);
            this.modal.Controls.Add(this.modal_yes);
            this.modal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modal.Location = new System.Drawing.Point(0, 0);
            this.modal.Name = "modal";
            this.modal.Size = new System.Drawing.Size(1120, 640);
            this.modal.TabIndex = 21;
            this.modal.Visible = false;
            // 
            // modal_text
            // 
            this.modal_text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modal_text.AutoEllipsis = true;
            this.modal_text.BackColor = System.Drawing.Color.Transparent;
            this.modal_text.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modal_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modal_text.ForeColor = System.Drawing.Color.White;
            this.modal_text.Location = new System.Drawing.Point(263, 87);
            this.modal_text.Name = "modal_text";
            this.modal_text.Size = new System.Drawing.Size(597, 296);
            this.modal_text.TabIndex = 2;
            this.modal_text.Text = "label1";
            this.modal_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modal_no
            // 
            this.modal_no.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modal_no.BackColor = System.Drawing.Color.Transparent;
            this.modal_no.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.modal_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modal_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modal_no.ForeColor = System.Drawing.Color.White;
            this.modal_no.Location = new System.Drawing.Point(573, 416);
            this.modal_no.Name = "modal_no";
            this.modal_no.Size = new System.Drawing.Size(287, 97);
            this.modal_no.TabIndex = 1;
            this.modal_no.Text = "No";
            this.modal_no.UseVisualStyleBackColor = false;
            // 
            // modal_yes
            // 
            this.modal_yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.modal_yes.BackColor = System.Drawing.Color.Transparent;
            this.modal_yes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.modal_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modal_yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modal_yes.ForeColor = System.Drawing.Color.White;
            this.modal_yes.Location = new System.Drawing.Point(263, 416);
            this.modal_yes.Name = "modal_yes";
            this.modal_yes.Size = new System.Drawing.Size(287, 97);
            this.modal_yes.TabIndex = 0;
            this.modal_yes.Text = "Yes";
            this.modal_yes.UseVisualStyleBackColor = false;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(475, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(161, 18);
            this.label.TabIndex = 20;
            this.label.Text = "Downloading Handler...";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label.Visible = false;
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.Transparent;
            this.back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.back.FlatAppearance.BorderSize = 0;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.ForeColor = System.Drawing.Color.Black;
            this.back.Location = new System.Drawing.Point(30, 3);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(20, 20);
            this.back.TabIndex = 22;
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.Back_Click);
            // 
            // next
            // 
            this.next.BackColor = System.Drawing.Color.Transparent;
            this.next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.next.FlatAppearance.BorderSize = 0;
            this.next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.next.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.next.ForeColor = System.Drawing.Color.Black;
            this.next.Location = new System.Drawing.Point(56, 3);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(20, 20);
            this.next.TabIndex = 23;
            this.next.UseVisualStyleBackColor = false;
            this.next.Click += new System.EventHandler(this.Next_Click);
            // 
            // home
            // 
            this.home.BackColor = System.Drawing.Color.Transparent;
            this.home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.home.FlatAppearance.BorderSize = 0;
            this.home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home.ForeColor = System.Drawing.Color.Black;
            this.home.Location = new System.Drawing.Point(4, 3);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(20, 20);
            this.home.TabIndex = 24;
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.Home_Click);
            // 
            // HubWebView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.Controls.Add(this.modal);
            this.Controls.Add(this.label);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.maximizeBtn);
            this.Controls.Add(this.next);
            this.Controls.Add(this.back);
            this.Controls.Add(this.home);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "HubWebView";
            this.Text = "hub.splitscreen.me";
            this.ResizeBegin += new System.EventHandler(this.HubWebView_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.HubWebView_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.HubWebView_ClientSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HubWebView_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.modal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Button minimizeBtn;
        private BufferedClientAreaPanel modal;
        private System.Windows.Forms.Label modal_text;
        private System.Windows.Forms.Button modal_no;
        private System.Windows.Forms.Button modal_yes;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button home;
    }
}