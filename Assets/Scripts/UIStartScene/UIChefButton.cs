using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class UIChefButton : UIStartBase
{
    public Transform TabSelectChef;

     [Header("Lists Characters")]
    public List<GameObject> characters = new List<GameObject>();

    private int currentCharacterNumber;
    private bool isAllowed;
    protected override void Start()
    {   
        base.Start();
        if (!File.Exists(GetFilePath()))
        {

            HandleCreatAndSaveFile();
        }
        HandleLoadFile();
        HandleChooseCharacter();
    }
    private void Reset()
    {
        TabSelectChef = transform.Find("TabSelectChef");
    }
    public void AllowChooseCharacter()
    {
        isAllowed =  true;
    }
    public void UnAllowChooseCharacter()
    {
        isAllowed = false;
    }
    public void ClickRight()
    {   if (!isAllowed) { return; }
        Debug.Log("currentChracrerCount = " + characters.Count);
        if (currentCharacterNumber >= characters.Count - 1)
        {
            currentCharacterNumber = 0;
        }
        else
        {
            currentCharacterNumber += 1;
        }
        Debug.Log("currentChracrer = " + currentCharacterNumber);
        HandleChooseCharacter();
    }
    public void ClickLeft()
    {
        if (!isAllowed) { return; }
        if (currentCharacterNumber <= 0)
        {
            currentCharacterNumber = characters.Count - 1;
        }
        else
        {
            currentCharacterNumber -= 1;
        }
        Debug.Log("currentChracrer = " + currentCharacterNumber);
        HandleChooseCharacter();
    }

    protected void HandleChooseCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (i == currentCharacterNumber)
            {
                characters[i].gameObject.SetActive(true);
            }
            else
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }
    protected void HandleLoadFile()
    {
        if (File.Exists(GetFilePath()))
        {
            try
            {
                string jsonCharacterNumber = File.ReadAllText(GetFilePath());
                FormCharacterNumber formCharacterNumber = JsonUtility.FromJson<FormCharacterNumber>(jsonCharacterNumber);
                currentCharacterNumber = formCharacterNumber.characterNumber;
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading coin data: " + e.Message);
            }
        }
    }
    private void HandleCreatAndSaveFile()
    {
        FormCharacterNumber formCharacterNumber = new FormCharacterNumber { characterNumber = currentCharacterNumber };
        string jsonCharacterNumber = JsonUtility.ToJson(formCharacterNumber, true);
        string filePath = GetFilePath();
        File.WriteAllText(filePath, jsonCharacterNumber);
    }
    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "FormCharacterNumber.Json");
    }

    public void OpenTabSelectChef()
    {
        TabSelectChef.gameObject.SetActive(true);
    }
    public void CloseTabSelectChef()
    {
        TabSelectChef.gameObject.SetActive(false);
        HandleLoadFile();
        HandleChooseCharacter();
    }
    public void SaveTabSelectChef()
    {
        TabSelectChef.gameObject.SetActive(false);
        HandleCreatAndSaveFile();
    }
}
