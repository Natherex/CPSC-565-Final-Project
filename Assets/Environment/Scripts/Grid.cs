using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inspired by https://youtu.be/waEsGu--9P8 
// Code assumes grid will always be 10 x 10, which is true for this simulation
public class Grid
{
    private int width;
    private int depth;
    private int[,] gridArray; // Holds positions
    private int[,] nutrientLevelArray; // Holds nutrient levels for those positions

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
                //Debug.DrawLine(getWorldPosition(x,z), getWorldPosition(x, z + 1), Color.white, 100f);
                //Debug.DrawLine(getWorldPosition(x, z), getWorldPosition(x + 1, z), Color.white, 100f);

                Debug.DrawLine(new Vector3(x, 1, z), new Vector3(x, 1, z + 1), Color.white, 100f);
                Debug.DrawLine(new Vector3(x, 1, z), new Vector3(x + 1,1, z), Color.white, 100f);

                // Assign a start nutrient level to the position in the nutrient
                // array correlating with each unit of grid
                nutrientLevelArray[x, z] = nutrientStartLevel;

                //Debug.Log(nutrientLevelArray[x, z]);

            }
        }
    }


    public int getGridLevel(int cellX, int cellZ, out int gridX, out int gridZ)
    {
        int nutrientLevel = 0;

        gridX = 0;
        gridZ = 0;

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                //Debug.Log("x" + x+" z"+z);
                if (cellX <= x && cellZ <= z)
                {
                    //Debug.Log("cellX is "+cellX + " cellZ is " + cellZ);
                   // Debug.Log("found position at " + x + " " + z+"\nthe nutrient level here is "+ nutrientLevelArray[x, z]);
                    
                    nutrientLevel = nutrientLevelArray[x, z];
                    gridX = x;
                    gridZ = z;

                    return nutrientLevel;
                }
                    
            }
        }

        return nutrientLevel;
    }


    public void subtractNutrientLevel(int gridX, int gridZ)
    {
        //Debug.Log("BEFORE "+nutrientLevelArray[gridX, gridZ] + " position: "+ gridX+" "+gridZ);
        nutrientLevelArray[gridX, gridZ] = nutrientLevelArray[gridX, gridZ] - 2;
        //Debug.Log("AFTER " + nutrientLevelArray[gridX, gridZ]);
    }


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
