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

    public void Attack(PlayerViewFinal view)
    {
        if (view.firePoint == null)
        {
            Debug.LogWarning("FirePoint no asignado en PlayerView.");
            return;
        }

        float dirX = view.spriteRenderer.flipX ? -1f : 1f;
        Vector2 dir = new Vector2(dirX, 0f);

        _factory.CreateBullet(
            view.firePoint.position,
            dir,
            _bulletSpeed,
            _bulletLifeTime,
            _bulletDamage
        );

        view.Attack();
    }



}
