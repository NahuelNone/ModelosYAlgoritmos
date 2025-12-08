using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateStorage
{

    public object[] parameters;

    public GameStateStorage(params object[] parameterWrapper)
    {

        parameters = new object[parameterWrapper.Length];

        for (int i = 0; i < parameterWrapper.Length; i++)
        {

            parameters[i] = parameterWrapper[i];

        }


    }

}
