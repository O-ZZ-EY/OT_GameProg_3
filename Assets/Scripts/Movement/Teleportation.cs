using UnityEngine;

public class Teleportation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(.02f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(.02f, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, .02f, 0);
        }
    }
}
