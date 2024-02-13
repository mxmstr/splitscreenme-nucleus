using Games;
using Ionic.Zip;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using NAudio.SoundFont;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Nucleus.Coop.Forms
{
    public partial class HubWebView : UserControl, IDynamicSized
    {
        private readonly string darkReaderFolder = Path.Combine(Application.StartupPath, $"webview\\darkreader");
        private readonly string cacheFolder = Path.Combine(Application.StartupPath, $"webview\\cache");
        private string downloadPath = Path.Combine(Application.StartupPath, "handlers\\handler.nc");
        private string scriptFolder = GameManager.Instance.GetJsScriptsPath();
        private readonly string hubUri = "https://hub.splitscreen.me/?fromWebview=true";
        private readonly string theme = Globals.ThemeFolder;

        private CoreWebView2DownloadOperation downloadOperation;
        private CoreWebView2Settings webViewSettings;
        private MainForm mainForm;

        private bool zipExtractFinished;
        private bool downloadCompleted;
        private bool hasFreshCahe;

        private int entriesDone = 0;
        private int numEntries;
        private float scale;
        private EventHandler Modal_Yes_Button_Event;
        private EventHandler Modal_No_Button_Event;

        private List<JObject> installedHandlers = new List<JObject>();

        public HubWebView(MainForm mainForm)
        {
            this.mainForm = mainForm;
            scale = mainForm.scale;
            
            InitializeComponent();

            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BackColor = Color.FromArgb(0, 0, 0, 0);

            modal.BackColor = Color.FromArgb(int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[0]),
                                               int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[1]),
                                               int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[2]),
                                               int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[3]));

            modal_yes.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            modal_yes.Cursor = mainForm.hand_Cursor;
            modal_no.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            modal_no.Cursor = mainForm.hand_Cursor;

            home.BackgroundImage = ImageCache.GetImage(theme + "home.png");
            home.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            home.BackColor = BackColor;
            home.Cursor = mainForm.hand_Cursor;

            back.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            back.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            back.BackColor = BackColor;
            back.Cursor = mainForm.hand_Cursor;

            next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            next.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            next.BackColor = BackColor;
            next.Cursor = mainForm.hand_Cursor;

            closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");
            closeBtn.FlatAppearance.MouseOverBackColor = mainForm.MouseOverBackColor;
            closeBtn.BackColor = BackColor;
            closeBtn.Cursor = mainForm.hand_Cursor;

            webView.DefaultBackgroundColor = mainForm.BackColor;
         
            button_Panel.BackColor = mainForm.BackColor;

            string debugUri = Path.Combine(Application.StartupPath, $"webview\\debugUri.txt");

            if (File.Exists(debugUri))
            {
                StreamReader local = new StreamReader(debugUri);

                string expectedUri = "http://localhost:3000/";
                string descResult = local.ReadToEnd();

                if(expectedUri == descResult)
                {
                    hubUri = expectedUri;
                }
               
                local.Dispose();
            }
            
            Load += OnLoad;
            closeBtn.Click += new EventHandler(mainForm.EnableGameList);
            Disposed += new EventHandler(mainForm.btn_AddGames_Set_BackColor);

            BuildHandlersDatas();
           

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        private void BuildHandlersDatas()
        {
            foreach(KeyValuePair<string,GenericGameInfo> game in GameManager.Instance.GameInfos)
            {
                if(GameManager.Instance.User.Games.All(g => g.GameGuid != game.Value.GUID ))
                {
                    continue;
                }

                string id = game.Value.HandlerId;
                int version = game.Value.Hub.Handler.Version;

                if (id != "" && version != -1 )
                {
                    installedHandlers.Add(new JObject { new JProperty("id", id), new JProperty("version", version) });
                }
            }
        }


        private async void OnLoad(object sender, EventArgs e)
        {
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {        
            CoreWebView2Environment environment;
            CoreWebView2EnvironmentOptions environmentOptions = new CoreWebView2EnvironmentOptions();
            environmentOptions.AreBrowserExtensionsEnabled = true;

            webView.CreationProperties = new CoreWebView2CreationProperties();
            webView.CreationProperties.UserDataFolder = cacheFolder;
           
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
 #if RELEASE 
            webViewSettings.AreDefaultContextMenusEnabled = false;
#endif
            webViewSettings.IsScriptEnabled = true;

            if (webView.Source.AbsolutePath == "blank")
            {
                webView.Source = new Uri(hubUri);
            }

            if (hasFreshCahe)
            {
               await webView.CoreWebView2.Profile.AddBrowserExtensionAsync(darkReaderFolder);          
            }

            webView.CoreWebView2.IsDefaultDownloadDialogOpenChanged += IsDefaultDownloadDialogOpenChanged;
            webView.CoreWebView2.DOMContentLoaded += new EventHandler<CoreWebView2DOMContentLoadedEventArgs>(DOMContentLoaded);
            webView.CoreWebView2.DownloadStarting += new EventHandler<CoreWebView2DownloadStartingEventArgs>(DownloadStarting);                 
            webView.CoreWebView2.NewWindowRequested += new EventHandler<CoreWebView2NewWindowRequestedEventArgs>(NewWindowRequested);
            webView.CoreWebView2.WebMessageReceived += new EventHandler<CoreWebView2WebMessageReceivedEventArgs>(WebMessageReceived);

            BringToFront();
        }

        private void WebMessageReceived(object sender , CoreWebView2WebMessageReceivedEventArgs e)
        {
            Console.WriteLine(e.WebMessageAsJson);
        }

        private void NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            CoreWebView2 currentWindow = (CoreWebView2)sender;
            e.Handled = true;
            currentWindow.Navigate(e.Uri);
        }

        private async void SendDatas()
        { 
            string json = JsonConvert.SerializeObject(installedHandlers, Formatting.Indented);         
            await webView.ExecuteScriptAsync($"NucleusWebview.setLocalHandlerLibraryArray({json})");
            // string test = JsonConvert.SerializeObject(new string[] { "test message" }, Formatting.Indented);
            // await webView.ExecuteScriptAsync($"window.chrome.webview.postMessage({test});");
        }

        private void DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            if(hasFreshCahe)
            {
                webView.CoreWebView2.Reload();
                hasFreshCahe = false;
            }

            webView.ZoomFactor = 0.8;
            SendDatas();                     
        }

        private void IsDefaultDownloadDialogOpenChanged(object sender, object e)
        {
            if (webView.CoreWebView2.IsDefaultDownloadDialogOpen) webView.CoreWebView2.CloseDefaultDownloadDialog();
        }

        private void DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {         
            if (!webView.CoreWebView2.Source.StartsWith("https://hub.splitscreen.me/"))
            {
               e.DownloadOperation.Cancel();
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
            closeBtn.PerformClick();
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

            if (GameManager.Instance.IsGameAlreadyInUserProfile(exeName))
            {
                GameManager.Instance.AddScript(frmHandleTitle, new bool[] { false, false });
                string gameGuid = GameManager.Instance.User.Games.Where(c => c.ExePath.Split('\\').Last().ToLower() == exeName.ToLower()).FirstOrDefault().GameGuid;
                string gameName = GameManager.Instance.Games.Where(c => c.Value.GUID == gameGuid).FirstOrDefault().Value.GameName;

                mainForm.controls.Where(c => c.Value.TitleText == gameName).FirstOrDefault().Value.GameInfo.UpdateAvailable = false;
                mainForm.button_UpdateAvailable.Visible = false;
                mainForm.RefreshGames();
               
                HideModal();

                return;
            }

            installedHandlers.Clear();
            GameManager.Instance.Initialize();
            BuildHandlersDatas();
            SendDatas();

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
            webView.Dispose();    
            downloadCompleted = false;          
            Dispose();
        }

        public void UpdateSize(float scale)
        {

            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            button_Panel.Left = (int)(button_Panel.Left * scale);
            button_Panel.Top = (int)(button_Panel.Top * scale);
            button_Panel.Width = (int)(button_Panel.Width * scale);
            button_Panel.Height = (int)(button_Panel.Height* scale);
            
            foreach (Control c in button_Panel.Controls)
            {
                c.Width = (int)(c.Width * scale);
                c.Height = (int)(c.Height * scale);
                c.Left = (int)(c.Location.X * scale);
                c.Top = (int)(c.Location.Y * scale);
            }
        
            //Width = (int)(Width * scale);
            //Height = (int)(Height * scale);

            //Top = (int)(Top * scale);
            //Left = (int)(Left * scale);
            button_Panel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, button_Panel.Width, button_Panel.Height, 20, 20));        
        }

    }
}
