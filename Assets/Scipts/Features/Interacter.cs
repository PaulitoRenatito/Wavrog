using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [ReadOnly][SerializeField] private bool isInteracting;
    [SerializeField] private LayerMask interactablesLayerMask;

    [Header("RayCast")] 
    [SerializeField] private float rayHeightOffset = 0.1f;
    [SerializeField] private float interactDistance = 2f;
    
    private Vector3 lastInteractDir;
    
    private IInteractable selectedInteractable;
    
    public event EventHandler<OnSelectedInteractableChangeEventsArgs> OnSelectedInteractableChange;
    public class OnSelectedInteractableChangeEventsArgs : EventArgs
    {
        public IInteractable selectedInteractable;
    }

    public void Interact()
    {
        selectedInteractable?.Interact(this);
    }
    
    public void HandleInteractions()
    {

        Transform myTransform = transform;

        lastInteractDir = myTransform.forward;

        Vector3 rayOrigin = myTransform.position + (Vector3.up * rayHeightOffset);

        if (Physics.Raycast(rayOrigin, lastInteractDir, out RaycastHit raycastHit,
                interactDistance, interactablesLayerMask))
        {
                
            Debug.DrawRay(rayOrigin, myTransform.forward * interactDistance, Color.green);

            if (raycastHit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable != selectedInteractable)
                {
                    SetSelectedInteractable(interactable);
                }
            }
            else
            {
                SetSelectedInteractable(null);
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin, myTransform.forward * interactDistance, Color.red);
                
            SetSelectedInteractable(null);
        }
    }

    private void SetSelectedInteractable(IInteractable selectedInteractable)
    {
        this.selectedInteractable = selectedInteractable;
    
        OnSelectedInteractableChange?.Invoke(this, new OnSelectedInteractableChangeEventsArgs()
        {
            selectedInteractable = this.selectedInteractable
        });
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void SetInteracting(bool isInteracting)
    {
        this.isInteracting = isInteracting;
    }
}
