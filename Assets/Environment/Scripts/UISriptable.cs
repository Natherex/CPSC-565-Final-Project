using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "UISettings", menuName = "ScriptableObjects/UISettings", order = 0)]
public class UISriptable : ScriptableObject
{
    [Header("Settings found in the simulation UI")]

    [Tooltip("How many times a cell can reproduce")]
    [SerializeField] public int reproductionLimit = 5;

    [Tooltip("Value of energy the agar holds")]
    [SerializeField] public int agarLevel = 1000;

    [Tooltip("The radius of how far the antibiotic can reach")]
    [SerializeField] public float ABRadius = 2f;

    [Tooltip("How much energy a cell must have before splitting")]
    [SerializeField] public float splitThreshold = 2f;
    // Make a % for the probability it will kill a cell in it's radius
    [Tooltip("How strong/lethal tetracycline is against cells")]
    [SerializeField] public int tetStrength = 5;

    [Tooltip("How much energy the cells each have")]
    [SerializeField] public int energy = 100;

    [Tooltip("How many cells the system starts with")]
    [SerializeField] public int numberOfCells = 10;

    [Tooltip("Mutation rate of LAI-1 production")]
    [SerializeField] public float LAI_1MutationRate = 10f;

    [Tooltip("Mutation rate of cell reproduction")]
    [SerializeField] public float reproductionMutationRate = 10f;

    [Tooltip("Mutation rate of the QS threshold")]
    [SerializeField] public float qsThresholdMutationRate = 10f;

}

// https://www.youtube.com/watch?v=aPXvoWVabPY 