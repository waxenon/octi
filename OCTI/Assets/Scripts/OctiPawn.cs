using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctiPawn : MonoBehaviour
{
    //is it red?

    [SerializeField] public bool isRed;
    [SerializeField] public int octiId;

    [SerializeField] Color highlightColor;
    [SerializeField] Color deathHighlightColor;

    [SerializeField] public List<Vector2Int> myDirections = new List<Vector2Int>();
    [SerializeField] public List<GameObject> myDDsRefrence = new List<GameObject>();

    [SerializeField] Vector2Int myCords = new Vector2Int();
    Vector2Int myOgCords = new Vector2Int();

    public bool hasSkipped = false;
    public bool isDeathHighlight = false;
    public bool isOnChain = false;
    bool isHighlighted = false;
    bool hasGotOgPos = false;

    public void DeathSelect()
    {
        GetComponent<SpriteRenderer>().color = deathHighlightColor;
        isDeathHighlight = true;
    }

    public Vector2Int GetOctiCords()
    {
        return myCords;
    }

    public Vector2Int GetOctiOgCords()
    {
        return myOgCords;
    }

    public void SetOctiCords(Vector2Int newPos)
    {
        myCords = newPos;
        if(!hasGotOgPos)
        {
            hasGotOgPos = true;
            myOgCords = newPos;
        }
    }

    public bool IsOctiHighlighted()
    {
        return isHighlighted;
    }

    public void ChangeHighlightState(bool isHighlight)
    {
        isHighlighted = isHighlight;
        if(isHighlight)
        {
            GetComponent<SpriteRenderer>().color = highlightColor;
            FindObjectOfType<ProcessManager>().isProcessNow = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            FindObjectOfType<ProcessManager>().isProcessNow = false;
            isOnChain = false;
        }
    }

    private void OnMouseDown()
    {
        bool isItMyTurn = (FindObjectOfType<TurnManager>().isItRedsTrun == isRed);
        bool findCurProcManag = FindObjectOfType<ProcessManager>().isProcessNow;

        if (isItMyTurn && !findCurProcManag && !isHighlighted)
        {
            FindObjectOfType<CellManager>().UpdateFreeSpaces();

            isHighlighted = true;

            FindObjectOfType<ProcessManager>().isProcessNow = true;

            FindObjectOfType<CellManager>().HighlightAvailableCells(gameObject.GetComponent<OctiPawn>());

            GetComponent<SpriteRenderer>().color = highlightColor;
        }
        else if(isHighlighted && !isOnChain)
        {
            FindObjectOfType<ProcessManager>().isProcessNow = false;
            GetComponent<SpriteRenderer>().color = new Color(255,255,255,1);

            FindObjectOfType<CellManager>().HideAllCells();
            isHighlighted = false;
        }

    }
}
