using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : Character
{
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
        currentState = new IdleState();
    }

    void Update()
    {
        currentState.Execute(this);
    }

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

    public void StopMoving()
    {
        rb.linearVelocity = Vector3.zero;
    }

    public void SetState(IEnemyState newState)
    {
        currentState = newState;
    }
}
