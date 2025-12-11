using System;
using UnityEngine;

[Serializable]
public class PlayerModelFinal
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public int maxJumps = 2;

    private int _jumpsUsed = 0;
    private bool _isGrounded = false;

    // 🔽 NUEVO: ATAQUE
    [Header("Ataque")]
    public float attackCooldown = 0.25f; // tiempo entre ataques
    private float _attackCooldownTimer = 0f;

    public bool IsGrounded => _isGrounded;
    public int JumpsUsed => _jumpsUsed;

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;

        if (grounded)
        {
            _jumpsUsed = 0;
        }
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

    // 🔽 NUEVO: ATAQUE
    public void TickAttackCooldown(float deltaTime)
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= deltaTime;
    }

    public bool CanAttack()
    {
        return _attackCooldownTimer <= 0f;
    }

    public void OnAttack()
    {
        _attackCooldownTimer = attackCooldown;
    }
}
