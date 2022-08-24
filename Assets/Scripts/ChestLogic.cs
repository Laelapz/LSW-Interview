using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour, IInteractable
{
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
        print("Abrir menu");
    }
}
