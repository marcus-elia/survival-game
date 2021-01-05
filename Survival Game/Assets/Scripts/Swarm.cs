using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Swarm : MonoBehaviour
{
    private Transform playerTransform;
    private float speed;
    private int curNumEnemies;

    public GameObject enemyPrefab;
    public int minNumEnemies = 4;
    public int maxNumEnemies = 10;
    public float amplitudeAverage = 5f;
    public float amplitudeRadius = 1.5f;
    public float frequencyAverage = 0.25f;
    public float frequencyRadius = 0.05f;
    public float offsetAverage = Mathf.PI;
    public float offsetRadius = Mathf.PI;

    public AudioSource audioSource;
    public AudioClip swarmMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
    public void InitializeAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = swarmMusic;
        audioSource.Play();
    }
    public void SpawnEnemies()
    {
        curNumEnemies = Random.Range(minNumEnemies, maxNumEnemies);
        for(int i = 0; i < curNumEnemies; i++)
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
            //enemy.InitializeAudioClips(audioManager);
        }
    }

    public void RemoveEnemy()
    {
        curNumEnemies--;
        audioSource.volume *= 0.75f;
        if(curNumEnemies == 0)
        {
            audioSource.Stop();
            Destroy(this.gameObject);
        }
    }
}
