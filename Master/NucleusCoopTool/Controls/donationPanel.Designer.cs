namespace Nucleus.Coop.Controls
{
    partial class donationPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(donationPanel));
            this.Ilyaki_label = new System.Windows.Forms.Label();
            this.Mikou_label = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.image3 = new System.Windows.Forms.PictureBox();
            this.image2 = new System.Windows.Forms.PictureBox();
            this.btn_credits = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.image3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image2)).BeginInit();
            this.SuspendLayout();
            // 
            // Ilyaki_label
            // 
            this.Ilyaki_label.AutoSize = true;
            this.Ilyaki_label.BackColor = System.Drawing.Color.Transparent;
            this.Ilyaki_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ilyaki_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Ilyaki_label.Location = new System.Drawing.Point(74, 58);
            this.Ilyaki_label.Name = "Ilyaki_label";
            this.Ilyaki_label.Size = new System.Drawing.Size(50, 16);
            this.Ilyaki_label.TabIndex = 5;
            this.Ilyaki_label.Text = "🔗Ilyaki";
            this.Ilyaki_label.Click += new System.EventHandler(this.Ilyaki_label_Click);
            // 
            // Mikou_label
            // 
            this.Mikou_label.AutoSize = true;
            this.Mikou_label.BackColor = System.Drawing.Color.Transparent;
            this.Mikou_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mikou_label.ForeColor = System.Drawing.Color.MediumPurple;
            this.Mikou_label.Location = new System.Drawing.Point(74, 114);
            this.Mikou_label.Name = "Mikou_label";
            this.Mikou_label.Size = new System.Drawing.Size(124, 16);
            this.Mikou_label.TabIndex = 7;
            this.Mikou_label.Text = "🔗Mikou27 (nene27)";
            this.Mikou_label.Click += new System.EventHandler(this.Mikou_label_Click);
            // 
            // title
            // 
            this.title.AllowDrop = true;
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(3, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(309, 16);
            this.title.TabIndex = 8;
            this.title.Text = "♥ Support the Nucleus Co-op developers ♥";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // image3
            // 
            this.image3.BackColor = System.Drawing.Color.Transparent;
            this.image3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("image3.BackgroundImage")));
            this.image3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.image3.Location = new System.Drawing.Point(18, 95);
            this.image3.Name = "image3";
            this.image3.Size = new System.Drawing.Size(50, 50);
            this.image3.TabIndex = 6;
            this.image3.TabStop = false;
            // 
            // image2
            // 
            this.image2.BackColor = System.Drawing.Color.Transparent;
            this.image2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("image2.BackgroundImage")));
            this.image2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.image2.Location = new System.Drawing.Point(18, 39);
            this.image2.Name = "image2";
            this.image2.Size = new System.Drawing.Size(50, 50);
            this.image2.TabIndex = 4;
            this.image2.TabStop = false;
            // 
            // btn_credits
            // 
            this.btn_credits.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_credits.BackColor = System.Drawing.Color.Transparent;
            this.btn_credits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_credits.FlatAppearance.BorderSize = 0;
            this.btn_credits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_credits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_credits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_credits.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_credits.ForeColor = System.Drawing.Color.Orange;
            this.btn_credits.Location = new System.Drawing.Point(124, 147);
            this.btn_credits.Margin = new System.Windows.Forms.Padding(2);
            this.btn_credits.Name = "btn_credits";
            this.btn_credits.Size = new System.Drawing.Size(66, 24);
            this.btn_credits.TabIndex = 302;
            this.btn_credits.Text = "Credits";
            this.btn_credits.UseVisualStyleBackColor = false;
            this.btn_credits.Click += new System.EventHandler(this.Btn_credits_Click);
            // 
            // donationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btn_credits);
            this.Controls.Add(this.title);
            this.Controls.Add(this.Mikou_label);
            this.Controls.Add(this.image3);
            this.Controls.Add(this.Ilyaki_label);
            this.Controls.Add(this.image2);
            this.DoubleBuffered = true;
            this.Name = "donationPanel";
            this.Size = new System.Drawing.Size(315, 173);
            ((System.ComponentModel.ISupportInitialize)(this.image3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Ilyaki_label;
        private System.Windows.Forms.PictureBox image2;
        private System.Windows.Forms.Label Mikou_label;
        private System.Windows.Forms.PictureBox image3;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button btn_credits;
    }
}
