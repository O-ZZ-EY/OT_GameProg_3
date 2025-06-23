using UnityEngine;

public class Momentum : MonoBehaviour
{
    Rigidbody2D RB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RB.AddForce(new Vector2(10, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RB.AddForce(new Vector2(-10, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RB.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
        
    }
}
