using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    private Transform playerTransform;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) > speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed);
        }
    }

    public void SetPlayerTransform(Transform input)
    {
        playerTransform = input;
    }
    public void SetPosition(Vector3 input)
    {
        transform.position = input;
    }
    public void SetSpeed(float inputSpeed)
    {
        speed = inputSpeed;
    }
}
