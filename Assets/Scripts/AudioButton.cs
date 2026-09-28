using UnityEngine;

public class AudioButton : MonoBehaviour
{
    // thanks again, Large Language Model
    // string: whatever i put in the "Name" field for the sound playlist object in AudioManager in unity interface
    public string musicPlaylistToStart;
    public void ButtonPlayMusic()
    {
        if (!string.IsNullOrEmpty(musicPlaylistToStart)) // AudioManager object disconnects from the button due to the singleton thing
        {                                                  // when the thing uhh idk this just fixes it 
            AudioManager.Instance.PlayMusic(musicPlaylistToStart);
            Debug.Log("button successfuly played music");
        }
    }
}
