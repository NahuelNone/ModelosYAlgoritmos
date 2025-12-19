using Builder;
using UnityEngine;

public class RangedAttackStrategy : IAttackStrategy
{
    private float _range;
    private float _cooldown;
    private float _bulletSpeed;
    private float _lastAttackTime = -999f;
    private GameObject _bulletPrefab;
    private Transform _firePoint;

    // Debug
    private bool _debug;
    private float _debugEvery;
    private float _nextDebugTime;

    public RangedAttackStrategy(
        float range,
        float cooldown,
        GameObject bulletPrefab,
        Transform firePoint,
        float bulletSpeed = 10f,
        bool debug = false,
        float debugEvery = 0.5f
    )
    {
        _range = range;
        _cooldown = cooldown;
        _bulletPrefab = bulletPrefab;
        _firePoint = firePoint;
        _bulletSpeed = bulletSpeed;

        _debug = debug;
        _debugEvery = Mathf.Max(0.1f, debugEvery);
    }

    public void Attack(EnemyFinal enemy, Transform target)
    {
        if (enemy == null) return;

        if (target == null) { Dbg(enemy, "target/player NULL"); return; }
        if (_bulletPrefab == null) { Dbg(enemy, "bulletPrefab NULL"); return; }

        Vector2 origin = _firePoint != null ? (Vector2)_firePoint.position : (Vector2)enemy.transform.position;
        float dist = Vector2.Distance(origin, target.position);

        if (dist > _range) { Dbg(enemy, $"fuera de rango dist={dist:0.00} range={_range:0.00}"); return; }
        if (Time.time < _lastAttackTime + _cooldown) { Dbg(enemy, "cooldown"); return; }

        Vector2 dir = ((Vector2)target.position - origin).normalized;

        GameObject bullet = Object.Instantiate(_bulletPrefab, origin, Quaternion.identity);
        var eb = bullet.GetComponent<EnemyBulletFinal>();
        if (eb != null)
            eb.Fire(dir, enemy.transform); 
        Debug.Log($"[RANGED] [{enemy.name}] Instancio bala: {bullet.name} at {origin}");


        IgnoreShooterCollision(enemy, bullet);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"[RANGED] [{enemy.name}] La bala NO tiene Rigidbody2D. No puede moverse con velocity.");
            return;
        }

        rb.gravityScale = 0f;
        rb.velocity = dir * _bulletSpeed;

        _lastAttackTime = Time.time;
    }

    private void IgnoreShooterCollision(EnemyFinal enemy, GameObject bullet)
    {
        var enemyCols = enemy.GetComponentsInChildren<Collider2D>();
        var bulletCols = bullet.GetComponentsInChildren<Collider2D>();

        if (enemyCols == null || bulletCols == null) return;

        foreach (var ec in enemyCols)
            foreach (var bc in bulletCols)
            {
                if (ec != null && bc != null)
                    Physics2D.IgnoreCollision(ec, bc, true);
            }
    }

    private void Dbg(EnemyFinal enemy, string msg)
    {
        if (!_debug) return;
        if (Time.time < _nextDebugTime) return;
        _nextDebugTime = Time.time + _debugEvery;

        Debug.Log($"[RANGED DEBUG] [{enemy.name}] {msg}");
    }
}
