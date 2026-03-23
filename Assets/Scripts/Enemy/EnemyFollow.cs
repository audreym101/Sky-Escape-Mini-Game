using UnityEngine;

// OOP - INHERITANCE:
// Enemy extends Character, inheriting speed and overriding Move() with AI movement.
// OOP - ENCAPSULATION:
// Internal state (currentState, rb) is private. External code uses SetState() to change behaviour.
// DESIGN PATTERN - STATE:
// Enemy delegates its behaviour to an IEnemyState object (Idle, Chase, Attack).
// Switching states changes behaviour without modifying the Enemy class itself.
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Character
{
    // STATE PATTERN: current behaviour is held as an IEnemyState reference
    private IEnemyState currentState;
    public Transform player;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.linearDamping = 4f;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Start in idle state — enemy waits until player is in range
        currentState = new IdleState();
    }

    void Update()
    {
        // STATE PATTERN: delegate all behaviour to the current state each frame
        currentState.Execute(this);
    }

    // OOP - POLYMORPHISM:
    // Overrides Character.Move() — enemy moves toward the player using physics.
    public override void Move()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 8f * Time.deltaTime);
        }
    }

    // ALGORITHM 2 - LINEAR SEARCH (Nearest Coin):
    // Problem: The enemy needs to find and move toward the closest uncollected coin.
    // Approach: Iterate all active Coin objects, track the one with minimum distance.
    // Time Complexity: O(n) where n = number of coins remaining in the scene.
    // This is the same class of algorithm as FindNearestEnemy() in Player.cs,
    // demonstrating the searching algorithm applied to a different target type.
    public Coin FindNearestCoin()
    {
        Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
        Coin nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (Coin coin in coins)
        {
            float distance = Vector3.Distance(transform.position, coin.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = coin;
            }
        }

        return nearest;
    }

    public void StopMoving()
    {
        rb.linearVelocity = Vector3.zero;
    }

    // STATE PATTERN: allows states to transition the enemy to a new behaviour
    public void SetState(IEnemyState newState)
    {
        currentState = newState;
    }
}
