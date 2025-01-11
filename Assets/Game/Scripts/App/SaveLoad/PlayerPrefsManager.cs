using UnityEngine;

namespace SampleGame.App.SaveLoad
{
    public static class PlayerPrefsManager
    {
        private const string LocalVersion = "LocalVersion";
        private const string RemoteVersion = "RemoteVersion";

        public static int GetCurrentVersion()
        {
            return Mathf.Max(PlayerPrefs.GetInt(LocalVersion), PlayerPrefs.GetInt(RemoteVersion));
        }

        public static int GetLocalVersion()
        {
            return PlayerPrefs.GetInt(LocalVersion);
        }

        public static void SetLocalVersion(int version)
        {
            if (version <= 0)
                return;

            PlayerPrefs.SetInt(LocalVersion, version);
            PlayerPrefs.Save();
        }

        public static void SetRemoteVersion(int version)
        {
            if (version <= 0)
                return;

            PlayerPrefs.SetInt(RemoteVersion, version);
            PlayerPrefs.Save();
        }
    }
}