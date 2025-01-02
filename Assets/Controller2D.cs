using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// CharacterController2D is based upon the 2D Character Controller for Unity by Sharp Coder Blog
// URL: https://www.sharpcoderblog.com/blog/2d-platformer-character-controller
//
// Adapted by: Tom Corbett on 6/4/24

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Controller2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D mainCollider;

    [SerializeField]
    private Animator PlayerAnimator;

    private float maxSpeed = 4f;
    public float jumpHeight = 13f;
    public float gravityScale = 1f;
    public static Controller2D controller;
    public int lengthJump = 2;

    private float moveDirection;
    InputAction moveAction;
    InputAction jumpAction;
    private bool isGrounded = true;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundLayerMask; // this mask is inclusive, tests for objects on these layers

    public bool faceLeft = false;

    private Vector3 startingPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.currentState = GameState.Playing;
        // get components
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        
        // preset some values
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = gravityScale;

        // set some variables
        moveDirection = 0f;
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // set the move direction
        moveDirection = moveAction.ReadValue<Vector2>().x;


        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            PlayerAnimator.SetTrigger("jump");

        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (faceLeft == false) { 
            transform.Rotate(new Vector3(0, -180, 0));
            }
            faceLeft = true;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (faceLeft == true)
            {
                transform.Rotate(new Vector3(0, -180, 0));
            }
            faceLeft = false;
        }

        if (transform.position.y < -20f)
        {
            //lives = c.LivesLeft;
            //score = c.ScorePoint;
            //Debug.Log("Lives: " + lives);
           
            LevelManager.S.levelEvent_RELOAD_LEVEL();
            GameManager.Instance.currentState = GameState.Playing;
            GameManager.Instance.lostLife();

            //GameManager c = FindFirstObjectByType<GameManager>();

            Debug.Log("Lives After: " + (GameManager.Instance.LivesLeft));
            //c.ScorePoint = score;
            
        }

        Vector2 currentSpeed = rb.velocity;
        PlayerAnimator.SetFloat("speed", Mathf.Abs(currentSpeed.x));
        PlayerAnimator.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        // check our grounded status
        isGrounded = false;
        // look for colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, groundLayerMask);
        // was there a collider
        if (colliders.Length == lengthJump || colliders.Length == 3) 
        { 
            isGrounded = true;

        }
        //Debug.Log("JumpKey: " + colliders.Length);

        rb.velocity = new Vector2((moveDirection) * maxSpeed, rb.velocity.y);
    }
    
    private void OnDrawGizmos()
    {
        //show the grounded radius
        if (isGrounded)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }

    public void died() 
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private IEnumerator wait2()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnimator.ResetTrigger("isStunned");
    }

    public void stunnedPlayer()
    {
        Vector3 newPos = transform.position;
        var jump = transform.position.x + ((rb.velocity.x) * 2);
        newPos.x = Mathf.Lerp(transform.position.x, jump, 0.5f);

        transform.position = newPos;
        PlayerAnimator.SetTrigger("isStunned");
        StartCoroutine(wait2());
    }
}