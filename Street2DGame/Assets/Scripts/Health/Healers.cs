using Lean.Pool;
using Nocturne.Health;
using UnityEngine;

public class Healers : MonoBehaviour
{
    [SerializeField] private float heal = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<IHealthSystem>(out var health) && !health.AtFullHealth())
        {
            health.Heal(heal);
            LeanPool.Despawn(this);
        }
    }
}