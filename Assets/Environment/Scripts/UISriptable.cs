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
    [SerializeField] public int agarLevel = 10000;

    [Tooltip("The radius of how far the antibiotic can reach")]
    [SerializeField] public float ABRadius = 2f;

    [Tooltip("The radius of how far cells can interact with LAI-1")]
    [SerializeField] public float QSRadius = 2f;

    [Tooltip("How strong/lethal tetracycline is against cells")]
    [SerializeField] public int tetStrength = 5;

    [Tooltip("How much energy the cells each have")]
    [SerializeField] public int energy = 4000;

    // TODO: NEEDS WORK
    [Tooltip("Rate at which energy is used by each cell")]
    [SerializeField] public float metabolism = 50f;

    [Tooltip("How many cells the system starts with")]
    [SerializeField] public int numberOfCells = 10;

}

// https://www.youtube.com/watch?v=aPXvoWVabPY 