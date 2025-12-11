using Builder;
using UnityEngine;

public class ChaseMoveStrategy : IMoveStrategy
{
    private Transform _player;
    private float _speed;
    private float _stopDistance;

    public ChaseMoveStrategy(Transform player, float speed, float stopDistance = 0.5f)
    {
        _player = player;
        _speed = speed;
        _stopDistance = stopDistance;
    }

    public void Move(EnemyFinal enemy, Rigidbody2D rb)
    {
        if (_player == null) return;

        float dist = Vector2.Distance(enemy.transform.position, _player.position);
        if (dist < _stopDistance)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 dir = (_player.position - enemy.transform.position).normalized;
        rb.velocity = dir * _speed;
    }
}
