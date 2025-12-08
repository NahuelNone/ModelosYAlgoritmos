using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAdvance : IAdvance
{
    private Transform _transform;
    private Transform _target;
    private float _speed;
    private float _stoppingRadius;
    private Vector3 _direction;
    public TargetAdvance(Transform transform, float speed , Transform target , float stoppingRadius )
    {
        _transform = transform; _speed = speed;
        _target = target;
        _stoppingRadius = stoppingRadius;

    }
    public void Advance()
    {
        _direction = _target.position - _transform.position;

        _transform.forward = _direction;

        if (_direction.magnitude < _stoppingRadius) return;

        _transform.position += _transform.forward * (_speed * Time.deltaTime);
    }
}
