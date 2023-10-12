using System;
using UnityEngine;

public class ObjectHolder : MonoBehaviour, IHoldableObjectParent, IInteractable
{
    public static event EventHandler OnAnyObjectPlacedHere;
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private Transform holdingPoint;
    [SerializeField] private HoldalbeObjectSO holdalbeObjectSO;
    private HoldableObject holdableObject;

    private void Start()
    {
        if (holdalbeObjectSO != null)
        {
            HoldableObject initialHoldableObject = HoldableObject.SpawnHoldableObject(holdalbeObjectSO, this);
            SetHoldableObject(initialHoldableObject);
        }
    }

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    
    public virtual void Interact(Interacter interacter)
    {
        if (!interacter.TryGetComponent(out Holder holder)) return;
        
        if (!this.HasHoldableObject())
        {
            if (holder.HasHoldableObject())
            {
                holder.GetHoldableObject().SetHoldalbeObjectParent(this);
            }
        }
        else
        {
            if (holder.HasHoldableObject())
            {
                HoldableObject playerHoldableObject = holder.GetHoldableObject();
                HoldableObject objectHolderObject = this.GetHoldableObject();
                HoldableObject Aux = playerHoldableObject;
                
                playerHoldableObject.SetHoldalbeObjectParent(this);
                objectHolderObject.SetHoldalbeObjectParent(holder);
                
                this.SetHoldableObject(Aux);
            }
            else
            {
                GetHoldableObject().SetHoldalbeObjectParent(holder);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }

    }

    public Transform GetHoldableObjectFollowTransform()
    {
        return holdingPoint;
    }

    public Transform GetHoldableObjectFollowTransformAlternate()
    {
        throw new NotImplementedException();
    }

    public void SetHoldableObject(HoldableObject holdableObject)
    {
        this.holdableObject = holdableObject;

        if (holdableObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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
