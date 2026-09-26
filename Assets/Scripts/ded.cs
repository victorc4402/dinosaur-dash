using UnityEngine;
using UnityEngine.SceneManagement;

public class ded : MonoBehaviour
{
    public string targetSceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        
    }
}