using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SkeletonAnimation sp;

    private Vector2 movementVector;
    
    [SerializeField] 
    float speed = 5.0f;
    
    [SerializeField] 
    float jumpHeight = 15.0f;

    public bool isGrounded;
    public Transform groundCheck;
    public float radiusGroundCheck = 0.2f;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SkeletonAnimation>();
    }
    private void Start()
    {
        
    }

    private void OnMove(InputValue inputMove)
    {
        movementVector = inputMove.Get<Vector2>();
    }
    private void OnJump()
    {
        if(isGrounded)
        {    
            rb.velocity = Vector2.up * jumpHeight;
        }
    }

    void Update()
    {
        MovePlayer();
        FlipPlayer();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,radiusGroundCheck,groundLayer);
    }

    void MovePlayer()
    {   
        rb.velocity = new Vector2(movementVector.x * speed, rb.velocity.y);
    }

    void FlipPlayer()
    {
        if (rb.velocity.x<-0.1f)
        {
            sp.Skeleton.ScaleX = -1;
        }
        if (rb.velocity.x > 0.1f)
        {
            sp.Skeleton.ScaleX = 1;
        }
    }
}
