using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDebugger : MonoBehaviour
{
    [SerializeField] private Transform _mainGameplay;
    [SerializeField] private Transform _miniGameplay;

    [SerializeField] private ScreenSimpleUI _pauseScreen;

    private bool isPaused = false;

    private void Start()
    {

        var main = new ScreenGameplay(_mainGameplay);

        ScreenManager.Instance.Push(main);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            ScreenManager.Instance.Push(new ScreenGameplay(Instantiate(_miniGameplay)));

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPaused)
        //        TogglePause(); // salir del menú de pausa
        //    else
        //        ScreenManager.Instance.Pop();
        //}
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            ScreenManager.Instance.Push(_pauseScreen);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            ScreenManager.Instance.Pop();
            _pauseScreen.gameObject.SetActive(false); // 🔹 aseguro que el canvas desaparezca
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
