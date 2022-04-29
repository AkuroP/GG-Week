using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject button;



    

    public GameObject mainMenu;

    

    public bool Menu = true;
    public void PlayGame(string scene)
    {
        SceneManager.LoadScene(scene);
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
