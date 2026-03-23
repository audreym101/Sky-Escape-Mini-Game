using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnAllCoinsCollected;
    public static event Action OnEnemyWon;

    public ScoreManager scoreManager;

    private int totalCoins;
    private int collectedCoins;
    private int enemyCollectedCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        // Count all coins in the newly loaded scene
        totalCoins = FindObjectsByType<Coin>(FindObjectsSortMode.None).Length;
        collectedCoins = 0;
        enemyCollectedCoins = 0;
    }

    public void AddScore(int value)
    {
        if (scoreManager != null)
            scoreManager.AddScore(value);

        collectedCoins++;
        if (totalCoins > 0 && collectedCoins >= totalCoins)
            OnAllCoinsCollected?.Invoke();
    }

    public void EnemyCollectedCoin()
    {
        enemyCollectedCoins++;
        if (totalCoins > 0 && enemyCollectedCoins >= totalCoins)
            OnEnemyWon?.Invoke();
    }
}
