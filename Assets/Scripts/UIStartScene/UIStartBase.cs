using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIStartBase : MonoBehaviour
{
    public static event Action<GameObject> clickButton;

    [Header("Button")]
    public Button myButton;
    public GameObject tabContents;


    private UIStartManager uiStartManager;
    public virtual void OnClick()
    {
        clickButton?.Invoke(tabContents);
    }

    protected virtual void Start()
    {
        uiStartManager = GameObject.FindFirstObjectByType<UIStartManager>();
        clickButton += OpenTabButton;
    }
    protected virtual void OnDestroy()
    {
        clickButton -= OpenTabButton;
    }
  
    protected virtual void OpenTabButton(GameObject obj)
    {   
        Image image = obj.GetComponent<Image>();
        uiStartManager.HandleTabButtonOnClick(image);
    }

    protected virtual string GetFilePath(string nameFile)
    {
        return Path.Combine(Application.persistentDataPath, nameFile);
    }
}
