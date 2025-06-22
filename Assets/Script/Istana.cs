using UnityEngine;

public class Istana : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Istana HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER: Istana hancur!");
        // Nanti bisa tambahkan UI Game Over, restart, dll
        Time.timeScale = 0;
    }
}
