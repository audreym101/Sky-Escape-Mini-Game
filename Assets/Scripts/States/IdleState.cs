using UnityEngine;

public class IdleState : IEnemyState
{
    private float detectionRange = 8f;

    public void Execute(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distance <= detectionRange)
            enemy.SetState(new ChaseState());
    }
}
