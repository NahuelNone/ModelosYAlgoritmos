using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupVelocity : MonoBehaviour
{
    [Header("Configuración del Boost de Velocidad")]
    public float speedMultiplier = 1.5f;   // cuánto se multiplica
    public float boostDuration = 4f;       // duración en segundos

    [Header("Efectos")]
    public GameObject pickupEffect;        // opcional (partículas o sonido)

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            // Aplica el boost de velocidad
            player.ApplySpeedBoost(speedMultiplier, boostDuration);

            // Efecto visual opcional
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Destruye el pickup
            Destroy(gameObject);
        }
    }
}