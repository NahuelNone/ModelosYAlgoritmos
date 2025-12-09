using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGOFinal : IScreenFinal
{
    public Transform root;

    public ScreenGOFinal(Transform root)
    {
        this.root = root;
    }

    // Se llama cuando esta pantalla vuelve a ser la activa en el stack
    public void Active()
    {
        // Si por algún motivo el objeto se desactivó, lo reactivamos.
        // Si siempre está activo, esto no rompe nada.
        if (root != null)
            root.gameObject.SetActive(true);
    }

    // IMPORTANTE: ya no desactivamos el mainGame acá.
    // La "pausa" la maneja exclusivamente PauseManager con Time.timeScale = 0.
    public void Deactivate()
    {
        // Intencionalmente vacío.
        // No tocamos root.gameObject.SetActive(false);
    }

    public string Free()
    {
        // Si alguna vez querés destruir el nivel (por ejemplo al cambiar de escena),
        // se hace acá.
        if (root != null)
            GameObject.Destroy(root.gameObject);

        return "Delete una pantalla jugable";
    }
}
