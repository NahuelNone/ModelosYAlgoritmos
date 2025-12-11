using Builder;
using UnityEngine;

public interface IMoveStrategy
{
    void Move(EnemyFinal enemy, Rigidbody2D rb);
}

public interface IAttackStrategy
{
    void Attack(EnemyFinal enemy, Transform target);
}
