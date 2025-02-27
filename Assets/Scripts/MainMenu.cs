using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Loads the main game scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Quits the game (only works in a built version)
    }
}
