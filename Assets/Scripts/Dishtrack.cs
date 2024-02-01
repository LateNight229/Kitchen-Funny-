using System.Collections.Generic;
using UnityEngine;

public class Dishtrack : MonoBehaviour
{
    private ObjectPool platePool;
    public List<GameObject> plateLists = new List<GameObject>();
    void Start()
    {
        platePool = GetComponentInParent<ObjectPool>();
        PlateCounter.HandleEventTakePlate += OffPlate;
        PlateCounter.HandleEventGetPlate += OnPlate;
    }
    private void OnDestroy()
    {
        PlateCounter.HandleEventTakePlate -= OffPlate;
        PlateCounter.HandleEventGetPlate -= OnPlate;
    }
    private void Reset()
    {
        LoadPlate();
    }
    void LoadPlate()
    {
        var GameObj = GameObject.Find("dishrack");
        if (GameObj != null)
        {
            foreach (Transform childTransform in GameObj.transform)
            {
                GameObject plateObj = childTransform.gameObject;
                plateLists.Add(plateObj);

            }
        }
    }

    public void OffPlate()
    {
        for(int i = 0; i < plateLists.Count; i++)
        {
            if (plateLists[i].activeSelf)
            {
                plateLists[i].SetActive(false);
                return;
            }
        }
    }
    public void OnPlate()
    {
        for (int i = 0; i < plateLists.Count; i++)
        {
            if (!plateLists[i].activeSelf)
            {
                plateLists[i].SetActive(true);
                return;
            }
        }
    }
}
