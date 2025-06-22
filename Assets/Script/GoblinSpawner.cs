using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;
    public Transform istana;
    public float spawnInterval = 2f;

    // Area untuk random spawn
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnGoblin), 1f, spawnInterval);
    }

    void SpawnGoblin()
    {
        Vector3 spawnPosition = Vector3.zero;

        int direction = Random.Range(0, 4); // 0 = kiri, 1 = kanan, 2 = atas, 3 = bawah

        switch (direction)
        {
            case 0: // Kiri
                spawnPosition = new Vector3(minX, Random.Range(minY, maxY), 0f);
                break;
            case 1: // Kanan
                spawnPosition = new Vector3(maxX, Random.Range(minY, maxY), 0f);
                break;
            case 2: // Atas
                spawnPosition = new Vector3(Random.Range(minX, maxX), maxY, 0f);
                break;
            case 3: // Bawah
                spawnPosition = new Vector3(Random.Range(minX, maxX), minY, 0f);
                break;
        }

        GameObject goblin = Instantiate(goblinPrefab, spawnPosition, Quaternion.identity);
        goblin.GetComponent<Goblin>().targetIstana = istana;
    }
}
