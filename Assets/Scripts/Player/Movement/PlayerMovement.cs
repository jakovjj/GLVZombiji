using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float TimeBetweenTheNextDash;
    public float dashSpeed;
    private float currentSpeed;
    public float speed;
    public Rigidbody2D rg2d;
    private Vector2 moveDir;
    public bool isDashing;
    public Animator animator;
    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        ProcessInput();

        //Apliciraj micanje
        rg2d.linearVelocity = new Vector2(moveDir.x * currentSpeed, moveDir.y * currentSpeed);
    }

    void ProcessInput()
    {
        //De�anje
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash(TimeBetweenTheNextDash));
        }
        //EndOfDe�anje


        //Micanje
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(movX, movY).normalized;
        //Micanje


        //Animations for future
        /*
        if (movX > 0f)
        {
            animator.SetBool("WalkRight", true);
            animator.SetBool("WalkLeft", false);
            if (movY == 0f)
            {
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkBack", false);
            }
        }
        if (movX < 0f)
        {
            animator.SetBool("WalkLeft", true);
            animator.SetBool("WalkRight", false);
            if (movY == 0f)
            {
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkBack", false);
            }
        }
        if (movY < 0f)
        {
            animator.SetBool("WalkBack", true);
            animator.SetBool("WalkUp", false);
            if (movX == 0f)
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
            }
        }
        if (movY > 0f)
        {
            animator.SetBool("WalkUp", true);
            animator.SetBool("WalkBack", false);
            if (movX == 0f)
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
            }
        }
        if (movY == 0f && movX == 0f)
        {
            animator.SetBool("WalkUp", false);
            animator.SetBool("WalkBack", false);
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkLeft", false);
        }
        //End of Animations
        */
    }

    //DE� Coroutine
    IEnumerator Dash(float timeBetweenTheNextDash)
    {
        isDashing = true;
        currentSpeed = dashSpeed;
        yield return new WaitForSeconds(0.1f);
        currentSpeed = speed;
        yield return new WaitForSeconds(timeBetweenTheNextDash);
        isDashing = false;
    }
}
