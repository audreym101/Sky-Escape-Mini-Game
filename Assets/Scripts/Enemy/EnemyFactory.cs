using UnityEngine;

// DESIGN PATTERN - FACTORY:
// EnemyFactory centralises enemy creation. Callers request an enemy type
// without needing to know how it is constructed or configured.
// OOP - ENCAPSULATION: spawning logic and prefab references are hidden inside this class.
// OOP - ABSTRACTION: the Spawn() method provides a simple interface for creating enemies.
public class EnemyFactory : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject basicEnemyPrefab;
    public GameObject fastEnemyPrefab;

    public enum EnemyType { Basic, Fast }

    // Factory method — returns a configured enemy based on the requested type
    public GameObject Spawn(EnemyType type, Vector3 position)
    {
        GameObject prefab = type == EnemyType.Fast ? fastEnemyPrefab : basicEnemyPrefab;
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);

        Enemy enemy = instance.GetComponent<Enemy>();

        // Configure speed based on enemy type
        if (type == EnemyType.Fast)
            enemy.speed = 8f;

        return instance;
    }
}
