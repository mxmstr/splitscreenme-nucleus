
namespace Nucleus.Coop
{
    partial class Caroussel
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
            this.showcaseBanner1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Container1 = new BufferedClientAreaPanel();
            this.Container1.SuspendLayout();
            this.SuspendLayout();
            // 
            // showcaseBanner1
            // 
            this.showcaseBanner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showcaseBanner1.AutoScroll = true;
            this.showcaseBanner1.BackColor = System.Drawing.Color.Transparent;
            this.showcaseBanner1.Location = new System.Drawing.Point(2, 3);
            this.showcaseBanner1.Name = "showcaseBanner1";
            this.showcaseBanner1.Size = new System.Drawing.Size(850, 251);
            this.showcaseBanner1.TabIndex = 69;
            this.showcaseBanner1.WrapContents = false;
            this.showcaseBanner1.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowcaseBanner1_Paint);
            // 
            // Container1
            // 
            this.Container1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Container1.BackColor = System.Drawing.Color.Transparent;
            this.Container1.Controls.Add(this.showcaseBanner1);
            this.Container1.Location = new System.Drawing.Point(16, -3);
            this.Container1.Name = "Container1";
            this.Container1.Size = new System.Drawing.Size(858, 246);
            this.Container1.TabIndex = 70;
            this.Container1.Paint += new System.Windows.Forms.PaintEventHandler(this.Container1_Paint);
            // 
            // Caroussel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Container1);
            this.DoubleBuffered = true;
            this.Name = "Caroussel";
            this.Size = new System.Drawing.Size(880, 243);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Caroussel_Paint);
            this.Container1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel showcaseBanner1;
        private BufferedClientAreaPanel Container1;
    }
}
