using System.Collections;
using UnityEngine;
using TMPro;

public class GoblinSpawner : MonoBehaviour {
    public GameObject goblinPrefab;
    public Transform istana;
    public Transform targetIstana;
    public Vector3 fixedSpawnPosition = new Vector3(7.5f, -2.5f, 0);
    public float spawnInterval = 3f;
    private int totalSpawned = 0;
    private int currentTier = 0;
    private int lastTier = -1; // untuk melacak apakah sudah naik tier

    [Header("Scaling")]
    public int baseHP = 20, maxHP = 60;
    public int baseDMG = 10, maxDMG = 30;
    public float baseSPD = 2f, maxSPD = 4f;
    public int scalingStep = 5;

    [Header("UI")]
    public TextMeshProUGUI waveText;

    void Start() {
        StartCoroutine(SpawnLoop());
        // InvokeRepeating(nameof(SpawnGoblin), 1f, spawnInterval);
    }

    IEnumerator SpawnLoop() {
        while (!GameManager.isGameOver) {
            SpawnGoblin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGoblin() {
        if (GameManager.isGameOver) return;

        GameObject goblinGO = Instantiate(goblinPrefab, fixedSpawnPosition, Quaternion.identity);
        Goblin goblin = goblinGO.GetComponent<Goblin>();

        if (goblin != null) {
            goblin.targetIstana = istana;
            currentTier = totalSpawned / scalingStep;       // Hitung level scaling berdasarkan total spawn

            int newHP = Mathf.Min(baseHP + (currentTier * 5), maxHP);
            int newDMG = Mathf.Min(baseDMG + (currentTier * 2), maxDMG);
            float newSPD = Mathf.Min(baseSPD + (currentTier * 0.2f), maxSPD);
            goblin.Init(newHP, newDMG, newSPD, istana);
            
            if (currentTier > lastTier) {
                Debug.Log($"⚔️ Goblin diperkuat! Tier {currentTier} → HP: {newHP}, DMG: {newDMG}, SPD: {newSPD}");
                if (waveText != null)
                    waveText.text = $"Tier: {currentTier}";
                lastTier = currentTier;
            }
        }

        totalSpawned++;
    }
}