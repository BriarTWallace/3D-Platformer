using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene"); // Restart the game
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen"); // Return to main menu
    }
}
