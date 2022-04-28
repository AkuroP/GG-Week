using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().canAttack = false;
            Destroy(this.gameObject);
        }
    }
}
