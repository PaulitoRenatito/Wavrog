using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Damage
{

    [SerializeField] private int rawDamage;
    [SerializeField] private DamageType damageType;

    public int RawDamage => rawDamage;
    public DamageType DamageType => damageType;
    

    public Damage(int rawDamage)
    {
        this.rawDamage = rawDamage;
    }
    
    public Damage(int rawDamage, DamageType damageType)
    {
        this.rawDamage = rawDamage;
        this.damageType = damageType;
    }
}
