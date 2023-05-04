using System;
using UnityEngine;

public class ScoreIncreaser : MonoBehaviour
{
    [SerializeField]
    private int scoreIncrease = 100;

    public static event Action<int> OnIncrease;

    public void Increase()
    {
        OnIncrease?.Invoke(scoreIncrease);
    }
}