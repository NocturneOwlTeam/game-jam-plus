using Lean.Pool;
using UnityEngine;

public class GeneralDespawn : MonoBehaviour
{
    [SerializeField]
    private float delay = 0f;
    public void Despawn()
    {
        LeanPool.Despawn(this, delay);
    }
}