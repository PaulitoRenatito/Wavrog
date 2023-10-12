using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponMeleeBase : MonoBehaviour, IWeapon
{
    
    protected enum AttackState
    {
        ExecutingAttack,
        WaitingAfterAttack,
        ReturningToInitialPosition
    }

    [ReadOnly][SerializeField] protected AttackState attackState;
    [ReadOnly][SerializeField] protected bool isAttacking;

    [SerializeField] protected Damage damage;

    [SerializeField] protected float attackDuration = 1f;
    [SerializeField] protected float waitAfterAttackDuration = .25f;
    [SerializeField] protected float returnToInitialPositionDuration = .25f;

    protected Collider weaponCollider;
    protected float timer;
    
    protected virtual void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    public abstract void Attack();

    public abstract bool IsAttacking();
    
    private void OnTriggerEnter(Collider other)
    {
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        damageable?.ReceiveDamage(damage);
        
    }
    
}
