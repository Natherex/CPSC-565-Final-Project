using UnityEngine;
using System.Collections;
using TMPro;

public class SimulationStats : Singleton<SimulationStats>
{
    public TextMeshProUGUI numberOfCells;
    public TextMeshProUGUI nutrientLevelText;

    public float agarNutrientLevel;

    public UISriptable UISettings;

    public int cellCount;


    private void Awake()
    {
        agarNutrientLevel = UISettings.agarLevel;
        cellCount = UISettings.numberOfCells;
    }


    // Start is called before the first frame update
    void Start()
    {
        numberOfCells.text = "Number of cells: " + cellCount;

        nutrientLevelText.text = "Agar level: " + agarNutrientLevel.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        changeUIWithAgarLevel();
        updateNumberOfCells();
    }


    private void changeUIWithAgarLevel()
    {
        nutrientLevelText.text = "Agar level: " + agarNutrientLevel.ToString();
    }

    private void updateNumberOfCells()
    {
        numberOfCells.text = "Number of cells: " + cellCount;
    }

}
