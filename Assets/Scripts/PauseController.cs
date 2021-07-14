using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private AudioSource click;
    public static bool gameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        GameAssets.i.button_click.Play();
        gameIsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        GameAssets.i.button_click.Play();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        GameAssets.i.button_click.Play();
        SceneManager.LoadScene("Scene");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        GameAssets.i.button_click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
