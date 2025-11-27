using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 25f; // Daño que causa al impactar

    private void OnCollisionEnter(Collision collision)
    {
        // Intentamos obtener el componente de vida del objeto impactado
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            // Si tiene vida, le aplicamos daño
            targetHealth.TakeDamage(damage);
        }

        // Destruimos la bala después del impacto
        Destroy(gameObject);
    }
}
