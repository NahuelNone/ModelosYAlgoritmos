using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Advance
{
    protected float _speed;
    protected Transform _transform;

    public Advance(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public abstract void Execute();
}
