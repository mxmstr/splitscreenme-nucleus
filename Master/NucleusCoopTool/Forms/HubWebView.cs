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
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;


namespace Nucleus.Coop.Forms
{
    public partial class HubWebView : BaseForm
    {
        
        private readonly string darkReaderFolder = Path.Combine(Application.StartupPath, $"webview\\darkreader");
        private readonly string cacheFolder = Path.Combine(Application.StartupPath, $"webview\\cache");
        private string downloadPath = Path.Combine(Application.StartupPath, "handlers\\handler.nc");
        private string scriptFolder = GameManager.Instance.GetJsScriptsPath();
        private const string hubUri = "https://hub.splitscreen.me/";
        private readonly string theme = Globals.ThemeFolder;
        private string prevValidUri;
       
        private CoreWebView2DownloadOperation downloadOperation;
        private CoreWebView2Settings webViewSettings;
        private MainForm mainForm;

        private bool zipExtractFinished;
        private bool downloadCompleted;
        private bool hasFreshCahe;

        private int entriesDone = 0;
        private int numEntries;
        
        private EventHandler Modal_Yes_Button_Event;
        private EventHandler Modal_No_Button_Event;     

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

            if (mainForm.roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");
            maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");
            minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");
            modal.BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");

            modal_text.BackColor = Color.FromArgb(80, 0, 0, 0);
            modal_yes.BackColor = Color.FromArgb(80, 0, 0, 0);
            modal_no.BackColor = Color.FromArgb(80, 0, 0, 0);

            modal_yes.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            modal_no.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;

            home.Text = "";
            home.BackgroundImage = ImageCache.GetImage(theme + "home.png");
            home.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            back.Text = "";
            back.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            back.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            next.Text = "";
            next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            next.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;

            webView.DefaultBackgroundColor = mainForm.BackColor;

            Load += OnLoad;
        }

        private async void OnLoad(object sender, EventArgs e)
        {
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            await InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }

            //environment.CreateWebResourceResponse() a voir avec r-mach
            //environment.CreateWebResourceRequest
            CoreWebView2Environment environment = null;
            CoreWebView2EnvironmentOptions environmentOptions = new CoreWebView2EnvironmentOptions();
            environmentOptions.AreBrowserExtensionsEnabled = true;

            webView.CreationProperties = new Microsoft.Web.WebView2.WinForms.CoreWebView2CreationProperties();
            webView.CreationProperties.UserDataFolder = cacheFolder + "\\WebViewCache";


            if (Directory.Exists(webView.CreationProperties.UserDataFolder))
            {           
                environment = await CoreWebView2Environment.CreateAsync(null, webView.CreationProperties.UserDataFolder, environmentOptions);
                await webView.EnsureCoreWebView2Async(environment);

                webView.Source = new Uri(hubUri);
                hasFreshCahe = false;
                return;
            }

            hasFreshCahe = true;

            environment = await CoreWebView2Environment.CreateAsync(null, webView.CreationProperties.UserDataFolder, environmentOptions);
            await webView.EnsureCoreWebView2Async(environment);
        }

        private async void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webViewSettings = webView.CoreWebView2.Settings;
            webViewSettings.IsWebMessageEnabled = true;
            webViewSettings.IsGeneralAutofillEnabled = true;
            webViewSettings.AreDefaultContextMenusEnabled = false;

            if (hasFreshCahe)
            {
               await webView.CoreWebView2.Profile.AddBrowserExtensionAsync(darkReaderFolder);
            }

            if (webView.Source.AbsolutePath == "blank")
            {
                webView.Source = new Uri(hubUri);
            }

            webView.CoreWebView2.IsDefaultDownloadDialogOpenChanged += IsDefaultDownloadDialogOpenChanged;
            webView.CoreWebView2.DOMContentLoaded += new EventHandler<CoreWebView2DOMContentLoadedEventArgs>(DOMContentLoaded);
            webView.CoreWebView2.DownloadStarting += new EventHandler<CoreWebView2DownloadStartingEventArgs>(DownloadStarting);                
            webView.CoreWebView2.SourceChanged += new EventHandler<CoreWebView2SourceChangedEventArgs>(SourceChanged);
            webView.CoreWebView2.ContentLoading += new EventHandler<CoreWebView2ContentLoadingEventArgs>(ContentLoading);
        }

        private void ContentLoading(object sender, CoreWebView2ContentLoadingEventArgs e) => BlockRedirection();

        private void SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e) => BlockRedirection();

        private void BlockRedirection()
        {
            string source = webView.CoreWebView2.Source;

            if (source.StartsWith("https://hub.splitscreen.me/"))
            {
                prevValidUri = webView.CoreWebView2.Source;
                JSInjectect();
                return;
            }

            webView.CoreWebView2.Stop();
            webView.CoreWebView2.Navigate(prevValidUri);
            JSInjectect();
        }

        private void JSInjectect()
        {
            //Main Page
            webView.ExecuteScriptAsync("document.getElementsByClassName('header ant-layout-header')[0].style.backgroundColor = '#000'; ");
            webView.ExecuteScriptAsync("document.getElementsByClassName('ant-menu ant-menu-dark ant-menu-root ant-menu-horizontal')[0].style.backgroundColor = '#000'; ");
            webView.ExecuteScriptAsync("document.getElementsByClassName('ant-menu-item')[0].style.display = 'none'; ");//splitscreen.me redirection link
            webView.ExecuteScriptAsync("document.getElementsByClassName('ant-menu-item')[3].style.display = 'none'; ");//Documentation link
            webView.ExecuteScriptAsync("document.getElementsByClassName('ant-menu-item')[5].style.display = 'none'; ");//Contributors search page
        }

        private void DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            webView.ZoomFactor = 0.8;
            JSInjectect();                     
            Show();
        }

        private void IsDefaultDownloadDialogOpenChanged(object sender, object e)
        {
            if (webView.CoreWebView2.IsDefaultDownloadDialogOpen) webView.CoreWebView2.CloseDefaultDownloadDialog();
        }

        private void DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            if (!webView.CoreWebView2.Source.StartsWith("https://hub.splitscreen.me/"))
            {
                downloadOperation.Cancel();
                return;
            }

            e.ResultFilePath = downloadPath;
            downloadOperation = e.DownloadOperation;
            downloadOperation.StateChanged += CheckDownloadState;
            label.Visible = true;
        }
      
        private void CheckDownloadState(object sender, object e)
        {
            if (downloadOperation.State == CoreWebView2DownloadState.Completed && !downloadCompleted)
            {
                label.Visible = false;
                zipExtractFinished = false;
                entriesDone = 0;
                numEntries = 0;
                downloadCompleted = true;
                ExtractHandler();
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
                modal_text.Text = "An existing handler with the name " + frmHandleTitle + ".js already exists.\nDo you wish to overwrite it?";

                Modal_Yes_Button_Event = (sender, e) => ModalAddHandler(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);
                modal_yes.Click += Modal_Yes_Button_Event;

                Modal_No_Button_Event = (sender, e) => Abort(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);
                modal_no.Click += Modal_No_Button_Event;
                modal.Visible = true;
                modal.BringToFront();
                return;
            }

            ModalAddHandler(zip, scriptTempFolder, frmHandleTitle, handlerFolders, exeName);
        }

        private void Abort(ZipFile zip, string scriptTempFolder, string frmHandleTitle, List<string> handlerFolders, string exeName)
        {
            zip.Dispose();
            Directory.Delete(scriptTempFolder, true);
            File.Delete(downloadPath);
            HideModal();
        }

        private void AddGameToList(string frmHandleTitle, string exeName)
        {
            GameManager.Instance.AddScript(frmHandleTitle, new bool[] { false, false });
            SearchGame.Search(mainForm, exeName);
            HideModal();
        }

        private void ModalAddHandler(ZipFile zip, string scriptTempFolder, string frmHandleTitle, List<string> handlerFolders, string exeName)
        {
            modal_yes.Click -= Modal_Yes_Button_Event;
            modal_no.Click -= Modal_No_Button_Event;

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

            Modal_Yes_Button_Event = (sender, e) => AddGameToList(frmHandleTitle, exeName);
            modal_yes.Click += Modal_Yes_Button_Event;
            Modal_No_Button_Event = (sender, e) => HideModal();
            modal_no.Click += Modal_No_Button_Event;

            modal_text.Text =
            "Downloading and extraction of " + frmHandleTitle +
            " handler is complete. Would you like to add this game to Nucleus now? " +
            "You will need to select the game executable to add it.";

            if (!modal.Visible)
            {
                modal.Visible = true;
                modal.BringToFront();
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

        private void HideModal()
        {
            modal_yes.Click -= Modal_Yes_Button_Event;
            modal_no.Click -= Modal_No_Button_Event;
            modal.Visible = false;
            downloadCompleted = false;
            modal.SendToBack();
        }

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

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            mainForm.hubView = null;
            Close();

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

    }
}
