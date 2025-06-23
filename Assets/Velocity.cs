
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float Speed;
    public float JumpPower;

    Vector2 vel;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = rb.linearVelocity;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }
        else
        {
            vel.x = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y = JumpPower;
        }
        rb.linearVelocity = vel;
    }
}
