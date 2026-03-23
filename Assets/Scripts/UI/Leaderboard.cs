using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public List<int> scores = new List<int>();
    private int lastScore = 0;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += TrackScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= TrackScore;
    }

    // Tracks the latest score so we can record it on session end
    private void TrackScore(int score) => lastScore = score;

    public void RecordSession()
    {
        scores.Add(lastScore);
        SortScores();
    }

    public void SortScores()
    {
        scores.Sort((a, b) => b.CompareTo(a));
    }
}
