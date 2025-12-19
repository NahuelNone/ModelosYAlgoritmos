using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerViewFinal : MonoBehaviour
{
    [Header("Componentes")]
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Ataque")]
    public Transform firePoint;   
    private float firePointOffsetX = 0.8f;

    [HideInInspector] public bool isGrounded;

    [Header("Vida UI")]
    public Slider healthSlider;

    [Header("Energia Ui")]
    public Slider energySlider;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (firePoint != null && Mathf.Approximately(firePointOffsetX, 0f))
        {
            firePointOffsetX = Mathf.Abs(firePoint.localPosition.x);
        }

    }

    public void Move(float horizontalInput, float moveSpeed)
    {
        Vector2 vel = rb.velocity;
        vel.x = horizontalInput * moveSpeed;
        rb.velocity = vel;

        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;   
            UpdateFirePointSide();
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;  
            UpdateFirePointSide();
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rb.velocity.y);
        }
    }


    public void Jump(float jumpForce)
    {
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
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public float FacingDirectionX
    {
        get
        {
            return spriteRenderer.flipX ? -1f : 1f;
        }
    }

    private void UpdateFirePointSide()
    {
        if (firePoint == null)
            return;

        Vector3 localPos = firePoint.localPosition;
        float x = Mathf.Abs(firePointOffsetX);  

        localPos.x = spriteRenderer.flipX ? -x : x;

        firePoint.localPosition = localPos;
    }

    public void InitHealthUI(float maxHealth)
    {

        Debug.Log("Vida UI!");

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }


    public void UpdateHealthUI(int current, int max)
    {

        Debug.Log("Actualizar Vida UI!");

        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }
        
    }

    public void UpdateEnergyUI(float current)
    {

        Debug.Log("Actualizar Energia UI!");

        if (energySlider != null)
        {
            
            energySlider.value = current;

        }
    }

    public void PlayDeath()
    {

        Debug.Log("Reproducir animación de muerte");

    }
}
