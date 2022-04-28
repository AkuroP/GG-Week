using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject button;

    public GameObject buttonSetting;

    public GameObject mainMenu;

    public GameObject settingsMenu;

    public bool Menu = true;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private void CheckMenu()
    {
        if (Menu)
        {
         EventSystem.current.firstSelectedGameObject = button;

        }
        else 
        {
            EventSystem.current.firstSelectedGameObject = buttonSetting;
        }
        
    }

    public void Update()
    {
        if (mainMenu.activeInHierarchy)
        {
            Menu = true;
            CheckMenu();
        }
        else
        {
            Menu = false;
            CheckMenu();
        }
    }
}
