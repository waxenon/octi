using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //so it's 20/1/2020 and so far this project is doing great. I am now going to add a lot of comments
    //to everything I've done so far, this is going to be rough

    //purpose of this script:
    //create a grid

    [Header("Objects")]
    [SerializeField] GameObject tileSample;
    [SerializeField] Transform deafultPos;
    [SerializeField] Vector3 startPos;

    [Header("Grid Settings")]
    [SerializeField] int numberOfLines;
    [SerializeField] int numberOfColumns;
    [SerializeField] int stepDistance;

    [Header("TileProperties")]
    [SerializeField] Color tileColor;

    [Header("Debug View")]
    [SerializeField] int totalSteps;

    private void Start()
    {
        //total steps is the amount of tiles there will be
        //it is called total steps because the process of the grid
        //creation is a loop which everytime it creates a cell it
        //"takes a step".

        totalSteps = numberOfColumns * numberOfLines;
        CreateGrid();
        if (Settings.loadRecGameSave)
        {
            List<Vector3Int> gameToLoad = SaveData.Instance.gameToLoad;

            if(gameToLoad == null)
            {
                FindObjectOfType<GameSave>().GameSaveStart();
            }
            else
            {
                var tmpGameToLoad = gameToLoad;
                SaveData.Instance.gameToLoad = null;
                FindObjectOfType<GameSave>().LoadGame(tmpGameToLoad);
            }

        }
    }

    public void CreateGrid()
    {
        int stepsDoneCount = 0;
        int stepsDoneInLine = 0;
        int timeWentDown = 0;

        //the position the cell will be created
        Vector3 cellPos = startPos;

        while(stepsDoneCount < totalSteps)
        {
            //new tile is the cell created each time this while loop repeats

            GameObject newTile = Instantiate(tileSample, gameObject.transform);

            //setting position and parent

            newTile.transform.position = cellPos;
            newTile.transform.SetParent(gameObject.transform, false);

            //assuming it's a cell with a "Cell" component, it
            //will assign its line number and column number

            if(newTile.GetComponent<Cell>())
            {
                newTile.GetComponent<Cell>().SetGridPlace(timeWentDown + 1, stepsDoneInLine + 1);
            }

            //setting color of cell to "tileColor"

            Color tilesColorComp = newTile.GetComponent<SpriteRenderer>().color;
            newTile.GetComponent<SpriteRenderer>().color = tileColor;

            //"taking a step", changing the cell creation position by adding a vector

            Vector3 temp = 
                new Vector3(stepDistance, 0, 0);
            cellPos += temp;

            //the while function ends when "stepsDoneCount" is bigger than the amount
            //of cells there are supposed to be at the end of the process,
            //so I use a var to make it a kind of a for loop

            //"stepsDoneInLine" is for checking to see if an entire
            //line has been created yet. Whenever it comes of as true,
            //cellPos is changed in a manner it would create a new line

            stepsDoneCount++;
            stepsDoneInLine++;

            if(stepsDoneInLine >= numberOfColumns)
            {
                stepsDoneInLine = 0;
                timeWentDown++;

                Vector3 temp2 = 
                    new Vector3 
                    (stepDistance * numberOfColumns * -1, stepDistance * -1, 0);

                cellPos += temp2;
            }
        }

        FindObjectOfType<CellManager>().UpdateFreeSpaces();
    }

}
