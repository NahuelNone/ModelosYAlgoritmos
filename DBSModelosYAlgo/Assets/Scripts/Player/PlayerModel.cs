using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    //Maneja funciones, estados etc

    public delegate void HealthChangedHandler(int current, int max);
    public delegate void CoinsChangedHandler(int coins);
    public delegate void DeathHandler();

    public event HealthChangedHandler OnHealthChanged;
    public event CoinsChangedHandler OnCoinsChanged;
    public event DeathHandler OnDeath;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;
    [SerializeField] public int coins;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int jumpForce = 100;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnCoinsChanged?.Invoke(coins);

    }

    public void Move (Vector3 inputDir)
    {

        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        transform.position += inputDir * moveSpeed * Time.deltaTime;

    }

    public void Jump(Vector3 inputDir)
    {

        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        transform.position += inputDir * jumpForce * Time.deltaTime;

    }

    public void TakeDamage (int amount)
    {

        if (amount <= 0 || currentHealth == 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log(currentHealth);

        if (currentHealth == 0)
        {

            OnDeath?.Invoke();

        }
        
    }

    public void heal(int amount)
    {

        if (amount <= 0 || currentHealth == maxHealth) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log(currentHealth);

    }

    public void AddCoin (int amount)
    {

        if (amount <= 0) return;
        coins += amount;
        OnCoinsChanged?.Invoke(coins);

        Debug.Log("moneditas: " + coins);

    }

    public void ResetStats()
    {

        currentHealth = maxHealth;
        coins = 0;
        OnHealthChanged?.Invoke(currentHealth , maxHealth);
        OnCoinsChanged?.Invoke(coins);

    }

}
