using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldableObjectParent
{
    public Transform GetHoldableObjectFollowTransform();
    
    public Transform GetHoldableObjectFollowTransformAlternate();

    public void SetHoldableObject(HoldableObject holdableObject);

    public HoldableObject GetHoldableObject();

    public void ClearHoldableObject();

    public bool HasHoldableObject();
}
