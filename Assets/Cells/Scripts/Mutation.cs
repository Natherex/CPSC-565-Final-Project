/**
 * Authors: Isha Afzaal, Sammy Elrafih, Ainslie Veltheon
 * Mutation.cs is a helper class used by CellBehaviour.cs for doing cell mutations.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mutation
{

    /*
     * Helper functions for mutating cells
     */
    public static int mutateInt(int original, float mutationRate, System.Random rand)
    {
        int range = (int)(mutationRate * 10);
        int modifier = Random.Range(-range, range);
        return Mathf.Abs(original + modifier);
    }

    public static float mutateFloat(float original, float mutationRate, System.Random rand)
    {
        float range = original * mutationRate;
        float modifier = (rand.Next((int)(-range * 100), (int)(range * 100))) / 100f;
        return Mathf.Abs(original + modifier);
    }

}
