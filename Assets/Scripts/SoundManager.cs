using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Background")]
    [SerializeField] private List<AudioClip> soundBGs = new List<AudioClip>();
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private string nameBGMusic;

    [Header("SFX")]
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    [SerializeField] private int countAudioSource = 3;

    public Dictionary<string, List<AudioSource>> soundPool = new Dictionary<string, List<AudioSource>>();

    [SerializeField] private bool isReset;

    public void HanldeSetVolumeBGMusic(float value)
    {
        volume = value;
        backgroundSource.volume = volume;
    }

    public void Reset()
    {
        backgroundSource = GetComponent<AudioSource>();
        LoadSoundBG();
        LoadSoundFX();
        isReset =  true;
    }
    private void LoadSoundFX()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("sfx");

        if (clips == null || clips.Length == 0)
        {
            Debug.LogError("Không tìm thấy tệp âm thanh trong thư mục MusicBG.");
            return;
        }

        // Thêm các tệp âm thanh vào danh sách soundBGs
        foreach (AudioClip clip in clips)
        {
            sounds.Add(clip);
        }
    }
    private void LoadSoundBG()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("MusicBG");

        if (clips == null || clips.Length == 0)
        {
            Debug.LogError("Không tìm thấy tệp âm thanh trong thư mục MusicBG.");
            return;
        }

        // Thêm các tệp âm thanh vào danh sách soundBGs
        foreach (AudioClip clip in clips)
        {
            soundBGs.Add(clip);
        }

        CheckAndChooseMusicBackground();
    }
    private void CheckAndChooseMusicBackground()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        switch (currentScene)
        {
            case 0:
                CheckAudioClip("John-Bartmann-A-Kwela-Fella(chosic.com)");
               // nameBGMusic = "John-Bartmann-A-Kwela-Fella(chosic.com)";
                break;
            case 1:
                CheckAudioClip("FastFeelBananaPeel-320bit(chosic.com)");
                //nameBGMusic = "FastFeelBananaPeel-320bit(chosic.com)";
                break;

        }
    }
    private void CheckAudioClip(string nameSound)
    {
        foreach(AudioClip clip in soundBGs)
        {
            if(clip.name == nameSound)
            {
                SetSoundBG(clip);
            }
        }
    }
    private void SetSoundBG(AudioClip audioClip)
    {
        backgroundSource.clip = audioClip;
        backgroundSource.playOnAwake = true;
        backgroundSource.loop = true;
        //backgroundSource.volume = volume;
        HanldeSetVolumeBGMusic(volume);
    }
    public void HandleBGMusic( bool On)
    {
        if (On)
        {
            backgroundSource.Play();
        }
        else
        {
            backgroundSource.Stop();
        }
    }
    private void Start()
    {
        foreach (var sound in sounds)
        {
            AddAudioClipToAudioSource(sound.name);
        }
        if(!isReset)
        {
            backgroundSource = GetComponent<AudioSource>();
            LoadSoundBG();
            LoadSoundFX();
            Debug.LogWarning("Sound BG not reset!");
        }
        HandleBGMusic( On: true);
    }
   
    private void AddAudioClipToAudioSource(string soundName)
    {   
        // create 1 list AudioSource
        if (!soundPool.ContainsKey(soundName))
        {
            List<AudioSource > audioSources = new List<AudioSource>();
            for(int i = 0; i < countAudioSource; i++)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();
                audioSources.Add(audioSource);
            }
            soundPool.Add(soundName, audioSources); 
        }
        else
        {
            Debug.LogWarning("sound: " + soundName + " already exists!");
        }
    }
    public void HandlePlaySound(string soundName, float volume)
    {
        if (soundPool.ContainsKey(soundName))
        {
            List<AudioSource> audioSources = soundPool[soundName];
            AudioSource availableSource = audioSources.Find(audioSource => !audioSource.isPlaying);
            if (availableSource == null) return;
            availableSource.volume = volume;
            availableSource.PlayOneShot(GetSoundClip(soundName));
        }
        else
        {
            Debug.LogWarning("sound: " + soundName + " not exists!");
        }
    }
    public void HandleStopSound(string soundName)
    {
        if (soundPool.ContainsKey(soundName))
        {
            List<AudioSource> audioSources = soundPool[soundName];
            AudioSource availableSource = audioSources.Find(audioSource => audioSource.isPlaying);
            if (availableSource == null) return;
            availableSource.Stop(); ;
        }
        else
        {
            Debug.LogWarning("sound: " + soundName + " not exists!");
        }
    }

    private AudioClip GetSoundClip(string soundName)
    {
        return sounds.Find(sound => sound.name == soundName);
    }

}
