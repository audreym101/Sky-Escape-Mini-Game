using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI heartLostText;

    private Coroutine heartLostCoroutine;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
        PlayerHealth.OnHealthChanged += UpdateHealth;
        PlayerHealth.OnHeartLost += ShowHeartLostNotification;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
        PlayerHealth.OnHealthChanged -= UpdateHealth;
        PlayerHealth.OnHeartLost -= ShowHeartLostNotification;
    }

    void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateHealth(int current, int max)
    {
        if (healthText == null) return;
        string display = "";
        for (int i = 0; i < max; i++)
            display += i < current ? "[HP] " : "[--] ";
        healthText.text = display.TrimEnd();
    }

    void ShowHeartLostNotification()
    {
        if (heartLostText == null) return;
        if (heartLostCoroutine != null) StopCoroutine(heartLostCoroutine);
        heartLostCoroutine = StartCoroutine(HeartLostRoutine());
    }

    private System.Collections.IEnumerator HeartLostRoutine()
    {
        heartLostText.text = "\u2665 -1 Heart!";
        heartLostText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        heartLostText.gameObject.SetActive(false);
    }
}