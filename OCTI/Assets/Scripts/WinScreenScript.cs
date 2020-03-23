using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenScript : MonoBehaviour
{
    [SerializeField] string GreenWinDisplay;
    [SerializeField] string RedWinDiplay;
    [SerializeField] TextMeshProUGUI myText;

    void Start()
    {
        if(WinTrack.winInt == 0)
        {
            myText.text = GreenWinDisplay;
        }
        else if(WinTrack.winInt == 1)
        {
            myText.text = RedWinDiplay;
        }
    }
}
