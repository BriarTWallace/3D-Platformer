using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the next level or victory scene
            SceneManager.LoadScene("NextLevel"); // Replace with your level's name
        }
    }
}
