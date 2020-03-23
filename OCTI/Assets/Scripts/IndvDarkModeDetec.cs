using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndvDarkModeDetec : MonoBehaviour
{
    Color myOgColor = new Color();

    private void Start()
    {
        myOgColor = GetComponent<Image>().color;
    }

    private void Update()
    {
        if(Settings.isDarkMode)
        {
            GetComponent<Image>().color = InvertColor(myOgColor);
        }
        else
        {
            GetComponent<Image>().color = myOgColor;
        }
    }

    private Color InvertColor(Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b);
    }
}
