 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask enemyLayer;
    public float playerRadius = 2f;
    public float enemyRadius = 2f;

    public Slider healthBar;

    public static int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, playerRadius + enemyRadius, enemyLayer))
        {
            health--;
        }
        healthBar.value = health;
    }


}
