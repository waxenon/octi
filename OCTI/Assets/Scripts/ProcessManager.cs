using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    [SerializeField] public bool isProcessNow = false;
    public bool isHighlightDD = false;

    private void Start()
    {
        isHighlightDD = false;
    }
}
