using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTree : MonoBehaviour
{
    private GameObject treeBaseGO;
    private Block treeBase;
    private float blockSize = 1f;

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
        treeBaseGO = Instantiate(genericBlock);
        treeBase = treeBaseGO.GetComponent<Block>();
        treeBase.SetPosition(transform.position + Vector3.up*blockSize/2);
        treeBase.SetSize(blockSize);
        treeBase.SetBlockType(BlockType.Wood);
        treeBase.InitializeChildrenList();

        Block block2 = treeBase.CreateChild(Vector3.up, BlockType.Wood);
        Block block3 = block2.CreateChild(Vector3.up, BlockType.Wood);
        Block block4 = block3.CreateChild(Vector3.up, BlockType.Wood);
        Block leaf1 = block4.CreateChild(Vector3.left, BlockType.Leaf);
        Block leaf2 = block4.CreateChild(Vector3.right, BlockType.Leaf);
        Block leaf3 = block4.CreateChild(Vector3.forward, BlockType.Leaf);
        Block leaf4 = block4.CreateChild(Vector3.back, BlockType.Leaf);
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
