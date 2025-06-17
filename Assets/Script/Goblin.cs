using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform targetIstana;
    public float speed = 1f;

    void Update()
    {
        if (targetIstana != null)
        {
            Vector3 direction = (targetIstana.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Istana"))
        {
            Debug.Log("Goblin reached the Istana!");
        }
    }

}
