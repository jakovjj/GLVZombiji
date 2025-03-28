using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float TimeBetweenTheNextDash;
    public float dashSpeed;
    private float currentSpeed;

    public manager manager;

    public float speed;
    [HideInInspector] public float movX;
    [HideInInspector] public float movY;
    public Rigidbody2D rg2d;
    public BoxCollider2D playerHitbox;
    private Vector2 moveDir;
    public bool isDashing;
    public Animator animator;
    void Start()
    {
        movX = 0; movY = 0;
        currentSpeed = speed;
        manager = GameObject.Find("gameManager").GetComponent<manager>();
    }

    void Update()
    {
        ProcessInput();

        //Apliciraj micanje
        rg2d.linearVelocity = new Vector2(moveDir.x * currentSpeed, moveDir.y * currentSpeed);

        if (movX != 0f || movY != 0f) animator.SetBool("walk", true);
        else animator.SetBool("walk", false);

        
        //Debug.Log(movX+" "+movY);

        if (movY < 0f) animator.SetBool("back", false);
        if (movY > 0f) animator.SetBool("back", true);


    }

    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash(TimeBetweenTheNextDash));
        }


        //Micanje
        movX = Input.GetAxisRaw("Horizontal");
        movY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(movX, movY).normalized;
    }

    //not currently used
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
