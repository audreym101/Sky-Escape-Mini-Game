using UnityEngine;

public class AttackState : IEnemyState
{
    private float attackRange = 2.5f;
    private float attackCooldown = 1f;
    private float timer = 0f;

    public void Execute(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance > attackRange)
        {
            enemy.SetState(new ChaseState());
            return;
        }

        // Stop moving while attacking
        enemy.StopMoving();

        // Face the player while attacking
        Vector3 dir = (enemy.player.position - enemy.transform.position).normalized;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            timer = 0f;
            // Search up the hierarchy to find PlayerHealth
            PlayerHealth ph = enemy.player.GetComponentInParent<PlayerHealth>();
            if (ph == null) ph = enemy.player.GetComponentInChildren<PlayerHealth>();
            if (ph == null) ph = enemy.player.GetComponent<PlayerHealth>();
            ph?.TakeDamage(1);
            Debug.Log("Enemy attacked player!");
        }
    }
}
