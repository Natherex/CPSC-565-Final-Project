/**
 * Authors: Sammy Elrafih, Ainslie Veltheon, Isha Afzaal
 * UIEventHandler.cs handles actions done in the UIs of the simulation.
 **/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public  class UIEventHandler : MonoBehaviour
{
    // Reference to our scriptable object
    public  UISriptable UISettings;

    // Variables related to the interactive UI
    public  GameObject reprodLimitField;
    public  string reprodLimitText;

    public  GameObject agarNutLevelField;
    public  string agarNutLevelText;

    public  GameObject abRadiusField;
    public  string abRadiusText;

    public  GameObject spField;
    public  string spText;

    public Slider tetStrengthSlider;

    public  GameObject energyField;
    public  string energyText;

    public GameObject cellCountField;
    public string cellCountText;

    public Dropdown dropDown;

    public Slider mutationSlider;

    // Reference to the single cell panel
    public GameObject panel;

    public GameObject canvasForDocumentation;

    /*
     * This method goes on the submit button
     * Saves the values inputted by user to the scriptable object
     */
    public  void storeValues()
    {
        int txt;

        // Disallow blank entries and negative values
        reprodLimitText = reprodLimitField.GetComponent<Text>().text;
        if (!reprodLimitText.Equals("") && Convert.ToInt32(reprodLimitText) > 0)
            UISettings.reproductionLimit = 1 * Convert.ToInt32(reprodLimitText);
        
        agarNutLevelText = agarNutLevelField.GetComponent<Text>().text;
        if (!agarNutLevelText.Equals("") && Convert.ToInt32(agarNutLevelText) > 0)
        {
            int newLevel = 1 * Convert.ToInt32(agarNutLevelText);
            UISettings.agarLevel = newLevel;
            
            SimulationManager.Instance.grid.resetNutrientLevels(newLevel);
        }
            
        abRadiusText = abRadiusField.GetComponent<Text>().text;
        if (!abRadiusText.Equals("") && Convert.ToInt32(abRadiusText) > 0)
            UISettings.ABRadius = 1 * Convert.ToInt32(abRadiusText);

        spText = spField.GetComponent<Text>().text;
        if (!spText.Equals("") && Convert.ToInt32(spText) > 0)
            UISettings.splitThreshold = 1 * Convert.ToInt32(spText);

        // Variable set to wherever the slider is
         UISettings.tetResistance = tetStrengthSlider.value;

        energyText = energyField.GetComponent<Text>().text;
        if (!energyText.Equals("") && Convert.ToInt32(energyText) > 0)
        {
            int newEnergy = 1 * Convert.ToInt32(energyText);
            UISettings.energy = newEnergy;
            
            GameObject[] cells = GameObject.FindGameObjectsWithTag("cell");
            foreach (GameObject cell in cells)
            {
                CellBehaviour script = cell.GetComponent<CellBehaviour>();
                script.energy = newEnergy;
            }
        }
            
        cellCountText = cellCountField.GetComponent<Text>().text;
        if (!cellCountText.Equals("") && Convert.ToInt32(cellCountText) > 0)
        {
            int newCellCount = 1 * Convert.ToInt32(cellCountText);
            UISettings.numberOfCells = newCellCount;            
        }
          
        // Different variables affected by the sliders value depending on which
        // dropdown is chosen
        if (dropDown.value == 0)
            UISettings.LAI_1MutationRadius = mutationSlider.value;

        else if (dropDown.value == 1)
            UISettings.reproductionMutationRadius = mutationSlider.value;

        else if (dropDown.value == 2)
            UISettings.qsThresholdMutationRadius = mutationSlider.value;
    }

    /*
     * Helps user understand parameters in the simulation
     */
    public void showDocumentation()
    {
        canvasForDocumentation.SetActive(true);
    }

    public void closeDocumentation()
    {
        canvasForDocumentation.SetActive(false);
    }
}
