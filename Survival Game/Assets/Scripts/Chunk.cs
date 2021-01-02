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

    public GameObject treePrefab;
    private VoxelTree tree;

    private Transform playerTransform;

    public static float treeProbability = 0.33f;

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
        if(tree)
        {
            tree.Enable();
        }
    }

    public void DisableChunk()
    {
        ground.SetActive(false);
        if(tree)
        {
            tree.Disable();
        }
    }

    // Setters
    public void InitializeGround()
    {
        ground = Instantiate(groundPrefab);
        ground.transform.position = transform.position;
        ground.transform.localScale = new Vector3(sideLength/10f, 1f, sideLength/10f);
    }
    public void SetChunkID(int inputID)
    {
        chunkID = inputID;
        chunkCoords = ChunkManager.chunkIDtoPoint2D(chunkID);
    }
    public void SetSideLength(int inputSideLength)
    {
        sideLength = inputSideLength;
        transform.position = new Vector3(sideLength * chunkCoords.x + sideLength / 2.0f, 0f, sideLength * chunkCoords.z + sideLength / 2.0f);
    }
    public void SetPlayerTransform(Transform input)
    {
        playerTransform = input;
    }
    public void CreateTree()
    {
        if(Random.Range(0,100) < treeProbability*100)
        {
            return;
        }
        tree = Instantiate(treePrefab).GetComponent<VoxelTree>();
        Vector3 randomPoint = getRandomPoint(sideLength / 5f);
        tree.SetPlayerTransform(playerTransform);
        tree.SetPosition(randomPoint);
        tree.Generate();
    }

    // Returns a random point on the plane of this chunk, that is not within buffer of the border
    private Vector3 getRandomPoint(float buffer)
    {
        float randomX = Random.Range(-sideLength / 2 + buffer, sideLength / 2 - buffer);
        float randomZ = Random.Range(-sideLength / 2 + buffer, sideLength / 2 - buffer);
        return transform.position + new Vector3(randomX, 0f, randomZ);
    }


}
