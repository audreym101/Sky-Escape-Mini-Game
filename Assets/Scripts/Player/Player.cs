using UnityEngine;
using UnityEngine.InputSystem;

// OOP - INHERITANCE:
// Player extends Character, inheriting the speed field and overriding Move().
// This avoids duplicating shared logic and keeps the class focused on player-specific behaviour.
[RequireComponent(typeof(Rigidbody))]
public class Player : Character
{
    public float jumpForce = 4f;
    public float rotationSpeed = 8f;
    public float acceleration = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.linearDamping = 8f;
    }

    // OOP - POLYMORPHISM:
    // Overrides Character.Move() with player-specific keyboard input logic.
    public override void Move()
    {
        if (Keyboard.current == null) return;

        float moveX = Keyboard.current.dKey.isPressed ? 1f : Keyboard.current.aKey.isPressed ? -1f : 0f;
        float moveZ = Keyboard.current.wKey.isPressed ? 1f : Keyboard.current.sKey.isPressed ? -1f : 0f;

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void Update()
    {
        Move();

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Apply force for smooth physics-based movement
        rb.AddForce(moveDirection * acceleration, ForceMode.VelocityChange);

        // Clamp horizontal speed so player doesn't accelerate forever
        Vector3 horizontal = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontal.magnitude > speed)
        {
            Vector3 clamped = horizontal.normalized * speed;
            rb.linearVelocity = new Vector3(clamped.x, rb.linearVelocity.y, clamped.z);
        }

        // Smooth rotation toward movement direction
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnCollisionStay(Collision collision) { isGrounded = true; }
    private void OnCollisionExit(Collision collision) { isGrounded = false; }

    // ALGORITHM 1 - LINEAR SEARCH (Nearest Enemy):
    // Problem: The player needs to identify the closest enemy in the scene.
    // Approach: Iterate through all Enemy instances, track the minimum distance found.
    // Time Complexity: O(n) where n = number of enemies.
    // This is optimal for an unsorted, dynamic set of game objects.
    public Enemy FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Enemy nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
