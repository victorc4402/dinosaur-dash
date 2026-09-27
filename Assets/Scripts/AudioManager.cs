using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
// YES CHATGPT MY HERO
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    // public method so i can just call this from any other scrip
    public static AudioManager Instance
    {
        get;
        private set;
    }

    // so i can do the drag and drop sounds to add more sounds without add more gameobject in unity
    [System.Serializable]
    public class Sound
    {
        // pairs a name with a clip (for easy referencing within scripts)
        public string name;
        public AudioClip[] clips;
    }

    public List<Sound> sounds = new List<Sound>();
    private AudioSource sfxSource;
    private AudioSource musicSource;

    // singleton thingy
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // sfx source
        sfxSource = GetComponent<AudioSource>();

        // Music Source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public void PlaySoundEffect(string soundName)
    {
        // look through list sounds to find the clip with matchig name
        Sound s = sounds.Find(sound => sound.name == soundName);

        if (s == null || s.clips.Length == 0)
        {
            Debug.LogWarning($"Sound \"{soundName}\" not found! (AudioManager)");
            return;
        }

        // random index (from number of clips in the list) for random sound
        int randomIndex = Random.Range(0, s.clips.Length);
        AudioClip clipToPlay = s.clips[randomIndex];
        sfxSource.PlayOneShot(clipToPlay);
    }

public void PlayMusic(string musicName)
    {
        Sound m = sounds.Find(Sound => Sound.name == musicName);

        if (m == null || m.clips.Length == 0)
        {
        Debug.LogWarning($"Music: \"{musicName}\" not found! (AudioManager)");
        return;
        }

        // figure out how to random looping music
        // only takes the first music clip m.clips[0]

        AudioClip musicClip = m.clips[0];

        // dont play music every frame lmao
        if (musicSource.clip == musicClip)
        {
            return;    
        }
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
