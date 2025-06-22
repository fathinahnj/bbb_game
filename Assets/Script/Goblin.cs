using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform targetIstana;
    public float speed = 1f;
    private bool hasReachedIstana = false;

    void Update()
    {
        if (targetIstana != null && !hasReachedIstana)
        {
            Vector3 direction = (targetIstana.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Istana") && !hasReachedIstana)
        {
            other.GetComponent<Istana>().TakeDamage(10);
            hasReachedIstana = true;
        }
    }
}
