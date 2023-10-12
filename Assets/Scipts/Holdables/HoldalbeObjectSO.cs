using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Holdables")]
public class HoldalbeObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectNames;
}
