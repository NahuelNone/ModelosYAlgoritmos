using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBulletSpeed : MonoBehaviour
{
    [Header("Configuración del Boost de Disparo")]
    public float cooldownMultiplier = 2f;  
    public float boostDuration = 4f;    
    [Header("Efectos")]
    public GameObject pickupEffect;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            player.ApplyAttackCooldownBoost(cooldownMultiplier, boostDuration);

            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
