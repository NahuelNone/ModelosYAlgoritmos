using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfazManager : MonoBehaviour
{

    public TextMeshProUGUI textoVida;

    public Health vidaCode;

    private void Update()
    {

        textoVida.text = vidaCode.currentHealth.ToString();

    }

}
