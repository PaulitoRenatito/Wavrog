using System.Reflection;
using UnityEngine;

public class IdleState : IEnemyState
{

    private EnemyController enemy;

    public IdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        Debug.Log($"{this.GetType()} does not have a implementation of {MethodBase.GetCurrentMethod()?.Name}");
    }

    public void UpdateState()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) < enemy.PerceiveTargetDistance)
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public void ExitState()
    {
        Debug.Log($"{this.GetType()} does not have a implementation of {MethodBase.GetCurrentMethod()?.Name}");
    }
}
