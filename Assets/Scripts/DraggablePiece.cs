using System.Collections.Generic;
using UnityEngine;

public class DraggablePiece : MonoBehaviour
{
    public List<Vector2Int> shape = new List<Vector2Int>();

    private Vector3 startPos;
    private Camera cam;
    private GameManager manager;

    void Start()
    {
        cam = Camera.main;
        manager = FindObjectOfType<GameManager>();
        startPos = transform.position;
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
            transform.position = startPos;
        }
    }
}
