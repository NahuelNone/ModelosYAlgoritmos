using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon.cs (ejemplo)
    [SerializeField] private Factory factory;      // puede ser BulletFactory
    [SerializeField] private Transform firePoint;
    [SerializeField] private float muzzleVelocity = 40f;

    void Fire()
    {
        var data = new SpawnData(
            firePoint.position,
            firePoint.rotation,
            firePoint.forward,
            muzzleVelocity,
            transform.root
        );

        factory.GetProduct(data);   // ← antes: GetIproduct(...)
    }

}
