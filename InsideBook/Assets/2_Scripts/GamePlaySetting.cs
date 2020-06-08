using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySetting
{
    public static int BaseDashCount = 1;
    public static bool IsDead = false;
    public static Checkpoint CurrentCheckPoint;

    #region Save Game
    public static void SaveInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
        PlayerPrefs.Save();
    }

    public static void SaveFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
        PlayerPrefs.Save();
    }

    public static void SaveString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
        PlayerPrefs.Save();
    }
    #endregion Save Game

    #region Load Game
    public static int LoadInt(string key, int defaultVal = 1)
    {
        return PlayerPrefs.GetInt(key, defaultVal);
    }

    public static float LoadFloat(string key, float defaultVal = 1.0f)
    {
        return PlayerPrefs.GetFloat(key, defaultVal);
    }

    public static string LoadString(string key, string defaultVal = "")
    {
        return PlayerPrefs.GetString(key, defaultVal);
    }
    #endregion Load Game
}

public class SaveKey
{
    public const string SoundVolume = "SoundVolume";
    public const string MusicVolume = "MusicVolume";
}