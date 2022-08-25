using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    [SerializeField] Image _image;
    [SerializeField] Sprite[] _moodIcons;
    [SerializeField] TMP_Text[] _bodyOptionsText;
    public Transform[] path;
    
    [SerializeField] private float moveSpeed = 2f;

    private bool _isWalking = false;
    private bool _buy = false;
    private bool _followingPath = true;
    private int waypointIndex = 0;
    private int moodState = 3;
    private float _speed = 2f;
    private Vector2 _movement = Vector2.zero;
    private Vector2 _previousPosition = Vector2.zero;
    

    public bool canWalk = true;
    public GameManager gameManager;
    public SO_CharacterBody npcBody;

    private void Start()
    {
        transform.position = path[waypointIndex].transform.position;

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
            _image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _image.color = new Color(1, 1, 1, 0);
        }

        _previousPosition = transform.position;

        if (_followingPath)
        {
            Move();
        }
        else
        {
            WalkInRandomDirection();
            _rigidbody.velocity = _movement * _speed;
        }


    }

    private async void Move()
    {
        if (waypointIndex <= path.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
               path[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            _movement.x = (transform.position.x - _previousPosition.x ) / Time.deltaTime;
            _movement.y = (transform.position.y - _previousPosition.y) / Time.deltaTime;

            UpdateAnimatorVariables();

            if (transform.position == path[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
        else
        {
            if (moodState <= 0)
            {
                gameManager.actualNpcNumber--;
                Destroy(gameObject);
            }
            else
            {
                _followingPath = false;
                await WaitForAttendance();

            }

        }
    }

    private async void WalkInRandomDirection()
    {
        if (_isWalking) return;

        if (!canWalk)
        {
            _movement.x = 0; _movement.y = 0;
            UpdateAnimatorVariables();

            return;
        }


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

    private async Task WaitForAttendance()
    {
        if (_buy) return;

        if(moodState <= 0 )
        {
            InvertPathToFollow();
            return;
        }

        float timeEnd = Time.time + 10;
        _interactableAnimation.SetIcon(_moodIcons[moodState-1]);
        _interactableAnimation.ShowIcon();

        while (Time.time < timeEnd)
        {
            await Task.Yield();
        }

        moodState--;
        await WaitForAttendance();
    }

    private void InvertPathToFollow()
    {
        Transform temp = this.path[0];
        this.path[0] = path[2];
        this.path[2] = temp;
        this.waypointIndex = 0;

        this._followingPath = true;
        _interactableAnimation.HideIcon();
    }

    private void PayPlayer()
    {
        gameManager.ReciveMoney(moodState * 20);
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
        if (moodState < 0 || _followingPath) return;
        
        canWalk = false;
        _buy = true;
        _clothesPanel.SetActive(true);
        var bodyPartsManager = _clothesPanel.GetComponent<BodyPartsSelector>();
        bodyPartsManager._bodyPartSelections = gameManager.inventoryParts;
        bodyPartsManager._bodyPartSelections[0].bodyPartNameTextComponent = _bodyOptionsText[0];
        bodyPartsManager._bodyPartSelections[1].bodyPartNameTextComponent = _bodyOptionsText[1];
        bodyPartsManager._bodyPartSelections[2].bodyPartNameTextComponent = _bodyOptionsText[2];
        bodyPartsManager._bodyPartSelections[3].bodyPartNameTextComponent = _bodyOptionsText[3];
    }

    public void ClosePanel()
    {
        canWalk = true;
        _clothesPanel.SetActive(false);
        InvertPathToFollow();
        PayPlayer();
        moodState = 0;
    }
}
