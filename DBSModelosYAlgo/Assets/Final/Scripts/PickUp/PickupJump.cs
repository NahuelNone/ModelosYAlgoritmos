using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupJump : MonoBehaviour
{
    [Header("Configuración del Boost")]
    public float boostMultiplier = 2f;
    public float boostDuration = 4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {

            player.ApplyJumpBoost(boostMultiplier, boostDuration);

            Destroy(gameObject);
        }
    }
}

