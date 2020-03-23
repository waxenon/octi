using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singeltone : MonoBehaviour
{
    [SerializeField] bool isActive = true;
    [SerializeField] public int singeltoneIndex;

    private void SingletoneAction()
    {
        Singeltone[] allSingeltones = FindObjectsOfType<Singeltone>();
        List<Singeltone> matchingSingeltones = new List<Singeltone>();

        foreach(Singeltone mySingletone in allSingeltones)
        {
            if(mySingletone.singeltoneIndex == singeltoneIndex 
                && mySingletone != GetComponent<Singeltone>())
            {
                matchingSingeltones.Add(mySingletone);
            }
        }

        if(matchingSingeltones.Count != 0)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SingletoneAction();
    }

    private void Update()
    {
        if(GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().volume = Settings.volume;
        }
    }

}
