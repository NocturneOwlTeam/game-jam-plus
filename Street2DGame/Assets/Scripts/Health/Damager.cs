using Nocturne.Health;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<HealthSystem>(out var health))
        {
            health.Damage(damage);
        }
    }
}