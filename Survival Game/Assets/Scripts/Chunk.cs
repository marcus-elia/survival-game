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
        tree.Enable();
    }

    public void DisableChunk()
    {
        ground.SetActive(false);
        tree.Disable();
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
    public void CreateTree()
    {
        tree = Instantiate(treePrefab).GetComponent<VoxelTree>();
        tree.SetPosition(transform.position);
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
