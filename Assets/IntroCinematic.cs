using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.InputSystem;  
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
public class IntroCinematic : MonoBehaviour
{
    [SerializeField] InputActionReference _exitCinematic;
    [SerializeField] InputActionAsset _inputBinding;
    // Start is called before the first frame update
    void Start()
    {
        _inputBinding.Enable();
        _exitCinematic.action.started += Action_started;
        Time.timeScale = 0f;
    }

     private void OnDestroy()
    {
        _exitCinematic.action.started -= Action_started;

    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        this.GetComponent<Animator>().SetTrigger("Off");
    }

    public void DestroyCinematic()
    {
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
