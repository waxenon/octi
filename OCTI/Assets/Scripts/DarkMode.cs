using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

//[ExecuteInEditMode]
public class DarkMode : MonoBehaviour
{
    public Material EffectMaterial;
    public Material EmptyMaterial;

    List<Color> ogColors = new List<Color>();
    List<TextMeshProUGUI> allTexts = new List<TextMeshProUGUI>();

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (EffectMaterial != null && Settings.isDarkMode)
        {
            Graphics.Blit(src, dst, EffectMaterial);
        }
        else
        {
            Graphics.Blit(src, dst, EmptyMaterial);
        }

        TextMeshProUGUI[] findAllTexts = FindObjectsOfType<TextMeshProUGUI>();

        foreach(TextMeshProUGUI text in findAllTexts)
        {
            if(!allTexts.Contains(text))
            {
                allTexts.Add(text);
                ogColors.Add(text.color);
            }
        }

        if(Settings.isDarkMode)
        {
            foreach(TextMeshProUGUI text in allTexts)
            {
                text.color = InvertColor
                    (ogColors.ElementAt(allTexts.IndexOf(text)));
            }
        }
        else
        {
            foreach(TextMeshProUGUI text in allTexts)
            {
                text.color = ogColors.ElementAt(allTexts.IndexOf(text));
            }
        }

    }

    public void ChangeDarkModeSetting()
    {
        Settings.isDarkMode = !Settings.isDarkMode;
    }

    private Color InvertColor(Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b);
    }
}

