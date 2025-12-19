using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBulletSpeed : MonoBehaviour
{
    [Header("Configuración del Boost de Disparo")]
    public float cooldownMultiplier = 2f;
    public float boostDuration = 4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            player.ApplyAttackCooldownBoost(cooldownMultiplier, boostDuration);

            Destroy(gameObject);
        }
    }
}
