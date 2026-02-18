using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public Transform gridOrigin;
    public Color lineColor = Color.gray;

    void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        for (int x = 0; x <= width; x++)
        {
            DrawLine(
                gridOrigin.position + new Vector3(x * cellSize, 0, 0),
                gridOrigin.position + new Vector3(x * cellSize, height * cellSize, 0)
            );
        }

        for (int y = 0; y <= height; y++)
        {
            DrawLine(
                gridOrigin.position + new Vector3(0, y * cellSize, 0),
                gridOrigin.position + new Vector3(width * cellSize, y * cellSize, 0)
            );
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject("GridLine");
        line.transform.parent = transform;

        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;
    }
}
