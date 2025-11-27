using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Atributos de Vida")]
    public float maxHealth = 100f;  // Vida máxima
    public float currentHealth;     // Vida actual

    private void Start()
    {
        currentHealth = maxHealth;   // Al iniciar, empieza con vida completa
    }

    // Método público para recibir daño
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Restamos vida
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método público para curar si lo necesitás más adelante
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    // Método para morir (destruir objeto o ejecutar animación)
    void Die()
    {
        Destroy(gameObject);
    }
}