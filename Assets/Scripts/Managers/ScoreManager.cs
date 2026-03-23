using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    private int score;

    public void AddScore(int value)
    {
        score += value;
        OnScoreChanged?.Invoke(score);
    }
}