using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Basic chunk properties
    private int chunkID;
    private Point2D chunkCoords; // top left
    private int sideLength;

    public GameObject groundPrefab;
    private GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableChunk()
    {
        ground.SetActive(true);
    }

    public void DisableChunk()
    {
        ground.SetActive(false);
    }

    // Setters
    public void InitializeGround()
    {
        ground = Instantiate(groundPrefab);
        ground.transform.position = transform.position;
        ground.transform.localScale = new Vector3(sideLength/10f, 1f, sideLength/10f);
    }
    public void setChunkID(int inputID)
    {
        chunkID = inputID;
        chunkCoords = ChunkManager.chunkIDtoPoint2D(chunkID);
    }
    public void setSideLength(int inputSideLength)
    {
        sideLength = inputSideLength;
        transform.position = new Vector3(sideLength * chunkCoords.x + sideLength / 2.0f, 0f, sideLength * chunkCoords.z + sideLength / 2.0f);
    }

    // Returns a random point on the plane of this chunk, that is not within buffer of the border
    private Vector3 getRandomPoint(float buffer)
    {
        float randomX = Random.Range(-sideLength / 2 + buffer, sideLength / 2 - buffer);
        float randomZ = Random.Range(-sideLength / 2 + buffer, sideLength / 2 - buffer);
        return transform.position + new Vector3(randomX, 0f, randomZ);
    }


}
