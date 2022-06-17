using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IMovement : MonoBehaviour
{
    private Vector2 movementVector;
    public float speed;
    private Rigidbody2D rigidbody2d;
    private SkeletonAnimation sp;

    PlayerInput playerInput;

    [SerializeField]
    string scheme;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        MovePlayer();
        scheme = playerInput.currentControlScheme.ToString();
        Debug.Log(sp.Skeleton.Skin);
    }

    private void FlipWalkingAnimation()
    {
        if (movementVector.x < 0f)
        {
            ChangeAnimation("camminata");
            sp.Skeleton.ScaleX = -1;
            sp.Skeleton.SetSkin("verso sx");
        }
        else if (movementVector.x > 0f)
        {
            ChangeAnimation("camminata");
            sp.Skeleton.ScaleX = 1;
            sp.Skeleton.SetSkin("verso dx");
        }
        else if (movementVector.x == 0f)
        {
            ChangeAnimation("idle");
        }
    }

    public void ChangeAnimation(string animationName)
    {
        sp.AnimationName = animationName;
    }

    private void MovePlayer()
    {
        //Debug.Log("SPEED: t:" + Time.deltaTime + " s: " + speed * Time.deltaTime);
        
        Vector2 actualPosition = new Vector2(transform.position.x, transform.position.y);

        rigidbody2d.MovePosition(Vector2.Lerp(actualPosition, actualPosition + movementVector, speed));
    }

    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        FlipWalkingAnimation();
    }
}
