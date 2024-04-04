using Nucleus.Gaming.Coop;
using System;

namespace Nucleus.Gaming.Tools.GameplayTimer
{
    public static class GameplayTimer
    {
        public static void SaveGameplayTime(UserGameInfo userGameInfo, int playedTime)
        {
            if (userGameInfo.TotalPlayTime == null)
            {
                userGameInfo.TotalPlayTime = playedTime.ToString();
            }
            else
            {
                userGameInfo.TotalPlayTime = (playedTime + int.Parse(userGameInfo.TotalPlayTime)).ToString();
            }

            userGameInfo.LastPlayedAt = DateTime.Now.ToString();

            GameManager.Instance.SaveUserProfile();
        }
    }
}
