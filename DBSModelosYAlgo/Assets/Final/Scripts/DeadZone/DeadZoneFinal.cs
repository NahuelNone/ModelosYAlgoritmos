using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class DeadZoneFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            player.ReceiveDamage(1000);
        }
    }
}
