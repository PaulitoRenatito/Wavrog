using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedInteractableVisual : MonoBehaviour
{
    [SerializeField] private GameObject interactableGameObject;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private IInteractable interactable;
    
    void Start()
    {
        interactableGameObject.TryGetComponent(out interactable);
        Player.Instance.GetInteracter().OnSelectedInteractableChange += Interacter_OnSelectedInteractableChange;
    }

    private void Interacter_OnSelectedInteractableChange(object sender, Interacter.OnSelectedInteractableChangeEventsArgs e)
    {
        if (e.selectedInteractable == interactable)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
