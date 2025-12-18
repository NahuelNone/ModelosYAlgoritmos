using System;
using UnityEngine;

public enum MoveType { Patrol, Chase }
public enum AttackType { Melee, Ranged }

public class EnemyFinal : PrototypeFinal, IDamagable
{
    [Header("Config general")]
    public MoveType moveType;
    public AttackType attackType;
    public float moveSpeed = 2f;

    [Header("Vida")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Movimiento Patrulla")]
    public Transform[] patrolPoints;

    [Header("Ataque")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;

    [Header("Referencias")]
    public Transform player;

    [Header("Hurt")]
    public Animator animator;

    private IMoveStrategy _moveStrategy;
    private IAttackStrategy _attackStrategy;
    private Rigidbody2D _rb;

    private bool _initialized;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // Fallback: si lo dejás armado a mano desde inspector
        if (!_initialized)
            BuildStrategies();
    }

    public void Init(Transform playerRef, Transform[] points)
    {
        player = playerRef;
        patrolPoints = points;
        BuildStrategies();
        _initialized = true;
    }

    private void BuildStrategies()
    {
        switch (moveType)
        {
            case MoveType.Patrol:
                _moveStrategy = new PatrolMoveStrategy(patrolPoints, moveSpeed);
                break;
            case MoveType.Chase:
                _moveStrategy = new ChaseMoveStrategy(player, moveSpeed);
                break;
        }

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

    private void FixedUpdate() => _moveStrategy?.Move(this, _rb);

    private void Update()
    {
        if (player == null) return;
        _attackStrategy?.Attack(this, player);
    }

    public EnemyFinal SetPosition(Transform t)
    {
        transform.position = t.position;
        return this;
    }

    public override PrototypeFinal Clone()
    {
        return Instantiate(this);
    }

    public void ReceiveDamage(int damage)
    {
        if (animator != null) animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
