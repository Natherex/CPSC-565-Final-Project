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

    public  GameObject qsRadiusField;
    public  string qsRadiusText;

    public  GameObject tetStrengthField;
    public  string tetStrengthText;

    public  GameObject energyField;
    public  string energyText;

    public GameObject cellCountField;
    public string cellCountText;

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

        qsRadiusText = qsRadiusField.GetComponent<Text>().text;
        if (!qsRadiusText.Equals(""))
            UISettings.QSRadius = 1 * Convert.ToInt32(qsRadiusText);

        tetStrengthText = tetStrengthField.GetComponent<Text>().text;
        if (!tetStrengthText.Equals(""))
            UISettings.tetStrength = 1 * Convert.ToInt32(tetStrengthText);

        energyText = energyField.GetComponent<Text>().text;
        if (!energyText.Equals(""))
            UISettings.energy = 1 * Convert.ToInt32(energyText);

        cellCountText = cellCountField.GetComponent<Text>().text;
        if (!cellCountText.Equals(""))
            UISettings.numberOfCells = 1 * Convert.ToInt32(cellCountText);

        Debug.Log("this agar " + agarNutLevelText + " scriptobj agar: " + UISettings.agarLevel);
        Debug.Log("this energy: " + energyText + " scriptobj energy: " + UISettings.energy);

    }




}
