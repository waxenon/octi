using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDmanager : MonoBehaviour
{
    [SerializeField] Color highlightedColor = new Color(255, 0, 0, 255);
    ProcessManager myProcessManager = new ProcessManager();

    //don't be overwhelmed, "HighlightAllDD" and "HideAllDD"
    //are basically the same function, the difference is that after
    //they find all the dds of said side, highlight all dds makes
    //them visible, and hide all dds makes them invisible

    //I could simply make a function which finds all dds, and
    //based on given paramaters would ethier hide them or show them
    //but I'm too lazy

    public void HighlightAllDD()
    {
        myProcessManager = FindObjectOfType<ProcessManager>();

        if(myProcessManager.isProcessNow || myProcessManager.isHighlightDD)
        {
            return;
        }
        else
        {
            myProcessManager.isHighlightDD = true;
            FindObjectOfType<ProcessManager>().isProcessNow = true;
        }

        //this script finds all dds in game and
        //highlights them said color

        OctiPawn[] pawns = FindObjectsOfType<OctiPawn>();
        List<GameObject> allDDs = new List<GameObject>();

        foreach (OctiPawn myPawn in pawns)
        {

            bool isItRedsTurn = FindObjectOfType<TurnManager>().isItRedsTrun;
            if(isItRedsTurn)
            {
                if(myPawn.isRed)
                {
                    foreach (Transform ddchild in myPawn.transform)
                    {
                        bool isDDactive = (ddchild.GetComponent<DD>().isInitialized == false);

                        if (isDDactive)
                        {
                            allDDs.Add(ddchild.gameObject);
                            ddchild.gameObject.SetActive(true);
                            ddchild.gameObject.GetComponent<SpriteRenderer>().color = highlightedColor;
                        }

                    }
                }
            }
            else if(!myPawn.isRed)
            {
                foreach (Transform ddchild in myPawn.transform)
                {
                    bool isDDactive = (ddchild.GetComponent<DD>().isInitialized == false);

                    if (isDDactive)
                    {
                        allDDs.Add(ddchild.gameObject);
                        ddchild.gameObject.SetActive(true);
                        ddchild.gameObject.GetComponent<SpriteRenderer>().color = highlightedColor;
                    }

                }
            }
        }
    }

    public void HideAllDD()
    {
        myProcessManager = FindObjectOfType<ProcessManager>();

        if (!FindObjectOfType<ProcessManager>().isProcessNow || !myProcessManager.isHighlightDD)
        {
            return;
        }
        else
        {
            myProcessManager.isHighlightDD = false;
            FindObjectOfType<ProcessManager>().isProcessNow = false;
        }

        FindObjectOfType<ProcessManager>().isProcessNow = false;

        OctiPawn[] pawns = FindObjectsOfType<OctiPawn>();
        List<GameObject> allDDs = new List<GameObject>();

        foreach (OctiPawn myPawn in pawns)
        {
                bool isItRedsTurn = FindObjectOfType<TurnManager>().isItRedsTrun;
                if (isItRedsTurn)
                {
                    if (myPawn.isRed)
                    {
                        foreach (Transform ddchild in myPawn.transform)
                        {
                            bool isDDactive = (ddchild.GetComponent<DD>().isInitialized == false);

                            if (isDDactive)
                            {
                                allDDs.Add(ddchild.gameObject);
                                ddchild.gameObject.SetActive(false);
                                ddchild.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                            }

                        }
                    }
                }
                else if (!myPawn.isRed)
                {
                    foreach (Transform ddchild in myPawn.transform)
                    {
                        bool isDDactive = (ddchild.GetComponent<DD>().isInitialized == false);

                        if (isDDactive)
                        {
                         allDDs.Add(ddchild.gameObject);
                         ddchild.gameObject.SetActive(false);
                         ddchild.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                        }

                    }
                }
        }
    }

    private void Start()
    {
        myProcessManager = FindObjectOfType<ProcessManager>();
    }


}
