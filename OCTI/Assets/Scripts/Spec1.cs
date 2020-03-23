using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec1 : MonoBehaviour
{
    [SerializeField] GameObject s1;

    private void Start()
    {
        if(!Settings.isBackAllowed)
        {
            s1.SetActive(false);
        }
    }
}
