using Lean.Pool;
using Nocturne.GeneralTools;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int speed;
    [SerializeField] private float movementRadius;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    private Animator anim;
    private bool isAttacking = false;
    private float attackInterval = 1f;
    private float attackTimer = 0f;

    private Vector3 currentScale;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (!player)
        {
            player = Helpers.player.transform;
        }
        currentScale = transform.localScale;
    }

    private void Update()
    {
        // Obtener la dirección hacia el jugador
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Verificar si está dentro del radio de movimiento
        if (distanceToPlayer > movementRadius || direction.y > 0.1 && distanceToPlayer > movementRadius || direction.y < -0.1)
        {
            // Mover al enemigo en la dirección del jugador
            transform.Translate(direction * speed * Time.deltaTime);
            anim.SetBool("Run", true);
            anim.SetBool("Atack", false);

            // Reiniciar el temporizador de ataque
            attackTimer = 0f;
        }
        else
        {
            anim.SetBool("Run", false);

            // Si no está atacando y ha pasado el intervalo de tiempo
            if (!isAttacking && attackTimer >= attackInterval)
            {
                // Iniciar el ataque
                Attack();
                //anim.SetBool("Atack", true);
                Invoke("Attack", 0.5f);
            }
        }

        // Girar al enemigo para que mire hacia el jugador
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1 * currentScale.x, currentScale.y, currentScale.z); // Invertir la escala en el eje X
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(currentScale.x, currentScale.y, currentScale.z); // Restaurar la escala original en el eje X
        }

        // Actualizar el temporizador de ataque
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        // Iniciar la animación de ataque
        anim.SetBool("Atack", true);

        // Establecer la bandera de ataque
        isAttacking = true;

        // Reiniciar el temporizador de ataque
        attackTimer = 0f;
        print("ATTACK!");

        // Reiniciar la bandera de ataque después de 1 segundo
        Invoke("ResetAttackFlag", 1f);
    }

    public void LaunchBullet()
    {
        var newBullet = LeanPool.Spawn(bullet, spawnPoint.position,Quaternion.identity).GetComponent<bullet>();
        newBullet.SetCourse(Mathf.Sign(transform.localScale.x));
    }

    private void ResetAttackFlag()
    {
        // Restablecer la bandera de ataque
        isAttacking = false;

        // Establecer el parámetro de animación de ataque a falso
        anim.SetBool("Atack", false);
    }
}