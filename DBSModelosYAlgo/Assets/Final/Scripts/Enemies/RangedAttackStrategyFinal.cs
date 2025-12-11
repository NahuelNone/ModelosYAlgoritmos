using Builder;
using UnityEngine;

public class RangedAttackStrategy : IAttackStrategy
{
    private float _range;
    private float _cooldown;
    private float _lastAttackTime = -999f;
    private GameObject _bulletPrefab;

    public RangedAttackStrategy(float range, float cooldown, GameObject bulletPrefab)
    {
        _range = range;
        _cooldown = cooldown;
        _bulletPrefab = bulletPrefab;
    }

    public void Attack(EnemyFinal enemy, Transform target)
    {
        if (target == null || _bulletPrefab == null) return;

        float dist = Vector2.Distance(enemy.transform.position, target.position);

        if (dist <= _range && Time.time >= _lastAttackTime + _cooldown)
        {
            Vector2 dir = (target.position - enemy.transform.position).normalized;

            GameObject bullet = Object.Instantiate(
                _bulletPrefab,
                enemy.transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = dir * 10f; // velocidad bala

            Debug.Log($"[{enemy.name}] dispara al jugador");

            _lastAttackTime = Time.time;
        }
    }
}
