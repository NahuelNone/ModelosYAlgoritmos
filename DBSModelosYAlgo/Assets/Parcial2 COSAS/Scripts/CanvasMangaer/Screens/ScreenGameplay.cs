using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameplay : IScreen
{
    private Transform _root;

    Dictionary<Behaviour, bool> _beforeDeactivation;

    public ScreenGameplay(Transform root)
    {
        _root = root;

        _beforeDeactivation = new Dictionary<Behaviour, bool>();

        foreach (Transform item in _root.transform)
        {
        
            item.GetComponent<Renderer>().material.color = Color.blue;
        
        }
    }

    public void Activate()
    {

        foreach (var pair in _beforeDeactivation)
        {

            pair.Key.enabled = pair.Value;

            pair.Key.GetComponent<Renderer>();

        }

    }

    public void Desactivate()
    {
        foreach (var behaviour in _root.GetComponentsInChildren<Behaviour>())
        {
        
            _beforeDeactivation[behaviour] = behaviour.enabled;
        
            behaviour.enabled = false;
        
            behaviour.GetComponent<Renderer>().material.color = Color.blue;
        
        }
    }

    public void Realease()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
