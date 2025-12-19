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

    public void Active()
    {
        if (root != null)
            root.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        // Intencionalmente vacío.
        // No tocamos root.gameObject.SetActive(false);
    }

    public string Free()
    {
        if (root != null)
            root.gameObject.SetActive(false);

        return "Pantalla jugable desactivada";
    }
}
