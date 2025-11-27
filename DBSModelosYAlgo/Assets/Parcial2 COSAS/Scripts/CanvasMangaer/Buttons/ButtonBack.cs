using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
   public void buttonClicked()
    {
        ScreenManager.Instance.Pop();
    }
}
