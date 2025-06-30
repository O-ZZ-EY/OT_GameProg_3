using UnityEngine;

public class Movementv3 : MonoBehaviour
{
    public float speed;
    public float JumpForce;

    public Rigidbody2D rb;
    public GameObject ProjectilePrefab;
    public Transform LaunchOffset;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0, JumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown("Fire1"))
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }
}
