using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float TimeBetweenTheNextDash;
    public float dashSpeed;
    private float currentSpeed;

    [HideInInspector] public manager manager;

    public static float PlayerSpeed;
    [HideInInspector] public float movX;
    [HideInInspector] public float movY;
    public Rigidbody2D rg2d;
    public BoxCollider2D playerHitbox;
    private Vector2 moveDir;
    public bool isDashing;
    public Animator animator;
    public AudioSource coinCollect;
    float speedSlowMultiplier;

    public FixedJoystick joystick;
    void Start()
    {
        speedSlowMultiplier = 1;
        movX = 0; movY = 0;
        currentSpeed = PlayerSpeed;
        manager = GameObject.Find("gameManager").GetComponent<manager>();
    }

    void Update()
    {
        if(gameObject.GetComponent<IsFrozen>() != null)
        {
            speedSlowMultiplier=gameObject.GetComponent<IsFrozen>().freezePercentage;
        }
        else
        {
            speedSlowMultiplier = 1;
        }
        if (manager.play == true) ProcessInput();
        else {movX = 0f; movY = 0f;}

        //Apliciraj micanje
        //rg2d.linearVelocity = new Vector2(moveDir.x * PlayerSpeed * speedSlowMultiplier, moveDir.y * PlayerSpeed*speedSlowMultiplier);
        rg2d.linearVelocity = new Vector2(joystick.Horizontal * PlayerSpeed * speedSlowMultiplier, joystick.Vertical * PlayerSpeed*speedSlowMultiplier);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f) animator.SetBool("walk", true);
        else animator.SetBool("walk", false);
        //Debug.Log(movX+" "+movY);

        if (joystick.Vertical < 0f) animator.SetBool("back", false);
        if (joystick.Vertical > 0f) animator.SetBool("back", true);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Coin"){
            coinCollect.Play();
            Destroy(collision.gameObject);
            manager.coin++;
        }
    }
    void ProcessInput()
    {   
        if (manager.play == false) {
            movX = 0f; movY = 0f; return;
        }

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
        currentSpeed = PlayerSpeed;
        yield return new WaitForSeconds(timeBetweenTheNextDash);
        isDashing = false;
    }
}
