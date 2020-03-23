using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    const string RECENT_KEY = "recent";

    [SerializeField] public bool isItRedsTrun = true;
    [SerializeField] public int redDDcount = 12;
    [SerializeField] public int greenDDcount = 12;

    [SerializeField] TextMeshProUGUI redDDcountText;
    [SerializeField] TextMeshProUGUI greenDDcountText;

    EndTurnRefrence myEndTurn = new EndTurnRefrence();
    SceneManagerRename mySceneManager = new SceneManagerRename();

    //change turn method switches the bool state

    public void ChangeTurn()
    {
        isItRedsTrun = !isItRedsTrun;
        FindObjectOfType<DDmanager>().HideAllDD();

        FindObjectOfType<CellManager>().HideAllCells();
        FindObjectOfType<CellManager>().UpdateFreeSpaces();

        myEndTurn.ChangeButtonState(false);

        Cell[] allCells = FindObjectsOfType<Cell>();

        foreach (Cell cell in allCells)
        {
            cell.isaSkipCell = false;
        }

        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();

        foreach(OctiPawn maybeDeathOcti in allOctis)
        {
            if(maybeDeathOcti.isDeathHighlight)
            {
                Destroy(maybeDeathOcti);
                Destroy(maybeDeathOcti.gameObject);
            }
        }

        //0 is green win
        //1 is red win
        //2 is no win event

        //IsRedWin() is an int return type

        WinManager myWinManager = FindObjectOfType<WinManager>();
        if(myWinManager.IsRedWin() == 0)
        {
            //print("green wins");

            SaveData.Instance.DeleteGame(RECENT_KEY, 9999);
            WinTrack.winInt = 0;
            mySceneManager.LoadNextSceneOnDelay(1);
        }
        else if(myWinManager.IsRedWin() == 1)
        {
            //print("red wins");

            SaveData.Instance.DeleteGame(RECENT_KEY, 9999);
            WinTrack.winInt = 1;
            mySceneManager.LoadNextSceneOnDelay(1);
        }


        //turned off for developig purposes
        //
        //Camera mainCam = FindObjectOfType<Camera>();
        //mainCam.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
    }

    //updates the UI text displaying how much DDs each
    //side got

    public void UpdateDDcountText()
    {
        redDDcountText.text = redDDcount.ToString();
        greenDDcountText.text = greenDDcount.ToString();
    }

    private void Start()
    {
        myEndTurn = FindObjectOfType<EndTurnRefrence>();
        mySceneManager = FindObjectOfType<SceneManagerRename>();
        WinTrack.winInt = 2;
    }


}
