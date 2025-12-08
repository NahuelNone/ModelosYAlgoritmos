using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float moveSpeed = 3f;        // Velocidad del enemigo
    public float attackDamage = 20f;    // Daño al tocar al player
    public float attackRate = 1f;       // Cada cuánto puede atacar
    private float nextAttackTime = 0f;

    private Transform player;

    void Start()
    {
        // Buscamos al player por su tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Moverse hacia el player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Mirar hacia el player (solo en eje Y)
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Si colisiona con el player, lo ataca
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            nextAttackTime = Time.time + attackRate;
        }
    }
}