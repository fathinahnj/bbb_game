using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
	public static bool isGameOver = false;
	public GameObject gameOverPanel;
	public static GameManager instance;
	public TextMeshProUGUI goldText;
	public TextMeshProUGUI goblinKillText;
	public static int gold = 0;
	public static int goblinKilled = 0;

	void Awake() {
		instance = this;
		isGameOver = false;

		if (gameOverPanel != null) {
			gameOverPanel.SetActive(false); // Pastikan awalnya disembunyikan
		}
	}

	void Start() {
		UpdateGoldUI();
	}

	public static void AddGold(int amount) {
		gold += amount;
		FindFirstObjectByType<GameManager>()?.UpdateGoldUI();
	}

	public static bool SpendGold(int amount) {
		if (gold >= amount) {
			gold -= amount;
			FindFirstObjectByType<GameManager>()?.UpdateGoldUI();
			return true;
		}
		return false;
	}

	void UpdateGoldUI() {
		if (goldText != null) {
			goldText.text = "Gold: " + gold;
		}
	}

	public static void GameOver() {
		isGameOver = true;
		Debug.Log("GAME OVER: Istana hancur!");

		// Hancurkan semua Goblin
		GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
		foreach (GameObject goblin in goblins)
		{
			GameObject.Destroy(goblin);
		}

		// Hancurkan semua Tower
		GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
		foreach (GameObject tower in towers)
		{
			GameObject.Destroy(tower);
		}

		// Hancurkan semua Istana (opsional, biasanya cuma 1)
		GameObject[] istanas = GameObject.FindGameObjectsWithTag("Istana");
		foreach (GameObject istana in istanas)
		{
			GameObject.Destroy(istana);
		}

		if (instance != null && instance.gameOverPanel != null)
		{
			instance.gameOverPanel.SetActive(true);
		}
	}
	void ShowGameOver() {
		Debug.Log("Menampilkan panel game over");
		if (gameOverPanel != null) {
			gameOverPanel.SetActive(true);
		}
	}

	void UpdateKillUI() {
		if (goblinKillText != null) {
			goblinKillText.text = "Goblin Killed: " + goblinKilled;
		}
	}

	public static void AddKill() {
		goblinKilled++;
		FindFirstObjectByType<GameManager>()?.UpdateKillUI();
	}
	
	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}