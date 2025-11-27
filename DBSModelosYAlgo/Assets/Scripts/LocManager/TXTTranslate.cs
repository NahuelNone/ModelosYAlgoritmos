using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TXTTranslate : MonoBehaviour
{
    public string ID;
    TextMeshProUGUI _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        // Me suscribo al evento de cambio de idioma
        if (LocalizationManager.instance != null)
        {

            LocalizationManager.instance.EventChangeLang += Translate;

        }

        // Traducción inicial
        Translate();

    }

    private void OnDisable()
    {
        // Me desuscribo para evitar memory leaks
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.EventChangeLang -= Translate;
        }
    }

    void Translate()
    {

        if (LocalizationManager.instance == null)
        {

            // Por si algo falló
            return;

        }

        _tmp.text = LocalizationManager.instance.Translate(ID);

    }

}

