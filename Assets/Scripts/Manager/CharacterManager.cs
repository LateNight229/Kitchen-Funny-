using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CharacterManager : MonoBehaviour
{   
    public List<GameObject> skins = new List<GameObject>();

    private int currentCharacterNumber;
    void Start()
    {
        if (!File.Exists(GetFilePath()))
        {

            HandleCreatAndSaveFile();
        }
        HandleLoadFile();
        HandleSkinCharacter();
    }

   void HandleSkinCharacter()
   {
        for(int i = 0; i < skins.Count; i++)
        {
            if(currentCharacterNumber == i)
            {
                skins[i].SetActive(true);
            }
            else
            {
                skins[i].SetActive(false);
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
    private void  HandleCreatAndSaveFile()
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
}
