using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        if (sceneName == "quit")
        {
            Debug.Log("game should quit!");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;   // for editor onyl, doesnt do anything if the game is built and played
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect("button_press");
            SceneManager.LoadScene(sceneName);
        }
    }
}