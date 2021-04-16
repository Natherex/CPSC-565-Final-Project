/**
 * Authors: Isha Afzaal, Sammy Elrafih, Ainslie Veltheon
 * Grid.cs is used to divide the petri-dish into a 10 x 10 grid.
 * Nutrient levels are assigned to each part of the array.
 * Reference: https://youtu.be/waEsGu--9P8 
 **/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    private int width;
    private int depth;
    private int[,] gridArray; // Contains positions
    private int[,] nutrientLevelArray; // Contains nutrient levels for those positions

    /*
     *  Initialize visual and nutrient array components of grid
     */
    public Grid(int width, int depth, int nutrientStartLevel)
    {
        this.width = width;
        this.depth = depth;

        gridArray = new int[width, depth];
        nutrientLevelArray = new int[width, depth];

        for (int x = 0; x < gridArray.GetLength(0); x++ )
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // Visual component of grid
                Debug.DrawLine(new Vector3(x, 1, z), new Vector3(x, 1, z + 1), Color.white, 100f);
                Debug.DrawLine(new Vector3(x, 1, z), new Vector3(x + 1,1, z), Color.white, 100f);

                // Assign a start nutrient level to the position in the nutrient array correlating with each unit of grid
                nutrientLevelArray[x, z] = nutrientStartLevel;
            }
        }
    }

    /*
     * Get the nutrient level at a place in the grid
     */
    public int getGridLevel(int cellX, int cellZ, out int gridX, out int gridZ)
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

                    return nutrientLevel;
                }
                    
            }
        }
        return nutrientLevel;
    }

    /*
     * Decrease nutrient level at a given place on the grid
     */
    public void subtractNutrientLevel(int gridX, int gridZ)
    {
        nutrientLevelArray[gridX, gridZ] = nutrientLevelArray[gridX, gridZ] - 2;
    }

    /*
     * Reset nutrient level at a grid level
     */
    public void resetNutrientLevels(int newAgarLevel)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // Reset each unit in grid with new agar level / 100
                nutrientLevelArray[x, z] = (newAgarLevel / 100);
            }
        }
    }
}
