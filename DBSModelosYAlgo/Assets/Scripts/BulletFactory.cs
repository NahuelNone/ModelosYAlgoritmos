using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : Factory
{
    [SerializeField] public Bullet _bulletPrefab;

    public override Iproduct GetIproduct(Vector3 position)
    {
        Iproduct obj = Instantiate(_bulletPrefab , position, Quaternion.identity);


        obj.Initialize();

        return obj;
        
    }
}
