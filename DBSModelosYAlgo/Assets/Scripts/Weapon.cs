using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Factory _factory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _factory.GetIproduct(Vector3.zero);
        }
    }

}
