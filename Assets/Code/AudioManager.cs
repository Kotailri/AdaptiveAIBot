using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified version of Brackey's Audio Manager from Youtube
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0f, 1f)]
    public float pitch = 1f;

    [Header("Variance")]
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void ChangeVolume(float vol)
    {
        if (source != null)
            source.volume = vol * volume;
    }

    public void Play()
    {
        source.volume = GameConfig.soundVolume * volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField]
    List<Sound> sounds = new List<Sound>();

    private void Start()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            _go.transform.SetParent(this.gameObject.transform);
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning(_name + " not found in Audio Manager");
    }
}
