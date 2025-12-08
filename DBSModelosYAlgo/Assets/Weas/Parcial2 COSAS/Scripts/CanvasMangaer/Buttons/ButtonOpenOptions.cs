using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenOptions : MonoBehaviour
{
    [SerializeField] private ScreenSimpleUI _screenOptions;

    public void ButtonClicked()
    {
        ScreenManager.Instance.Push(_screenOptions);
    }    

}
