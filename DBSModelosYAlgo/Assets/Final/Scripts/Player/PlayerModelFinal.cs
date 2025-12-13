using System;
using UnityEngine;

[Serializable]
public class PlayerModelFinal
{

    // EVENTOS
    public event Action<int, int> OnHealthChanged;   // current, max
    public event Action<float, float> OnEnergyChanged;     // current, max
    public event Action OnDeath;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public int maxJumps = 2;

    private int _jumpsUsed = 0;
    private bool _isGrounded = false;

    [Header("Ataque")]
    public float attackCooldown = 0.25f;
    private float _attackCooldownTimer = 0f;

    // 🔹 VIDA
    [Header("Vida")]
    public int maxHealth = 100;
    private int _currentHealth;

    public bool IsGrounded => _isGrounded;
    public int JumpsUsed => _jumpsUsed;

    public float CurrentHealth => _currentHealth;

    public float AttackCooldownTimer => _attackCooldownTimer;
    public bool IsDead => _currentHealth <= 0;

    public void Init()
    {

        _currentHealth = maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);

    }

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;
        if (grounded) _jumpsUsed = 0;
    }

    public bool CanJump()
    {
        return _isGrounded || _jumpsUsed < maxJumps;
    }

    public void OnJump()
    {
        _jumpsUsed++;
        _isGrounded = false;
    }

    public float TickAttackCooldown(float deltaTime)
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= deltaTime;

        return _attackCooldownTimer;

    }

    public bool CanAttack()
    {
        return _attackCooldownTimer <= 0f;
    }

    public void OnAttack()
    {
        _attackCooldownTimer = attackCooldown;
    }

    // 🔹 Recibir daño / curación
    public void TakeDamage(float amount)
    {
        _currentHealth = (int)Mathf.Clamp(_currentHealth - amount, 0f, maxHealth);
    }

}
