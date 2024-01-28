using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    List<Sound> sounds;
    Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
    public static SoundManager Instance;
    [SerializeField]
    AudioSource audioSource;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        foreach (var sound in sounds)
        {
            clipDictionary.Add(sound.Name, sound.Clip);
        }
    }

    public void PlaySound(string name)
    {
        if (clipDictionary.TryGetValue(name, out AudioClip audioClip))
        {
            audioSource.PlayOneShot(audioClip, 1);
        }
        else
        {
            Debug.LogError($"can not find sound with name {name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
