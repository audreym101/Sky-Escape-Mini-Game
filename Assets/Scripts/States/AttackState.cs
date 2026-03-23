using UnityEngine;

// DESIGN PATTERN - STATE:
// AttackState activates when the enemy is close enough to the player.
// The enemy stops moving and deals damage on a cooldown timer.
// OOP - ABSTRACTION: implements IEnemyState, isolating attack logic from the Enemy class.
// OOP - ENCAPSULATION: timer and cooldown are private, managed internally.
public class AttackState : IEnemyState
{
    private float attackRange = 2.5f;
    private float attackCooldown = 1f;
    private float timer = 0f;

    public void Execute(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // If player moved out of range, go back to chasing
        if (distance > attackRange)
        {
            enemy.SetState(new ChaseState());
            return;
        }

        enemy.StopMoving();

        // Face the player while attacking
        Vector3 dir = (enemy.player.position - enemy.transform.position).normalized;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);

        // Deal damage once per cooldown interval
        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            timer = 0f;
            // OOP - ENCAPSULATION: TakeDamage() hides health logic inside PlayerHealth
            PlayerHealth ph = enemy.player.GetComponentInParent<PlayerHealth>();
            if (ph == null) ph = enemy.player.GetComponentInChildren<PlayerHealth>();
            if (ph == null) ph = enemy.player.GetComponent<PlayerHealth>();
            ph?.TakeDamage(1);
        }
    }
}
