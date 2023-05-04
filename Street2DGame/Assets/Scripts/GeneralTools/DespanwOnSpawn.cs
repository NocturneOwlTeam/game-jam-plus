using Lean.Pool;
using UnityEngine;

public class DespanwOnSpawn : MonoBehaviour
{
    [SerializeField]
    private float delay = 1f;

    //private float newDelay
    private void OnEnable()
    {
        print("He sido spawneado");
        LeanPool.Despawn(this, delay);
    }
}