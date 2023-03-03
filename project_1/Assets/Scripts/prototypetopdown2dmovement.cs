using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prototypetopdown2dmovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
        transform.position += Vector3.up * vertical * speed * Time.deltaTime;
    }
}
