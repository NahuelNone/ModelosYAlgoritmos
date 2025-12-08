using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoManage : MonoBehaviour
{

    public List<GameState> allStates;

    private void Start()
    {

        allStates = new List<GameState>();
        var arrayTemp = FindObjectsOfType<GameState>();

        foreach (var state in arrayTemp)
        {

            allStates.Add(state);   

        }

    }

}
