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

        if (LocalizationManager.instance != null)
        {

            LocalizationManager.instance.EventChangeLang += Translate;

        }

        Translate();

    }

    private void OnDisable()
    {
        
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.EventChangeLang -= Translate;
        }
    }

    void Translate()
    {

        if (LocalizationManager.instance == null)
        {

            
            return;

        }

        _tmp.text = LocalizationManager.instance.Translate(ID);

    }

}

