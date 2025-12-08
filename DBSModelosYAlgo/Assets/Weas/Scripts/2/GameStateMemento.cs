using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class GameStateMemento
{

    List<GameStateStorage> _rememberPosition;

    public GameStateMemento()
    {

        _rememberPosition = new List<GameStateStorage>();

    }

    public int MemoriesQuantity()
    {

        return _rememberPosition.Count;

    }

    public GameStateStorage Remember()
    {

        int index = _rememberPosition.Count - 1;

        var currentPos = _rememberPosition[index];

        _rememberPosition.RemoveAt(index);
        
        return currentPos;

    }

    public void Rec(params object[] parameterWrapper)
    {

        _rememberPosition.Add(new GameStateStorage(parameterWrapper));

    }

}
