using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class Player : MonoBehaviour
{
    public GameObject lifeDimension;
    public GameObject deathDimension;

    [Header("True = Life; False = Death")]
    [SerializeField]
    private bool lifeOrDeath;
    // Start is called before the first frame update
    
    private void Awake()
    {
        lifeOrDeath = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(this.lifeOrDeath)
            {
                lifeDimension.SetActive(false);
                deathDimension.SetActive(true);
                lifeOrDeath = false;
            }
            else
            {
                lifeDimension.SetActive(true);
                deathDimension.SetActive(false);
                lifeOrDeath = true;
            }
        }
    }
}
