using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] GameObject myPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(myPanel.activeInHierarchy == true)
            {
                myPanel.SetActive(false);
            }
            else
            {
                myPanel.SetActive(true);
            }
        }
    }

    public void ExitGameExitPanel()
    {
        //Debug.Log("quitting game");
        Application.Quit();
    }
}
