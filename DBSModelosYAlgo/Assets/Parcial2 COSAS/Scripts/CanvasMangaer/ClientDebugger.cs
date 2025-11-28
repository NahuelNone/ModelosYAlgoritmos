using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDebugger : MonoBehaviour
{
    [SerializeField] private Transform _mainGameplay;
    [SerializeField] private Transform _miniGameplay;

    [SerializeField] private ScreenSimpleUI _pauseScreen;

    private bool isPaused = false;

    private ScreenGameplay _miniScreen;
    private Transform _miniInstance;
    private bool _miniActive = false;

    private void Start()
    {
        var main = new ScreenGameplay(_mainGameplay);
        ScreenManager.Instance.Push(main);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMiniGameplay();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            HideMiniGameplay();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void ShowMiniGameplay()
    {
        if (_miniActive) return;

        _miniInstance = Instantiate(_miniGameplay);
        _miniScreen = new ScreenGameplay(_miniInstance);

        ScreenManager.Instance.Push(_miniScreen);
        _miniActive = true;
    }

    void HideMiniGameplay()
    {
        if (!_miniActive) return;

        ScreenManager.Instance.Pop();

        if (_miniInstance != null)
        {
            _miniInstance.gameObject.SetActive(false);
        }

        _miniScreen = null;
        _miniInstance = null;
        _miniActive = false;
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
            _pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
