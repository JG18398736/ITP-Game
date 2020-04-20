using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Move speed variables
    public float moveSpeed;
    public Rigidbody2D myRigidbody;

    public bool canMove;

    //Jump variable
    public float jumpHeight;

    //Ground check variables 
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator myAnimator;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public QuizManager theQuizManager;

    public GameObject killBox;

    public float knockbackPower;
    public float knockbackLength;
    private float knockbackCounter;

    public float invincibilityLength;
    private float invincibilityCounter;

    public AudioSource jumpSound;
    public AudioSource hurtSound;

    public GameObject questionPanel;
    public Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        respawnPosition = transform.position;

        theLevelManager = FindObjectOfType<LevelManager>();

        canMove = true;

        theQuizManager = FindObjectOfType<QuizManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check the position of the player using OverlapCircle method
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Code for player movement
        //Kncokback
        if(knockbackCounter <= 0 && canMove)
        {
            //Moving forward
            if(Input.GetAxisRaw("Horizontal") > 0f)
            { 
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                //Right
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
                //Moving backwards
            else if(Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                //Left
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
                //Checking if the user is pressing a key, if not the unit will be stationary, no sliding after movement
            else{
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpHeight, 0f);
                jumpSound.Play();
            }

            
        }

        if(knockbackCounter > 0 )
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackPower, knockbackPower, 0f);
            }
            else
            {
               myRigidbody.velocity = new Vector3(knockbackPower, knockbackPower, 0f); 
            }
        }
        
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if(invincibilityCounter <= 0)
        {
            theLevelManager.invincible = false;
        }

        //Animations
        //To ensure speed is the same for both directions
        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnimator.SetBool("Grounded", isGrounded);

        if(myRigidbody.velocity.y < 0)
        {
            killBox.SetActive(true);
        }
        else
        {
            killBox.SetActive(false);
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            //gameObject.SetActive(false);
            //transform.position = respawnPosition;

            //Checks the kill plane to determine repawn
            theLevelManager.Respawn();


        }

        if(other.tag == "Checkpoint")
        {
            

        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    
}
