using System;
using UnityEngine;

public class Holder : MonoBehaviour, IHoldableObjectParent
{
    
    public event EventHandler<OnPickedSomethingEventsArgs> OnPickedSomething;

    public class OnPickedSomethingEventsArgs : EventArgs
    {
        public HoldableObject holdableObject;
    }
    
    [SerializeField] private Transform rightHoldPoint;
    [SerializeField] private Transform leftHoldPoint;
    [SerializeField] private HoldableObject holdableObject;
    
    public Transform GetHoldableObjectFollowTransform()
    {
        return rightHoldPoint;
    }

    public Transform GetHoldableObjectFollowTransformAlternate()
    {
        return leftHoldPoint;
    }

    public void SetHoldableObject(HoldableObject holdableObject)
    {
        this.holdableObject = holdableObject;

        if (holdableObject != null)
        {
            OnPickedSomething?.Invoke(this, new OnPickedSomethingEventsArgs()
            {
                holdableObject = this.holdableObject
            });
        }
    }

    public HoldableObject GetHoldableObject()
    {
        return holdableObject;
    }

    public void ClearHoldableObject()
    {
        holdableObject = null;
    }

    public bool HasHoldableObject()
    {
        return holdableObject != null;
    }
}
