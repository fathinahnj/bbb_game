using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;
    public Transform spawnPoint;
    public Transform istana;

    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnGoblin), 1f, spawnInterval);
    }

    void SpawnGoblin()
    {
        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.identity);
        goblin.GetComponent<Goblin>().targetIstana = istana;
    }
}
