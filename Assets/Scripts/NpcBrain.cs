using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class NpcBrain : MonoBehaviour, IInteractable
{
    [SerializeField] BodyPartsManager bodyPartsManager;
    [SerializeField] BodyPartsSelector bodyPartsSelector;

    [SerializeField] SO_BodyPart[] hairBodyParts;
    [SerializeField] SO_BodyPart[] bodyBodyParts;
    [SerializeField] SO_BodyPart[] topBodyParts;
    [SerializeField] SO_BodyPart[] bottomBodyParts;

    [SerializeField] CheckSurroundings _checkSurroundings;
    [SerializeField] InteractableAnimation _interactableAnimation;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _clothesPanel;
    [SerializeField] Rigidbody2D _rigidbody;

    private bool _isWalking = false;
    private float _speed = 2f;
    private Vector2 _movement = Vector2.zero;

    public bool canWalk = true;
    public SO_CharacterBody npcBody;
    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    private void Start()
    {
       
        npcBody = ScriptableObject.CreateInstance("SO_CharacterBody") as SO_CharacterBody;

        var hairPart = hairBodyParts[Random.Range(0, hairBodyParts.Length)];
        var bodyPart = bodyBodyParts[Random.Range(0, bodyBodyParts.Length)];
        var topPart = topBodyParts[Random.Range(0, topBodyParts.Length)];
        var bottomPart = bottomBodyParts[Random.Range(0, bottomBodyParts.Length)];

        npcBody.Init( hairPart, bodyPart, topPart, bottomPart );

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
        WalkInRandomDirection();

        _rigidbody.velocity = _movement * _speed;

        }

    private async void WalkInRandomDirection()
    {
        if (!canWalk)
        {
            _movement.x = 0; _movement.y = 0;
            UpdateAnimatorVariables();

            return;
        }

        if (_isWalking ) return;

        _movement.x = Random.Range(-1, 2);
        _movement.y = Random.Range(-1, 2);

        UpdateAnimatorVariables();

        await StopWalking();
    }
    

    private void UpdateAnimatorVariables()
    {
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private async Task StopWalking()
    {
        _isWalking = true;
        float timeEnd = Time.time + 2;

        while(Time.time < timeEnd)
        {
            await Task.Yield();
        }

        _isWalking = false;
    }

    public void Interact()
    {
        canWalk = false;
        _clothesPanel.SetActive(true);
        _clothesPanel.GetComponent<BodyPartsSelector>()._bodyPartSelections = inventoryParts;
    }

    public void ClosePanel()
    {
        canWalk = true;
        _clothesPanel.SetActive(false);
    }
}
