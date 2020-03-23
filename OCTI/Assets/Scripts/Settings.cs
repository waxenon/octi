using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public static float volume = 1f;
    public static bool isDarkMode = false;
    public static bool loadRecGameSave = false;
    public static bool isBackAllowed = true;

    public Settings()
    {
        ;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetInt("isDarkMode", BoolToInt(isDarkMode));
        PlayerPrefs.SetInt("isBackAllowed", BoolToInt(isBackAllowed));
        PlayerPrefs.Save();
    }

    public void LoadSavedSettings()
    {
        volume = PlayerPrefs.GetFloat("volume");
        isDarkMode = IntToBool(PlayerPrefs.GetInt("isDarkMode"));
        isBackAllowed = IntToBool(PlayerPrefs.GetInt("isBackAllowed"));
    }

    private int BoolToInt(bool bl)
    {
        if(bl)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private bool IntToBool(int it)
    {
        if(it == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
