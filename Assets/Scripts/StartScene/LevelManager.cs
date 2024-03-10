using System;
using UnityEngine;
using System.IO;
public class LevelManager : MonoBehaviour
{
    public int CurrentLevel
    {
        get { return _level; }
        set { _level = value; }
    }

    private int _level;
    public ControlSwitchScene _scene;
    private UILevel uiLevel;

    public void HandleContinueGame()
    {   
        Debug.Log("Level = " +  _level + ", " + CurrentLevel);
        _scene.HandleChangeScene(_level + 1);
    }

    void Start()
    {
        if (!File.Exists(GetFilePath()))
        {
            HandleCreatAndSaveFile();
            uiLevel.HandlelevelLockIcon(CurrentLevel);
        }
        //HandleLoadFile();
        HandleCreatAndSaveFile();
        LoadComponent();
        uiLevel.HandlelevelLockIcon(CurrentLevel);
    }
    private void LoadComponent()
    {
        uiLevel = GameObject.FindFirstObjectByType<UILevel>();
        _scene = FindFirstObjectByType<ControlSwitchScene>();
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            HandleLoadFile();
            uiLevel.HandlelevelLockIcon(CurrentLevel);
        }
    }
    protected string GetFilePath()
    {
        Debug.Log("Path to data.json: " + Path.Combine(Application.persistentDataPath, "FormLevel.Json"));
        return Path.Combine(Application.persistentDataPath, "FormLevel.Json");
    }

    protected void HandleCreatAndSaveFile()
    {
        FormLevel formLevel = new FormLevel { Level = CurrentLevel};
        string jsonLevel = JsonUtility.ToJson(formLevel, true);
        string filePath = GetFilePath();
        File.WriteAllText(filePath, jsonLevel);
    }

    protected void HandleLoadFile()
    {
        if(File.Exists(GetFilePath()))
        {
            try
            {
                string jsonLevel = File.ReadAllText(GetFilePath());
                FormLevel formLevel = JsonUtility.FromJson<FormLevel>(jsonLevel);
                CurrentLevel = formLevel.Level;
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading coin data: " + e.Message);
            }
        }
    }

}
