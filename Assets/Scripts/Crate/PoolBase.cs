using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
   protected virtual void Spawn(GameObject prefab, List<GameObject> list, Transform parent, int countPrefabs)
   {   
        for( int i = 0; i < countPrefabs; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.parent = parent;
            list.Add(obj);
        }
   }

    protected virtual GameObject HandleGetPrefab(List<GameObject> list)
    {
        for(int i = 0;i < list.Count;i++)
        {
            if (!list[i].activeInHierarchy)
            {
                list[i].SetActive(true);

                return list[i];
            }
        }
        return null;
    }
    protected virtual void HandleTakePrefab(List<GameObject> list, GameObject prefab, Transform parent)
    {
        prefab.SetActive(false);
        list.Add(prefab);
        prefab.transform.parent = parent;
    }

}
