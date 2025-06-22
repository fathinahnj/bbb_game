using UnityEngine;

public class Tower : MonoBehaviour {
    public float range = 3f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float fireCountdown = 0f;

    void Update() {
        fireCountdown -= Time.deltaTime;

        GameObject target = FindNearestGoblin();
        if (target != null) {
            Debug.DrawLine(transform.position, target.transform.position, Color.red);
        }

        if (target != null && fireCountdown <= 0f) {
            Shoot(target);
            fireCountdown = 1f / fireRate;
        }
    }

    GameObject FindNearestGoblin() {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        GameObject nearest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject goblin in goblins) {
            float distance = Vector3.Distance(transform.position, goblin.transform.position);
            if (distance < shortestDistance && distance <= range) {
                shortestDistance = distance;
                nearest = goblin;
            }
        }

        return nearest;
    }

    void Shoot(GameObject target) {
        if (firePoint == null) {
            Debug.LogError("Tower firePoint belum diset!");
            return;
        }

        if (projectilePrefab == null) {
            Debug.LogError("Tower projectilePrefab belum diset!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(target.transform);
    }

    void OnDrawGizmos() {
    #if UNITY_EDITOR
        // Gizmos.color = new Color(0f, 1f, 0f, 0.15f); // hijau transparan
        Gizmos.color = new Color(0f, 0f, 1f, 0.15f); // biru transparan
        Gizmos.DrawSphere(transform.position, range);
    #endif
    }
}
