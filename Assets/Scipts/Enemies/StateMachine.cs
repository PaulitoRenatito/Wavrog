using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Evade,
        Flee
    }

    [ReadOnly][SerializeField] private EnemyState currentState;

    private Dictionary<EnemyState, Action> stateActions = new Dictionary<EnemyState, Action>();
    private Dictionary<EnemyState, StateTransitions> stateTransitions = new Dictionary<EnemyState, StateTransitions>();

    private Enemy_1 enemy1;

    public StateMachine(Enemy_1 enemy1)
    {
        this.enemy1 = enemy1;
        stateActions[EnemyState.Idle] = IdleStateAction;
        stateActions[EnemyState.Chase] = ChaseStateAction;
        stateActions[EnemyState.Attack] = AttackStateAction;

        stateTransitions[EnemyState.Idle] = new StateTransitions(EnemyState.Chase);
        stateTransitions[EnemyState.Chase] = new StateTransitions(EnemyState.Idle, EnemyState.Attack);
        stateTransitions[EnemyState.Attack] = new StateTransitions(EnemyState.Chase);
    }

    public void Update()
    {
        if (stateActions.TryGetValue(currentState, out Action currentStateAction))
        {
            currentStateAction?.Invoke();
        }
    }

    public void EnterState(EnemyState newState)
    {
        currentState = newState;
    }

    public void ChangeState(int transitionIndex)
    {
        if (!stateTransitions.TryGetValue(currentState, out StateTransitions transitions)) return;

        if (transitions.NextStates.Count > transitionIndex && transitionIndex >= 0)
        {
            currentState = transitions.NextStates[transitionIndex];
            EnterState(currentState);
        }
        else
        {
            Debug.LogError($"Transition index of {transitionIndex} does not exists for {currentState}");
        }
    }
    
    private void IdleStateAction()
    {
        if (Vector3.Distance(enemy1.transform.position, enemy1.target.position) < enemy1.perceiveTargetDistance)
        {
            ChangeState(0);
        }
    }
    
    private void ChaseStateAction()
    {
        if (Vector3.Distance(enemy1.transform.position, enemy1.target.position) <= enemy1.attackTargetDistance)
        {
            ChangeState(1);
        }
        else if (Vector3.Distance(enemy1.transform.position, enemy1.target.position) > enemy1.perceiveTargetDistance)
        {
            ChangeState(0);
        }

        Vector3 targetPosition = enemy1.target.position;
        enemy1.movement.MoveByDestination(targetPosition);
                
        Quaternion rotateDirection = Quaternion.LookRotation(targetPosition - enemy1.transform.position);
        rotateDirection.x = 0f;
        rotateDirection.z = 0f;
        enemy1.movement.Rotate(rotateDirection);
    }
    
    private void AttackStateAction()
    {
        if (enemy1.holder.GetHoldableObject().TryGetComponent(out IWeapon weapon))
        {
            if (Vector3.Distance(enemy1.transform.position, enemy1.target.position) > enemy1.attackTargetDistance
                && !weapon.IsAttacking())
            {
                ChangeState(0);
            }
            else
            {
                weapon?.Attack();
            }
        }
    }
}
