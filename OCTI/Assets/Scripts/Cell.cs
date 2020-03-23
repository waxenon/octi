using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Cell Settings")]
    [SerializeField] bool willShow;
    [SerializeField] [Range(0f, 1f)] float opacity = 0.3f;

    [Header("WorldSetting")]
    [SerializeField] float cellToOctiOffset = 0.875f;

    [Header("Debug View")]
    [SerializeField] int lineNumber = 0;
    [SerializeField] int columnNumber = 0;
    [SerializeField] Vector2Int cords = new Vector2Int();
    [SerializeField] public Vector2 worldPosForOct = new Vector2();

    EndTurnRefrence myEndTurn = new EndTurnRefrence();

    public bool isActive { get; private set; } = false;
    public bool isaSkipCell = false;

    public OctiPawn blockingOcti;

    public Vector2Int GetMyCellCords()
    {
        return cords;
    }

    public void ChangeMyState(bool isShow)
    {
        if(isShow)
        {
            Color myColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(myColor.r, myColor.g, myColor.b, 1f);
            isActive = true;
        }
        else
        {
            Color myColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(myColor.r, myColor.g, myColor.b, 0f);
            isActive = false;
        }
    }

    //sets the values of the cell's line number and column number
    public void SetGridPlace(int newLineNumber, int newColumnNumber)
    {
        lineNumber = newLineNumber;
        columnNumber = newColumnNumber;

        cords.x = columnNumber;
        cords.y = lineNumber;
    }

    //when created, depending on the bool "willShow",
    //it will be visible or not

    private void Start()
    {
        CalculateWorldPosForOcti();

        if (!willShow)
        {
            Color myColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(myColor.r, myColor.g, myColor.b, 0f);
        }
        else
        {
            isActive = true;
        }

        myEndTurn = FindObjectOfType<EndTurnRefrence>();
    }

    private void OnMouseDown()
    {
        if(isActive)
        {
            MoveOctiToCell(false);
        }
    }

    public void MoveOctiToCell(bool isLoad)
    {
        Vector3Int gameSaveVar = new Vector3Int();

        CalculateWorldPosForOcti();

        OctiPawn[] allOcti = FindObjectsOfType<OctiPawn>();
        OctiPawn theChosenOcti = null;

        Vector2Int oldOctPos = new Vector2Int();
        Vector2Int avragePos = new Vector2Int();

        bool hasOctiMovPotential = false;

        //find selected octi

        foreach (OctiPawn octi in allOcti)
        {
            if (octi.IsOctiHighlighted())
            {
                theChosenOcti = octi;

                gameSaveVar.x = theChosenOcti.octiId;
                gameSaveVar.y = 1;
                oldOctPos = octi.GetOctiCords();

                octi.transform.position = new Vector3
                    (
                    worldPosForOct.x,
                    worldPosForOct.y,
                    -2
                    );

                octi.SetOctiCords(cords);

                //the rest of this code is for
                //eating an octi in case there is one to eat
                //by avraging the pos of the chosen octi
                //with the cell to get the cell inbetween

                int avragePosX = Avrage(oldOctPos.x, cords.x);
                int avragePosY = Avrage(oldOctPos.y, cords.y);

                Vector2Int directionVec = new Vector2Int();
                directionVec.x = cords.x - oldOctPos.x;
                directionVec.y = cords.y - oldOctPos.y;

                foreach (Vector2Int curDirection in theChosenOcti.myDirections)
                {
                    if(curDirection == directionVec)
                    {
                        DD[] allOctisDDs =
                            theChosenOcti.GetComponentsInChildren<DD>();

                        foreach (DD dd in allOctisDDs)
                        {
                            if(dd.direction == curDirection)
                            {
                                gameSaveVar.z = dd.directionID;
                            }
                        }
                    }
                    else if(curDirection * 2 == directionVec)
                    {
                        DD[] allOctisDDs =
                            theChosenOcti.GetComponentsInChildren<DD>();

                        foreach (DD dd in allOctisDDs)
                        {
                            if (dd.direction == curDirection)
                            {
                                gameSaveVar.z = dd.directionID;
                            }
                        }
                    }
                }

                avragePos = new Vector2Int(avragePosX, avragePosY);

                OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();
                foreach (OctiPawn octiPawn in allOctis)
                {
                    bool isPosSame = (octiPawn.GetOctiCords() == avragePos);
                    bool isSkipCell = isaSkipCell;

                    bool isSkip = (isPosSame && isSkipCell);
                    bool isOctiEnemy = (octiPawn.isRed != theChosenOcti.isRed);

                    if (isSkip && isOctiEnemy)
                    {
                        if (theChosenOcti.isRed && !octiPawn.isDeathHighlight)
                        {
                            FindObjectOfType<TurnManager>().redDDcount += octiPawn.myDirections.Count;
                            FindObjectOfType<TurnManager>().UpdateDDcountText();
                        }
                        else if (!octiPawn.isDeathHighlight)
                        {
                            FindObjectOfType<TurnManager>().greenDDcount += octiPawn.myDirections.Count;
                            FindObjectOfType<TurnManager>().UpdateDDcountText();
                        }
                        octiPawn.DeathSelect();
                    }
                }
            }
        }

        FindObjectOfType<CellManager>().HideAllCells();
        FindObjectOfType<CellManager>().UpdateFreeSpaces();

        if (theChosenOcti != null && isaSkipCell)
        {
            FindObjectOfType<CellManager>().HighlightAvialabeChainCells(theChosenOcti);
            Cell[] allCells = FindObjectsOfType<Cell>();

            foreach (Cell myCell in allCells)
            {
                if (myCell.isActive)
                {
                    hasOctiMovPotential = true;
                }
            }
        }
        if(!isLoad)
        {
            FindObjectOfType<GameSave>().SaveGameData(gameSaveVar);
            print(gameSaveVar);
        }

        if (hasOctiMovPotential)
        {
            myEndTurn.ChangeButtonState(true);
            theChosenOcti.isOnChain = true;
        }
        else
        {
            theChosenOcti.ChangeHighlightState(false);
            FindObjectOfType<TurnManager>().ChangeTurn();
            FindObjectOfType<ProcessManager>().isProcessNow = false;
        }
    }

    private void CalculateWorldPosForOcti()
    {
        worldPosForOct = new Vector2(transform.position.x + 0.875f, transform.position.y + 0.875f);
    }

    private int Avrage(float x, float y)
    {
        float sum = x + y;
        return Mathf.RoundToInt(sum / 2);
    }

    private float AvrageFloat(float x, float y)
    {
        float sum = x + y;
        return sum / 2;
    }
}
