using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ppp : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject resumeButton;

   

    

    public GameObject PauseMenuUI;
    [SerializeField] InputActionReference _pause;
    [SerializeField] InputActionAsset _inputBinding;

    private void Start()
    {
        _inputBinding.Enable();

        _pause.action.started += Action_started;

        _pause.asset.Enable();

    }

    private void Update()
    {
        if (PlayerHealth.instance.currentHealth <= 0)
        {
            resumeButton.SetActive(false);
        }
    }




    private void OnDestroy()
    {
        _pause.action.started -= Action_started;

    }

    public void Pause()
    {
        if (!isPaused)
        {
            Paused();

        }
        else
        {
            Resume();
        }
    }

    void Paused()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            
    }
    public void RetryButton()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("J-MSceneMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    private void Action_started(InputAction.CallbackContext obj)
    {
        Pause();

        //EventSystem.current.SetSelectedGameObject();
    }


    
}
