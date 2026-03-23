using UnityEngine;

// DESIGN PATTERN - STATE:
// ChaseState is one concrete state in the enemy State Machine.
// When active, the enemy chases the nearest coin first.
// If no coins remain or the player is in attack range, it transitions states.
// OOP - ABSTRACTION: implements IEnemyState, hiding chase logic from the Enemy class.
public class ChaseState : IEnemyState
{
    private float attackRange = 2f;
    private float loseRange = 12f;

    public void Execute(Enemy enemy)
    {
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // Transition to attack if close enough to the player
        if (distanceToPlayer <= attackRange)
        {
            enemy.SetState(new AttackState());
            return;
        }

        // Transition back to idle if player is too far
        if (distanceToPlayer > loseRange)
        {
            enemy.SetState(new IdleState());
            return;
        }

        // ALGORITHM 2 - NEAREST COIN SEARCH:
        // Enemy finds the closest coin and moves toward it instead of the player.
        // This makes enemies compete with the player for coins.
        // If no coins are left, the enemy falls back to chasing the player.
        Coin nearestCoin = enemy.FindNearestCoin();

        if (nearestCoin != null)
        {
            // Move toward the nearest coin
            Vector3 direction = (nearestCoin.transform.position - enemy.transform.position).normalized;
            direction.y = 0f;
            enemy.transform.position += direction * enemy.speed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRot, 8f * Time.deltaTime);
            }
        }
        else
        {
            // No coins left — chase the player directly
            enemy.Move();
        }
    }
}
