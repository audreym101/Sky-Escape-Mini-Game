using System;
using UnityEngine;

// DESIGN PATTERN - OBSERVER:
// ScoreManager fires OnScoreChanged whenever the score updates.
// UIManager and WinManager subscribe to this event to update the display.
// OOP - ENCAPSULATION: the score value is private; only AddScore() can modify it.
public class ScoreManager : MonoBehaviour
{
    // OBSERVER: any class can subscribe to receive the updated score
    public static event Action<int> OnScoreChanged;

    private int score;

    public void AddScore(int value)
    {
        score += value;
        // Notify all subscribers with the new score
        OnScoreChanged?.Invoke(score);
    }
}
