using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataData
{
    public List<int> xPos = new List<int>();
    public List<int> yPos = new List<int>();
    public List<int> zPos = new List<int>();

    public List<string> names = new List<string>();
    public List<int> length = new List<int>();

    public SaveDataData(SaveData sd)
    {
        Dictionary<string, List<Vector3Int>> games = sd.GetAllGames();
        List<List<Vector3Int>> gameSteps = new List<List<Vector3Int>>();
        List<string> gameStepsNames = new List<string>();

        foreach(string key in games.Keys)
        {
            gameStepsNames.Add(key);
            gameSteps.Add(games[key]);
        }

        int curListIndex = 0;

        foreach(List<Vector3Int> oneSetOfGameSteps in gameSteps)
        {
            int curLength = 0;
            foreach(Vector3Int singleGameStep in oneSetOfGameSteps)
            {
                curLength++;
                xPos.Add(singleGameStep.x);
                yPos.Add(singleGameStep.y);
                zPos.Add(singleGameStep.z);
            }
            length.Add(curLength);
            names.Add(gameStepsNames[curListIndex]);
            curListIndex++;
        }
    }

}
