using Ionic.Zip;
using Microsoft.Web.WebView2.Core;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class HubWebView : BaseForm
    {
        private string downloadPath = Path.Combine(Application.StartupPath, "handlers\\hubHandler.nc");
        private string scriptFolder = GameManager.Instance.GetJsScriptsPath();
        private const string hubUri = "https://hub.splitscreen.me/";
        private readonly string theme = Globals.Theme;
   
        private CoreWebView2DownloadOperation downloadOperation;
        private MainForm mainForm;
      
        private bool zipExtractFinished;

        private int numEntries;
        private int entriesDone = 0;

        private EventHandler yesEvent;
        private EventHandler noEvent;

        public HubWebView(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            StartPosition = FormStartPosition.Manual;

            Rectangle area = Screen.PrimaryScreen.Bounds;  
            
            if (ini.IniReadValue("Misc", "DownloaderWindowLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "DownloaderWindowLocation").Split('X');

                if (ScreensUtil.AllScreens().All(s => !s.MonitorBounds.Contains(int.Parse(windowLocation[0]), int.Parse(windowLocation[1]))))
                {
                    CenterToScreen();
                }
                else
                {
                    Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
                }
            }
            else
            {
                CenterToScreen();
            }

            webView.DefaultBackgroundColor = mainForm.BackColor;

            if (mainForm.roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            webView.Focus();

            webView.ZoomFactor = 0.8;
            webView.Source = new Uri(hubUri);

            closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");
            maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");
            minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");
            modal.BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");

            text.BackColor = Color.FromArgb(50,0,0,0);
            yes.BackColor = Color.FromArgb(50, 0, 0, 0);
            no.BackColor = Color.FromArgb(50, 0, 0, 0);

            yes.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 58, 129, 210);
            no.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 58, 129, 210);
           
            home.Text = "";
            home.BackgroundImage = ImageCache.GetImage(theme + "home.png");
            home.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 58, 129, 210);
            back.Text = "";
            back.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            back.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 58, 129, 210);
            next.Text = "";
            next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            next.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 58, 129, 210);
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView.CoreWebView2.DownloadStarting += new EventHandler<CoreWebView2DownloadStartingEventArgs>(DownloadStarting);
            webView.CoreWebView2.IsDefaultDownloadDialogOpenChanged += CloseDownloadDialog;
            webView.CoreWebView2.DOMContentLoaded += new EventHandler<CoreWebView2DOMContentLoadedEventArgs>(ContentLoaded);

        }

        private void ContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            
            webView.ExecuteScriptAsync("document.getElementsByClassName('header ant-layout-header')[0].style.display = 'none'; ");
            webView.ExecuteScriptAsync("document.getElementsByClassName('content ant-layout-content')[0].style.backgroundColor = '#606060'; ");
            webView.ExecuteScriptAsync("document.getElementsByClassName('inner-content')[0].style.backgroundColor = '#C0C0C0'; ");

            //webView.ExecuteScriptAsync("document.getElementsByClassName('ant-typography')[0].style.backgroundColor  = '#000'; "); 

            webView.ExecuteScriptAsync("const ListeA = document.querySelectorAll('ant-col ant-col-xs-24 ant-col-sm-12 ant-col-md-12 ant-col-lg-8 ant-col-xl-6 ant-col-xxl-4');\r\nListeA.forEach(function (element) {\r\n    element.style.backgroundColor = 'black';\r\n});");
            //webView.ExecuteScriptAsync("const ListeP = document.querySelectorAll(\"ant-typography\");\r\nListeP.forEach(function (element) {\r\n    element.style.color = \"white\";\r\n});");
        }

        private void CloseDownloadDialog(object sender, object e)
        {
            if (webView.CoreWebView2.IsDefaultDownloadDialogOpen) webView.CoreWebView2.CloseDefaultDownloadDialog();
        }

        private void DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            e.ResultFilePath = downloadPath;
            downloadOperation = e.DownloadOperation;
            downloadOperation.StateChanged += CheckDownloadState;
            label.Visible = true;
        }

        private void CheckDownloadState(object sender, object e)
        {
            if (downloadOperation.State == CoreWebView2DownloadState.Completed)
            {
                label.Visible = false;
                zipExtractFinished = false;
                entriesDone = 0;
                numEntries = 0;
                ExtractHandler();
                return;
            }
        }

        private void ExtractHandler()
        {
            ZipFile zip = new ZipFile(downloadPath);

            zip.ExtractProgress += ExtractProgress;
            numEntries = zip.Entries.Count;

            List<string> handlerFolders = new List<string>();

            string scriptTempFolder = scriptFolder + "\\temp";

            if (!Directory.Exists(scriptTempFolder))
            {
                Directory.CreateDirectory(scriptTempFolder);
            }

            foreach (ZipEntry ze in zip)
            {
                if (ze.IsDirectory)
                {
                    int count = 0;
                    foreach (char c in ze.FileName)
                    {
                        if (c == '/')
                        {
                            count++;
                        }
                    }

                    if (count == 1)
                    {
                        handlerFolders.Add(ze.FileName.TrimEnd('/'));
                    }

                    ze.Extract(scriptTempFolder, ExtractExistingFileAction.OverwriteSilently);
                }
                else
                {
                    ze.Extract(scriptTempFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            Regex pattern = new Regex("[\\/:*?\"<>|]");
            string frmHandleTitle = pattern.Replace(downloadPath, "");
            string exeName = null;
            int found = 0;

            foreach (string line in System.IO.File.ReadAllLines(Path.Combine(scriptTempFolder, "handler.js")))
            {
                if (line.ToLower().StartsWith("game.executablename"))
                {
                    int start = line.IndexOf("\"");
                    int end = line.LastIndexOf("\"");
                    exeName = line.Substring(start + 1, (end - start) - 1);
                    found++;
                }
                else if (line.ToLower().StartsWith("game.gamename"))
                {
                    int start = line.IndexOf("\"");
                    int end = line.LastIndexOf("\"");
                    frmHandleTitle = pattern.Replace(line.Substring(start + 1, (end - start) - 1), "");
                    found++;
                }

                if (found == 2)
                {
                    break;
                }
            }

            if (File.Exists(Path.Combine(scriptFolder, frmHandleTitle + ".js")))
            {
                text.Text = "An existing handler with the name " + frmHandleTitle + ".js already exists.\nDo you wish to overwrite it?";

                yesEvent = (sender, e) => ModalAddHandler(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);
                yes.Click += yesEvent;

                noEvent = (sender, e) => Abort(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);
                no.Click += noEvent;
                modal.Visible = true;
                return;
            }

            ModalAddHandler(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);   
        }

        private void Abort(ZipFile zip, string scriptTempFolder, string frmHandleTitle, List<string> handlerFolders, string exeName)
        {                               
            zip.Dispose();
            Directory.Delete(scriptTempFolder, true);
            File.Delete(downloadPath);

            yes.Click -= yesEvent;
            no.Click -= noEvent;

            modal.Visible = false;
        }

        private void HideModal()
        {
            yes.Click -= yesEvent;
            no.Click -= noEvent;      
            modal.Visible = false;
        }

        private void AddGameToList(string frmHandleTitle, string exeName)
        {
            GameManager.Instance.AddScript(frmHandleTitle, new bool[] { false, false });
            SearchGame.Search(mainForm, exeName);

            yes.Click -= yesEvent;
            no.Click -= noEvent;

            modal.Visible = false;
        }

        private void ModalAddHandler(ZipFile zip, string scriptTempFolder,string frmHandleTitle, List<string> handlerFolders,string exeName)
        {
            yes.Click -= yesEvent;
            no.Click -= noEvent;

            if (Directory.Exists(Path.Combine(scriptFolder, frmHandleTitle)))
            {
                Directory.Delete(Path.Combine(scriptFolder, frmHandleTitle), true);
            }

            if (File.Exists(Path.Combine(scriptFolder, frmHandleTitle + ".js")))
            {
                File.Delete(Path.Combine(scriptFolder, frmHandleTitle + ".js"));
            }

           File.Move(Path.Combine(scriptTempFolder, "handler.js"), Path.Combine(scriptFolder, frmHandleTitle + ".js"));

            if (handlerFolders.Count > 0)
            {
                string gameFolder = Path.Combine(scriptFolder, frmHandleTitle);

                Directory.CreateDirectory(gameFolder);

                foreach (string hFolder in handlerFolders)
                {
                    string newFolder = Path.Combine(scriptTempFolder, hFolder);

                    foreach (string dir in Directory.GetDirectories(newFolder, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(Path.Combine(gameFolder, dir.Substring(newFolder.Length + 1)));
                    }

                    foreach (string file_name in Directory.GetFiles(newFolder, "*", SearchOption.AllDirectories))
                    {
                       File.Move(file_name, Path.Combine(gameFolder, file_name.Substring(newFolder.Length + 1)));
                    }

                    Directory.Delete(newFolder, true);
                }
            }

            Directory.Delete(scriptTempFolder, true);

            while (!zipExtractFinished)
            {
                Application.DoEvents();
            }
         
            zip.Dispose();

            File.Delete(downloadPath);

            yesEvent = (sender, e) => AddGameToList(frmHandleTitle, exeName);
            yes.Click += yesEvent;
            noEvent = (sender, e) => HideModal();
            no.Click += noEvent;

            text.Text =
            "Downloading and extraction of " + frmHandleTitle +
            " handler is complete. Would you like to add this game to Nucleus now? You will need to select the game executable to add it." +
            "Download finished! Add to Nucleus?";

            if (!modal.Visible)
            {
                modal.Visible = true;
            }
        }

        private void ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
            {
                entriesDone++;
            }

            if (e.EventType == ZipProgressEventType.Extracting_AfterExtractAll || (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry && entriesDone == numEntries))
            {
                zipExtractFinished = true;
            }
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void HubWebView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

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

        private void CloseBtn_Click(object sender, EventArgs e)
        {           
            if (Location.X == -32000 || Width == 0)
            {
                return;
            }

            ini.IniWriteValue("Misc", "DownloaderWindowSize", Width + "X" + Height);
            ini.IniWriteValue("Misc", "DownloaderWindowLocation", Location.X + "X" + Location.Y);

           Visible = false;
        }

        private void MaximizeBtn_Click(object sender, EventArgs e) => WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

        private void MinimizeBtn_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void HubWebView_ClientSizeChanged(object sender, EventArgs e)
        {
            if (mainForm == null)
            {
                return;
            }

            if (mainForm.roundedcorners)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
                }
                else
                {
                    Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                }
            }

            Invalidate();
        }

        private void CloseBtn_MouseEnter(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close_mousehover.png");

        private void CloseBtn_MouseLeave(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");

        private void MaximizeBtn_MouseEnter(object sender, EventArgs e) => maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize_mousehover.png");

        private void MaximizeBtn_MouseLeave(object sender, EventArgs e) => maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");

        private void MinimizeBtn_MouseLeave(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");

        private void MinimizeBtn_MouseEnter(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize_mousehover.png");

        private void Back_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.GoBack();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.GoForward();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            webView.CoreWebView2.Navigate(hubUri);
        }
    }
}
