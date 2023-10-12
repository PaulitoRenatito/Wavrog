using UnityEngine;

public class UpgradeMachine : MonoBehaviour, IInteractable
{
    public void Interact(Interacter interacter)
    {
        if (interacter.TryGetComponent(out Health health))
        {
            health.TotalHealth += 10;
            health.CurrentHealth = health.TotalHealth;
        }
    }
}
