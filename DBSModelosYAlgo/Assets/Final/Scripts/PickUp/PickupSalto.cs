using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSalto : MonoBehaviour
{
    [Header("Configuración del Boost")]
    public float boostMultiplier = 2f;
    public float boostDuration = 4f;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            // Aplica el boost desde el player
            player.ApplyJumpBoost(boostMultiplier, boostDuration);

            // Efecto visual opcional
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Destruye el pickup
            Destroy(gameObject);
        }
    }
}

