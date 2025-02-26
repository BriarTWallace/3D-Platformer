using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damageAmount = 10;  // Amount of damage to apply to the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);  // Apply damage to the player
            }
        }
    }
}
