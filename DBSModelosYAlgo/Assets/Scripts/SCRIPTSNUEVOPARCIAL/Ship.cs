using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _StoppingRadius;

    private IAdvance _linealAdvance;
    private IAdvance _sinAdvance;
    private IAdvance _targetAdvance;

    private IAdvance _currentAdvance;


    void Awake()
    {
        _linealAdvance = new LinealAdvance(transform , _speed);
        _sinAdvance = new SinAdvance(transform , _speed);
        _targetAdvance = new TargetAdvance(transform , _speed , _target , _StoppingRadius);

        _currentAdvance = _linealAdvance;
    }
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentAdvance = _linealAdvance;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentAdvance = _sinAdvance;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentAdvance = _targetAdvance;
        }

        _currentAdvance.Advance();

    }





}
