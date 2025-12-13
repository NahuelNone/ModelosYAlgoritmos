using System;
using UnityEngine;

public enum MoveType { Patrol, Chase }
public enum AttackType { Melee, Ranged }

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFinal : PrototypeFinal, IDamagable
{
    [Header("Config general")]
    public MoveType moveType;
    public AttackType attackType;
    public float moveSpeed = 2f;

    [Header("Vida")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    [Header("Movimiento Patrulla")]
    public Transform[] patrolPoints;

    [Header("Ataque")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;

    [Header("Referencias")]
    public Transform player; // arrastrás el player acá en el inspector

    [Header("Hurt")]
    public Animator animator;

    private IMoveStrategy _moveStrategy;
    private IAttackStrategy _attackStrategy;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        // inicializar vida
        currentHealth = maxHealth;

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

    private void FixedUpdate()
    {
        _moveStrategy?.Move(this, _rb);
    }

    private void Update()
    {
        if (player == null) return;
        _attackStrategy?.Attack(this, player);
    }

    // ----------- VIDA ------------

    public void ReceiveDamage(int damage)
    {

        animator.SetTrigger("Hurt");

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"[{name}] recibe {damage} de daño. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[{name}] muere");
        // acá podés poner animación, sonido, etc.
        Destroy(gameObject);
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

    // ==== PROTOTYPE helpers (fluentes) ====

    //public EnemyFinal SetColor(Color col)
    //{
    //    var sr = GetComponent<SpriteRenderer>();
    //    if (sr != null)
    //        sr.color = col;
    //
    //    return this;
    //}

    public EnemyFinal SetPosition(GameObject goP)
    {
        transform.position = goP.transform.position;
        return this;
    }

    //public EnemyFinal SetScale(float x = 1, float y = 1, float z = 1)
    //{
    //    transform.localScale = new Vector3(x, y, z);
    //    return this;
    //}

    // Implementación del Prototype
    public override PrototypeFinal Clone()
    {
        // Esto clona TODO el prefab: Sprite, Rigidbody2D, EnemyFinal, estrategias, etc.
        EnemyFinal clone = Instantiate(this);

        // Si querés randomizar algo automáticamente en cada clon, podés hacerlo acá:
        // clone.SetColor(Random.ColorHSV());

        return clone;
    }
}
