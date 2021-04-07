using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public  class UIEventHandler : MonoBehaviour
{
    public  UISriptable UISettings;

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

    public GameObject panel;

    // This method goes on the submit button
    public  void storeValues()
    {
        int txt;

        reprodLimitText = reprodLimitField.GetComponent<Text>().text;
        if (!reprodLimitText.Equals(""))
            UISettings.reproductionLimit = 1 * Convert.ToInt32(reprodLimitText);
        
        agarNutLevelText = agarNutLevelField.GetComponent<Text>().text;
        if (!agarNutLevelText.Equals(""))
            UISettings.agarLevel = 1 * Convert.ToInt32(agarNutLevelText);

        abRadiusText = abRadiusField.GetComponent<Text>().text;
        if (!abRadiusText.Equals(""))
            UISettings.ABRadius = 1 * Convert.ToInt32(abRadiusText);

        spText = spField.GetComponent<Text>().text;
        if (!spText.Equals(""))
            UISettings.splitThreshold = 1 * Convert.ToInt32(spText);

         UISettings.tetStrength = tetStrengthSlider.value;

        energyText = energyField.GetComponent<Text>().text;
        if (!energyText.Equals(""))
            UISettings.energy = 1 * Convert.ToInt32(energyText);

        cellCountText = cellCountField.GetComponent<Text>().text;
        if (!cellCountText.Equals(""))
            UISettings.numberOfCells = 1 * Convert.ToInt32(cellCountText);

        if (dropDown.value == 0)
            UISettings.LAI_1MutationRate = mutationSlider.value;

        else if (dropDown.value == 1)
            UISettings.reproductionMutationRate = mutationSlider.value;

        else if (dropDown.value == 2)
            UISettings.qsThresholdMutationRate = mutationSlider.value;
    }

    


    public void closeSingCellStats()
    {
        panel.SetActive(false);
    }

  

}
