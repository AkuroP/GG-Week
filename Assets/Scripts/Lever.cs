using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject platformToActivate;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().interactableObj = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().interactableObj = null;
        }
    }

    public void ActivateLever()
    {
        platformToActivate.SetActive(true);
    }
}
