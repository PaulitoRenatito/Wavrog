using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    [SerializeField] private HoldalbeObjectSO holdalbeObjectSO;

    private IHoldableObjectParent holdableObjectParent;

    public void SetHoldalbeObjectParent(IHoldableObjectParent holdableObjectParent)
    {
        if (this.holdableObjectParent != null)
        {
            this.holdableObjectParent.ClearHoldableObject();
        }

        this.holdableObjectParent = holdableObjectParent;

        if (holdableObjectParent.HasHoldableObject())
        {
            Debug.LogWarning("IHoldableObjectParent already have a HoldableObject");
        }
        
        holdableObjectParent.SetHoldableObject(this);

        transform.parent = holdableObjectParent.GetHoldableObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    
    public static HoldableObject SpawnHoldableObject(HoldalbeObjectSO holdalbeObjectSO,
        IHoldableObjectParent holdableObjectParent)
    {
        Transform holdableObjectTransform = Instantiate(holdalbeObjectSO.prefab);
        
        HoldableObject holdableObject = holdableObjectTransform.GetComponent<HoldableObject>();
        
        holdableObject.SetHoldalbeObjectParent(holdableObjectParent);

        return holdableObject;
    }
    
    public void DestroySelf()
    {
        holdableObjectParent.ClearHoldableObject();
        
        Destroy(gameObject);
    }
    
    public HoldalbeObjectSO GetHoldalbeObjectSO()
    {
        return holdalbeObjectSO;
    }
    
    public IHoldableObjectParent GetHoldableObjectParent()
    {
        return holdableObjectParent;
    }
    
    
}
