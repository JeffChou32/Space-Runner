using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEditor.Experimental.GraphView.Port;
public class shipscript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float acceleration = 30f;
    public float maxSpeed = 10f;
    public float friction = 0.98f;
    private float velocityX = 0f;
    public LogicScript logic;
    public static bool shipIsAlive = true;
    public static bool boost = false;        
    public static float boostDuration = 6f;            
    public static int multiplier = 1;       
    public int speedBoosts;
    public Animator animator;    
    private float startingYPosition;
    public Collider2D myCollider;    
    public static float boostTimer = 0f;
    public float maxYPosition = 5f;    
    public static bool waitingForReturn = false;       
    private float decrementTimer = 0f;
    public AudioClip explosionSound; 
    public float soundVolume = 1.0f;
    public AudioClip thrusterSound;
    public AudioClip thrusterSound2;
    public AudioClip thrusterSound3;   

    void Start()
    {        
        startingYPosition = transform.position.y;
        shipIsAlive = true;
        boost = false;
        boostTimer = 0;
        multiplier = 1;        
        transform.position = new Vector3(0, startingYPosition, 0);        

        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>(); 
        }

        if (logic == null)
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>(); 
        }        
    }

    void Update()
    {
        if (boostTimer > 0) //BOOST LOGIC
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                boostTimer = 0;
                boost = false;
                waitingForReturn = true;                
            }
        }

        if (!boost && multiplier > 1) //GRADUAL SLOWDOWN
        {
            decrementTimer += Time.deltaTime; 
            if (decrementTimer >= 0.5f)
            {
                decrementTimer = 0f; 
                multiplier -= 1; 
            }
        }

        float displacement = .25f * (multiplier-1); //SHIP DISPLACEMENT UNDER BOOST 
        if (waitingForReturn)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, startingYPosition, 1.5f * Time.deltaTime),
                transform.position.z);
        } else
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, Mathf.Min(startingYPosition + displacement, maxYPosition), 1.5f * Time.deltaTime),
                transform.position.z);
        }                          

        if (waitingForReturn && multiplier == 1) waitingForReturn = false;
        boost = boostTimer > 0;

        int defaultLayer = LayerMask.NameToLayer("Default"); //COLLISION LOGIC
        int brownAsteroidLayer = LayerMask.NameToLayer("BrownAsteroid");
        int blueAsteroidLayer = LayerMask.NameToLayer("BlueAsteroid");
        int whiteAsteroidLayer = LayerMask.NameToLayer("WhiteAsteroid");
        if (multiplier >= 2)
        {
            Physics2D.IgnoreLayerCollision(defaultLayer, brownAsteroidLayer, true);            
        }
        if (multiplier >= 3)
        {
            Physics2D.IgnoreLayerCollision(defaultLayer, blueAsteroidLayer, true);
            boostDuration = 3;
        }
        if (multiplier >= 4)
        {
            Physics2D.IgnoreLayerCollision(defaultLayer, whiteAsteroidLayer, true);
        }
        if (multiplier == 1 && Mathf.Approximately(transform.position.y, startingYPosition))
        {
            Physics2D.IgnoreLayerCollision(defaultLayer, brownAsteroidLayer, false);
            Physics2D.IgnoreLayerCollision(defaultLayer, blueAsteroidLayer, false);
            Physics2D.IgnoreLayerCollision(defaultLayer, whiteAsteroidLayer, false);
            boostDuration = 6;
        }        

        if (Input.GetKey(KeyCode.A) && shipIsAlive) //KEYBINDS
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
            velocityX -= acceleration * Time.deltaTime;
        }        
        else if (Input.GetKey(KeyCode.D) && shipIsAlive)
        {
            velocityX += acceleration * Time.deltaTime;
            animator.SetBool("left", false);
            animator.SetBool("right", true);
        }
        else
        {            
            velocityX *= friction;
            animator.SetBool("left", false);
            animator.SetBool("right", false);
        }        
        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);        
        myRigidBody.linearVelocity = new Vector2(velocityX, myRigidBody.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.W) && (speedBoosts > 0) && !waitingForReturn)
        {
            ActivateSpeedBoost();
        }
    }
    public void ActivateSpeedBoost()
    {
        boost = true;
        speedBoosts -= 1;
        speedhud.boosts -= 1;        
        boostTimer += boostDuration;
        multiplier += 1;
        if (multiplier == 2) AudioSource.PlayClipAtPoint(thrusterSound, transform.position, soundVolume);
        if (multiplier == 3) AudioSource.PlayClipAtPoint(thrusterSound2, transform.position, soundVolume);
        if (multiplier >= 4) AudioSource.PlayClipAtPoint(thrusterSound3, transform.position, soundVolume);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        animator.SetTrigger("death");
        myCollider.enabled = false;
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, soundVolume);
        Destroy(gameObject, 1f);
        logic.gameOver();
        shipIsAlive = false;        
    }
    public static void ActivateSpeedRamp()
    {
        boost = true;               
        boostTimer += boostDuration;
        multiplier += 1;
    }    
}
