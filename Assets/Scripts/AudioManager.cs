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

    // playlist stuff
    private List<AudioClip> musicPlaylist = new List<AudioClip>();
    private int currentTrackIndex = 0;
    private bool isPlaylistActive = false;

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
        musicSource.loop = false;
    }

    void Update()
    {
        // play next when current clip stops
        if (isPlaylistActive && !musicSource.isPlaying)
        {
            PlayNextTrack();
        }
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
        Debug.Log("PlayMusic Triggered!");
        Sound m = sounds.Find(Sound => Sound.name == musicName);

        if (m == null || m.clips.Length == 0)
        {
        Debug.LogWarning($"Music: \"{musicName}\" not found! (AudioManager)");
        return;
        }

        // playlist list creation & shuffling
        musicPlaylist.Clear();
        musicPlaylist.AddRange(m.clips);
        ShufflePlaylist();

        // play playlist from beginning
        currentTrackIndex = 0;
        isPlaylistActive = true;

        PlayTrackAtIndex();
    }

    private void PlayNextTrack()
    {
        currentTrackIndex++;
        // reshuffle & replay playlist from beginning upon playlist completion
        if (currentTrackIndex >= musicPlaylist.Count)
        {
            ShufflePlaylist();
            currentTrackIndex = 0;
        }

        PlayTrackAtIndex();
    }

    private void PlayTrackAtIndex()
    {
        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.Play();
    }

    private void ShufflePlaylist()
    {
        // Fisher-Yates Shuffle: a standard way to randomly reorder a list
        // thank you very much, kind Large Language Model!
        for (int i = 0; i < musicPlaylist.Count; i++)
        {
            int randomIndex = Random.Range(i, musicPlaylist.Count);
            AudioClip temp = musicPlaylist[i];
            musicPlaylist[i] = musicPlaylist[randomIndex];
            musicPlaylist[randomIndex] = temp;
        }
    }
    public void StopMusic()
    {
        Debug.Log("stop music tiggered");
        isPlaylistActive = false;
        musicSource.Stop();
        // Destroy(gameObject);
    }
}
