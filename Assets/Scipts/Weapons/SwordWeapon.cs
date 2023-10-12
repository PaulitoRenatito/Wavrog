using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponMeleeBase
{
    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            weaponCollider.enabled = true;
        }
    }

    public override bool IsAttacking()
    {
        return isAttacking;
    }
    
    private void Update()
    {
        if (!isAttacking) return;

        switch (attackState)
        {
            case AttackState.ExecutingAttack:
                ExecuteAttack();
                break;
            case AttackState.WaitingAfterAttack:
                WaitAfterAttack();
                break;
            case AttackState.ReturningToInitialPosition:
                ReturnToInitialPosition();
                break;
        }
        
    }
    
    private void ExecuteAttack()
    {
        
        timer += Time.deltaTime;

        float progress = timer / attackDuration;
            
        Quaternion begin = Quaternion.Euler(Vector3.zero);
        Quaternion end = Quaternion.Euler(90f, 0f, 0f);
        Quaternion rotation = Quaternion.Slerp(begin, end, progress);

        transform.localRotation = rotation;

        if (timer > attackDuration)
        {
            timer = 0f;

            attackState = AttackState.WaitingAfterAttack;
        }
    }
    
    private void WaitAfterAttack()
    {
        
        timer += Time.deltaTime;
        
        if (timer > waitAfterAttackDuration)
        {
            timer = 0f;

            attackState = AttackState.ReturningToInitialPosition;
        }
    }
    
    private void ReturnToInitialPosition()
    {

        timer += Time.deltaTime;
        
        float progress = timer / returnToInitialPositionDuration;
        
        Quaternion begin = Quaternion.Euler(Vector3.zero);
        Quaternion end = Quaternion.Euler(90f, 0f, 0f);
        Quaternion rotation = Quaternion.Slerp(end, begin, progress);
        
        transform.localRotation = rotation;
        
        if (timer > returnToInitialPositionDuration)
        {
            timer = 0f;

            attackState = AttackState.ExecutingAttack;

            isAttacking = false;
            
            weaponCollider.enabled = false;
        }
    }
}
