using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementNavMesh : MonoBehaviour
{

    [Header("WALK")]
    [ReadOnly][SerializeField] private bool isWalking;
    public bool IsWalking => isWalking;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    public void MoveByOffset(Vector3 moveDirection)
    {
        
        float moveDistance = Time.deltaTime * agent.speed;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * agent.height,
            agent.radius, moveDirection, moveDistance);
        
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x < -.5f || moveDirection.x > +.5f) && 
                      !Physics.CapsuleCast(
                          transform.position, 
                          transform.position + Vector3.up * agent.height, 
                          agent.radius, moveDirX, moveDistance);

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
                              transform.position + Vector3.up * agent.height, 
                              agent.radius, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            agent.Move(moveDirection);
        }

        isWalking = (moveDirection != Vector3.zero);

    }

    public void MoveByDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        if (ReachDestination())
        {
            agent.ResetPath();
            return;
        }
        isWalking = true;
    }

    public bool ReachDestination()
    {
        if (agent.pathPending) return false;

        if (agent.remainingDistance > agent.stoppingDistance) return false;

        if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;
        
        isWalking = false;

        return true;
    }

    public float GetStoppingDistance()
    {
        return agent.stoppingDistance;
    }

    public void Rotate(Quaternion rotateDirection)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateDirection, Time.deltaTime * agent.angularSpeed);
    }
}
