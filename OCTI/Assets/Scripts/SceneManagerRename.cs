using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerRename : MonoBehaviour
{
    //a simple Scene manager class, not to be confused with UniyEngine.SceneManagement.SceneManager
    //(I'm fucking dumb shouldn't of named it that way)

    //nvm fixed it (kinda lazy tho)

    const string RECENT_KEY = "recent";
    const string GO_TURN_BACK_KEY = "goback";

    public void ResetScene(bool willLoadGame, string nameOfSave)
    {
        if(willLoadGame)
        {
            SaveData.Instance.AddGame(nameOfSave, FindObjectOfType<GameSave>().GetCurGame());
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        int activeSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(activeSceneIndex + 1);
    }

    public void LoadNextSceneOnDelay(int delay)
    {
        StartCoroutine(LoadNextSceneWithDelayPrivate(delay));
    }

    public void LoadPreviousScene()
    {
        int activeSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(activeSceneIndex - 1);
    }

    public void LoadSceneByName(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void RecentGameSave(string scenename)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            FindObjectOfType<GameSave>().RecentGameSaveO();
        }
        else if(SaveData.Instance.GetGame("recent") != null)
        {
            if(SaveData.Instance.gameToLoad == null)
            {
                LoadNewGame();
            }
            Settings.loadRecGameSave = true;
        }
        else
        {
            LoadNewGame();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(scenename);
    }

    public void LoadRecentGame()
    {
        Settings.loadRecGameSave = true;
        SaveData.Instance.gameToLoad = SaveData.Instance.GetGame(RECENT_KEY);
        if (SaveData.Instance.gameToLoad == null)
        {
            return;
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadNewGame()
    {
        Settings.loadRecGameSave = false;
        SceneManager.LoadScene("Game");
    }

    private IEnumerator LoadNextSceneWithDelayPrivate(int delay)
    {
        yield return new WaitForSeconds(delay);

        int activeSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(activeSceneIndex + 1);
    }

    private void OnApplicationQuit()
    {
        SaveData.Instance.SaveGameData();
    }

    public void Aaa()
    {
        SaveData.Instance.SaveGameData();
    }
}
