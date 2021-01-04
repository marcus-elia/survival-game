using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Transform playerTransform;
    public float pickupDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ProjectileManager.DistanceXZ(playerTransform.position, transform.position) < pickupDistance)
        {
            ScoreManager.score++;
            Destroy(this.gameObject);
        }
    }

    public void SetPlayerTransform(Transform input)
    {
        playerTransform = input;
    }
}
