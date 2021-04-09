using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://youtu.be/waEsGu--9P8 

public class Grid
{
    private int width;
    private int depth;
    private int[,] gridArray; // Holds positions
    private int[,] nutrientLevelArray; // Holds nutrient levels for those positions
    private int cellSize;

    public Grid(int width, int depth, int cellSize, int nutrientStartLevel)
    {
        this.width = width;
        this.depth = depth;
        this.cellSize = cellSize;

        gridArray = new int[width, depth];
        nutrientLevelArray = new int[width, depth];

        for (int x = 0; x < gridArray.GetLength(0); x++ )
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // Visual component of grid
                Debug.DrawLine(getWorldPosition(x,z), getWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x, z), getWorldPosition(x + 1, z), Color.white, 100f);

                // Assign a start nutrient level to the position in the nutrient
                // array correlating with each unit of grid
                nutrientLevelArray[x, z] = nutrientStartLevel;

            }
        }
    }


    private Vector3 getWorldPosition(int x, int z)
    {
        return new Vector3(x, 1, z) * cellSize;
    }


    public int getGridLevel(float cellX, float cellZ, out int gridX, out int gridZ)
    {
        int nutrientLevel = 0;

        gridX = 0;
        gridZ = 0; 

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                if (cellX <= x && cellZ <= z)
                {
                    nutrientLevel = nutrientLevelArray[x, z];
                    gridX = x;
                    gridZ = z;
                }
                    
            }
        }

        return nutrientLevel;
    }

    public void subtractNutrientLevel(int gridX, int gridZ)
    {
        Debug.Log("BEFORE "+nutrientLevelArray[gridX, gridZ]);
        nutrientLevelArray[gridX, gridZ] = nutrientLevelArray[gridX, gridZ] - 2;
        Debug.Log("AFTER " + nutrientLevelArray[gridX, gridZ]);
    }
}
