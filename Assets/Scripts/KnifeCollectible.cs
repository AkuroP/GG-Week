using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectible : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().canAttack = true;
            Destroy(this.gameObject);
        }
    }
}
