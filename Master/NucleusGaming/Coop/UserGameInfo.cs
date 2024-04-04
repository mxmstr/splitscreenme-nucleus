using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Nucleus.Gaming.Coop
{
    public class UserGameInfo
    {
        private GenericGameInfo game;
        private List<GameProfile> profiles;
        private string exePath;
        private string gameGuid = "";
        private bool favorite = false;
        private bool keepSymLink;
        private bool firstLaunch;
        private string lastPlayedAt;
        private string totalPlayTime;

        [JsonIgnore]
        public GenericGameInfo Game
        {
            get
            {
                if (game == null)
                {
                    GameManager.Instance.Games.TryGetValue(gameGuid, out game);
                }
                return game;
            }
        }

        [JsonIgnore]
        public Bitmap Icon
        {
            get;
            set;
        }

        public string GameGuid
        {
            get => gameGuid;
            set => gameGuid = value;
        }

        private bool disableProfiles;
        public bool DisableProfiles
        {
            get => disableProfiles;
            set => disableProfiles = value;
        }

        public string ExePath
        {
            get => exePath;
            set => exePath = value;
        }

        public bool Favorite
        {
            get => favorite;
            set => favorite = value;
        }

        public bool KeepSymLink
        {
            get => keepSymLink;
            set => keepSymLink = value;
        }

        public bool FirstLaunch
        {
            get => firstLaunch;
            set => firstLaunch = value;
        }

        public string LastPlayedAt
        {
            get => lastPlayedAt;
            set => lastPlayedAt = value;
        }

        public string TotalPlayTime
        {
            get => totalPlayTime;
            set => totalPlayTime = value;
        }

        public UserGameInfo()
        {

        }

        public string GetLastPlayed()
        {
            if (lastPlayedAt == null)
            {
                return "...";
            }

            return lastPlayedAt.Split(' ')[0];//dispaly the date only
        }

        public string GetPlayTime()
        {
            if (totalPlayTime == null)
            {
                return "00h:00m:00s";
            }

            int totalSeconds = int.Parse(totalPlayTime);

            int seconds = (totalSeconds % 60);
            int minutes = (totalSeconds % 3600) / 60;
            int hours = (totalSeconds % 86400) / 3600;

            string formatHours = hours >= 10 ? "" : "0";
            string formatMinutes = minutes >= 10 ? "" : "0";
            string formatSecondes = seconds >= 10 ? "" : "0";

            return $"{formatHours}{hours}h:{formatMinutes}{minutes}m:{formatSecondes}{seconds}s";
        }

        /// <summary>
        /// If the game exists
        /// </summary>
        /// <returns></returns>
        public bool IsGamePresent()
        {
            return File.Exists(exePath);
        }

        public void InitializeDefault(GenericGameInfo game, string exePath)
        {
            this.game = game;
            gameGuid = game.GUID;
            this.exePath = exePath.Replace("I", "i");
            profiles = new List<GameProfile>();
        }
    }
}
