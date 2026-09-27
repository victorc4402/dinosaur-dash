using System.Collections.Generic;
using UnityEngine;
// YES CHATGPT MY HERO
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
        public AudioClip clip;
    }

    public List<Sound> sounds = new List<Sound>();
    private AudioSource audioSource;

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
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string soundName)
    {
        // look through list sounds to find the clip with matchig name
        Sound s = sounds.Find(sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogWarning($"Sound \"{soundName}\" not found in AudioManager!");
            return;
        }

        audioSource.PlayOneShot(s.clip);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
