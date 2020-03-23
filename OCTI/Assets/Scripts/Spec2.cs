using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spec2 : MonoBehaviour
{
    [SerializeField] private bool isRed = false;

    private void Start()
    {
        if (Settings.isDarkMode)
        {
            var txt = GetComponent<TextMeshProUGUI>();
            if (!isRed)
            {
                txt.text = "Red DD Count:";
            }
            else
            {
                txt.text = "Green DD count:";
            }
        }
    }
}
