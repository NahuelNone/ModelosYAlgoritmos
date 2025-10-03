using UnityEngine;

public class BulletFactory : Factory
{
    [SerializeField] private Bullet bulletPrefab;

    public override IProduct GetProduct(SpawnData data)
    {
        Bullet b = Instantiate(bulletPrefab, data.position, data.rotation);
        b.Initialize(data);
        return b;
    }
}
