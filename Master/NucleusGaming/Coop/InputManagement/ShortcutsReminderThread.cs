using System.Threading;

namespace Nucleus.Gaming.Coop.InputManagement
{
    public static class ShortcutsReminderThread
    {
        private static Thread reminderThread;

        public static void ToggleShortcutsReminder()
        {
            if (reminderThread == null)
            {
                reminderThread = new Thread(delegate ()
                {
                    //ShortcutsReminder shortcutsReminder = new ShortcutsReminder(7);
                    System.Windows.Threading.Dispatcher.Run();
                });

                reminderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                reminderThread.Start();

                //foreach(var player in GameProfile.Instance.DevicesList)
                //{
                //    if(player.ProcessData != null)
                //    {
                //        if(player.ProcessData.HWnd != null)
                //        {
                //            player.ProcessData.HWnd.TopMost = false;
                //        }
                //    }                  
                //}

                //IntPtr hWnd = User32Interop.FindWindow(null, "Shortcuts Reminder");

                //if (hWnd != IntPtr.Zero)
                //{
                //    HwndInterface.MakeTopMost(hWnd);
                //    User32Interop.BringWindowToTop(hWnd);
                //}
            }
            else
            {
                //var gameInfo = GenericGameHandler.Instance.currentGameInfo;

                //if (gameInfo != null)
                //{
                //    foreach (var player in GameProfile.Instance.DevicesList)
                //    {
                //        if (!gameInfo.NotTopMost)
                //        {
                //            if (player.ProcessData != null)
                //            {
                //                if (player.ProcessData.HWnd != null)
                //                {
                //                    player.ProcessData.HWnd.TopMost = true;
                //                }
                //            }
                //        }
                //    }

                //    if (gameInfo.SetForegroundWindowElsewhere)
                //    {
                //        GlobalWindowMethods.ChangeForegroundWindow();
                //    }
                //}

                reminderThread.Abort();
                reminderThread = null;
                Thread.Sleep(1000);//Be carefull from where you call the thread 
            }
        }
    }
}
