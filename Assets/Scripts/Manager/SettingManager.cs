using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SettingManager : MonoBehaviour
{
    [SerializeField] protected GameObject TabSettings;
    [Header("Music Volume")]
    [SerializeField] protected Image barValueMusic;
    [SerializeField] protected TextMeshProUGUI NumberMusicVolumeText;

    [Header("Sfx Volume")]
    [SerializeField] protected Image barValueSfx;
    [SerializeField] protected TextMeshProUGUI NumberSfxVolumeText;

    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;

    [SerializeField] private float _currentVolumeMusic = 0.5f;
    [SerializeField] private float _currentVolumeSfx = 1f;


    public void HandleUpMusicVolume()
    {
        CurrentVolumeMusic += 0.1f;
    }

    public void HandleDownMusicVolume()
    {
        CurrentVolumeMusic -= 0.1f;
    }

    public void HandleUpSfxVolume()
    {
        CurrentSfxMusic += 0.1f;
    }

    public void HandleDownSfxVolume()
    {
        CurrentSfxMusic -= 0.1f;
    }

    public float CurrentVolumeMusic
    {
        get { return _currentVolumeMusic; }
        set
        {
            _currentVolumeMusic = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
            HandleTimerBarUI(barValueMusic, CurrentVolumeMusic, NumberMusicVolumeText);
        }
    }

    public float CurrentSfxMusic
    {
        get { return _currentVolumeSfx; }
        set
        {
            _currentVolumeSfx = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
            HandleTimerBarUI(barValueSfx, CurrentSfxMusic, NumberSfxVolumeText);
        }
    }
    private void Start()
    {   
        HandleTimerBarUI(barValueMusic, CurrentVolumeMusic, NumberMusicVolumeText);
        HandleTimerBarUI(barValueSfx, CurrentSfxMusic, NumberSfxVolumeText);
        if (!File.Exists(GetFilePath()))
        {
            HandleCreatAndSaveFile();
        }
        HandleLoadFile();
    }
    public virtual void HandleTimerBarUI(Image timerBar, float percentValue, TextMeshProUGUI NumberValuetext)
    {
        timerBar.fillAmount = percentValue;
        NumberValuetext.text  = Mathf.RoundToInt(percentValue * 100f).ToString();
        SoundManager.instance.HanldeSetVolumeBGMusic(percentValue);
    }
    protected void HandleLoadFile()
    {
        if (File.Exists(GetFilePath()))
        {
            try
            {
                string jsonMusicBGNumher = File.ReadAllText(GetFilePath());
                FormBGMusicNumber formBGMusicNumber = JsonUtility.FromJson<FormBGMusicNumber>(jsonMusicBGNumher);
                CurrentVolumeMusic = formBGMusicNumber._currentVolumeMusic;
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading coin data: " + e.Message);
            }
        }
    }
    private void HandleCreatAndSaveFile()
    {
        FormBGMusicNumber formBGMusicNumber = new FormBGMusicNumber { _currentVolumeMusic = CurrentVolumeMusic };
        string jsonMusicBGNumher = JsonUtility.ToJson(formBGMusicNumber, true);
        string filePath = GetFilePath();
        File.WriteAllText(filePath, jsonMusicBGNumher);
    }
    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "FormBGMusicNumber.Json");
    }
    public void HandleSaveTabSetting()
    {
        TabSettings.gameObject.SetActive(false);
        HandleCreatAndSaveFile();
    }
    public void HandleCancelTabSetting()
    {
        HandleLoadFile();
    }
}
