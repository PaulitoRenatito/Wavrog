using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{

    [SerializeField] private ItemSO itemSO;

    public void Interact(Interacter interacter)
    {
        if (interacter.gameObject.TryGetComponent(out Inventory inventory))
        {
            inventory.AddItem(itemSO);
            Destroy(this.gameObject);
        }
    }
    
}
