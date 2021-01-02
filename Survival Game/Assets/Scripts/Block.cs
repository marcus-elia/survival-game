using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType { Wood, Leaf };

public class Block : MonoBehaviour
{
    public GameObject woodBlockPrefab;
    public GameObject leafBlockPrefab;
    public GameObject genericBlock;

    private GameObject physicalBlock;
    public static float blockPrefabSize = 1f;

    public GameObject projectile;

    private float size;

    private BlockType blockType;
    private List<Block> children;
    private Block parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Setters called during construction
    public void SetPosition(Vector3 input)
    {
        transform.position = input;
    }
    public void SetSize(float inputSize)
    {
        size = inputSize;
    }
    public void SetParent(Block input)
    {
        parent = input;
    }
    public void SetBlockType(BlockType inputType)
    {
        this.blockType = inputType;
        if(inputType == BlockType.Wood)
        {
            physicalBlock = Instantiate(woodBlockPrefab);
        }
        else
        {
            physicalBlock = Instantiate(leafBlockPrefab);
        }
        physicalBlock.transform.position = transform.position;
        physicalBlock.transform.localScale = new Vector3(size / blockPrefabSize, size / blockPrefabSize, size / blockPrefabSize);
    }
    public void InitializeChildrenList()
    {
        children = new List<Block>();
    }

    public void Disable()
    {
        physicalBlock.SetActive(false);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Disable();
        }
    }
    public void Enable()
    {
        physicalBlock.SetActive(true);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Enable();
        }
    }

    // Child Block functions
    public Block CreateChild(Vector3 direction, BlockType type)
    {
        Block child = Instantiate(genericBlock).GetComponent<Block>();
        child.SetPosition(transform.position + direction * this.size);
        child.SetSize(this.size);
        child.SetBlockType(type);
        child.InitializeChildrenList();
        child.SetParent(this);
        children.Add(child);
        return child;
    }
    public void RemoveChild(Block child)
    {
        children.Remove(child);
    }
    public void Destroy()
    {
        for(int i = 0; i < children.Count; i++)
        {
            children[i].Destroy();
        }
        GameObject proj = Instantiate(projectile);
        proj.transform.position = transform.position;
        ProjectileManager.AddProjectile(proj);

        Destroy(physicalBlock);
        Destroy(gameObject);

    }
}
