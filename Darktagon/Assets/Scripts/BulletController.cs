using UnityEngine;

public class BulletController : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // Check if the other object has the "Player" tag
        if (other.CompareTag("Player")) {
            // Handle trigger with the player
            HandleTrigger();
        }
    }

    private void HandleTrigger()
    {
        // Add your logic for what should happen when the bullet triggers with the player
        Debug.Log("Bullet triggered with the player");

        // Destroy the bullet GameObject
        Destroy(gameObject);
    }
}

