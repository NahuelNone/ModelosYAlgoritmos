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

    // Este método se llama automáticamente cuando otro objeto entra en el collider del JumpPad
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificamos si el objeto que colisiona tiene un Rigidbody2D
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Normalizamos la dirección para que siempre tenga magnitud 1
            Vector2 normalizedDirection = jumpDirection.normalized;

            // Aplicamos la fuerza multiplicando la dirección por la intensidad
            rb.velocity = normalizedDirection * jumpForce;

            // Activamos partículas o sonido si los hay
            if (activateEffect != null)
                activateEffect.Play();

            if (activateSound != null)
                activateSound.Play();
        }
    }
}
