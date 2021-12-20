using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(0, 20f);
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform Bow;
    float GravityAtStart;

    Vector2 moveInput;
    Animator Anim;
    Rigidbody2D myrigidbody;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;
    bool isAlive = true;
    bool isJumping;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        GravityAtStart = myrigidbody.gravityScale;
    }


    void Update()
    {
        if(!isAlive){ return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if(!isAlive){ return;}
        moveInput = value.Get<Vector2>();
        Anim.SetBool("Shooting",false);
        
    }

    void OnJump(InputValue value)
    {
        if(!isAlive){ return;}
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
        if(value.isPressed)
        {
            myrigidbody.velocity += new Vector2(0,jumpSpeed);
            Anim.SetBool("isJumping",true);            
        }
    }
    void OnLandingEvent()
    {
        if(myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            Anim.SetBool("isJumping",false);
        }
    }

    void OnFire(InputValue value)
    {
        if(!isAlive){ return;}
        if(value.isPressed)
        {
            Anim.SetBool("Shooting",true);
        }
    }

    void IntantiateArrowEvents()
    {
        Instantiate(Arrow,Bow.position,transform.rotation);
        Anim.SetBool("Shooting",false);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        Anim.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Anim.SetBool("isClimbing",false);
            myrigidbody.gravityScale = GravityAtStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(myrigidbody.velocity.x,moveInput.y * climbSpeed);
        myrigidbody.velocity = climbVelocity;
        bool playerHasVelocitySpeed = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;
        Anim.SetBool("isClimbing",playerHasVelocitySpeed);
        Anim.SetBool("isJumping",false);
        myrigidbody.gravityScale = 0;   

    }

    void Die()
    {
        if(myBody.IsTouchingLayers(LayerMask.GetMask("Enemy","Harzards")))
        {
            isAlive = false;
            Anim.SetTrigger("Dying");
            myrigidbody.velocity = deathKick;
            Destroy(gameObject,2f);
        }
    }
    
}
