using Nucleus.Gaming.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Controls
{
    public partial class Tutorial : UserControl
    {
        public Tutorial()
        {
            InitializeComponent();

            PictureBox container = new PictureBox()
            {
                Dock = DockStyle.Fill,  
                BackColor = Color.Black,
                
                BackgroundImageLayout = ImageLayout.Stretch,
                
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            container.Click += ContainerClick;

            Controls.Add(container);

            InitContainer(container);
        }

        private void InitContainer(PictureBox pb)
        {
            pb.Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "gui\\tutorial\\tutorial.gif"));
            pb.Cursor = Theme_Settings.Hand_Cursor;
        }

        private void ContainerClick(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
