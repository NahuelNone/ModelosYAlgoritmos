using Builder;
using UnityEngine;

public class PatrolMoveStrategy : IMoveStrategy
{
    private Transform[] _points;
    private int _currentIndex;
    private float _speed;

    public PatrolMoveStrategy(Transform[] points, float speed)
    {
        _points = points;
        _speed = speed;
        _currentIndex = 0;
    }

    public void Move(Enemy enemy, Rigidbody2D rb)
    {
        if (_points == null || _points.Length == 0) return;

        Transform target = _points[_currentIndex];

        Vector2 dir = (target.position - enemy.transform.position).normalized;
        rb.velocity = dir * _speed;

        if (Vector2.Distance(enemy.transform.position, target.position) < 0.1f)
        {
            _currentIndex = (_currentIndex + 1) % _points.Length;
        }
    }
}
