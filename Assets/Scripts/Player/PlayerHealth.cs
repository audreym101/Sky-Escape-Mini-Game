using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int, int> OnHealthChanged;
    public static event Action OnHeartLost;

    public GameObject gameOverUI;
    public int maxHealth = 3;

    private int health;
    private float damageCooldown = 1f;
    private float lastDamageTime = -1f;

    void Start()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        lastDamageTime = Time.time;

        health = Mathf.Max(0, health - amount);
        OnHealthChanged?.Invoke(health, maxHealth);
        OnHeartLost?.Invoke();
        Debug.Log("Player HP: " + health);

        if (health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            TakeDamage(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(1);
    }

    private void Die()
    {
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
}
