using UnityEngine;
using UnityEngine.UI;

public class Istana : MonoBehaviour
{
    public int maxHP = 60;
    private int currentHP;

    public Image healthBarFill; // Drag HealthBar_Fill ke sini via Inspector

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (GameManager.isGameOver) return;

        currentHP -= damage;
        Debug.Log("Istana HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.GameOver();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHP / maxHP;
        }
    }
}
