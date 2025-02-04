using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawnBehaviour : MonoBehaviour
{
    private List<GameObject> roads;
    public GameObject roadPrefab;

    [SerializeField] private float SpawnEveryXUnitsValue;
    private float oldPos;
    private float unitsTraveled = 0;

    private void Update()
    {
        // Calculate how many units have been traveled since last frame
        float pos = transform.position.z;
        unitsTraveled += pos - oldPos;
        Debug.Log(unitsTraveled);

        // check if units traveled in total are big enough to spawn
        if (unitsTraveled >= SpawnEveryXUnitsValue)
        {
            unitsTraveled -= SpawnEveryXUnitsValue;
            Spawn();
        }

        // Save position for caluclation in next frame
        oldPos = pos;
    }

    private void Spawn()
    {
       Vector3 pos = transform.position;
        pos.x = 0;
        pos.y = 0;
       Instantiate(roadPrefab, pos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position.z;

        /*
        GameObject hello = Instantiate(roadPrefab);
        roads.Add(hello);
        
        roads.Remove(hello);
        Destroy(hello);
        */
    }

    
}
