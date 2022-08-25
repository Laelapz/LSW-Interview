using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLogic : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] CheckSurroundings _checkSurroundings;
    [SerializeField] InteractableAnimation _interactableAnimation;

    private void Update()
    {
        var collision = _checkSurroundings.SearchForInteractable();

        if (collision.collider != null)
        {
            _interactableAnimation.ShowIcon();
        }
        else
        {
            _interactableAnimation.HideIcon();
        }
    }

    public void Interact()
    {
        _shopManager.OpenShopMenu();
    }
}
