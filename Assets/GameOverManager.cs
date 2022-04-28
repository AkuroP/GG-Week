using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus de 1 GameOverManager");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("J-MSceneMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Le jeu quite frero");
    }

}
