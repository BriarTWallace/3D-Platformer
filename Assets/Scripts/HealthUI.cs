using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;        // Reference to the health slider UI
    public PlayerStats playerStats;    // Reference to the PlayerStats ScriptableObject

    private void Update()
    {
        if (playerStats != null && healthSlider != null)
        {
            // Update the slider value based on the player's current health
            healthSlider.value = playerStats.currentHealth;
        }
    }
}
