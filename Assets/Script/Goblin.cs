using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {
    [Header("Target & Movement")]
    public Transform targetIstana;
    public float speed = 1f;
    public float waypointDistance = 0.1f;
    private List<Node> path;
    private int currentWaypointIndex = 0;
    private bool hasReachedIstana = false;
    private Pathfinding pathfinding;
    private GridFromTilemap grid;

    [Header("Health & Damage")]
    public int maxHP = 20;
    public int damageToIstana = 10;
    private int currentHP;

    public void Init(int hp, int damage, float speedVal, Transform target) {
        maxHP = hp;
        damageToIstana = damage;
        speed = speedVal;
        targetIstana = target;
        currentHP = maxHP;

        pathfinding = Object.FindFirstObjectByType<Pathfinding>();
        if (targetIstana != null) {
            path = pathfinding.FindPath(transform.position, targetIstana.position);
        }
    }

    public void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
        GameManager.AddGold(25);       // Tambah gold
        GameManager.AddKill();         // Tambah kill counter
    }

    void Update() {
        if (GameManager.isGameOver || hasReachedIstana || path == null || currentWaypointIndex >= path.Count)
            return;

        Vector3 targetPos = path[currentWaypointIndex].worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < waypointDistance) {
            currentWaypointIndex++;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (GameManager.isGameOver || hasReachedIstana) return;

        if (other.CompareTag("Istana")) {
            other.GetComponent<Istana>()?.TakeDamage(damageToIstana);
            hasReachedIstana = true;
        }
    }
}