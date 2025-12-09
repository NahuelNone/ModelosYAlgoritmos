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

    public bool IsGrounded => _isGrounded;
    public int JumpsUsed => _jumpsUsed;

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;

        if (grounded)
        {
            // Si tocamos el piso, reseteamos los saltos
            _jumpsUsed = 0;
        }
    }

    public bool CanJump()
    {
        // Puede saltar si está en el piso o si todavía le queda un salto extra
        return _isGrounded || _jumpsUsed < maxJumps;
    }

    public void OnJump()
    {
        _jumpsUsed++;
        _isGrounded = false;
    }
}
