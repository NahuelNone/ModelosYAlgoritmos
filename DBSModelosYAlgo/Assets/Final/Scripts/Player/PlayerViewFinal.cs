using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerViewFinal : MonoBehaviour
{
    [Header("Componentes")]
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [HideInInspector] public bool isGrounded;

    private void Reset()
    {
        // Se completa solo cuando agregás el script
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Move(float horizontalInput, float moveSpeed)
    {
        Vector2 vel = rb.velocity;
        vel.x = horizontalInput * moveSpeed;
        rb.velocity = vel;

        // Flip visual
        if (horizontalInput > 0.01f)
            spriteRenderer.flipX = false;
        else if (horizontalInput < -0.01f)
            spriteRenderer.flipX = true;

        // Animación simple (si tenés Animator configurado)
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rb.velocity.y);
        }
    }

    public void Jump(float jumpForce)
    {
        // Reseteo la velocidad vertical para que el salto sea consistente
        Vector2 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;

        if (animator != null)
        {
            animator.SetBool("IsGrounded", grounded);
        }
    }

    public void Attack()
    {
        
        Debug.Log("Player attacked!");

    }


}
