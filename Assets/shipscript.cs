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
    public float boostDuration = 3f;            
    public static int multiplier = 1;    
    public Text numBoosts;
    public int speedBoosts;
    public Animator animator;
    //private float initialYPosition;
    private float startingYPosition;
    public Collider2D myCollider;
    //int boostsActive;
    private List<float> boostTimers = new List<float>();    

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
            Mathf.MoveTowards(transform.position.y, startingYPosition + multiplier-1, 2 * Time.deltaTime),
            transform.position.z
        );     
        for (int i = boostTimers.Count - 1; i >= 0; i--)
        {
            boostTimers[i] -= Time.deltaTime;            
            if (boostTimers[i] <= 0)
            {
                boostTimers.RemoveAt(i);
                multiplier -= 1;
                //boostsActive -= 1;
            }
        }
        //myCollider.enabled = boostTimers.Count == 0 && Mathf.Approximately(transform.position.y, startingYPosition);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Asteroid Layer"), !Mathf.Approximately(transform.position.y, startingYPosition));
        boost = !(boostTimers.Count == 0);        

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

        if (Input.GetKeyDown(KeyCode.UpArrow) && (speedBoosts > 0))
        {
            ActivateSpeedBoost();
        }
    }
    public void ActivateSpeedBoost()
    {
        boost = true;
        speedBoosts -= 1;
        numBoosts.text = speedBoosts.ToString();
        boostTimers.Add(boostDuration);
        multiplier += 1;        
        //boostsActive += 1;
        //initialYPosition = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        animator.SetTrigger("death");        
        logic.gameOver();
        shipIsAlive = false;        
    }   
}
