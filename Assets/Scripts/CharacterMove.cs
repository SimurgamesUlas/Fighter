using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Animator anim;

    private Rigidbody2D rb;

    float moveHorizontal;
    bool facingRight = true;
    public float jumpForce;
    public bool isGrounded = false;
    public bool canDoubleJump;
    PlayerCombat playercombat;
    Enemy enemys;
    void Start()
    {
        moveSpeed = 5;
        moveHorizontal = Input.GetAxis("Horizontal");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playercombat = GetComponent<PlayerCombat>();
        enemys = GetComponent<Enemy>();
    }


    void Update()
    {
        charactermovement();
        chracterAnimation();
        characterAttack();
        characterRunAttack();
        characterJump();
    }

    void charactermovement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
    }

    void chracterAnimation()
    {

        if (moveHorizontal > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (moveHorizontal < 0)
        {
            anim.SetBool("isRunning", true);
        }

        if (!facingRight && moveHorizontal > 0)
        {
            chracterFlip();
        }
        if (facingRight && moveHorizontal < 0)
        {
            chracterFlip();
        }
    }

    void chracterFlip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void characterAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal == 0)
        {
            anim.SetTrigger("isAttack");
   
            playercombat.damageEnemy();
        }
    }

    void characterRunAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal > 0 || Input.GetKeyDown(KeyCode.E) && moveHorizontal < 0)
        {
            anim.SetTrigger("isRunAttack");
        
            playercombat.damageEnemy();
        }
    }

    void characterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                jumpForce = jumpForce / 1.5f;
                rb.velocity = Vector2.up * jumpForce;

                canDoubleJump = false;
                jumpForce = jumpForce * 1.5f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetBool("isJumping", false);
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }

    }
    void OnCollisionStay2D(Collision2D col)
    {
        anim.SetBool("isJumping", false);
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {


        if (col.collider.CompareTag("Grounded"))
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);

        }
    }
}

