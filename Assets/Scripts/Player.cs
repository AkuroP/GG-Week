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
    [SerializeField]
    private int jumpCount;

    [Header("Dimension Switch")]
    [Header("True = Life; False = Death")]
    public GameObject lifeDimension;
    public GameObject deathDimension;
    [SerializeField]
    private bool lifeOrDeath;


    // Start is called before the first frame update
    
    private void Awake()
    {
        lifeOrDeath = true;
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove * playerSpeed, rb.velocity.y);
    }

    public void Switch(InputAction.CallbackContext context)
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

    public void PlayerMovement(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if(context.performed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
            jumpCount--;
        }

        if(context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }
}
