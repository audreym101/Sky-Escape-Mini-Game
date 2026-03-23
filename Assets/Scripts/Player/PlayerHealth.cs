using System;
using UnityEngine;

// OOP - ENCAPSULATION:
// PlayerHealth hides health logic. External classes call TakeDamage() — they never
// directly modify the health value.
// DESIGN PATTERN - OBSERVER:
// Fires OnHealthChanged and OnHeartLost so UIManager can react without being coupled here.
public class PlayerHealth : MonoBehaviour
{
    // OBSERVER: broadcast health changes and heart-lost notifications
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

    // OOP - ENCAPSULATION: damage logic and cooldown are managed internally
    public void TakeDamage(int amount)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        lastDamageTime = Time.time;

        health = Mathf.Max(0, health - amount);

        // OBSERVER: notify UI to update health display and show notification
        OnHealthChanged?.Invoke(health, maxHealth);
        OnHeartLost?.Invoke();

        if (health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) TakeDamage(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) TakeDamage(1);
    }

    private void Die()
    {
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
}
