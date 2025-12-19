using UnityEngine;

public class PauseManagerFinal : MonoBehaviour
{
    public GameObject pauseMenuUI;
    bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        isPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
