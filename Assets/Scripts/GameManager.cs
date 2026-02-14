using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public Transform gridOrigin;

    public BlockPool pool;
    public TMP_Text scoreText;

    private GridSystem grid;
    private Dictionary<Vector2Int, GameObject> activeBlocks = new Dictionary<Vector2Int, GameObject>();

    private int score;

    void Start()
    {
        grid = new GridSystem(width, height);
        UpdateScore(0);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 local = worldPos - gridOrigin.position;
        int x = Mathf.FloorToInt(local.x / cellSize);
        int y = Mathf.FloorToInt(local.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return gridOrigin.position + new Vector3(
            gridPos.x * cellSize,
            gridPos.y * cellSize,
            0);
    }

    public bool TryPlace(List<Vector2Int> shape, Vector2Int basePos)
    {
        if (!grid.CanPlace(shape, basePos))
            return false;

        int placedCount = grid.Place(shape, basePos);

        // Spawn visuals
        for (int i = 0; i < shape.Count; i++)
        {
            Vector2Int pos = basePos + shape[i];
            GameObject block = pool.Get();
            block.transform.position = GridToWorld(pos);
            activeBlocks[pos] = block;
        }

        UpdateScore(placedCount * 10);

        // Clear lines
        int lines = grid.ClearLines(out List<Vector2Int> cleared);

        if (lines > 0)
        {
            for (int i = 0; i < cleared.Count; i++)
            {
                if (activeBlocks.TryGetValue(cleared[i], out GameObject block))
                {
                    pool.Return(block);
                    activeBlocks.Remove(cleared[i]);
                }
            }

            UpdateScore(lines * 100);
        }

        return true;
    }

    void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
