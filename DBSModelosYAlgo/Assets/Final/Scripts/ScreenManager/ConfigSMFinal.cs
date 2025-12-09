using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSMFinal : MonoBehaviour
{
    public Transform mainGame = null;
    public Transform gameOver = null;

    // Ya no necesitamos guardar la referencia a la instancia de pausa
    //public GameObject pausapantalla;

    public bool pausa;

    private void Start()
    {
        // Pantalla inicial: el juego
        ScreenManagerFinal.Instance.Push(new ScreenGOFinal(mainGame));
        pausa = false;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        // 1) Si hay un mensaje arriba de la pausa → cerramos SOLO el mensaje
        if (FindObjectOfType<ScreenMessageFinal>() != null)
        {
            ScreenManagerFinal.Instance.Pop();
            return;
        }

        // 2) Si no hay mensaje, togglear la pausa normal
        if (!pausa)
            OpenPause();
        else
            ClosePause();
    }

    public void OpenPause()
    {
        if (pausa) return;

        // Cargamos el prefab de la pausa desde Resources/PausaScreen.prefab
        GameObject prefab = Resources.Load<GameObject>("PausaScreen");
        GameObject go = Instantiate(prefab);
        ScreenPauseFinal pauseScreen = go.GetComponent<ScreenPauseFinal>();

        ScreenManagerFinal.Instance.Push(pauseScreen);

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();  // Pausa el juego

        pausa = true;
        Debug.Log("Pausa");

    }

    public void ClosePause()
    {
        if (!pausa) return;

        // Sacamos la pantalla de pausa del stack
        ScreenManagerFinal.Instance.Pop();

        // Reanudamos el juego
        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();

        pausa = false;
        Debug.Log("Cerrando pausa");
    }


}
