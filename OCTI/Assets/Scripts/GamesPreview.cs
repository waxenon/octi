using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamesPreview : MonoBehaviour
{
    [SerializeField] GameObject nameDisplayObj;
    int indexOfView = 0;
    int absoluteid = 0;

    private void Start()
    {
        if(SaveData.Instance.GetSavedGamesLength() < 2)
        {           
            var allButtons = GetComponentsInChildren<Button>();
            foreach(Button button in allButtons)
            {
                Destroy(button);
            }
            return;
        }

        int id = Modulo(indexOfView, SaveData.Instance.GetSavedGamesLength() - 1);

        absoluteid = id;

        nameDisplayObj.GetComponent<TextMeshProUGUI>().text = 
            SaveData.Instance.GetGameNameByID(id);
    }

    public void ChangeGameDisplayed(int changefactor)
    {
        indexOfView += changefactor;
        if(indexOfView < 0)
        {
            indexOfView = SaveData.Instance.GetSavedGamesLength() - 2;
        }

        int id = Modulo(indexOfView, SaveData.Instance.GetSavedGamesLength() - 1);

        absoluteid = id;

        nameDisplayObj.GetComponent<TextMeshProUGUI>().text =
            SaveData.Instance.GetGameNameByID(id);
    }

    public void LoadGameDisplayed()
    {
        SaveData.Instance.gameToLoad = SaveData.Instance.GetGame
            (nameDisplayObj.GetComponent<TextMeshProUGUI>().text);

        FindObjectOfType<SceneManagerRename>().RecentGameSave("Game");
    }

    public void DeleteAllSaves()
    {
        SaveData.Instance.DeleteAllGames();
    }

    public void DeleteSelectedSave()
    {
        var textDisplayed = nameDisplayObj.GetComponent<TextMeshProUGUI>().text;

        SaveData.Instance.DeleteGame
            (textDisplayed, absoluteid);
        FindObjectOfType<SceneManagerRename>().LoadSceneByName("AllSavesScene");
    }

    private int Modulo(int i, int division)
    {
        int tmpI = i;
        int tmpDivision = division;

        if(i >= division && division != 0)
        {
            while(tmpI >= tmpDivision)
            {
                tmpI -= tmpDivision;
            }
        }

        return tmpI;

    }
}
