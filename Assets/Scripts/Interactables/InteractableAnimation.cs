using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimation : MonoBehaviour
{
    Vector3 _newPosition = Vector3.zero;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        _newPosition = transform.position;
        _newPosition.y += Mathf.Sin(Time.time*2) * Time.deltaTime * 0.2f;
        transform.position = _newPosition;
    }

    public void SetIcon(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void ShowIcon()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void HideIcon()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 0);
    }
}
