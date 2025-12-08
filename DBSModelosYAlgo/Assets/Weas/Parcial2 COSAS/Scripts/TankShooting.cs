using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    // Prefab del proyectil
    public GameObject bulletPrefab;

    // Punto desde donde se dispara (asignar el FirePoint)
    public Transform firePoint;

    // Fuerza o velocidad del proyectil
    public float bulletSpeed = 20f;

    // Tiempo entre disparos (para evitar spam)
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

   // public float damage = 25f;

    void Update()
    {
        // Si presionamos Espacio y ya pasó el tiempo de espera
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instanciamos el proyectil en la posición y rotación del FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Tomamos su Rigidbody para aplicarle velocidad
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        // Destruimos el proyectil después de unos segundos para no llenar la escena
        Destroy(bullet, 3f);
    }

   /* private void OnCollisionEnter(Collision collision)
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
    }*/
}
