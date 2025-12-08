using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealAdvance : Advance
{

    public LinealAdvance(Transform transform, float speed) : base(transform, speed)
    { }
 

    public override void Execute()
    {
        _transform.position += _transform.forward * (_speed * Time.deltaTime);
    }
}
