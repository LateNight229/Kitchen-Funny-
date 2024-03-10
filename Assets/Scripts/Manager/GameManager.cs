using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject lv1Manager;
    public GameObject loadScene;

    private bool _loadSceneDone;

    public bool LoadSceneDone { get { return _loadSceneDone; } set {  _loadSceneDone = value; } }

    void Awake()
    {
        HandleRunLoadScene();
        StartCoroutine(SpawnLevelManager());
    }
    IEnumerator SpawnLevelManager()
    {
        yield return new WaitUntil(() => LoadSceneDone);
        Instantiate(lv1Manager);

    }
    public void HandleRunLoadScene()
    {
        if (!loadScene.activeInHierarchy)
        {
            loadScene.SetActive(true);
        }
    }
}
