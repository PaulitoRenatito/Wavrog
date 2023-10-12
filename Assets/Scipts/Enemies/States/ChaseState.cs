using System.Reflection;
using UnityEngine;

public class ChaseState : IEnemyState
{
    
    private EnemyController enemy;
    
    public ChaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    
    public void EnterState()
    {
        Debug.Log($"{this.GetType()} does not have a implementation of {MethodBase.GetCurrentMethod()?.Name}");
    }

    public void UpdateState()
    {
        
        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) < enemy.AttackTargetDistance)
        {
            enemy.ChangeState(new AttackState(enemy));
        }
        else if (Vector3.Distance(enemy.transform.position, enemy.Target.position) > enemy.PerceiveTargetDistance)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
        
        ChaseTarget();
    }

    public void ExitState()
    {
        Debug.Log($"{this.GetType()} does not have a implementation of {MethodBase.GetCurrentMethod()?.Name}");
    }
    
    private void ChaseTarget()
    {
        // Vector3 moveDir = (enemy.Target.position - enemy.transform.position).normalized;
        // enemy.Movement.Move(moveDir);
        
        enemy.Movement.MoveByDestination(enemy.Target.position);
        
        Quaternion rotateDirection = Quaternion.LookRotation(enemy.Target.position - enemy.transform.position);
        rotateDirection.x = 0f;
        rotateDirection.z = 0f;
        enemy.Movement.Rotate(rotateDirection);
    }
}
