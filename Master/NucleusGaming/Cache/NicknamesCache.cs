using System.Collections.Generic;

namespace Nucleus.Gaming.Cache
{    /// <summary>
     /// All nicknames are cached here so we can easily
     /// share/update the nicknames list between profile 
     /// settings and globals settings
     /// </summary>
    public static class NicknamesCache
    {
        private static List<string> nicknamesList = new List<string>();

        public static List<string> Get
        {
            get => nicknamesList;
        }

        public static void Add(string nickname)
        {
            if (!nicknamesList.Contains(nickname))
            {
                nicknamesList.Add(nickname);
            }
        }
    }
}
