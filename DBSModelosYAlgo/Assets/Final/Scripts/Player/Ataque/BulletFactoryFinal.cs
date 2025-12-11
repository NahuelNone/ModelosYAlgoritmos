using UnityEngine;

public class BulletFactoryFinal
{
    private BulletPoolFinal _pool;

    public BulletFactoryFinal(BulletPoolFinal pool)
    {
        _pool = pool;
    }

    /// <summary>
    /// Crea/configura una bala usando el pool.
    /// </summary>
    public BulletFinal CreateBullet(Vector2 position, Vector2 direction, float speed, float lifeTime, int damage)
    {
        BulletFinal bulletFinal = _pool.GetBullet();

        bulletFinal.transform.position = position;
        bulletFinal.transform.rotation = Quaternion.identity;

        bulletFinal.Init(_pool, direction, speed, lifeTime, damage);

        return bulletFinal;
    }
}
