using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen"); // Return to main menu
    }
}

