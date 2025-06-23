using UnityEngine;

public class VelocityEasing3D : MonoBehaviour
{
    public float Speed;
    public float JumpPower;
    Rigidbody rb;
    public bool grounded;
    Vector3 vel; //current player velocity
    Vector3 inputDir; //current input direction
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //find our input axes direction
        Vector3 dir = Direction();
        //storing current velocity
        vel = rb.linearVelocity;

        //dir = (-1,-1,0) to (1,1,0)
        //speed = an arbitrary float from 0 to infinity
        //let's say speed = 3
        //when you multiply a vector by a float, it applies the multiplication to each of the individual vector components
        // dir * speed = (-3,-3,0) to (3,3,0)
        dir *= Speed;

        //before applying the LERP, let's check to see if the player
        //is changing directions from Left>Right or Right>Left
        if (dir.x > 0) //if Dir > 0 and vel < 0, the player is requesting Right but the myRB is moving Left
        {
            if (vel.x < 0) { vel.x = 0; } //instead of LERP from -Speed to Speed, this halves the distance so we only need to LERP from 0 to Speed
        }
        if (dir.x < 0)
        {
            if (vel.x > 0) { vel.x = 0; } //instead of LERP from Speed to -Speed, this halves the distance so we only need to LERP from 0 to -Speed
        }

        //LERP stands for linear interpolation
        //      A (0%) -------------float time (percentage) ----------------------------- (100%)B 
        //LERP takes two values and finds the value that is TIME% between the two
        //in other words, if t = .40f == 40% , then find a value that is 40% from A TOWARDS B
        vel.x = Mathf.Lerp(vel.x, dir.x, .3f);


        if (Input.GetKey(KeyCode.Space) && grounded)   //Why does my character keep floating up?
        {
            vel.y = JumpPower;
            Debug.Log("jumping");
        }



        rb.linearVelocity = vel;
        rb.AddTorque(vel.normalized);
    }

    Vector3 Direction()
    {
        float h;
        float v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        inputDir = new Vector3(h, 0, v);

        return inputDir;
    }
}