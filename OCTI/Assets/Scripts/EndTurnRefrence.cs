using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnRefrence : MonoBehaviour
{
    [SerializeField] public GameObject refrence;

    public void ChangeButtonState(bool active)
    {
        if(!refrence)
        {
            return;
        }
        refrence.SetActive(active);
    }

}
