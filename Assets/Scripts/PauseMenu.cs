using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject PauseMenuUI;

    void Update()
    {
       
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void  Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

        }
    }
}
