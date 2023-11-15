using UnityEngine;

public class MirrorBulletController : MonoBehaviour
{
    public string wallTag = "Wall";
    public int maxCollisions = 4; // Adjust the maximum number of collisions before destroying the bullet
    private int collisionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wallTag))
        {
            // Reverse direction while keeping the velocity magnitude
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = -rb.velocity;

            // Increase the collision count
            collisionCount++;

            // Check if the maximum number of collisions is reached
            if (collisionCount >= maxCollisions+1)
            {
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        // Destroy the bullet GameObject
        Destroy(gameObject);
    }
}