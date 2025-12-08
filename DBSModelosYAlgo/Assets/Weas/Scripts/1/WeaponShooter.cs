using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private Transform firePoint;

    [Header("Disparo")]
    [SerializeField] private float muzzleVelocity = 40f;
    [SerializeField] private float spawnOffset = 0.15f; // empuja la bala fuera del cañón
    [SerializeField] private KeyCode fireKey = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(fireKey)) Fire();
    }

    public void Fire()
    {
        if (!bulletFactory || !firePoint)
        {
            Debug.LogWarning("[Weapon] Falta bulletFactory o firePoint.");
            return;
        }

        Debug.DrawRay(firePoint.position, firePoint.forward * 2f, Color.green, 0.25f);

        var data = new SpawnData(
            position: firePoint.position + firePoint.forward * spawnOffset,
            rotation: firePoint.rotation,
            direction: firePoint.forward,
            speed: muzzleVelocity,
            owner: transform.root
        );

        var prod = bulletFactory.GetProduct(data);
        Debug.Log($"[Weapon] Disparo OK. Spawn en {data.position}, dir {data.direction}, vel {muzzleVelocity}. Prod: {prod}");

    }

}
