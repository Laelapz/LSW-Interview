using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBrain : MonoBehaviour, IInteractable
{
    [SerializeField] BodyPartsManager bodyPartsManager;
    [SerializeField] BodyPartsSelector bodyPartsSelector;

    [SerializeField] SO_BodyPart[] bodyBodyParts;
    [SerializeField] SO_BodyPart[] topBodyParts;
    [SerializeField] SO_BodyPart[] bottomBodyParts;

    [SerializeField] CheckSurroundings _checkSurroundings;
    [SerializeField] InteractableAnimation _interactableAnimation;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _clothesPanel;
    [SerializeField] Rigidbody2D _rigidbody;
    public SO_CharacterBody npcBody;
    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    private void Start()
    {
       
        npcBody = ScriptableObject.CreateInstance("SO_CharacterBody") as SO_CharacterBody;

        var bodyPart = bodyBodyParts[Random.Range(0, bodyBodyParts.Length)];
        var topPart = topBodyParts[Random.Range(0, topBodyParts.Length)];
        var bottomPart = bottomBodyParts[Random.Range(0, bottomBodyParts.Length)];

        npcBody.Init( bodyPart, topPart, bottomPart );

        bodyPartsManager._characterBody = npcBody;
        bodyPartsSelector._characterBody = npcBody;

        bodyPartsManager.Start();
    }

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
        _clothesPanel.SetActive(true);
        _clothesPanel.GetComponent<BodyPartsSelector>()._bodyPartSelections = inventoryParts;
    }

    public void ClosePanel()
    {
        _clothesPanel.SetActive(false);
    }
}
