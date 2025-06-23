using UnityEngine;

public class OldMovementv2 : MonoBehaviour
{
    Rigidbody2D RB;
    Vector3 direction;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RB.AddForce(Direction() * speed * Time.fixedDeltaTime);
    }


    Vector3 Direction()
    {
        float h;
        float v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        direction = new Vector3(h, v, 0);

        return direction;
    }




}
