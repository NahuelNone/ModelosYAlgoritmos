using UnityEngine;

public class RangedWeaponFinal : IWeaponFinal
{
    private BulletFactoryFinal _factory;
    private float _bulletSpeed;
    private float _bulletLifeTime;
    private float _bulletDamage;

    public RangedWeaponFinal(BulletFactoryFinal factory, float bulletSpeed, float bulletLifeTime, float bulletDamage)
    {
        _factory = factory;
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;
        _bulletDamage = bulletDamage;
    }

    public void Attack(PlayerViewFinal viewFinal)
    {
        if (viewFinal.firePoint == null)
        {
            Debug.LogWarning("FirePoint no asignado en PlayerView.");
            return;
        }

        // Dirección según para qué lado mira el sprite
        float dirX = viewFinal.spriteRenderer.flipX ? -1f : 1f;
        Vector2 dir = new Vector2(dirX, 0f);

        _factory.CreateBullet(viewFinal.firePoint.position, dir, _bulletSpeed, _bulletLifeTime, _bulletDamage);

        // También disparo animación si la hay
        viewFinal.Attack();
    }
}
