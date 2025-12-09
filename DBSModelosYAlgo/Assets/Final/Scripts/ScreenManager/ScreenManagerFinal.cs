using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScreenManagerFinal : MonoBehaviour
{

    Stack<IScreenFinal> _stack;
    
    public string lastResult;
    
    static public ScreenManagerFinal Instance;
    
    void Awake()
    {
    
        Instance = this;
    
        _stack = new Stack<IScreenFinal>();
    
    }
    
    public void Pop()
    {
    
        if (_stack.Count <= 1) return;
    
        lastResult = _stack.Pop().Free();
    
        if (_stack.Count > 0)
        {
    
            _stack.Peek().Active();
    
        }
    
    }
    
    public void Push(IScreenFinal screen)
    {
    
        if (_stack.Count > 0)
        {
    
            _stack.Peek().Deactivate();
    
        }

        _stack.Push(screen);

        screen.Active();

    }
    
    public void Push(string resources)
    {
    
        var go = Instantiate(Resources.Load<GameObject>(resources));
    
        Push(go.GetComponent<IScreenFinal>());
    
    }

}