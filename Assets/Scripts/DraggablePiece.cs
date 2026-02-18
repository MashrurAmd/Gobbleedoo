using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DraggablePiece : MonoBehaviour
{
    public List<Vector2Int> shape = new List<Vector2Int>();

    private Vector3 startPosition;
    private Camera cam;
    private GameManager manager;

    void Start()
    {
        cam = Camera.main;
        manager = FindObjectOfType<GameManager>();
        startPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        transform.position = mouse;
    }

    void OnMouseUp()
    {
        Vector2Int gridPos = manager.WorldToGrid(transform.position);

        if (manager.TryPlace(shape, gridPos))
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
