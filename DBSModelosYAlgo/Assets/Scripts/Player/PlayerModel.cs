using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    //Maneja funciones, estados etc

    public delegate void Healthchangedhandler(int current, int max);
    public delegate void Coinschangedhandler(int coins);
    public delegate void DeathHandler();

    public event Healthchangedhandler OnHealthchanged;
    public event Coinschangedhandler OnCoinschanged;
    public event DeathHandler OnDeath;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealt;
    [SerializeField] private int coins;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        currentHealt = maxHealth;
    }
    void Start()
    {
        OnHealthchanged?.Invoke(currentHealt, maxHealth);
        OnCoinschanged?.Invoke(coins);
    }

    public void Move (Vector3 inputDir)
    {
        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        transform.position = inputDir * moveSpeed * Time.deltaTime;  
    }

    public void TakeDamage (int amount)
    {
        if (amount <= 0 || currentHealt == 0) return;

        currentHealt = Mathf.Max(0, currentHealt - amount);
        OnHealthchanged?.Invoke(currentHealt, maxHealth);

        if (currentHealt == 0)
        {
            OnDeath?.Invoke();
        }
        
    }

    public void heal(int amount)
    {
        if (amount <= 0 || currentHealt == maxHealth) return;

        currentHealt = Mathf.Min(maxHealth, currentHealt + amount);
        OnHealthchanged?.Invoke(currentHealt, maxHealth);
    }

    public void AddCoin (int amount)
    {
        if (amount <= 0) return;
        coins += amount;
        OnCoinschanged?.Invoke(coins);
    }

    public void ResetStats()
    {
        currentHealt = maxHealth;
        coins = 0;
        OnHealthchanged?.Invoke(currentHealt , maxHealth);
        OnCoinschanged?.Invoke(coins);
    }

}
