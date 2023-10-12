using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{

    [SerializeField] private StateMachine state;
    [SerializeField] public Transform target;
    [SerializeField] public float perceiveTargetDistance = 8f;
    [SerializeField] public float attackTargetDistance = 1.5f;

    public MovementNavMesh movement;
    public Holder holder;

    private void Awake()
    {
        movement = GetComponent<MovementNavMesh>();
        holder = GetComponent<Holder>();
    }

    void Start()
    {
        state = new StateMachine(this);
        state.EnterState(StateMachine.EnemyState.Idle);
        target = Player.Instance.transform;
    }

    private void Update()
    {
        state?.Update();
    }
}
