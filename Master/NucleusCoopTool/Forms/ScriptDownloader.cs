using ListViewSorter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class ScriptDownloader : BaseForm, IDynamicSized
    {

        private const string api = "https://hub.splitscreen.me/api/v1/";

        private readonly List<Handler> searchHandlers = new List<Handler>();

        private MainForm mainForm;

        private string lastSearch;

        private ListViewColumnSorter lvwColumnSorter;
        private List<Control> ctrls = new List<Control>();

        private bool grabAll = false;
        private bool canResize;
        private JArray handlers;

        private int entriesPerPage;
        private int entryIndex = 0;
        private int sortColumn = 0;
        private int verCount = 0;
        private int lastVer = 0;
        private float fontSize;
        private static Hub hub = new Hub();
        private SortOrder sortOrder = SortOrder.Ascending;
        private float scale;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
       int nLeftRect,     // x-coordinate of upper-left corner
       int nTopRect,      // y-coordinate of upper-left corner
       int nRightRect,    // x-coordinate of lower-right corner
       int nBottomRect,   // y-coordinate of lower-right corner
       int nWidthEllipse, // width of ellipse
       int nHeightEllipse // height of ellipse
       );

        private void controlscollect()
        {
            foreach (Control control in Controls)
            {
                ctrls.Add(control);
                foreach (Control container1 in control.Controls)
                {
                    ctrls.Add(container1);
                    foreach (Control container2 in container1.Controls)
                    {
                        ctrls.Add(container2);
                        foreach (Control container3 in container2.Controls)
                        {
                            ctrls.Add(container3);
                        }
                    }
                }
            }
        }

        private int prevWidth;
        private void ScriptDownloader_ResizeBegin(object sender, EventArgs e)
        {
            foreach (Control c in ctrls)
            {
                c.Visible = false;
            }
            prevWidth = Width;
            Opacity = 0.6D;
        }

        private void ScriptDownloader_ResizeEnd(object sender, EventArgs e)
        {
            foreach (Control c in ctrls)
            {
                if (c.Name != "chkBox_Verified")
                    c.Visible = true;
            }

            list_Games.Columns[7].Width += Width - prevWidth;
            Opacity = 1.0D;
        }

        public void button_Click(object sender, EventArgs e)
        {
            if (mainForm.mouseClick)
                mainForm.SoundPlayer(mainForm.theme + "button_click.wav");
        }

        public ScriptDownloader(MainForm mf)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "HandlerDownloaderFontSize"));

            InitializeComponent();

            default_Cursor = mf.default_Cursor;
            Cursor.Current = default_Cursor;
            hand_Cursor = mf.hand_Cursor;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.DefaultConnectionLimit = 9999;

            lvwColumnSorter = new ListViewColumnSorter();
            list_Games.ListViewItemSorter = lvwColumnSorter;
            //list_Games.DrawItem += (sender, e) => { e.DrawDefault = true; };
            //list_Games.DrawSubItem += (sender, e) => { e.DrawDefault = true; };

            cmb_Sort.SelectedIndex = 0;
            cmb_NumResults.SelectedIndex = 1;
            entriesPerPage = Convert.ToInt32(cmb_NumResults.SelectedItem);

            SuspendLayout();

            if (mf.roundedcorners)
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));
            BackgroundImage = new Bitmap(mf.theme + "other_backgrounds.jpg");
            //Controls Pictures
            btn_Next.BackColor = mf.buttonsBackColor;
            btn_Prev.BackColor = mf.buttonsBackColor;
            btn_ViewAll.BackColor = mf.buttonsBackColor;
            btn_Info.BackColor = mf.buttonsBackColor;
            btn_Search.BackColor = mf.buttonsBackColor;
            btn_Close.BackColor = mf.buttonsBackColor;
            btn_Download.BackColor = mf.buttonsBackColor;
            btn_Extract.BackColor = mf.buttonsBackColor;
            //
            //MouseOverColor
            //
            btn_Next.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Prev.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_ViewAll.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Info.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Search.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Close.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Download.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            btn_Extract.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;

            controlscollect();

            foreach (Control control in ctrls)
            {
                control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                if (control.GetType() == typeof(Button))
                {
                    control.Cursor = hand_Cursor;
                }
                else
                {
                    control.Cursor = default_Cursor;
                }
            }

            if (mf.useButtonsBorder)
            {
                btn_Next.FlatAppearance.BorderSize = 1;
                btn_Next.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Prev.FlatAppearance.BorderSize = 1;
                btn_Prev.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Download.FlatAppearance.BorderSize = 1;
                btn_Download.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Info.FlatAppearance.BorderSize = 1;
                btn_Info.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Close.FlatAppearance.BorderSize = 1;
                btn_Close.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Extract.FlatAppearance.BorderSize = 1;
                btn_Extract.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_ViewAll.FlatAppearance.BorderSize = 1;
                btn_ViewAll.FlatAppearance.BorderColor = mf.ButtonsBorderColor;

                btn_Search.FlatAppearance.BorderSize = 1;
                btn_Search.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
            }

            ResumeLayout();

            if (mf.mouseClick)
            {
                foreach (Control button in this.Controls) { if (button is Button) { button.Click += new System.EventHandler(this.button_Click); } }
            }

            mainForm = mf;

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public new void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
            SuspendLayout();

            if (scale > 1.0F)
            {
                float newFontSize = Font.Size * scale;
                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) ^ c.GetType() == typeof(ComboBox) ^ c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(GroupBox) ^ c.GetType() == typeof(Panel) ^ c.GetType() == typeof(ListView))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
                    }
                }
            }

            ResumeLayout();
        }
        //private int cover_index = 0;

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            if (this.WindowState == FormWindowState.Normal)
            {
                switch (m.Msg)
                {

                    case 0x0084/*NCHITTEST*/ :
                        base.WndProc(ref m);

                        if ((int)m.Result == 0x01/*HTCLIENT*/)
                        {
                            Point screenPoint = new Point(m.LParam.ToInt32());
                            Point clientPoint = this.PointToClient(screenPoint);
                            if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)12/*HTTOP*/ ;
                                else
                                    m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                            }
                            else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)10/*HTLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)2/*HTCAPTION*/ ;
                                else
                                    m.Result = (IntPtr)11/*HTRIGHT*/ ;
                            }
                            else
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                                else
                                    m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                            }
                        }
                        return;
                }
            }

            base.WndProc(ref m);
        }

        public Handler GetHandler(string id)
        {
            if (id == "")
            {
                return null;
            }

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.DefaultConnectionLimit = 9999;
            }
            catch (Exception)
            { }

            string resp = Get(api + "handler/" + id);

            if (resp == null || resp == "{}")
            {
                return null;
            }

            JObject jObject = JsonConvert.DeserializeObject(resp) as JObject;

            if (jObject == null)
            {
                return null;
            }

            JArray array = jObject["Handlers"] as JArray;

            if (array == null || array.Count != 1)
            {
                return null;
            }

            Handler handler = new Handler
            {
                Id = array[0]["_id"].ToString(),
                Owner = array[0]["owner"].ToString(),
                OwnerName = array[0]["ownerName"].ToString(),
                Description = array[0]["description"].ToString(),
                Title = array[0]["title"].ToString(),
                GameName = array[0]["gameName"].ToString(),
                GameDescription = array[0]["gameDescription"].ToString(),
                GameCover = array[0]["gameCover"].ToString(),
                GameId = array[0]["gameId"].ToString(),
                GameUrl = array[0]["gameUrl"].ToString(),
                CreatedAt = array[0]["createdAt"].ToString(),
                UpdatedAt = array[0]["updatedAt"].ToString(),
                Stars = array[0]["stars"].ToString(),
                DownloadCount = array[0]["downloadCount"].ToString(),
                Verified = array[0]["verified"].ToString(),
                Private = array[0]["private"].ToString(),
                CommentCount = array[0]["commentCount"].ToString(),
                CurrentVersion = array[0]["currentVersion"].ToString(),
                CurrentPackage = array[0]["currentPackage"].ToString()
            };

            return handler;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (txt_Search.Text.Contains("\\") || txt_Search.Text.Contains("/"))
            {
                MessageBox.Show("Search cannot contain the characters \"/\" or \"\\\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_Search.Text.StartsWith("*") || txt_Search.Text == ".")
            {
                MessageBox.Show("Illegal search query, please try something else.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((!string.IsNullOrEmpty(txt_Search.Text) && txt_Search.Text.Replace(" ", string.Empty).Length > 0) || grabAll)
            {
                btn_Prev.Enabled = false;
                btn_Next.Enabled = false;

                lastSearch = txt_Search.Text;

                lbl_Status.Text = "";

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.DefaultConnectionLimit = 9999;

                string searchParam = Uri.EscapeDataString(txt_Search.Text);
                string rawHandlers = null;
                if (grabAll)
                {
                    rawHandlers = Get(api + "allhandlers");
                    grabAll = false;
                }
                else
                {
                    rawHandlers = Get(api + "handlers/" + searchParam);
                }

                txt_Search.Clear();

                if (rawHandlers == null)
                {
                    return;
                }
                else if (rawHandlers == "{}")
                {
                    MessageBox.Show("No results found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Search.Focus();
                    return;
                }

                JObject jObject = JsonConvert.DeserializeObject(rawHandlers) as JObject;

                JArray array = jObject["Handlers"] as JArray;

                switch (cmb_Sort.SelectedIndex)
                {
                    case 0: // Alphabetical
                        handlers = new JArray(array.OrderBy(obj => (string)obj["gameName"]));
                        sortColumn = 0;
                        sortOrder = SortOrder.Ascending;
                        break;
                    case 1: // Most Downloaded
                        handlers = new JArray(array.OrderByDescending(obj => (int)obj["downloadCount"]));
                        sortColumn = 3;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 2: // Most Likes
                        handlers = new JArray(array.OrderByDescending(obj => (int)obj["stars"]));
                        sortColumn = 4;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 3: // Most Recently Uploaded
                        handlers = new JArray(array.OrderByDescending(obj => (DateTime)obj["createdAt"]));
                        sortColumn = 5;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 4: // Most Recently Updated
                        handlers = new JArray(array.OrderByDescending(obj => (DateTime)obj["updatedAt"]));
                        sortColumn = 6;
                        sortOrder = SortOrder.Descending;
                        break;
                }

                entryIndex = 0;
                FetchHandlers(entryIndex);

                if (handlers.Count > entriesPerPage)
                {
                    btn_Next.Enabled = true;
                }
            }
        }
        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void FetchHandlers(int startIndex)
        {
            ImageList imageList = new ImageList
            {
                ImageSize = new Size(35, 35)
            };

            list_Games.Items.Clear();
            searchHandlers.Clear();
            btn_Download.Enabled = false;
            btn_Info.Enabled = false;

            list_Games.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;

            int entriesToView = entriesPerPage;
            if ((startIndex + entriesPerPage) > handlers.Count)
            {
                entriesToView = handlers.Count - startIndex;
            }

            int vo = 0;
            if (chkBox_Verified.Checked)
            {
                verCount = 0;
                foreach (JToken jtoken in handlers)
                {
                    if (bool.Parse(jtoken["verified"].ToString()))
                    {
                        verCount++;
                    }
                }

                if ((startIndex + entriesPerPage) > verCount)
                {
                    entriesToView = verCount - startIndex;
                }

                if (startIndex == 0)
                {
                    lastVer = 0;
                }
            }

            for (int i = 0; i < entriesToView; i++)
            {
                lbl_Status.Text = string.Format("Fetching {0}/{1} handlers", (i + 1), entriesToView);
                Handler handler = new Handler();

                int offset = startIndex;
                if (chkBox_Verified.Checked)
                {
                    offset = lastVer;
                }

                handler.Id = handlers[vo + i + offset]["_id"].ToString();
                handler.Owner = handlers[vo + i + offset]["owner"].ToString();
                handler.OwnerName = handlers[vo + i + offset]["ownerName"].ToString();
                handler.Description = handlers[vo + i + offset]["description"].ToString();
                handler.Title = handlers[vo + i + offset]["title"].ToString();
                handler.GameName = handlers[vo + i + offset]["gameName"].ToString();
                handler.GameDescription = handlers[vo + i + offset]["gameDescription"].ToString();
                handler.GameCover = handlers[vo + i + offset]["gameCover"].ToString();
                handler.GameId = handlers[vo + i + offset]["gameId"].ToString();
                handler.GameUrl = handlers[vo + i + offset]["gameUrl"].ToString();
                handler.CreatedAt = handlers[vo + i + offset]["createdAt"].ToString();
                handler.UpdatedAt = handlers[vo + i + offset]["updatedAt"].ToString();
                handler.Stars = handlers[vo + i + offset]["stars"].ToString();
                handler.DownloadCount = handlers[vo + i + offset]["downloadCount"].ToString();
                handler.Verified = handlers[vo + i + offset]["verified"].ToString();
                handler.Private = handlers[vo + i + offset]["private"].ToString();
                handler.CommentCount = handlers[vo + i + offset]["commentCount"].ToString();
                handler.CurrentVersion = handlers[vo + i + offset]["currentVersion"].ToString();
                handler.CurrentPackage = handlers[vo + i + offset]["currentPackage"].ToString();

                if (chkBox_Verified.Checked && !bool.Parse(handler.Verified))
                {
                    i--;
                    vo++;
                    continue;
                }
                else if (chkBox_Verified.Checked && bool.Parse(handler.Verified) && (i + 1) == entriesToView)
                {
                    lastVer = vo + i + startIndex + 1;
                }

                searchHandlers.Add(handler);

                string vSymb;
                if (handler.Verified == "True")
                {
                    vSymb = "ü";
                }
                else
                {
                    vSymb = string.Empty;
                }

                Bitmap bmp = new Bitmap(mainForm.theme + "no_cover.png");
                string _cover = $@"https://images.igdb.com/igdb/image/upload/t_micro/{handler.GameCover}.jpg";

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_cover);
                    request.UserAgent = "request";
                    WebResponse resp = request.GetResponse();
                    Stream respStream = resp.GetResponseStream();
                    bmp = new Bitmap(respStream);
                    respStream.Dispose();
                }
                catch (Exception) { }

                imageList.Images.Add(bmp);
                list_Games.SmallImageList = imageList;

                string[] handlerDisplayCols = { handler.OwnerName, vSymb, handler.DownloadCount, handler.Stars, handler.CreatedAt, handler.UpdatedAt, handler.Description, handler.Id };
                list_Games.Items.Add(" " + handler.GameName).SubItems.AddRange(handlerDisplayCols);
                list_Games.Items[(list_Games.Items.Count - 1)].ImageIndex = (list_Games.Items.Count - 1);

                if (list_Games.Items[(list_Games.Items.Count - 1)].SubItems[5].Text.Contains(" "))
                {
                    list_Games.Items[(list_Games.Items.Count - 1)].SubItems[5].Text = list_Games.Items[(list_Games.Items.Count - 1)].SubItems[5].Text.Substring(0, list_Games.Items[(list_Games.Items.Count - 1)].SubItems[5].Text.IndexOf(' '));
                }

                if (int.Parse(handler.CurrentVersion) > 1)
                {
                    if (list_Games.Items[(list_Games.Items.Count - 1)].SubItems[6].Text.Contains(" "))
                    {
                        list_Games.Items[(list_Games.Items.Count - 1)].SubItems[6].Text = list_Games.Items[(list_Games.Items.Count - 1)].SubItems[6].Text.Substring(0, list_Games.Items[(list_Games.Items.Count - 1)].SubItems[6].Text.IndexOf(' '));
                    }
                }
                else
                {
                    list_Games.Items[(list_Games.Items.Count - 1)].SubItems[6].Text = string.Empty;
                }

                if (list_Games.Items[(list_Games.Items.Count - 1)].SubItems[7].Text.Length > 50)
                {
                    list_Games.Items[(list_Games.Items.Count - 1)].SubItems[7].Text = list_Games.Items[(list_Games.Items.Count - 1)].SubItems[7].Text.Substring(0, 50) + "...";
                }

                foreach (ListViewItem lvi in list_Games.Items)
                {
                    lvi.UseItemStyleForSubItems = false;
                }

                list_Games.Items[(list_Games.Items.Count - 1)].SubItems[2].Font = new Font(new FontFamily("Wingdings"), 10, FontStyle.Bold);
                list_Games.Items[(list_Games.Items.Count - 1)].SubItems[2].ForeColor = Color.Green;


            }

            list_Games.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            list_Games.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            list_Games.Columns[8].Width = 0;

            lvwColumnSorter.SortColumn = sortColumn;
            lvwColumnSorter.Order = sortOrder;
            list_Games.SetSortIcon(0, lvwColumnSorter.Order);

            list_Games.Sort();

            list_Games.EndUpdate();

            if (startIndex + entriesToView == handlers.Count || startIndex + entriesToView == verCount)
            {
                btn_Next.Enabled = false;
            }
            else
            {
                btn_Next.Enabled = true;
            }

            if (startIndex == 0)
            {
                btn_Prev.Enabled = false;
            }
            else
            {
                btn_Prev.Enabled = true;
            }

            Cursor.Current = Cursors.Default;

            int tot = handlers.Count;
            if (chkBox_Verified.Checked)
            {
                tot = verCount;
            }
            lbl_Status.Text = string.Format("Viewing results {0}-{1}. Total: {2}", (startIndex + 1), entriesToView + startIndex, tot);
        }

        public string Get(string uri)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.DefaultConnectionLimit = 9999;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.UserAgent = "request";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)//ex)
            {
                //MessageBox.Show(string.Format("{0}: {1}", ex.ToString(), ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Search_Click(this, new EventArgs());
            }
        }

        private void ScriptDownloader_Resize(object sender, EventArgs e)
        {
            list_Games.BeginUpdate();
            list_Games.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            list_Games.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            list_Games.Columns[8].Width = 0;
            list_Games.EndUpdate();
        }

        private void list_Games_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to descending.
                lvwColumnSorter.SortColumn = e.Column;
                if (e.Column == 0)
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }

            }

            // Perform the sort with these new sort options.
            myListView.Sort();
            myListView.SetSortIcon(e.Column, lvwColumnSorter.Order);
            myListView.EnsureVisible(0);
        }

        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (list_Games.SelectedItems.Count == 1)
            {
                Handler handler = null;
                foreach (Handler hndl in searchHandlers)
                {
                    if (list_Games.SelectedItems[0].SubItems[8].Text == hndl.Id)
                    {
                        handler = hndl;
                        break;
                    }
                }

                if (handler == null)
                {
                    MessageBox.Show("Error fetching handler", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                HandlerInfo handlerInfo = new HandlerInfo(handler, mainForm);
                handlerInfo.ShowDialog();
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            if (list_Games.SelectedItems.Count == 1)
            {
                Handler handler = null;
                foreach (Handler hndl in searchHandlers)
                {
                    if (list_Games.SelectedItems[0].SubItems[8].Text == hndl.Id)
                    {
                        handler = hndl;
                        break;
                    }
                }

                if (handler == null)
                {
                    MessageBox.Show("Error fetching handler", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DownloadPrompt downloadPrompt = new DownloadPrompt(handler, mainForm, null);
                downloadPrompt.ShowDialog();

            }
        }

        private void chkBox_Verified_Click(object sender, EventArgs e)
        {
            if (chkBox_Verified.Checked)
            {
                if (list_Games.Items.Count > 0)
                {
                    foreach (ListViewItem game in list_Games.Items)
                    {
                        if (game.SubItems[2].Text != "ü")
                        {
                            game.Remove();
                        }
                    }
                }
            }
            else
            {
                if (lastSearch == string.Empty)
                {
                    grabAll = true;
                }
                else
                {
                    txt_Search.Text = lastSearch;
                }
                btn_Search.PerformClick();
            }
        }

        private void btn_ViewAll_Click(object sender, EventArgs e)
        {
            grabAll = true;
            btn_Search.PerformClick();
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            entryIndex -= entriesPerPage;
            if (entryIndex < 0)
            {
                entryIndex = 0;
            }

            //chkBox_Verified.Checked = false;
            FetchHandlers(entryIndex);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            int tot = handlers.Count;
            if (chkBox_Verified.Checked)
            {
                tot = verCount;
            }

            entryIndex += entriesPerPage;
            if (entryIndex > tot)
            {
                entryIndex = tot - entriesPerPage;
            }

            //chkBox_Verified.Checked = false;
            FetchHandlers(entryIndex);
        }

        private void cmb_NumResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_NumResults.SelectedItem.ToString() == "All")
            {
                entriesPerPage = 9999;
            }
            else
            {
                entriesPerPage = Convert.ToInt32(cmb_NumResults.SelectedItem);
            }

            if (handlers != null && handlers.Count > 0)
            {
                chkBox_Verified.Checked = false;
                FetchHandlers(entryIndex);
            }
        }

        private void cmb_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_Games.Items.Count > 0)
            {
                switch (cmb_Sort.SelectedIndex)
                {
                    case 0: // Alphabetical
                        sortColumn = 0;
                        sortOrder = SortOrder.Ascending;
                        break;
                    case 1: // Most Downloaded
                        sortColumn = 3;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 2: // Most Likes
                        sortColumn = 4;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 3: // Most Recently Uploaded
                        sortColumn = 5;
                        sortOrder = SortOrder.Descending;
                        break;
                    case 4: // Most Recently Updated
                        sortColumn = 6;
                        sortOrder = SortOrder.Descending;
                        break;
                }

                list_Games.EnsureVisible(0);

                lvwColumnSorter.SortColumn = sortColumn;
                lvwColumnSorter.Order = sortOrder;
                list_Games.SetSortIcon(sortColumn, lvwColumnSorter.Order);

                list_Games.Sort();
            }
        }

        private void list_Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_Games.SelectedItems.Count == 1)
            {
                btn_Download.Enabled = true;
                btn_Info.Enabled = true;
            }
        }

        private void btn_Extract_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select a game handler to extract",
                DefaultExt = "nc",
                InitialDirectory = Gaming.GameManager.Instance.GetJsScriptsPath(),
                Filter = "nc files (*.nc)|*.nc"
            };

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DownloadPrompt downloadPrompt = new DownloadPrompt(null, mainForm, ofd.FileName);
                downloadPrompt.ShowDialog();
            }
        }

        private void ScriptDownloader_ClientSizeChanged(object sender, EventArgs e)
        {
            Invalidate();
            if (mainForm != null)

                if (mainForm.roundedcorners)
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                }
                else
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
                }
        }

    }
}
