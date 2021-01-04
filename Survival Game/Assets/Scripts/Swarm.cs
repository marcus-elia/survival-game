using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    private Transform playerTransform;
    private float speed;

    public GameObject enemyPrefab;
    public int numEnemies = 5;
    public float amplitudeAverage = 5f;
    public float amplitudeRadius = 1.5f;
    public float frequencyAverage = 0.25f;
    public float frequencyRadius = 0.05f;
    public float offsetAverage = Mathf.PI;
    public float offsetRadius = Mathf.PI;

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
    public void SpawnEnemies()
    {
        for(int i = 0; i < numEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.SetParent(this.gameObject.transform);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.SetAmplitude(amplitudeAverage, amplitudeRadius);
            enemy.SetFrequency(frequencyAverage, frequencyRadius);
            enemy.SetOffset(offsetAverage, offsetRadius);
            enemy.ComputeCoefficient();
            enemy.SetInitialPosition();
            enemy.SetPlayerTransform(playerTransform);
        }
    }
}
