using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator ani;
    CapsuleCollider2D myCapsule;
    BoxCollider2D myFeet;
    [SerializeField] float playerSpeed=8f;
    [SerializeField] float playerSpeedBasic = 8f;
    [SerializeField] float playerSpeedSlowDown = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20, 20);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityStart = 5f;
    bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        myCapsule=GetComponent<CapsuleCollider2D>();
        gravityStart = rb.gravityScale;
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return;  }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    public void SlowDown()
    {
        playerSpeed = playerSpeedSlowDown;
    }
    public void EscapeSlowDown()
    {
        playerSpeed = playerSpeedBasic;
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);

    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {            
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*playerSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        ani.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            ani.SetBool("isClimbing", false);
            rb.gravityScale = gravityStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y*climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;
        bool climbHasSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        ani.SetBool("isClimbing", climbHasSpeed);
    }
    void Die()
    {
        if (myCapsule.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            ani.SetTrigger("Die");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
