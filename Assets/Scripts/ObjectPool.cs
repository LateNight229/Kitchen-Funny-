using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : PoolBase
{
    public GameObject Prefab;
    public List<GameObject> listPrefabs;
    public int countPrefabs;
    public Transform parent;
    void Start()
    {
        Spawn(Prefab, listPrefabs, parent, countPrefabs);
    }

    public GameObject GetPrefab()
    {
        return HandleGetPrefab(listPrefabs);
    }

    public void ReturnPrefab(GameObject prefab)
    {
        if(Prefab != null)
        {
            HandleTakePrefab(listPrefabs, prefab, parent);
        }
    }
}
