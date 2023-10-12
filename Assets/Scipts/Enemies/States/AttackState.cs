using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AttackState : IEnemyState
{
    
    private EnemyController enemy;

    private IWeapon enemyWeapon;

    private IEnemyState nextState;
    
    public AttackState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        if (enemy.Holder.HasHoldableObject())
        {
            enemyWeapon = enemy.Holder.GetHoldableObject().GetComponent<IWeapon>();
        }
    }

    public void UpdateState()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) > enemy.AttackTargetDistance 
            && !enemyWeapon.IsAttacking())
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
        else
        {
            enemyWeapon?.Attack();
        }
    }

    public void ExitState()
    {
        Debug.Log($"{this.GetType()} does not have a implementation of {MethodBase.GetCurrentMethod()?.Name}");
    }

    // public void SetNextState(IEnemyState nextState)
    // {
    //     this.nextState = nextState;
    // }
}
