using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ladder : MonoBehaviour, IInteractable
{
    
    [SerializeField] private Transform bottomHoldingPoint;
    [SerializeField] private Transform topHoldingPoint;
    private Vector3 bottomHoldingPointDefaultPosition;
    [SerializeField] private bool isOnLadder;
    [SerializeField] private bool isGoingUp;

    private Interacter interacter;

    private void Start()
    {
        bottomHoldingPointDefaultPosition = bottomHoldingPoint.position;
    }

    public void Interact(Interacter interacter)
    {

        this.interacter = interacter;
        interacter.SetInteracting(true);

        isGoingUp = IsGoingUp();

        if (!isOnLadder)
        {
            if (!isGoingUp)
            {
                bottomHoldingPoint.position += (Vector3.up * topHoldingPoint.position.y);
            }
            
            interacter.transform.position = bottomHoldingPoint.position;
            interacter.transform.parent = bottomHoldingPoint;
            //player.GetMovement().GetRigidbody().useGravity = false;
        }
        else
        {
            interacter.transform.parent = null;
            //player.GetMovement().GetRigidbody().useGravity = true;
            bottomHoldingPoint.position = bottomHoldingPointDefaultPosition;
        }
        
        isOnLadder = !isOnLadder;
    }

    private void Update()
    {
        if (isOnLadder)
        {
            if (isGoingUp)
            {
                bottomHoldingPoint.position += (Vector3.up/10f); // 0.1f up in Y axis

                if (bottomHoldingPoint.position.y >= topHoldingPoint.position.y)
                {
                    interacter.transform.parent = null;
                    //player.GetMovement().GetRigidbody().useGravity = true;
                    isOnLadder = false;
                    bottomHoldingPoint.position = bottomHoldingPointDefaultPosition;
                    interacter.transform.position = topHoldingPoint.position;
                    interacter.SetInteracting(false);
                }
            }
            else
            {
                bottomHoldingPoint.position += (Vector3.down/10f); // 0.1f down in Y axis

                if (bottomHoldingPoint.position.y <= bottomHoldingPointDefaultPosition.y)
                {
                    interacter.transform.parent = null;
                    //player.GetMovement().GetRigidbody().useGravity = true;
                    isOnLadder = false;
                    bottomHoldingPoint.position = bottomHoldingPointDefaultPosition;
                    interacter.SetInteracting(false);
                }
            }
        }
    }

    private bool IsGoingUp()
    {
        float bottom = Mathf.Abs(interacter.transform.position.y - bottomHoldingPoint.position.y);
        float top = Mathf.Abs(interacter.transform.position.y - topHoldingPoint.position.y);
        
        Debug.Log("Bottom: " + interacter.transform.position.y + " - " + bottomHoldingPoint.position.y + " = " + bottom);
        Debug.Log("Top: " + interacter.transform.position.y + " - " + topHoldingPoint.position.y + " = " + top);

        return bottom < top;
    }
}
