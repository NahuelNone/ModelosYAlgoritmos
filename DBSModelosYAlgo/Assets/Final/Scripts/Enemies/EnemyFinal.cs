using UnityEngine;

public enum MoveType { Patrol, Chase }
public enum AttackType { Melee, Ranged }

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Config general")]
    public MoveType moveType;
    public AttackType attackType;
    public float moveSpeed = 2f;

    [Header("Movimiento Patrulla")]
    public Transform[] patrolPoints;

    [Header("Ataque")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;

    [Header("Referencias")]
    public Transform player; // arrastrás el player acá en el inspector

    private IMoveStrategy _moveStrategy;
    private IAttackStrategy _attackStrategy;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Elegir estrategia de movimiento
        switch (moveType)
        {
            case MoveType.Patrol:
                _moveStrategy = new PatrolMoveStrategy(patrolPoints, moveSpeed);
                break;

            case MoveType.Chase:
                _moveStrategy = new ChaseMoveStrategy(player, moveSpeed);
                break;
        }

        // Elegir estrategia de ataque
        switch (attackType)
        {
            case AttackType.Melee:
                _attackStrategy = new MeleeAttackStrategy(attackRange, attackCooldown);
                break;

            case AttackType.Ranged:
                _attackStrategy = new RangedAttackStrategy(attackRange, attackCooldown, bulletPrefab);
                break;
        }
    }

    private void Update()
    {
        if (player == null) return;

        _moveStrategy?.Move(this, _rb);
        _attackStrategy?.Attack(this, player);
    }

    // Permite cambiar la estrategia en runtime (opcional)
    public void SetMoveStrategy(IMoveStrategy newStrategy)
    {
        _moveStrategy = newStrategy;
    }

    public void SetAttackStrategy(IAttackStrategy newStrategy)
    {
        _attackStrategy = newStrategy;
    }
}
