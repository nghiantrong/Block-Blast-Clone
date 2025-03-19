using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    public int gridSize = 8;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GridLayoutGroup gridLayout = gridParent.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = gridSize;
        }

        
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            Instantiate(cellPrefab, gridParent);
        }
    }
}
