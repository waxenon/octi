using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSave : MonoBehaviour
{
    //below explaination
    /*
     the gamesave model is the following:
     there is a List of Vector3Int which each item
     is a record of a move done.
     The idea is to replicate a game by it's moves with zero
     player input.

        each Vector3Int has 3 items, and every one of them records
        a different part of a move taken

        x value - Octi ID
        ~~~~~~~~~~~~~~~~~~
        each Octi has a unique ID, and it lets identify which
        Octi has taken a turn

        y value - IsMoving
        ~~~~~~~~~~~~~~~~~~~
        each Octi is ethier having a DD initialized, or moving.
        
        0 = dd
        1 = move

        z value - Direction
        ~~~~~~~~~~~~~~~~~~~~
        Starting from forward (Direction ID = 1) to
        forward left (Direction ID = 8) there are eight directions
     */

    const string RECENT_KEY = "recent";
    const string GO_TURN_BACK_KEY = "goback";

    [SerializeField] bool willResetGameToLoad = true;

    [SerializeField] List<Vector3Int> localGameSave = new List<Vector3Int>();
    List<Vector3Int> mostRecentGameLoaded = new List<Vector3Int>();

    public List<Vector3Int> GetCurGame()
    {
        return localGameSave;
    }

    public void SaveGameData(Vector3Int moveRecord)
    {
        localGameSave.Add(moveRecord);
    }

    public void SaveCurGameAs(string name)
    {        
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        SaveData.Instance.AddGame(name, localGameSave);
    }

    public void RecentGameSaveO()
    {
        var keyOfRecentGameLoaded = SaveData.Instance.GetGameByValue(mostRecentGameLoaded);
        if (keyOfRecentGameLoaded == RECENT_KEY || keyOfRecentGameLoaded == "ABCDEFG")
        {
            SaveData.Instance.AddGame(RECENT_KEY, localGameSave);
        }
    }

    public void SaveGameAsTMPinput(TMP_InputField myField)
    {
        string nameOfSave = myField.text;
        var recGame = SaveData.Instance.GetGame(RECENT_KEY); 

        if (myField == null || string.IsNullOrEmpty(nameOfSave))
        {
            return;
        }

        SaveData.Instance.AddGame(nameOfSave, recGame);
    }

    public void ClearCurrentGame()
    {
        localGameSave.Clear();
    }

    public void GameSaveStart()
    {
        if (SaveData.Instance.GetGame(RECENT_KEY) != null)
        {
            LoadGame(SaveData.Instance.GetGame(RECENT_KEY));
        }
    }

    public void GoTurnBack()
    {
        if (localGameSave.Count == 0 || !Settings.isBackAllowed)
        {
            return;
        }

        if (localGameSave.Count == 1)
        {
            Settings.loadRecGameSave = false;
            FindObjectOfType<SceneManagerRename>().ResetScene(true, RECENT_KEY);
        }

        localGameSave.RemoveAt(localGameSave.Count - 1);
        Settings.loadRecGameSave = true;
        SaveCurGameAs(RECENT_KEY);

        FindObjectOfType<SceneManagerRename>().ResetScene(true, RECENT_KEY);
    }

    public void QueueOnLoad(TMP_InputField myField)
    {
        string nameOfQueue = myField.text;

        List<Vector3Int> game = SaveData.Instance.GetGame(nameOfQueue);
        if(game == null)
        {
            var mp = FindObjectOfType<MessegePanel>();
                mp.OpenMPanel("No game called " + nameOfQueue + " found");
            return;
        }
        Settings.loadRecGameSave = true;
        SaveData.Instance.gameToLoad = game;
        FindObjectOfType<SceneManagerRename>().LoadSceneByName("Game");
    }

    public void LoadGame(List<Vector3Int> gameSave)
    {
        List<Vector3Int> dupGameSave = gameSave;
        mostRecentGameLoaded = gameSave;
        localGameSave.Clear();

        //0 = DD
        //1 = move

        foreach (Vector3Int moveRecord in dupGameSave)
        {
            localGameSave.Add(moveRecord);

            if (moveRecord.y == 0)
            {
                OctiPawn chosenOcti = GetOctiById(moveRecord.x);
                DD chosenDD = GetDDbyId(moveRecord.z, chosenOcti);

                chosenDD.gameObject.SetActive(true);
                chosenOcti.myDirections.Add(chosenDD.direction);

                chosenDD.isInitialized = true;

                bool isOctiRed = chosenOcti.isRed;

                if (isOctiRed)
                {
                    FindObjectOfType<TurnManager>().redDDcount -= 1;
                }
                else
                {
                    FindObjectOfType<TurnManager>().greenDDcount -= 1;
                }

                FindObjectOfType<TurnManager>().UpdateDDcountText();
                FindObjectOfType<TurnManager>().ChangeTurn();
            }
            else if (moveRecord.y == 1)
            {
                OctiPawn chosenOcti = GetOctiById(moveRecord.x);
                DD chosenDD = GetDDbyId(moveRecord.z, chosenOcti);

                chosenOcti.ChangeHighlightState(true);

                Cell[] allCells = FindObjectsOfType<Cell>();
                List<Cell> activeCells = new List<Cell>();

                FindObjectOfType<CellManager>().HighlightAvailableCells(chosenOcti);

                foreach (Cell potActCell in allCells)
                {
                    if (potActCell.isActive)
                    {
                        activeCells.Add(potActCell);
                    }
                }

                foreach (Cell actCell in activeCells)
                {
                    Vector2Int direction = actCell.GetMyCellCords();
                    direction -= chosenOcti.GetOctiCords();

                    if (direction == chosenDD.direction)
                    {
                        actCell.MoveOctiToCell(true);
                    }
                    else if (direction == chosenDD.direction * 2)
                    {
                        actCell.MoveOctiToCell(true);
                    }

                }

            }
        }
    }

    private OctiPawn GetOctiById(int ID)
    {
        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();

        foreach(OctiPawn octi in allOctis)
        {
            if(octi.octiId == ID)
            {
                return octi;
            }
        }

        return new OctiPawn();
    }

    private DD GetDDbyId(int ID, OctiPawn host)
    {
        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();
        List<DD> allDDs = new List<DD>();

        foreach(OctiPawn octi in allOctis)
        {
            foreach(GameObject dd in octi.myDDsRefrence)
            {
                allDDs.Add(dd.GetComponent<DD>());
            }
        }

        foreach(DD dd in allDDs)
        {
            if(dd.directionID == ID && host.myDDsRefrence.Contains(dd.gameObject))
            {
                return dd;
            }
        }

        return new DD();
    }

    private void OnDestroy()
    {
        //if (mostRecentGameLoaded.Count == 0 && willResetGameToLoad)
        //{
        //    SaveData.Instance.gameToLoad = localGameSave;
        //}
        if(willResetGameToLoad)
        {
            SaveData.Instance.gameToLoad = localGameSave;
        }
    }
}
