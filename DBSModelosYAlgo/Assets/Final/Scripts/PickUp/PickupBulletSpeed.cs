using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBulletSpeed : MonoBehaviour
{
    [Header("Configuración del Boost de Disparo")]
    public float cooldownMultiplier = 2f;   // Cuánto se reduce (x2 = la mitad del tiempo)
    public float boostDuration = 4f;        // Cuánto dura el efecto

    [Header("Efectos")]
    public GameObject pickupEffect;         // Opcional: partículas o sonido

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            // Aplica el boost de disparo rápido
            player.ApplyAttackCooldownBoost(cooldownMultiplier, boostDuration);

            // Efecto visual opcional
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Destruye el pickup
            Destroy(gameObject);
        }
    }
}
