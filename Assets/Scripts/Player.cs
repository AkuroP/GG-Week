using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class Player : MonoBehaviour
{

    public Rigidbody rb;
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

    //public CircleCollider2D circleCollider;

    
    [Header("Stab")]
    public GameObject stabParticle;
    public List<GameObject> stabClips;


    public Animator animator;
    // Start is called before the first frame update

     public static Player instance;

    public bool canAttack;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus de playerMovement dans la scene heal");
            lifeOrDeath = true;

            return;
        }

        instance = this;
    }
    
    

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        jumpCount = maxJumpCount;
        hitBoxPoint = hitBoxPointRight;
        lifeDimension = GameObject.FindWithTag("LifeDimension");
        deathDimension = GameObject.FindWithTag("DeathDimension");
        deathDimension.SetActive(false);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * playerSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        GroundCheck();
        
    }

    //interverti entre le monde des vivant et le monde des morts
    public void STAB(InputAction.CallbackContext context)
    {
        if(context.performed /*&& isGrounded*/)
        {
            if(canAttack)
            {
                int random = Random.Range(0, stabClips.Count);
                if(lifeOrDeath)
                {
                    Instantiate(stabClips[random]);
                    if(this.GetComponent<SpriteRenderer>().flipX)
                    {
                        Instantiate(stabParticle, this.transform.position, Quaternion.Euler(0f, -90f, 0f));
                    }
                    else
                    {
                        Instantiate(stabParticle, this.transform.position, Quaternion.Euler(0f, 90f, 0f));
                    }
                    SwitchDimension();
                }
            }
        }
    }

    public void SwitchDimension()
    {
        GameObject[] ghostPlatform = GameObject.FindGameObjectsWithTag("GhostPlatform");
        
        if(ghostPlatform != null)
        {
            foreach(GameObject ghostGO in ghostPlatform)
            {
                GhostPlatform ghostPlat = ghostGO.GetComponent<GhostPlatform>();
                if(this.lifeOrDeath)
                {
                    if(ghostPlat.LifeOrDeathGhost)
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = false;
                        ghostGO.GetComponent<MeshRenderer>().material.color = ghostGO.GetComponent<GhostPlatform>().deathDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = false;
                        
                    }
                    else
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = true;
                        ghostGO.GetComponent<MeshRenderer>().material.color = ghostGO.GetComponent<GhostPlatform>().deathDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = true;
                        
                    }
                    
                }
                else
                {
                    if(ghostPlat.LifeOrDeathGhost)
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = true;
                        ghostGO.GetComponent<MeshRenderer>().material.color = ghostGO.GetComponent<GhostPlatform>().lifeDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = true;
                        
                    }
                    else
                    {
                        ghostGO.GetComponent<GhostPlatform>().enabled = false;
                        ghostGO.GetComponent<MeshRenderer>().material.color = ghostGO.GetComponent<GhostPlatform>().lifeDimColor;
                        ghostGO.GetComponent<GhostPlatform>().platformCollider.enabled = false;
                    }
                    
                    
                }
            }
        }
        if(this.lifeOrDeath)
        {
            lifeOrDeath = false;
            lifeDimension.SetActive(false);
            deathDimension.SetActive(true);
            PlayerHealth.instance.StartCoroutine(PlayerHealth.instance.TakeDamageInDeathWorld(1));
        }
        else
        {
            lifeOrDeath = true;
            lifeDimension.SetActive(true);
            deathDimension.SetActive(false);
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
            animator.SetTrigger("Jump");
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
        RaycastHit yes;
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, out yes, raycastDistance, whatIsGround);
        
        if(isGrounded && !hasJumped)
        {
            jumpCount = maxJumpCount;
        }
    }

    public void PlayerAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(lifeOrDeath)
            {
                if(canAttack)
                {
                    Collider[] enemiesInRange = new Collider[10];
                    int numbEnemies = Physics.OverlapSphereNonAlloc(hitBoxPoint.transform.position, attackRange, enemiesInRange, enemyLayer);
                    if(numbEnemies > 0)
                    {
                        foreach(Collider enemy in enemiesInRange)
                        {
                            if(enemy != null)
                            {
                                enemy.GetComponent<Enemy>().Hit(attackDamage);
                            }
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

            }
            else
            {
                SwitchDimension();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector3.down * raycastDistance);
        Gizmos.DrawWireSphere(hitBoxPointRight.transform.position, attackRange);
    }
}
