using Builder;
using UnityEngine;

public interface IMoveStrategy
{
    void Move(Enemy enemy, Rigidbody2D rb);
}

public interface IAttackStrategy
{
    void Attack(Enemy enemy, Transform target);
}
