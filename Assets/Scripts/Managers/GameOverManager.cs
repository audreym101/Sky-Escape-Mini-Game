using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += TrackScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= TrackScore;
    }

    private void TrackScore(int score)
    {
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;
    }

    public void Restart()
    {
        Debug.Log("Restart called — scene: " + SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Debug.Log("GoToMenu called");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}
