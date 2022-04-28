using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform finalPoint;
    [SerializeField]
    private float platformSpeed;
    public float deathDimensionPlatformSpeed;
    public bool doesReturn;
    public bool isAffectedByDimShift;
    private Transform posToMove;
    private Player player;
    private SpriteRenderer platformRenderer;
    // Start is called before the first frame update
    void Start()
    {
        posToMove = finalPoint;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        deathDimensionPlatformSpeed = platformSpeed / 5;
        platformRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if(!doesReturn)
        {
            
            if(finalPoint.position == this.transform.position)
            {
                this.transform.position = startPoint.position;
            }
            else
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, finalPoint.position, platformSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(isAffectedByDimShift)
            {
                if(player.lifeOrDeath)
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, posToMove.position, platformSpeed * Time.deltaTime);
                    platformRenderer.color = Color.white;
                }
                else
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, posToMove.position, deathDimensionPlatformSpeed * Time.deltaTime);
                    platformRenderer.color = Color.black;
                }
            }
            else
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, posToMove.position, platformSpeed * Time.deltaTime);
            }
            if(this.transform.position == posToMove.position)
            {
                if(posToMove == finalPoint)
                {
                    posToMove = startPoint;
                }
                else if(posToMove == startPoint)
                {
                    posToMove = finalPoint;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().isGrounded)
            {
                collision.gameObject.transform.SetParent(this.transform);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
