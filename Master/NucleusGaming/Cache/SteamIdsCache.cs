using System.Collections.Generic;

namespace Nucleus.Gaming.Cache
{    /// <summary>
     /// All steam ids are cached here so we can easily
     /// share/update the nicknames list between profile 
     /// settings and globals settings
     /// </summary>
    public static class SteamIdsCache
    {
        private static List<string> steamIdsList = new List<string>();

        public static List<string> Get
        {
            get => steamIdsList;
        }

        public static void Add(string steamid)
        {
            if (!steamIdsList.Contains(steamid))
            {
                steamIdsList.Add(steamid);
            }
        }
    }
}
