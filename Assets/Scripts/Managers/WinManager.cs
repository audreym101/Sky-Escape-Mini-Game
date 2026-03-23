using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject enemyWinPanel;
    public TextMeshProUGUI winScoreText;

    private void OnEnable()
    {
        GameManager.OnAllCoinsCollected += ShowWinScreen;
        GameManager.OnEnemyWon += ShowEnemyWinScreen;
        ScoreManager.OnScoreChanged += TrackScore;
    }

    private void OnDisable()
    {
        GameManager.OnAllCoinsCollected -= ShowWinScreen;
        GameManager.OnEnemyWon -= ShowEnemyWinScreen;
        ScoreManager.OnScoreChanged -= TrackScore;
    }

    private void TrackScore(int score)
    {
        if (winScoreText != null)
            winScoreText.text = "Final Score: " + score;
    }

    private void ShowWinScreen()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    private void ShowEnemyWinScreen()
    {
        if (enemyWinPanel != null) enemyWinPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}
