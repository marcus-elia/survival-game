using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static List<GameObject> projectiles = new List<GameObject>();

    public static int maxNumProjectiles = 256;

    public static int maxDistance = 300;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddProjectile(GameObject newProj)
    {
        projectiles.Add(newProj);
        if(projectiles.Count > maxNumProjectiles)
        {
            Destroy(projectiles[0]);
            projectiles.RemoveAt(0);
        }
    }
}
