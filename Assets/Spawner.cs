using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public static Spawner instance;

    List<GameObject> prefabsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        instance = this;

        AddRandomPrefab();
        AddRandomPrefab();
        AddRandomPrefab();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity);
    }

    GameObject GetRandomPrefab()
    {
        AddRandomPrefab();
        GameObject r = prefabsList[0];
        prefabsList.RemoveAt(0);
        return r;
    }

    void AddRandomPrefab()
    {
        while (true)
        {
            GameObject g = prefabs[Random.Range(0, prefabs.Length)];
            if (prefabsList.Count == 0 || prefabsList[0] != g)
            {
                prefabsList.Add(g);
                break;
            }
        }
    }
}
