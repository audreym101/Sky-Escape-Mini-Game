using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject basicEnemyPrefab;
    public GameObject fastEnemyPrefab;

    public enum EnemyType { Basic, Fast }

    public GameObject Spawn(EnemyType type, Vector3 position)
    {
        GameObject prefab = type == EnemyType.Fast ? fastEnemyPrefab : basicEnemyPrefab;
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);

        Enemy enemy = instance.GetComponent<Enemy>();
        if (type == EnemyType.Fast)
            enemy.speed = 8f;

        return instance;
    }
}
