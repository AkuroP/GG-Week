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

    [Header("Dimension Switch")]
    [Header("True = Life; False = Death")]
    public GameObject lifeDimension;
    public GameObject deathDimension;
    [SerializeField]
    public bool lifeOrDeath;


    // Start is called before the first frame update
    
    private void Awake()
    {
        lifeOrDeath = true;
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount;
        
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * playerSpeed, rb.velocity.y);
        GroundCheck();
        
    }

    private void OnDestroy()
    {

    }

    //interverti entre le monde des vivant et le monde des morts
    public void SwitchDimension(InputAction.CallbackContext context)
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

    //mouvement du joueur
    public void PlayerMovement(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
        if(context.performed)
        {
            if(horizontalMove < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector2.down * raycastDistance);
    }
}
