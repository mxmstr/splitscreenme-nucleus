using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public partial class CustomComboBox : Panel, IDynamicSized
    {
        public List<string> Items = new List<string>();

        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                MainItem.Text = selectedItem;
            }
        }

        private int Delta = 0;
        private int ItemIndex = 0;

        private int scrollOffset = 5;
        public int ScrollOffset
        {
            get => scrollOffset;
            set => scrollOffset = value;
        }

        private float _scale;

        public bool Opened = false;
        private bool outOfBounds = false;

        public TextBox MainItem;
        private Label Expand;
        //private Label Real;
        public CustomComboBox()
        {
            InitializeComponent();
            //SetStyle( ControlStyles.FixedHeight,true);

            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.FixedSingle;

            Height = 20;

            Font font = new Font("Franklin Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            MainItem = new TextBox()
            {
                Name = "Main",
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.FixedSingle,

                Font = font,
                BackColor = Color.FromArgb(0, 180, 12),
                ForeColor = Color.White,

            };

            //Real = new Label()
            //{
            //    Name = "Real",
            //   // Dock = DockStyle.Top,
            //   AutoSize = true,
            //    BorderStyle = BorderStyle.None,
            //    TextAlign = ContentAlignment.MiddleLeft,
            //    Font = font,
            //    BackColor = Color.FromArgb(0,0, 180, 12),
            //    ForeColor = Color.White,

            //};

            //Real.MouseEnter += new EventHandler(Real_MouseEnter);
            //Real.MouseLeave += new EventHandler(Real_MouseLeave);

            Expand = new Label()
            {
                Name = "Expand",
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackgroundImageLayout = ImageLayout.Zoom,
                BackgroundImage = new Bitmap(Properties.Resources.dropdown_closed),
                BorderStyle = BorderStyle.FixedSingle,// BorderStyle.FixedSingle,
                Font = font,
                BackColor = Color.FromArgb(50, 0, 180, 12),
                ForeColor = Color.White,
            };

            Expand.Size = new Size(MainItem.Height, MainItem.Height);
            Expand.Click += new EventHandler(Expand_Click);

            MainItem.Controls.Add(Expand);
            // Controls.Add(Real);

            Controls.Add(MainItem);
            // Real.Location = MainItem.Location;

            this.MouseWheel += new MouseEventHandler(Scrolling);
            DPIManager.Register(this);
        }

        private void Real_MouseEnter(object sender, EventArgs e)
        {
            MainItem.BringToFront();
            //MainItem.Focus();
        }

        private void Real_MouseLeave(object sender, EventArgs e)
        {
            // Real.Text = MainItem.Text;
            // Real.BringToFront();
        }

        public void Expand_Click(object sender, EventArgs e)//refresh the whole list
        {
            MainItem.Width = Width;

            if (Opened)
            {
                Controls.Clear();
                Controls.Add(MainItem);
                Height = MainItem.Height;
                Opened = false;
                return;
            }

            Font font = new Font("Franklin Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            for (int i = 0; i < Items.Count; i++)
            {
                string text = Items[i];

                Label item = new Label()
                {
                    Name = Items[i],
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = font,
                    BackColor = Color.FromArgb(130, 28, 47, 32),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = text,
                    Height = (int)(20 * _scale)
                };

                item.Size = new Size((int)(MainItem.Width), (int)(MainItem.Height));
                item.Location = new Point(0, Controls[i].Bottom);
                item.Click += new EventHandler(Item_Click);

                Height += MainItem.Height;
                Controls.Add(item);
            }

            Opened = true;
            Refresh();
        }


        private void ReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            Label item = sender as Label;
            MainItem.Text = item.Text;
            selectedItem = item.Text;
            // Real.Text = MainItem.Text;
            Expand_Click(null, null);
        }

        private void Scrolling(object sender, MouseEventArgs e)
        {
            Delta = (int)e.Delta;

            if (!Opened)//change MainItem text on scrolling 
            {
                if (Delta > 0)
                {
                    if (ItemIndex >= 1)
                    {
                        ItemIndex--;
                        Controls[0].Text = Items[ItemIndex];
                    }
                }

                if (Delta < 0)
                {
                    if (ItemIndex < Items.Count)
                    {
                        Controls[0].Text = Items[ItemIndex];
                        ItemIndex++;
                    }
                }
            }

            if (Opened && outOfBounds)//Scroll Items list 
            {
                if (Delta > 0 && Controls[1].Top != Controls[0].Bottom)
                {
                    for (int i = 1; i < Controls.Count; i++)
                    {
                        Controls[i].Location = new Point(0, Controls[i].Location.Y + Controls[i].Height * ScrollOffset);
                    }

                    Height += Controls[0].Height * ScrollOffset;
                }

                if (Delta < 0 && Controls[Controls.Count - 2].Top != Controls[0].Bottom)
                {
                    for (int i = 1; i < Controls.Count; i++)
                    {
                        Controls[i].Location = new Point(0, Controls[i].Location.Y - Controls[i].Height * ScrollOffset);
                    }

                    Height -= Controls[0].Height * ScrollOffset;
                }
            }
        }

        public void SelectedDefaultText(string text)
        {
            MainItem.Text = text;
            // Real.Text = MainItem.Text;
        }

        protected override void OnPaint(PaintEventArgs e)///voir pour utiliser drawstring seulement si readonly
        {
            Graphics g = e.Graphics;

            Rectangle parentBounds = new Rectangle(Parent.Location.X, Parent.Location.Y, Parent.Width, Parent.Height);
            Rectangle bounds = new Rectangle(0, 0, Width, Height);
            Rectangle border = new Rectangle(MainItem.Location.X, MainItem.Location.Y, MainItem.Width, MainItem.Height - 3);
            //Pen pen = new Pen(Color.Yellow,1);
            //g.DrawRectangle(pen, border);

            // Size textSize = TextRenderer.MeasureText(MainItem.Text, MainItem.Font);
            //float ratio = (float)(textSize.Width / textSize.Height)/10;
            //Real.Width = textSize.Width;
            //Real.Size = new Size(textSize.Width, MainItem.Height);
            //Console.WriteLine(ratio);
            //RectangleF textBounds = new RectangleF((float)MainItem.Location.X+4, (float)MainItem.Location.Y, (float)(MainItem.Width-2)*ratio, (float)MainItem.Height);
            //g.DrawString(MainItem.Text, new Font("Franklin Gothic", textSize.Height-2, FontStyle.Regular, GraphicsUnit.Pixel, 0), Brushes.White,textBounds);
            //Real.Text = MainItem.Text;
            if (bounds.Bottom > parentBounds.Bottom)
            {
                outOfBounds = true;
            }
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
            //Font = new Font("Franklin Gothic", 10.25F * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            MainItem.Font = new Font("Franklin Gothic", 12f * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            _scale = scale;
        }
    }
}
