using UnityEngine;

// DESIGN PATTERN - STATE:
// IdleState is the default enemy state. The enemy stands still and watches for the player.
// When the player enters detection range, it transitions to ChaseState.
// OOP - ABSTRACTION: implements IEnemyState, keeping idle logic self-contained.
public class IdleState : IEnemyState
{
    private float detectionRange = 8f;

    public void Execute(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // If player is within detection range, start chasing
        if (distance <= detectionRange)
            enemy.SetState(new ChaseState());
    }
}
