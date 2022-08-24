using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeLogic : MonoBehaviour, IInteractable
{
    [SerializeField] CheckSurroundings _checkSurroundings;
    [SerializeField] InteractableAnimation _interactableAnimation;
    [SerializeField] PlayerMenuManager _playerMenuManager;

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
        print("opa");
        _playerMenuManager.SetupMenu();
    }
}
