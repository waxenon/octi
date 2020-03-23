using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD : MonoBehaviour
{
    public bool isInitialized = false;
    [SerializeField] public Vector2Int direction = new Vector2Int();
    [SerializeField] public int directionID;

    private void OnMouseDown()
    {
        //if the dd hasn't been initialized yet, only then clicking it would have effects

        if(!isInitialized)
        {
            TurnManager turnManager = FindObjectOfType<TurnManager>();

            GetComponentInParent<OctiPawn>().myDirections.Add(direction);
            int x = GetComponentInParent<OctiPawn>().octiId;
            int z = directionID;

            FindObjectOfType<GameSave>().SaveGameData(new Vector3Int(x, 0, z));
            print(new Vector3Int(x, 0, z));

            //unless the side's which is playing has no direction darts,
            //the dd clicked on would change into deafult settings

            if (turnManager.isItRedsTrun && turnManager.redDDcount != 0)
            {
                isInitialized = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                FindObjectOfType<DDmanager>().HideAllDD();

                turnManager.redDDcount = turnManager.redDDcount - 1;
                turnManager.UpdateDDcountText();
                turnManager.ChangeTurn();
            }
            else if (turnManager.greenDDcount != 0)
            {
                isInitialized = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                FindObjectOfType<DDmanager>().HideAllDD();

                turnManager.greenDDcount = turnManager.greenDDcount - 1;
                turnManager.UpdateDDcountText();
                turnManager.ChangeTurn();
            }
        }
    }
}
