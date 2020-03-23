using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] List<Vector2Int> redWinSpaces = new List<Vector2Int>();
    [SerializeField] List<Vector2Int> greenWinSpaces = new List<Vector2Int>();

    public int IsRedWin()
    {
        //0 is green win
        //1 is red win
        //2 is no win event

        OctiPawn[] allOctis = FindObjectsOfType<OctiPawn>();
        List<OctiPawn> redOctis = new List<OctiPawn>();
        List<OctiPawn> greenOctis = new List<OctiPawn>();

        foreach(OctiPawn octi in allOctis)
        {
            if(octi.isRed && !octi.isDeathHighlight)
            {
                redOctis.Add(octi);
            }
            else if(!octi.isDeathHighlight)
            {
                greenOctis.Add(octi);
            }
        }

        bool isRedEmpty = (redOctis.Count == 0);
        bool isGreenEmpty = (greenOctis.Count == 0);

        if(isRedEmpty)
        {
            return 0;
        }
        else if(isGreenEmpty)
        {
            return 1;
        }
        else
        {
            foreach(OctiPawn redOcti in redOctis)
            {
                if(redWinSpaces.Contains(redOcti.GetOctiCords()))
                {
                    return 1;
                }
            }

            foreach(OctiPawn greenOcti in greenOctis)
            {
                if(greenWinSpaces.Contains(greenOcti.GetOctiCords()))
                {
                    return 0;
                }
            }
        }

        return 2;
    }
}
