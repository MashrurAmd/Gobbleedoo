using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    public int Width { get; }
    public int Height { get; }

    private int[,] grid;

    public GridSystem(int width, int height)
    {
        Width = width;
        Height = height;
        grid = new int[width, height];
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public bool IsEmpty(int x, int y)
    {
        return grid[x, y] == 0;
    }

    public bool CanPlace(List<Vector2Int> shape, Vector2Int basePos)
    {
        for (int i = 0; i < shape.Count; i++)
        {
            int x = basePos.x + shape[i].x;
            int y = basePos.y + shape[i].y;

            if (!IsInside(x, y) || !IsEmpty(x, y))
                return false;
        }

        return true;
    }

    public int Place(List<Vector2Int> shape, Vector2Int basePos)
    {
        for (int i = 0; i < shape.Count; i++)
        {
            int x = basePos.x + shape[i].x;
            int y = basePos.y + shape[i].y;
            grid[x, y] = 1;
        }

        return shape.Count;
    }

    public int ClearLines(out List<Vector2Int> clearedCells)
    {
        clearedCells = new List<Vector2Int>();
        int linesCleared = 0;

        // Check rows
        for (int y = 0; y < Height; y++)
        {
            bool full = true;
            for (int x = 0; x < Width; x++)
            {
                if (grid[x, y] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                linesCleared++;
                for (int x = 0; x < Width; x++)
                {
                    grid[x, y] = 0;
                    clearedCells.Add(new Vector2Int(x, y));
                }
            }
        }

        // Check columns
        for (int x = 0; x < Width; x++)
        {
            bool full = true;
            for (int y = 0; y < Height; y++)
            {
                if (grid[x, y] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                linesCleared++;
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = 0;
                    clearedCells.Add(new Vector2Int(x, y));
                }
            }
        }

        return linesCleared;
    }
}
