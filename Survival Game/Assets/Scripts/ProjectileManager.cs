using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileManager : MonoBehaviour
{
    public static List<GameObject> droppedProjectiles = new List<GameObject>();

    public static int maxNumProjectiles = 256;

    public static int maxDistance = 300;
    public static int maxInventorySize = 256;

    public Transform playerTransform;
    public float playerRadius = 0.6f;

    private static int numPlayerProjectiles = 0;

    public TextMeshProUGUI numProjText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        numProjText.text = numPlayerProjectiles.ToString();

        for(int i = 0; i < droppedProjectiles.Count; i++)
        {
            if(DistanceXZ(droppedProjectiles[i].transform.position, playerTransform.position) < 2*playerRadius)
            {
                AddProjectileToInventory();
                Destroy(droppedProjectiles[i]);
                droppedProjectiles.RemoveAt(i);
                i--;
            }
        }
    }

    public static void AddProjectile(GameObject newProj)
    {
        droppedProjectiles.Add(newProj);
        if(droppedProjectiles.Count > maxNumProjectiles)
        {
            Destroy(droppedProjectiles[0]);
            droppedProjectiles.RemoveAt(0);
        }
    }

    public static bool AddProjectileToInventory()
    {
        if (numPlayerProjectiles < maxInventorySize)
        {
            numPlayerProjectiles++;
            return true;
        }
        return false;
    }

    public static bool RemoveProjectileFromInventory()
    {
        if(numPlayerProjectiles > 0)
        {
            numPlayerProjectiles--;
            return true;
        }
        return false;
    }

    public static float DistanceXZ(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    }
}
