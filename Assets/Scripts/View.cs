using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public PlayerStats playerStats; // Reference to the ScriptableObject
    public Slider healthSlider; // Reference to the UI slider

    private void Start()
    {
        // Initialize health bar
        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.currentHealth;
    }

    private void Update()
    {
        // Update health bar based on player stats
        healthSlider.value = playerStats.currentHealth;
    }
}
