using Builder;
using UnityEngine;

public class MeleeAttackStrategy : IAttackStrategy
{
    private float _range;
    private float _cooldown;
    private float _lastAttackTime = -999f;

    public MeleeAttackStrategy(float range, float cooldown)
    {
        _range = range;
        _cooldown = cooldown;
    }

    public void Attack(EnemyFinal enemy, Transform target)
    {
        if (target == null) return;

        float dist = Vector2.Distance(enemy.transform.position, target.position);

        if (dist <= _range && Time.time >= _lastAttackTime + _cooldown)
        {
            // Acá aplicarías el daño de verdad (llamar al Player, animación, etc.)
            Debug.Log($"[{enemy.name}] pega ataque melee al jugador");

            _lastAttackTime = Time.time;
        }
    }
}
