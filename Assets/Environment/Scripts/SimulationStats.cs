using UnityEngine;
using System.Collections;
using TMPro;

public class SimulationStats : Singleton<SimulationStats>
{
    public TextMeshProUGUI numberOfCells;
    public TextMeshProUGUI nutrientLevelText;

    public UISriptable UISettings;

    public int cellCount;


    private void Awake()
    {
        cellCount = UISettings.numberOfCells;
    }


    // Start is called before the first frame update
    void Start()
    {
        numberOfCells.text = "Number of cells: " + cellCount;

        nutrientLevelText.text = "Agar level: " + UISettings.agarLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        changeUIWithAgarLevel();
        updateNumberOfCells();
    }


    private void changeUIWithAgarLevel()
    {
        nutrientLevelText.text = "Agar level: " + UISettings.agarLevel.ToString();
    }

    private void updateNumberOfCells()
    {
        numberOfCells.text = "Number of cells: " + cellCount;
    }

}
