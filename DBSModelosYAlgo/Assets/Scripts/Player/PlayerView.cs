
using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;
    public TMP_Text healthText;     
    public TMP_Text coinsText;

    public Renderer bodyRenderer;


    private void OnEnable()
    {

        if (playerModel == null)
        {

            Debug.Log("Weas andan mal");
            return;

        }

        // (suscripciones)
        playerModel.OnHealthChanged += HandleHealthChanged;
        playerModel.OnCoinsChanged += HandleCoinsChanged;
        playerModel.OnDeath += HandleDeath;

    }


    private void OnDisable()
    {

        playerModel.OnHealthChanged -= HandleHealthChanged;
        playerModel.OnCoinsChanged -= HandleCoinsChanged;
        playerModel.OnDeath -= HandleDeath;

    }

    private void HandleHealthChanged(int current , int max)
    {

        if (healthText != null)
        
            healthText.text = $"Vida:{current}/{max}";

        if (bodyRenderer != null)
        {
        
            float t = max > 0 ? (float) current/max : 0f ;
            Color c = Color.Lerp(Color.red, Color.green, t);
            bodyRenderer.material.color = c;
        
        }
        
    }

    private void HandleCoinsChanged (int coins)
    {
        if (coinsText != null)
        {

            coinsText.text = $"Monedas: {coins} ";

        }
    }

    private void HandleDeath ()
    {
        Debug.Log("[VIEW TMP] El player fallecio. Mostra pantalla de game over");
    }
}
