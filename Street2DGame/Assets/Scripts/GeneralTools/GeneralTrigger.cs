using UnityEngine;
using UnityEngine.Events;

public class GeneralTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTrigger.Invoke();
        }
    }
}