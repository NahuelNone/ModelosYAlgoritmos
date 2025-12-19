using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TXTTranslateFinal : MonoBehaviour
{
    public string ID;
    TextMeshProUGUI _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {

        if (LocalizationManagerFinal.instance != null)
        {

            LocalizationManagerFinal.instance.EventChangeLang += Translate;

        }

        Translate();

    }

    private void OnDisable()
    {
        
        if (LocalizationManagerFinal.instance != null)
        {
            LocalizationManagerFinal.instance.EventChangeLang -= Translate;
        }
    }

    void Translate()
    {

        if (LocalizationManagerFinal.instance == null)
        {

            
            return;

        }

        _tmp.text = LocalizationManagerFinal.instance.Translate(ID);

    }

}

