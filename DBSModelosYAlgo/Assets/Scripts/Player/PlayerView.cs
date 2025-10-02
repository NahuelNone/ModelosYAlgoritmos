
using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;
    public TMP_Text healtText;
    public TMP_Text coinsText;

    public Renderer bodyRenderer;


    private void OnEnable()
    {
        if (playerModel != null) return;

        //playerModel.OnHealthchanged += ...
        //playerModel.OnCoinschanged += ...
        //playerModel.OnDeath += ...

    }

    private void OnDisable()
    {
        //playerModel.OnHealthchanged -= ...
        //playerModel.OnCoinschanged -= ...
        //playerModel.OnDeath -= ...

    }
  private void HandHealthChanged(int current , int max)
    {
        if (healtText != null)
        
            healtText.text = $"Vida:{current}/{max}";

        if (bodyRenderer != null )
        {
            float t = max > 0 ? (float) current/max : 0f ;
            Color c = Color.Lerp(Color.red, Color.green, t);
            bodyRenderer.material.color = c;
        }
        
    }

    private void HandleCoinsChanged (int coins)
    {
        if ( coinsText != null)
        {
            coinsText.text = $"Monedas: {coins} ";
        }
    }

    private void HandleDeath ()
    {
        Debug.Log("[VIEW TMP] El player fallecio. Mostra pantalla de game over");
    }
}
