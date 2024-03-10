using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public List<GameObject> gameObjectLevels = new List<GameObject>();

    public Transform TabImageLevel;
    public Transform contents;

    private void Reset()
    {
        TabImageLevel = transform.Find("TabImageLevel");
        contents = transform.Find("Contents");

        foreach(Transform child in contents)
        {
            gameObjectLevels.Add(child.gameObject);
        }
    }
  
    public void HandlelevelLockIcon(int currentLevel)
    {
        for(int i = 0; i < gameObjectLevels.Count; i++)
        {
            if(i <= currentLevel)
            {
                HandleIconLock iconLock =  gameObjectLevels[i].GetComponent<HandleIconLock>();
                iconLock.HandleActiveIconLock(false);
            }
            else
            {
                HandleIconLock iconLock = gameObjectLevels[i].GetComponent<HandleIconLock>();
                iconLock.HandleActiveIconLock(true);

            }
        }
    }
    public void LoadLevel(int level)
    {
        ControlSwitchScene controlSwitchScene = GameObject.FindFirstObjectByType<ControlSwitchScene>();
        controlSwitchScene.HandleChangeScene(level);
    }
    public void OpenTabImageLevel()
    {
        TabImageLevel.gameObject.SetActive(true);
    }
    public void CloseTabImageLevel()
    {
        TabImageLevel.gameObject.SetActive(false);
    }
}
