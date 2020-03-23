using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SaveData
{
    private static readonly object padlock = new object();

    private static SaveData instance = null;

    Dictionary<string, List<Vector3Int>> savedGames;
    Dictionary<int, string> savedGamesNameByID;

    public List<Vector3Int> gameToLoad { get; set; } = new List<Vector3Int>();

    private SaveData()
    {
        savedGames = new Dictionary<string, List<Vector3Int>>();
        savedGamesNameByID = new Dictionary<int, string>();
    }
    
    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SaveData();
                    }
                }
            }
            return instance;
        }
    }   

    public void AddGame(string name, List<Vector3Int> gameSteps)
    {
        if (gameSteps.Count == 0)
        {
            return;
        }

        var savedGamesWithoutRecent = new Dictionary<string, List<Vector3Int>>();
        foreach(var key in savedGames.Keys)
        {
            if(key != "recent")
            {
                savedGamesWithoutRecent.Add(key, savedGames[key]);
            }
        }

        if (savedGamesWithoutRecent.ContainsValue(gameSteps))
        {
            MessegePanel mp = MonoBehaviour.FindObjectOfType<MessegePanel>();
            if(mp)
            {
                mp.OpenMPanel("An identical game has been saved as a different name");
            }
            return;
        }
        else if(savedGames.ContainsKey(name))
        {
            MessegePanel mp = MonoBehaviour.FindObjectOfType<MessegePanel>();
            if(mp)
            {
                mp.OpenMPanel("A game with the same name exists, it will be deleted and replaced" +
                    "with the current save");
            }
            savedGames.Remove(name);
        }

        savedGames.Add(name, gameSteps);

        if(name != "recent")
        {
            savedGamesNameByID.Add(savedGamesWithoutRecent.Count, name);
        }

        
        //foreach(var key in savedGames.Keys)
        //{
        //    Debug.Log(key);
        //    Debug.Log(GetGame(key));
        //}
    }

    public List<Vector3Int> GetGame(string name)
    {
        if (!savedGames.Keys.Contains(name))
            return null;

        return savedGames[name];
    }

    public string GetGameNameByID(int id)
    {
        return savedGamesNameByID[id];
    }

    public int GetSavedGamesLength()
    {
        return savedGames.Count();
    }

    public string GetGameByValue(List<Vector3Int> game)
    {
        foreach(string key in savedGames.Keys)
        {
            if(savedGames[key] == game)
            {
                return key;
            }
        }

        string noKeyFound = "ABCDEFG";
        return noKeyFound;
    }

    public void DeleteGame(string name, int id)
    {
        savedGames.Remove(name);
        savedGamesNameByID.Remove(id);

        Dictionary<int, string> duplicateOfDic = new Dictionary<int, string>();
        foreach(KeyValuePair<int, string> item in savedGamesNameByID)
        {
            duplicateOfDic.Add(item.Key, item.Value);
        }
        savedGamesNameByID.Clear();

        foreach(KeyValuePair<int, string> item in duplicateOfDic)
        {
            if(item.Key < id)
            {
                savedGamesNameByID.Add(item.Key, duplicateOfDic[item.Key]);
            }
            else
            {
                savedGamesNameByID.Add(item.Key - 1, duplicateOfDic[item.Key]);
            }
        }

    }

    public void DeleteAllGames()
    {
        savedGames.Clear();
    }

    public Dictionary<string, List<Vector3Int>> GetAllGames()
    {
        return savedGames;
    }

    public void SaveGameData()
    {
        SaveDataFile.SaveSaveDataData(this);
    }

    public void LoadGameData()
    {
        var sdd = SaveDataFile.LoadSaveData();

        Dictionary<string, List<Vector3Int>> tmp = new Dictionary<string, List<Vector3Int>>();
        Dictionary<int, string> tmp2 = new Dictionary<int, string>();

        int timesIndex = 0;
        int stepIndex = 0;
        int tmp2Counter = 0;

        foreach(int i in sdd.length)
        {
            List<Vector3Int> theGame = new List<Vector3Int>();
            for(int k = 0; k < i; k++)
            {
                Vector3Int step = new Vector3Int();
                step.x = sdd.xPos[stepIndex];
                step.y = sdd.yPos[stepIndex];
                step.z = sdd.zPos[stepIndex];

                stepIndex++;

                theGame.Add(step);
            }
            tmp.Add(sdd.names[timesIndex], theGame);
            if(sdd.names[timesIndex] != "recent")
            {
                tmp2.Add(tmp2Counter, sdd.names[timesIndex]);
                tmp2Counter++;
            }
            timesIndex++;
        }
        savedGames = tmp;
        savedGamesNameByID = tmp2;
    }
}

