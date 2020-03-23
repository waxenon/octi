using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnce : MonoBehaviour
{
    void Start()
    {
        var sett = new Settings();
        sett.LoadSavedSettings();
        SaveData.Instance.LoadGameData();
    }

    private void OnDestroy()
    {
        var sett = new Settings();
        sett.SaveSettings();
    }
}
