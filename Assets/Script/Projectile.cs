using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f;
    public int damage = 10;
    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() {
        target.GetComponent<Goblin>().TakeDamage(damage);
        Destroy(gameObject);
    }
}