using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().canInteract = true;
            collider.GetComponent<Player>().interactableObj = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().canInteract = false;
            collider.GetComponent<Player>().interactableObj = null;
        }
    }

    public void ActivateLever()
    {
        Debug.Log("Lever Activated");
    }
}
