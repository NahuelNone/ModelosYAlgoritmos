    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallFinal : MonoBehaviour
{

    public Collider2D Collider2D;


    private void OnCollisionEnter2D(Collision2D Collider2D)
    {
        
        if (Collider2D.gameObject.CompareTag("Player"))
        {

            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        }

    }

}
