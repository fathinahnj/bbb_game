using UnityEngine;

public class Castle : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Castle destroyed!");
            // Tambahkan efek kalah/game over
        }
    }
}
