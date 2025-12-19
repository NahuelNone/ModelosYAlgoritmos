using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class DeadZoneFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entró es el Player
        PlayerControllerFinal player = other.GetComponent<PlayerControllerFinal>();

        if (player != null)
        {
            // Mata al jugador
            player.ReceiveDamage(1000);
        }
    }
}
