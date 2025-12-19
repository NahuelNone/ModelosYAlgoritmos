using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    
    [Header("Configuración del salto")]
    [Tooltip("Fuerza con la que el JumpPad impulsará al jugador.")]
    public float jumpForce = 15f;

    [Tooltip("Dirección del impulso. Por defecto es hacia arriba.")]
    public Vector2 jumpDirection = Vector2.up;

    [Header("Efectos opcionales")]
    [Tooltip("Partículas o efectos visuales al activarse (opcional).")]
    public ParticleSystem activateEffect;

    [Tooltip("Sonido al activarse (opcional).")]
    public AudioSource activateSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 normalizedDirection = jumpDirection.normalized;

            rb.velocity = normalizedDirection * jumpForce;

            if (activateEffect != null)
                activateEffect.Play();

            if (activateSound != null)
                activateSound.Play();
        }
    }
}
