using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingChanger : MonoBehaviour
{
    public void ChangeBackAllowedState(bool state)
    {
        Settings.isBackAllowed = state;
    }

    public void ToggleIsBackAllowedState()
    {
        Settings.isBackAllowed = !Settings.isBackAllowed;
    }
}
