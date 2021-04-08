using UnityEngine;
using System.Collections;
using TMPro;

public class SimulationStats : Singleton<SimulationStats>
{
    public TextMeshProUGUI numberOfCells;
    public TextMeshProUGUI nutrientLevelText;

    public float agarNutrientLevelQ1;
    public float agarNutrientLevelQ2;
    public float agarNutrientLevelQ3;
    public float agarNutrientLevelQ4;

    public UISriptable UISettings;

    public int cellCount;


    private void Awake()
    {
        agarNutrientLevelQ1 = UISettings.agarLevel / 4;
        agarNutrientLevelQ2 = UISettings.agarLevel / 4;
        agarNutrientLevelQ3 = UISettings.agarLevel / 4;
        agarNutrientLevelQ4 = UISettings.agarLevel / 4;

        cellCount = UISettings.numberOfCells;
    }


    // Start is called before the first frame update
    void Start()
    {
        numberOfCells.text = "Number of cells: " + cellCount;

        nutrientLevelText.text = "Agar levels: " + agarNutrientLevelQ1.ToString()
            + " " + agarNutrientLevelQ2.ToString() + " " +
            agarNutrientLevelQ3.ToString() + " " + agarNutrientLevelQ4.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        changeUIWithAgarLevel();
        updateNumberOfCells();
    }


    private void changeUIWithAgarLevel()
    {
        nutrientLevelText.text = "Agar levels: " + agarNutrientLevelQ1.ToString()
            + " " + agarNutrientLevelQ2.ToString() + " " +
            agarNutrientLevelQ3.ToString() + " " + agarNutrientLevelQ4.ToString();
    }

    private void updateNumberOfCells()
    {
        numberOfCells.text = "Number of cells: " + cellCount;
    }

}
