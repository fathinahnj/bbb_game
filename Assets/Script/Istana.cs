using UnityEngine;
using UnityEngine.UI;

public class Istana : MonoBehaviour {
    public int maxHP = 100;
    private int currentHP;
    public Image healthBarFill;

    void Start() {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage) {
        if (GameManager.isGameOver) return;

        currentHP -= damage;
        Debug.Log($"Istana terkena serangan! Sisa HP: {currentHP}");
        UpdateHealthBar(); 

        if (currentHP <= 0) {
            Debug.Log("HP Istana habis, Game Over!");
            currentHP = 0;
            UpdateHealthBar();
            GameManager.GameOver();
        }
    }

    void UpdateHealthBar() {
        if (healthBarFill != null) {
            healthBarFill.fillAmount = (float)currentHP / maxHP;
        }
    }
}
