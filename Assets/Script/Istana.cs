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
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log("Istana HP: " + currentHP);
        UpdateHealthBar();

        if (currentHP == 0)
        {
            Debug.Log("GAME OVER: Istana hancur!");
            // TODO: Trigger game over screen
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
