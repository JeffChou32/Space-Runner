using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public static float boostDuration = 3f;            
    public static int multiplier = 1;    
    public Text numBoosts;
    public int speedBoosts;
    public Animator animator;    
    private float startingYPosition;
    public Collider2D myCollider;    
    public static float boostTimer = 0f;
    public float maxYPosition = 5f;
    //public static bool boostOver = false;
    private bool waitingForReturn = false;


    void Start()
    {        
        startingYPosition = transform.position.y;
        shipIsAlive = true;
        boost = false;
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
        
        transform.position = new Vector3(
        transform.position.x,
        Mathf.MoveTowards(transform.position.y, Mathf.Min(startingYPosition + multiplier - 1, maxYPosition), 2 * Time.deltaTime),
        transform.position.z
        );               
    
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                boostTimer = 0;                
                multiplier = 1;
                boost = false;
                waitingForReturn = true;
            }
        }
        if (waitingForReturn && Mathf.Approximately(transform.position.y, startingYPosition)) waitingForReturn = false;

        int defaultLayer = LayerMask.NameToLayer("Default");
        int brownAsteroidLayer = LayerMask.NameToLayer("BrownAsteroid");
        int blueAsteroidLayer = LayerMask.NameToLayer("BlueAsteroid");
        int whiteAsteroidLayer = LayerMask.NameToLayer("WhiteAsteroid");
        
        //Physics2D.IgnoreLayerCollision(defaultLayer, brownAsteroidLayer, multiplier >= 2);        
        //Physics2D.IgnoreLayerCollision(defaultLayer, blueAsteroidLayer, multiplier >= 3);        
        //Physics2D.IgnoreLayerCollision(defaultLayer, whiteAsteroidLayer, multiplier >= 4);
        if (multiplier >= 2) Physics2D.IgnoreLayerCollision(defaultLayer, brownAsteroidLayer, true);         
        if (multiplier >= 3) Physics2D.IgnoreLayerCollision(defaultLayer, blueAsteroidLayer, true);                 
        if (multiplier >= 4) Physics2D.IgnoreLayerCollision(defaultLayer, whiteAsteroidLayer, true);        
        if (Mathf.Approximately(transform.position.y, startingYPosition))
        {
            Physics2D.IgnoreLayerCollision(defaultLayer, brownAsteroidLayer, false);
            Physics2D.IgnoreLayerCollision(defaultLayer, blueAsteroidLayer, false);
            Physics2D.IgnoreLayerCollision(defaultLayer, whiteAsteroidLayer, false);
        }

        boost = boostTimer > 0;

        if (Input.GetKey(KeyCode.LeftArrow) && shipIsAlive)
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
            velocityX -= acceleration * Time.deltaTime;
        }        
        else if (Input.GetKey(KeyCode.RightArrow) && shipIsAlive)
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
        // Limit the speed to avoid infinite acceleration
        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);
        // Apply movement using Rigidbody2D
        myRigidBody.linearVelocity = new Vector2(velocityX, myRigidBody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && (speedBoosts > 0) && !waitingForReturn)
        {
            ActivateSpeedBoost();
        }
    }
    public void ActivateSpeedBoost()
    {
        boost = true;
        speedBoosts -= 1;
        numBoosts.text = speedBoosts.ToString();
        boostTimer += boostDuration;
        multiplier += 1;              
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        animator.SetTrigger("death");
        myCollider.enabled = false;
        Destroy(gameObject, 1f);
        logic.gameOver();
        shipIsAlive = false;        
    }
    
}
