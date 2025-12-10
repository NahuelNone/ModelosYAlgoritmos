using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigSMFinal : MonoBehaviour
{
    public Transform mainGame = null;          // root del juego
    public ScreenMenuFinal menuScreen = null;  // referencia al menú en escena

    public bool pausa;

    private void Start()
    {
        // 1) El juego todavía no va
        if (mainGame != null)
            mainGame.gameObject.SetActive(false);

        // 2) Arrancamos en el MENÚ
        ScreenManagerFinal.Instance.Push(menuScreen);

        pausa = false;
    }

    private void Update()
    {
        // No procesamos ESC si no se presionó
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        // No queremos pausa en el menú:
        if (mainGame == null || !mainGame.gameObject.activeInHierarchy)
            return;

        // Si hay un mensaje arriba, primero cerramos el mensaje
        if (FindObjectOfType<ScreenMessageFinal>() != null)
        {
            ScreenManagerFinal.Instance.Pop();
            return;
        }

        // Toglear pausa solo cuando estamos en el juego
        if (!pausa)
            OpenPause();
        else
            ClosePause();
    }

    // ---- llamado por ScreenMenuFinal.BTN_Play ----
    public void StartGame()
    {
        if (mainGame != null)
            mainGame.gameObject.SetActive(true);

        // Apilamos el juego como nueva pantalla
        ScreenManagerFinal.Instance.Push(new ScreenGOFinal(mainGame));
    }

    public void OpenPause()
    {
        if (pausa) return;

        GameObject prefab = Resources.Load<GameObject>("PausaScreen");
        GameObject go = Instantiate(prefab);
        ScreenPauseFinal pauseScreen = go.GetComponent<ScreenPauseFinal>();

        ScreenManagerFinal.Instance.Push(pauseScreen);

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();  // Time.timeScale = 0

        pausa = true;
        Debug.Log("Pausa");
    }

    public void ClosePause()
    {
        if (!pausa) return;

        ScreenManagerFinal.Instance.Pop();

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();  // Time.timeScale = 1

        pausa = false;
        Debug.Log("Cerrando pausa");
    }

    public void ReturnToMenu()
    {
        // Asegurarnos de que el tiempo vuelve a la normalidad
        Time.timeScale = 1f;
        pausa = false;

        // Recargamos la escena actual
        Scene sceneActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneActual.buildIndex);
    }



}
