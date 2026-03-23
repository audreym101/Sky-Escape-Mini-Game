using UnityEngine;

public class ChaseState : IEnemyState
{
    private float attackRange = 2f;
    private float loseRange = 12f;

    public void Execute(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance <= attackRange)
        {
            enemy.SetState(new AttackState());
            return;
        }

        if (distance > loseRange)
        {
            enemy.SetState(new IdleState());
            return;
        }

        enemy.Move();
    }
}
