using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher.Utils
{
    internal class ModdedLobbyCheck
    {
        public static bool IsThisAModdedLobby()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out object gamemodeObject);
                string gamemode = gamemodeObject.ToString();
                if (gamemode.Contains("MODDED_"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
