using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TXTTranslate : MonoBehaviour
{

    public string ID;
    TextMeshProUGUI _tmp;

    private void Start()
    {
        
        _tmp = GetComponent<TextMeshProUGUI>();

        Translate();

    }

    void Translate()
    {

        _tmp.text = LocalizationManager.instance.Translate(ID);

    }

}
