using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ppp : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject PauseMenuUI;
    [SerializeField] InputActionReference _pause;
    [SerializeField] InputActionAsset _inputBinding;

    private void Start()
    {
        _inputBinding.Enable();

        _pause.action.started += Action_started;
       
        _pause.asset.Enable();

    }




    private void OnDestroy()
    {
        _pause.action.started -= Action_started;

    }

    public void Pause()
    {
        if (!isPaused)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

        }
        else
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    
    private void Action_started(InputAction.CallbackContext obj)
    {
        
        Debug.Log("lalalalalalalalalala");
        
        Pause();
        
        throw new System.NotImplementedException();
        

        
    }

}
