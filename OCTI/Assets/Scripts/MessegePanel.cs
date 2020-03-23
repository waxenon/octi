using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessegePanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] TextMeshProUGUI textOfPanel;

    public void OpenMPanel(string messege)
    {
        textOfPanel.text = messege;
        Panel.SetActive(true);
    }
}
