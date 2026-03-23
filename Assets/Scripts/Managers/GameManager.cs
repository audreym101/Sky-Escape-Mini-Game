using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// DESIGN PATTERN - SINGLETON:
// GameManager ensures only one instance exists across all scenes using DontDestroyOnLoad.
// All other scripts access it via GameManager.Instance without needing a direct reference.
// DESIGN PATTERN - OBSERVER:
// Fires events (OnAllCoinsCollected, OnEnemyWon) that other managers subscribe to.
// This decouples game logic from UI — GameManager doesn't know about WinManager.
// OOP - ENCAPSULATION: coin counts are private; external code calls AddScore() or EnemyCollectedCoin().
public class GameManager : MonoBehaviour
{
    // SINGLETON: static instance accessible globally
    public static GameManager Instance;

    // OBSERVER: events broadcast game outcomes to any subscribed listener
    public static event Action OnAllCoinsCollected;
    public static event Action OnEnemyWon;

    public ScoreManager scoreManager;

    private int totalCoins;
    private int collectedCoins;
    private int enemyCollectedCoins;

    private void Awake()
    {
        // SINGLETON: destroy duplicate instances that appear after scene reload
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

    private void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    private void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        totalCoins = FindObjectsByType<Coin>(FindObjectsSortMode.None).Length;
        collectedCoins = 0;
        enemyCollectedCoins = 0;
    }

    // Called when the player collects a coin
    public void AddScore(int value)
    {
        if (scoreManager != null)
            scoreManager.AddScore(value);

        collectedCoins++;

        // OBSERVER: notify all listeners if player collected all coins
        if (totalCoins > 0 && collectedCoins >= totalCoins)
            OnAllCoinsCollected?.Invoke();
    }

    // Called when an enemy collects a coin
    public void EnemyCollectedCoin()
    {
        enemyCollectedCoins++;

        // OBSERVER: notify all listeners if enemy collected all coins
        if (totalCoins > 0 && enemyCollectedCoins >= totalCoins)
            OnEnemyWon?.Invoke();
    }
}
