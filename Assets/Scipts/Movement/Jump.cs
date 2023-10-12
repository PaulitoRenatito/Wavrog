using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    
    [SerializeField] private float jumpforce = 5.5f;
    [SerializeField] private bool isJumping;
    
    private Rigidbody myRigidbody;
    
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (isJumping)
        {
            isJumping = IsOnTheGround();
        }
    }
    
    public void UpdateMovement(Vector3 moveDir)
    {
        if (IsOnTheGround())
        {
            isJumping = true;
            myRigidbody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    
    private bool IsOnTheGround()
    {
        float raycastMaxDistance = 1f;
        Vector3 raycastOrigin = transform.position + Vector3.up;
        return Physics.Raycast(raycastOrigin, Vector3.down, raycastMaxDistance);
    }
    
    public bool IsJumping()
    {
        return isJumping;
    }
}
