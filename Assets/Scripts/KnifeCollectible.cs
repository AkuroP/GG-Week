using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectible : MonoBehaviour
{
    public GameObject cinematic;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.GetComponent<Player>().canAttack = true;
            collider.GetComponent<Player>().animator.SetBool("HasKnife", true);
            cinematic.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
