using UnityEngine;

public class LevelPortalFinal : MonoBehaviour
{
    public string playerTag = "Player";
    bool _used = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_used) return;
        if (!other.CompareTag(playerTag)) return;

        _used = true;

        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
        {
            config.GoToNextLevel();
        }
        else
        {
            Debug.LogWarning("ConfigSMFinal no encontrado en la escena.");
        }
    }
}
