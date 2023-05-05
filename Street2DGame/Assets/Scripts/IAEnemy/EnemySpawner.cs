using Lean.Pool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    private void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Debe haber al menos un punto spawneo");
        }

        if (enemies == null || enemies.Length == 0)
        {
            Debug.LogError("Debe haber al menos un enemigo");
        }
    }

    public void SpawnEnemies()
    {
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            LeanPool.Spawn(enemies[Random.Range(0, enemies.Length)], spawnPoints[i].position, Quaternion.identity);
        }

        print("Spawneo");
    }
}