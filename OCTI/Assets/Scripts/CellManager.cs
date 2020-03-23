using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] List<Vector2Int> freeSpaces = new List<Vector2Int>();
    List<Vector2Int> takenSpaces = new List<Vector2Int>();

    public void UpdateFreeSpaces()
    {
        //clean lists
        freeSpaces.Clear();
        takenSpaces.Clear();

        //find all cords

        Cell[] allCells = FindObjectsOfType<Cell>();

        foreach(Cell myCell in allCells)
        {
            freeSpaces.Add(myCell.GetMyCellCords());
        }

        //find all taken cords

        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();
        takenSpaces = new List<Vector2Int>();

        //subtract taken cords from all cords

        foreach(OctiPawn myOcti in allOctis)
        {
            takenSpaces.Add(myOcti.GetOctiCords());
        }

        List<Vector2Int> crossedCords = new List<Vector2Int>();

        foreach(Vector2Int freeSpace in freeSpaces)
        {
            foreach(Vector2Int takenSpace in takenSpaces)
            {
                if(takenSpace == freeSpace)
                {
                    crossedCords.Add(freeSpace);
                }
            }
        }

        foreach(Vector2Int crossedCord in crossedCords)
        {
            freeSpaces.Remove(crossedCord);
        }
    }

    public void HideAllCells()
    {
        Cell[] allCells = FindObjectsOfType<Cell>();
        foreach(Cell myCell in allCells)
        {
            myCell.ChangeMyState(false);
        }
    }

    public void DisableAllOctis(bool willChangeTurn)
    {
        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();

        foreach(OctiPawn octi in allOctis)
        {
            octi.ChangeHighlightState(false);
        }

        if(willChangeTurn)
        {
            FindObjectOfType<TurnManager>().ChangeTurn();
            FindObjectOfType<ProcessManager>().isProcessNow = false;
        }
    }

    public void HighlightAvailableCells(OctiPawn myOcti)
    {
        Vector2Int octPos = myOcti.GetOctiCords();

        HighlightCellBasedDirection
            (octPos, MovementDirection.Backward, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Forward, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Right, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Left, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Backwardright, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Backwardleft, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Forwardright, myOcti);
        HighlightCellBasedDirection
            (octPos, MovementDirection.Forwardleft, myOcti);
    }

    public void HighlightAvialabeChainCells
        (OctiPawn myOcti)
    {
        Vector2Int octPos = myOcti.GetOctiCords();

        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Backward, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Forward, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Right, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Left, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Forwardright, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Forwardleft, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Backwardright, myOcti);
        HighlightSkipCellBasedDirection
            (octPos, MovementDirection.Backwardleft, myOcti);
    }

    private void HighlightCellBasedDirection
        (Vector2Int originalPos, Vector2Int directionPos, OctiPawn myOcti)
    {
        Vector2Int futurePos = originalPos;
        futurePos += directionPos;

        if(freeSpaces.Contains(futurePos))
        {
            Cell[] allCells = FindObjectsOfType<Cell>();

            foreach (Cell myCell in allCells)
            {
                bool isSpaceFree = (myCell.GetMyCellCords() == futurePos);
                bool hasOctiDirection = (myOcti.myDirections.Contains(directionPos));

                if(isSpaceFree && hasOctiDirection)
                {
                    myCell.ChangeMyState(true);
                }
            }
        }
        else
        {
            futurePos += directionPos;
            Vector2Int blockingOctPos = futurePos;

            if (freeSpaces.Contains(futurePos))
            {
                Cell[] allCells = FindObjectsOfType<Cell>();

                foreach (Cell myCell in allCells)
                {
                    bool isSpaceFree = (myCell.GetMyCellCords() == futurePos);
                    bool hasOctiDirection = (myOcti.myDirections.Contains(directionPos));

                    if (isSpaceFree && hasOctiDirection)
                    {
                        myCell.ChangeMyState(true);
                        myCell.isaSkipCell = true;
                    }
                }
            }
        }
    }

    private void HighlightSkipCellBasedDirection
        (Vector2Int originalPos, Vector2Int directionPos, OctiPawn myOcti)
    {
        Vector2Int blockingOctPos = originalPos;
        blockingOctPos += directionPos;

        Vector2Int futurePos = originalPos;
        futurePos += directionPos;
        futurePos += directionPos;

        bool isThereBlock = (takenSpaces.Contains(blockingOctPos));
        bool isThereFreeSpaceAfterwards = (freeSpaces.Contains(futurePos));

        if(isThereBlock && isThereFreeSpaceAfterwards)
        {
            Cell[] allCells = FindObjectsOfType<Cell>();

            foreach(Cell myCell in allCells)
            {
                bool hasOctiDirection = (myOcti.myDirections.Contains(directionPos));
                bool isSpaceFree = (myCell.GetMyCellCords() == futurePos);

                if (isSpaceFree && hasOctiDirection)
                {
                    myCell.ChangeMyState(true);
                    myCell.isaSkipCell = true;
                }
            }
        }
    }

}
