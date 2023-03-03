using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader : MonoBehaviour
{
    public GameObject leader;
    public float moveSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        
        //big ugly if statement, didnt know how to use switch cases
        if (hor > 0 && vert == 0) //right
        {

        }
        else if (hor < 0 && vert == 0) //left
        {

        }
        else if (hor == 0 && vert > 0) //up
        {

        }
        else if (hor == 0 && vert < 0 ) //down
        {

        }
        else if (hor > 0 && vert > 0) //top right
        {

        }
        else if (hor > 0 && vert < 0) //down right
        {

        }
        else if (hor < 0 && vert > 0) // up left
        {

        }
        else if (hor < 0 && vert < 0) //dpwm left
        {

        }
        
    }
}
