using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;
    private AudioSource audioSource;
    private bool collected = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Auto-add trigger collider if missing
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            SphereCollider sc = gameObject.AddComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = 0.5f;
        }
        else if (!col.isTrigger)
        {
            col.isTrigger = true;
        }
    }

    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin trigger hit by: " + other.gameObject.name + " tag: " + other.tag);
        if (other.CompareTag("Player")) Collect(false);
        else if (other.CompareTag("Enemy")) Collect(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coin collision hit by: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player")) Collect(false);
        else if (collision.gameObject.CompareTag("Enemy")) Collect(true);
    }

    private void Collect(bool byEnemy)
    {
        if (collected) return;
        collected = true;

        Debug.Log("Coin collected by " + (byEnemy ? "enemy" : "player"));

        if (coinSound != null)
            audioSource.PlayOneShot(coinSound);

        if (GameManager.Instance != null)
        {
            if (byEnemy) GameManager.Instance.EnemyCollectedCoin();
            else GameManager.Instance.AddScore(1);
        }
        else
            Debug.LogError("GameManager.Instance is NULL — make sure GameManager is in the scene");

        GetComponent<Collider>().enabled = false;

        // Hide all renderers including children (coin prefabs have child meshes)
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        Destroy(gameObject, coinSound != null ? coinSound.length : 0.1f);
    }
}