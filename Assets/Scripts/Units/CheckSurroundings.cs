using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSurroundings : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public RaycastHit2D SearchForInteractable()
    {
        return Physics2D.CircleCast(transform.position, 2, Vector2.zero, 2, layerMask);   
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
