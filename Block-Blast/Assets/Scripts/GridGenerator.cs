using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    public int gridSize = 8;
    public float cellSize = 50f;

    private Vector2[,] gridPositions;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        gridPositions = new Vector2[gridSize, gridSize];
        GridLayoutGroup gridLayout = gridParent.GetComponent<GridLayoutGroup>();

        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = gridSize;
            gridLayout.cellSize = new Vector2(cellSize, cellSize);
        }

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                gridPositions[x, y] = (Vector2)cell.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    public Vector2 GetNearestGridPosition(Vector2 worldPosition)
    {
        RectTransform gridRectTransform = gridParent.GetComponent<RectTransform>();

        // Convert world to local space within the grid
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gridRectTransform,
            worldPosition,
            Camera.main,
            out localPosition
        );

        float cellSizeWithSpacing = cellSize + gridParent.GetComponent<GridLayoutGroup>().spacing.x;

        // Calculate the nearest cell's index
        int col = Mathf.RoundToInt(localPosition.x / cellSizeWithSpacing);
        int row = Mathf.RoundToInt(-localPosition.y / cellSizeWithSpacing);

        // Clamp to grid boundaries
        col = Mathf.Clamp(col, 0, gridSize - 1);
        row = Mathf.Clamp(row, 0, gridSize - 1);

        // Calculate actual cell position
        float snappedX = col * cellSizeWithSpacing + (cellSize / 2);
        float snappedY = -row * cellSizeWithSpacing - (cellSize / 2);

        return new Vector2(snappedX, snappedY);
    }


}
