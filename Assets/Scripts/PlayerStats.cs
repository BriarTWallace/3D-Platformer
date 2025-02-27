using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth = 100;
    public int currentHealth;

    public void Initialize()
    {
        currentHealth = maxHealth; // Reset health when the game starts
    }
    
    // Function to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }
}

