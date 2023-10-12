using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerWeapon : WeaponMeleeBase
{
    
    public override void Attack()
    {
        if (isAttacking) return;
        
        isAttacking = true;
        weaponCollider.enabled = true;
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

        Vector3 beginPosition = new Vector3(-0.55f, 0.5f, 0.2f);
        Vector3 endPosition = new Vector3(-0.55f, 0f, 0.4f);
        Vector3 position = Vector3.Lerp(beginPosition, endPosition, progress);
            
        Quaternion beginRotation = Quaternion.Euler(Vector3.zero);
        Quaternion endRotation = Quaternion.Euler(90f, 0f, 0f);
        Quaternion rotation = Quaternion.Slerp(beginRotation, endRotation, progress);

        transform.localPosition = position;
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
        
        Vector3 beginPosition = Vector3.zero;
        Vector3 endPosition = transform.localPosition;
        Vector3 position = Vector3.Lerp(endPosition, beginPosition, progress);
        
        Quaternion begin = Quaternion.Euler(Vector3.zero);
        Quaternion end = transform.localRotation;
        Quaternion rotation = Quaternion.Slerp(end, begin, progress);
        
        transform.localPosition = position;
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
