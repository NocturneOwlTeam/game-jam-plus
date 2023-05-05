using Lean.Pool;
using Nocturne.Health;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float damage = 4f;
    private Rigidbody2D bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Evitara que sea persistente y llene la memoria.
        //Creanme, es una pesadilla
        LeanPool.Despawn(this, 5f);
    }

    public void SetCourse(float cour)
    {
        Speed *= cour;
        bulletRigidbody.velocity = new Vector2(Speed, bulletRigidbody.velocity.y);
        print("Listo");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<HealthSystem>(out var health))
        {
            health.Damage(damage);
            LeanPool.Despawn(this);
        }
    }

    private void OnEnable()
    {
        Speed = 10f;
    }

    private void OnDisable()
    {
    }
}