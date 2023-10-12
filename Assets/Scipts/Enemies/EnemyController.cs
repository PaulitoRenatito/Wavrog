using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private MovementNavMesh movement;
    private Holder holder;
    private IEnemyState currentState;
    
    [SerializeField] private Transform target;
    [SerializeField] private float perceiveTargetDistance = 8f;
    [SerializeField] private float attackTargetDistance = 1.5f;

    public MovementNavMesh Movement => movement;
    public Holder Holder => holder;

    public Transform Target => target;

    public float PerceiveTargetDistance => perceiveTargetDistance;

    public float AttackTargetDistance => attackTargetDistance;

    private void Awake()
    {
        movement = GetComponent<MovementNavMesh>();
        holder = GetComponent<Holder>();
        currentState = new IdleState(this);
    }

    private void Start()
    {
        target = Player.Instance.transform;
    }

    private void Update()
    {
        if (target == null) return;
        
        currentState.UpdateState();
    }
    
    public void ChangeState(IEnemyState newState)
    {
        currentState.ExitState();

        currentState = newState;
        
        currentState.EnterState();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, perceiveTargetDistance);
            
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackTargetDistance);
    }
}
