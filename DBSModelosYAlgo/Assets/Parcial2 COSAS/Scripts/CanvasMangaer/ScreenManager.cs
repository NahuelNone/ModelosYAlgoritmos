using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
   public static ScreenManager Instance {  get; private set; }

    private Stack<IScreen> _screenStack;

    private void Awake()
    {
        Instance = this;

        _screenStack = new Stack<IScreen>();
    }

    public void Push(IScreen newScreen)
    {
        if (_screenStack.Count > 0)
        {
            var oldScreen = _screenStack.Peek();

            oldScreen.Desactivate();

        }

        _screenStack.Push(newScreen);

        newScreen.Activate();
    }

    public void Pop()
    {
     if ( _screenStack.Count == 1 ) return;

        var oldScreen = _screenStack.Pop();

        oldScreen.Realease();

        var currentScreen = _screenStack.Peek();    

        currentScreen.Activate();
    }

}
