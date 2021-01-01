using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTree : MonoBehaviour
{
    private GameObject treeBaseGO;
    private Block treeBase;
    private float blockSize = 1f;
    private int treeSize;

    public GameObject genericBlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector3 inputPosition)
    {
        transform.position = inputPosition;
    }
    public void Generate()
    {
        treeSize = Random.Range(2,6);
        treeBaseGO = Instantiate(genericBlock);
        treeBase = treeBaseGO.GetComponent<Block>();
        treeBase.SetPosition(transform.position + Vector3.up*blockSize/2);
        treeBase.SetSize(blockSize);
        treeBase.SetBlockType(BlockType.Wood);
        treeBase.InitializeChildrenList();

        Block block2 = treeBase.CreateChild(Vector3.up, BlockType.Wood);
        Block block3 = block2.CreateChild(Vector3.up, BlockType.Wood);
        Block curWoodBlock = block3;
        for (int i = treeSize; i > 0; i--)
        {
            curWoodBlock = curWoodBlock.CreateChild(Vector3.up, BlockType.Wood);
            this.AddLayer(curWoodBlock, i);
            curWoodBlock = curWoodBlock.CreateChild(Vector3.up, BlockType.Wood);
        }
        curWoodBlock.CreateChild(Vector3.up, BlockType.Leaf);
    }

    // Creates a layer of leaves around a trunk block
    public void AddLayer(Block centerBlock, int layerSize)
    {
        Block forward = centerBlock;
        Block back = centerBlock;
        Block left = centerBlock;
        Block right = centerBlock;
        for(int i = 0; i < layerSize; i++)
        {
            // Move out one in each of the four directions
            forward = forward.CreateChild(Vector3.forward, BlockType.Leaf);
            back = back.CreateChild(Vector3.back, BlockType.Leaf);
            left = left.CreateChild(Vector3.left, BlockType.Leaf);
            right = right.CreateChild(Vector3.right, BlockType.Leaf);

            Block forwardRight = forward.CreateChild(Vector3.right, BlockType.Leaf);
            Block rightBack = right.CreateChild(Vector3.back, BlockType.Leaf);
            Block backLeft = back.CreateChild(Vector3.left, BlockType.Leaf);
            Block leftForward = left.CreateChild(Vector3.forward, BlockType.Leaf);
            // Then iterate to the right of the direction
            for (int j = 0; j < layerSize - 1; j++)
            {
                forwardRight = forwardRight.CreateChild(Vector3.right, BlockType.Leaf);
                rightBack = rightBack.CreateChild(Vector3.back, BlockType.Leaf);
                backLeft = backLeft.CreateChild(Vector3.left, BlockType.Leaf);
                leftForward = leftForward.CreateChild(Vector3.forward, BlockType.Leaf);
            }
        }

    }

    public void Disable()
    {
        treeBase.Disable();
    }
    public void Enable()
    {
        treeBase.Enable();
    }
}
