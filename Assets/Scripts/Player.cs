using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    [Header("Player Move")]
    public float playerSpeed;
    public float playerJumpForce;
    private float horizontalMove;
    
    [Header("Player Jump")]
    public int maxJumpCount;
    [SerializeField]
    private int jumpCount;
    
    public bool isGrounded;
    [SerializeField]
    private bool hasJumped;
    [SerializeField]
    public float raycastDistance;
    public LayerMask whatIsGround;

    [Header("True = Life; False = Death")]
    [Header("Dimension Switch")]
    public GameObject lifeDimension;
    public GameObject deathDimension;
    [SerializeField]
    public bool lifeOrDeath = true;

    [Header("Interaction")]
    public GameObject interactableObj;
    
    [Header("Attack")]
    public int attackDamage;
    public Transform hitBoxPointLeft;
    public Transform hitBoxPointRight;
    private Transform hitBoxPoint = null;
    public float attackRange;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    
    private void Awake()
    {
        lifeOrDeath = true;
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
        hitBoxPoint = hitBoxPointRight;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * playerSpeed, rb.velocity.y);
        GroundCheck();
        
    }

    //interverti entre le monde des vivant et le monde des morts
    public void STAB(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            GameObject[] ghostPlatform = GameObject.FindGameObjectsWithTag("GhostPlatform");
            
            foreach(GameObject ghostGO in ghostPlatform)
            {
                GhostPlatform ghostPlat = ghostGO.GetComponent<GhostPlatform>();
                
                if(this.lifeOrDeath)
                {
                    if(ghostPlat.LifeOrDeathGhost)
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = false;
                        ghostGO.GetComponent<SpriteRenderer>().color = ghostGO.GetComponent<GhostPlatform>().deathDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = false;
                        
                    }
                    else
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = true;
                        ghostGO.GetComponent<SpriteRenderer>().color = ghostGO.GetComponent<GhostPlatform>().deathDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = true;
                        
                    }
                    lifeDimension.SetActive(false);
                    deathDimension.SetActive(true);
                    
                }
                else
                {
                    if(ghostPlat.LifeOrDeathGhost)
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = true;
                        ghostGO.GetComponent<SpriteRenderer>().color = ghostGO.GetComponent<GhostPlatform>().lifeDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = true;
                        
                    }
                    else
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = false;
                        ghostGO.GetComponent<SpriteRenderer>().color = ghostGO.GetComponent<GhostPlatform>().lifeDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = false;
                    }
                    lifeDimension.SetActive(true);
                    deathDimension.SetActive(false);
                    
                    
                }
            }
            if(this.lifeOrDeath)
            {
                lifeOrDeath = false;
            }
            else
            {
                lifeOrDeath = true;
            }
        }
    }

    //mouvement du joueur
    public void PlayerMovement(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
        if(context.performed)
        {
            if(horizontalMove < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
                hitBoxPoint = hitBoxPointLeft; 
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
                hitBoxPoint = hitBoxPointRight; 
            }
        }
    }

    //saut du joueur
    public void PlayerJump(InputAction.CallbackContext context)
    {
        if(context.performed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
            jumpCount--;
            hasJumped = true;
        }

        if(context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
            hasJumped = false;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(this.transform.position, Vector2.down, raycastDistance, whatIsGround);
        if(isGrounded && !hasJumped)
        {
            jumpCount = maxJumpCount;
        }
    }

    public void PlayerAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(hitBoxPoint.transform.position, attackRange, enemyLayer);
            foreach(Collider2D enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().Hit(attackDamage);
            }
            if(interactableObj != null)
            {
                switch(interactableObj.tag)
                {
                    case "Lever":
                    interactableObj.GetComponent<Lever>().ActivateLever();
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector2.down * raycastDistance);
        if(hitBoxPoint != null)
        {
            Gizmos.DrawWireSphere(hitBoxPoint.transform.position, attackRange);
        }
    }
}
