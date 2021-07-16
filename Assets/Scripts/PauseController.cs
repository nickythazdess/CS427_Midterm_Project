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
            Debug.Log("pause");
            if (!gameIsPaused) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        gameIsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        click.Play();
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        click.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scene");
        click.Play();
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
        click.Play();
    }
}
