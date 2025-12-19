using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupVelocity : MonoBehaviour
{
    [Header("Configuración del Boost de Velocidad")]
    public float speedMultiplier = 1.5f;  
    public float boostDuration = 4f;    

    [Header("Efectos")]
    public GameObject pickupEffect;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            player.ApplySpeedBoost(speedMultiplier, boostDuration);

            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}