 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    // Moving sinusoidally relative to the swarm center in all 3 axes
    private Vector3 amplitude;
    private Vector3 frequency;
    private Vector3 offset;
    // To store calculations
    private Vector3 coefficient;

    private Vector3 location;

    private int frameNumber = 0;
    public int framesPerReduction = 50;
    public float scaleFactor = 0.9f;

    public float physicalRadius = 0.5f;
    public LayerMask projectileMask;

    private Transform playerTransform;

    public GameObject pointPrefab;

    /*public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    private AudioSource audioSource;

    private int framesSinceLastSound = 0;
    private int randomSoundWait = 1;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        frameNumber++;
        transform.localPosition = ComputePosition(frameNumber);

        // If this got hit by a projectile, destroy it
        CheckProjectiles();

        if(frameNumber % framesPerReduction == 0)
        {
            Reduce(scaleFactor);
        }

        transform.LookAt(playerTransform);

        if(Vector3.Distance(transform.position, playerTransform.position) < 3)
        {
            HealthManager.health--;
        }

        /*framesSinceLastSound++;
        if(framesSinceLastSound == randomSoundWait)
        {
            framesSinceLastSound = 0;
            PlayRandomSound();
            ChooseRandomSoundWait();
        }*/
    }

    public Vector3 MakeRandomVector(float average, float radius)
    {
        return new Vector3(Random.Range(average - radius, average + radius), Random.Range(average - radius, average + radius), Random.Range(average - radius, average + radius));
    }
    public void SetAmplitude(float average, float radius)
    {
        amplitude = MakeRandomVector(average, radius);
    }
    public void SetFrequency(float average, float radius)
    {
        frequency = MakeRandomVector(average, radius);
    }
    public void SetOffset(float average, float radius)
    {
        offset = MakeRandomVector(average, radius);
    }
    public void ComputeCoefficient()
    {
        coefficient = 2*Mathf.PI * frequency;
    }
    public void SetInitialPosition()
    {
        transform.localPosition = ComputePosition(0);
    }
    public void SetPlayerTransform(Transform input)
    {
        playerTransform = input;
    }
    /*public void InitializeAudioClips(AudioManager audioManager)
    {
        audioSource = GetComponent<AudioSource>();
        clip1 = audioManager.enemy1;
        clip2 = audioManager.enemy2;
        clip3 = audioManager.enemy3;
        clip4 = audioManager.enemy4;
        clip5 = audioManager.enemy5;
    }*/

    public static Vector3 MultiplyVectors(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public static Vector3 VectorSine(Vector3 v)
    {
        return new Vector3(Mathf.Sin(v.x), Mathf.Sin(v.y), Mathf.Sin(v.z));
    }

    private Vector3 ComputePosition(int t)
    {
        return MultiplyVectors(amplitude, VectorSine(t * coefficient + offset));
    }

    private void CheckProjectiles()
    {
        if(Physics.CheckSphere(transform.position, physicalRadius, projectileMask))
        {
            SpawnPoint();
            transform.parent.GetComponent<Swarm>().RemoveEnemy();
            Destroy(gameObject);
        }
    }
    private void SpawnPoint()
    {
        GameObject point = Instantiate(pointPrefab);
        point.GetComponent<Point>().SetPlayerTransform(playerTransform);
        point.transform.position = transform.position;
    }

    // Reduce the amplitude
    public void Reduce(float scaleFactor)
    {
        amplitude *= scaleFactor;
    }


    /*public void ChooseRandomSoundWait()
    {
        randomSoundWait = (int)Random.Range(100, 150);
    }
    private void PlayRandomSound()
    {
        float rand = Random.Range(0, 5);
        if(rand < 1)
        {
            audioSource.clip = clip1;
        }
        else if(rand < 2)
        {
            audioSource.clip = clip2;
        }
        else if (rand < 3)
        {
            audioSource.clip = clip3;
        }
        else if (rand < 4)
        {
            audioSource.clip = clip4;
        }
        else
        {
            audioSource.clip = clip5;
        }
        audioSource.Play();
    }*/
}
