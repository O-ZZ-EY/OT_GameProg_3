using UnityEngine;

public class simpleEnemy : MonoBehaviour
{
    public int pointValue;

    public float speed;
    public float distance;

    public bool grounded;
    public bool flip;

    


    public Collider2D Radius;
    public Collider2D Attack1;
    public Collider2D Attack2;

    public GameObject player;
    public bool playerDetected;

    public Rigidbody2D rb;

    Vector3 vel;
    Vector3 inputDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Vector3 scale = transform.localScale;


        if (playerDetected)
        {
            if (player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);

            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                transform.Translate(x: speed * Time.deltaTime * -1, y: 0, z: 0);
            }
        }


            transform.localScale = scale;


        //distance = Vector3.Distance(transform.position, player.transform.position);
        //Vector3 direction = player.transform.position - transform.position;

        if (playerDetected)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 dir = Direction();
        vel = rb.linearVelocity;

        dir *=speed;

        if (dir.x > 0)
        {
            if (vel.x < 0) { vel.x = 0; }
            if (dir.x < 0)
            {
                if (vel.x > 0) { vel.x = 0; }
            }


            vel.x = Mathf.Lerp(vel.x, dir.x, .3f);
            rb.linearVelocity = vel;
        }
    }
    
       
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Weapon")
            {
                //PlayerScript.instance.AddScore(pointValue);
                Destroy(gameObject);
            }
        }
      
    

        Vector3 Direction()
        {
            float h;
            float v;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            inputDir = new Vector3(h, v, 0);

            return inputDir;
        }

    


}
