/**
 * Authors: Sammy Elrafih, Ainslie Veltheon, Isha Afzaal
 * SimulationStats.cs is used to update the on-screen text UI
 * that informs users of the current cell count on the dish and agar
 * levels.
 **/

using UnityEngine;
using System.Collections;
using TMPro;

public class SimulationStats : Singleton<SimulationStats>
{
    public TextMeshProUGUI numberOfCells;
    public TextMeshProUGUI nutrientLevelText;
    public UISriptable UISettings;
    public int cellCount;

    /*
     * Pull the current number of cells on the petri-dish
     */
    private void Awake()
    {
        cellCount = UISettings.numberOfCells;
    }

    /*
     * Initialize text UI
     */
    void Start()
    {
        numberOfCells.text = "Number of cells: " + cellCount;
        nutrientLevelText.text = "Agar level: " + UISettings.agarLevel.ToString();
    }

    /*
     * Keep cell and agar value updated in the UI
     */
    void Update()
    {
        changeUIWithAgarLevel();
        updateNumberOfCells();
    }

    /*
     * Helper functions for updating the text UI
     */
    private void changeUIWithAgarLevel()
    {
        nutrientLevelText.text = "Agar level: " + UISettings.agarLevel.ToString();
    }

    private void updateNumberOfCells()
    {
        numberOfCells.text = "Number of cells: " + cellCount;
    }
}
