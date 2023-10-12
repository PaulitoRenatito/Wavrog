using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("FORM")]
    [SerializeField] private float moverRadius = .7f;
    [SerializeField] private float moverHeight = 2f;
    
    [Header("WALK")]
    [SerializeField] private float walkSpeed = 7f;
    [ReadOnly][SerializeField] private bool isWalking;
    public bool IsWalking => isWalking;
    
    [Header("ROTATE")]
    [SerializeField] private float rotateSpeed = 5f;

    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveDirection)
    {
        
        float moveDistance = Time.deltaTime * walkSpeed;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * moverHeight,
            moverRadius, moveDirection, moveDistance);
        
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x < -.5f || moveDirection.x > +.5f) && 
                      !Physics.CapsuleCast(
                          transform.position, 
                          transform.position + Vector3.up * moverHeight, 
                          moverRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = (moveDirection.z < -.5f || moveDirection.z > +.5f) && 
                          !Physics.CapsuleCast(
                              transform.position, 
                              transform.position + Vector3.up * moverHeight, 
                              moverRadius, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            transform.position += moveDirection * moveDistance;
        }

        isWalking = (moveDirection != Vector3.zero);

    }

    public void Rotate(Quaternion rotateDirection)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateDirection, Time.deltaTime * rotateSpeed);
    }

    public void Dash(Vector3 dashDirection)
    {
        dashDirection = new Vector3(dashDirection.x, 0f, dashDirection.z);
        myRigidbody.AddForce(dashDirection * 10f, ForceMode.Impulse);
    }
    
}
