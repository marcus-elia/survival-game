using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileManager : MonoBehaviour
{
    public static List<GameObject> droppedProjectiles = new List<GameObject>();
    public static List<GameObject> thrownProjectiles = new List<GameObject>();

    public static int maxNumDroppedProjectiles = 256;
    public static int maxNumThrownProjectiles = 128;

    public static int maxDistance = 300;
    public static int maxInventorySize = 256;

    public Transform playerTransform;
    public float playerRadius = 0.6f;
    public Transform cameraTransform;

    public float throwingForce = 10f;

    public GameObject projPrefab;

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
            if(DistanceXZ(droppedProjectiles[i].transform.position, playerTransform.position) < 2 * playerRadius)
            {
                AddProjectileToInventory();
                Destroy(droppedProjectiles[i]);
                droppedProjectiles.RemoveAt(i);
                i--;
            }
        }

        for(int i = 0; i < thrownProjectiles.Count; i++)
        {
            if(thrownProjectiles[i].transform.position.y < 1)
            {
                Destroy(thrownProjectiles[i]);
                thrownProjectiles.RemoveAt(i);
                i--;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(RemoveProjectileFromInventory())
            {
                Vector3 forceVector = cameraTransform.forward * throwingForce;
                ThrowProjectile(forceVector);
            }
        }
    }

    public static void AddDroppedProjectile(GameObject newProj)
    {
        droppedProjectiles.Add(newProj);
        if(droppedProjectiles.Count > maxNumDroppedProjectiles)
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

    public void ThrowProjectile(Vector3 forceVector)
    {
        GameObject newProj = Instantiate(projPrefab);
        newProj.transform.position = cameraTransform.position + cameraTransform.forward * playerRadius;
        newProj.GetComponent<Rigidbody>().velocity = forceVector;
        newProj.GetComponent<Rigidbody>().angularVelocity = RandomRotation();
        Debug.Log(forceVector);
        if (thrownProjectiles.Count > maxNumThrownProjectiles)
        {
            Destroy(thrownProjectiles[0]);
            thrownProjectiles.RemoveAt(0);
        }
        thrownProjectiles.Add(newProj);
    }

    public static float DistanceXZ(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    }

    public static Vector3 RandomRotation()
    {
        return new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
    }
}
