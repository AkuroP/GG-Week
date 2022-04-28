using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    public GameObject portail;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            StartCoroutine(Teleport());
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);

        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector2 (portail.transform.position.x, portail.transform.position.y);
        
    }

    
}
