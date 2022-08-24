using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseEnterManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Tilemap roof;
    [SerializeField] Tilemap frontWall;
    [SerializeField] CompositeCollider2D compositeCollider2D;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mainCamera.orthographicSize = 3;
        roof.color = new Color(1, 1, 1, 0);
        frontWall.color = new Color(1, 1, 1, 0);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mainCamera.orthographicSize = 6;
        roof.color = new Color(1, 1, 1, 1);
        frontWall.color = new Color(1, 1, 1, 1);
    }
}
