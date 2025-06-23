using UnityEngine;


public class SimplePlayerController : MonoBehaviour
{
    SpriteRenderer mySprite;
    Animator myAnim;
    Rigidbody2D myRB;

    float speed = 5f; 

    //the enum is a new dataType - it's essentially an int under the hood
    //
    public enum PlayerState
    {
        WALKING = 0,
        RUNNING = 1,
        AIRBORNE = 2,
        SWIMMING = 3,
        IDLE = 4
    }

    public PlayerState myState;
    public bool jumped = false;
    public Vector3 direction;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //here in update, I'm checking player inputs to see which state I should currently be in
        //to do this we first declare a placeholder PlayerState s
        PlayerState s = PlayerState.IDLE;     //Why not say myState = PlayerState.IDLE;                                 Question

        //listen for a jump request here
        if(Input.GetKeyUp(KeyCode.Space))
        {
            jumped = true;
            s = PlayerState.AIRBORNE;
        }
        //if I'm not jumping, the player direction input may matter, so check that second
        else if(direction != Vector3.zero)  //What does Vector3.zero do again?
        {
            if (myRB.linearVelocity.magnitude > 5f)  //What about this? what is magnitude?                                  Question
            {
                s = PlayerState.RUNNING;
            }
            else { s = PlayerState.WALKING; }  //I'm guessing this is just checking for the speed of the player and if it's above 5 it starts RUNNING Clarification
        }

        //finally, run the SetState function to update the player state
        SetState(s);
        myRB.AddForce(Direction() * speed * Time.fixedDeltaTime);

        myAnim.SetFloat("Speed", Mathf.Abs(myRB.linearVelocity.magnitude));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //first run any functions that may CHANGE the player state
        //things like jumping, getting stunned, any events that may change the player state during a physics loop
        if(jumped)   
        { Jump(10f); }   //How does this control our jump? what's the code to make the jump happen?

        //inside this switch statement we call movement code and functions or other functions
        //that we want to run EACH frame, depending on the current MODE of the playerState
        switch (myState)
        {
            
            //in this switch statement, I'm doing 2 things:
            //FIRST - I'm running the associated movement code for each mode
            //SECOND - I'm setting player sprite color as a DEBUG check, I'll turn that off when I finish the game

            case PlayerState.WALKING:
                myAnim.Play("Walking");
                Move(1f);
                mySprite.color = Color.green;
            break;

            case PlayerState.RUNNING:
                Move(3f);
                //code that runs when running goes here
                mySprite.color = Color.yellow;
            break;

            case PlayerState.AIRBORNE:
                //code that runs when airborne goes here
                Move(.1f);
                mySprite.color = Color.cyan;
            break;

            case PlayerState.SWIMMING:
                //code that runs when swimming goes here
                Swim(1f);
                mySprite.color = Color.blue;
            break;

            case PlayerState.IDLE:
                mySprite.color = Color.grey;
            break;
        }
        
    }

    public void Move(float s)
    {

    }

    public void Swim(float s)
    {
        //move specific to swimming goes here
    }

    public void Jump(float jumpForce)
    {
        //write code that executes a jump here

        //you can also call SetState from functions like Jump if you know you'll want to enter a new state
        //based off a given event occuring. You can call setState from other scripts when events (like collisions) occur as well
        //in this case, jump can run in fixedUpdate and may require a SetState before the next Update loop
        SetState(PlayerState.AIRBORNE);
    }

    Vector3 Direction()     //How can we implement this together with the velocity script?                                  Question
    {
        float h;
        float v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        direction = new Vector3(h, v, 0);
        return direction;
    }


    //the SetState function is used to confirm which state we are trying to enter
    //and either enter that state or reject the request if it was made in error
    public void SetState(PlayerState s)
    {
        //checks to see if we're already in the given state
        //if we are, end the function early, don't bother checking or changing state
        
        if (myState == s) { return; }   // do we need the {} for return?                                                    Question
        myState = s;

        switch(myState)
        {
            //you could also run this with a switch statement
        }

        //inside setState we can change variables that should only change ONCE when the state changes
        //things like the animation currently playing, or sound cues
        if(myState == PlayerState.IDLE)
        {
            myAnim.Play("Idle");
        }
        else if(myState == PlayerState.WALKING)
        {
            myAnim.Play("Walk");
        }
        else if(myState == PlayerState.RUNNING)
        {
            myAnim.Play("Run");
        }
    }
}