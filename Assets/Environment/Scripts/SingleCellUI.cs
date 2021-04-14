using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Handles the single cell UI panel
public class SingleCellUI : Singleton<SingleCellUI>
{
    public GameObject panel;
    public Text panelText;

    public GameObject cellPrefab;

    private bool isQSTriggered;
    private int qsThreshold;
    private float target_time_for_LAI_1;
    private int energy;
    private int cellsReproduced;
    private float target_time;

    private bool cellClicked = false;


    // What should be displayed when the panel opens
    public void openPanel(bool isQSTriggered, int qsThreshold,
        float target_time_for_LAI_1, int energy, int cellsReproduced,
        float target_time)
    {
        cellClicked = true;

        this.isQSTriggered = isQSTriggered;
        this.qsThreshold = qsThreshold;
        this.target_time_for_LAI_1 = target_time_for_LAI_1;
        this.energy = energy;
        this.cellsReproduced = cellsReproduced;
        this.target_time = target_time;

        if (panel != null)
        {
            panel.SetActive(true);

            panelText.text = "<b>QS triggered: </b>" + isQSTriggered.ToString()
                + "\n\n<b>QS threshold: </b>" + qsThreshold.ToString()
                + "\n\n<b>LAI-1 production rate: </b>" + target_time_for_LAI_1.ToString()
                + "\n\n<b>Energy: </b>" + energy.ToString()
                + "\n\n<b>Cells reproduced: </b>" + cellsReproduced.ToString()
                + "\n\n<b>Reproduction rate(if enough energy): </b>" + target_time;

        };
    }


    // Goes on the "x" button on the single cell stats panel
    public void closeSingCellStats()
    {
        panel.SetActive(false);
    }



    // Restarts simulation with the chosen cell
    public void streakPlate()
    {
        // If this one variable is not null, the rest should be good too
        if (cellClicked)
        {
            SimulationManager.Instance.clearDish();

            // Reset SO

            System.Random rand = new System.Random();

            // Instan the chosen cell
            GameObject newCell = Instantiate(cellPrefab) as GameObject;
            

            // TODO: Update the location where cells first spawn and change the random we use to System
            newCell.transform.position = new Vector3(Random.Range(0, 10), 1, Random.Range(0, 8));

            CellBehaviour script = newCell.GetComponent<CellBehaviour>();

            script.setSeed(rand.Next());

            script.qsOn = isQSTriggered;
            script.setQsThreshold(qsThreshold);
            script.setTarget_time_for_LAI_1(target_time_for_LAI_1);
            script.energy = energy;
            script.cellsReproduced = cellsReproduced;
            script.setTarget_time(target_time);
        }
        
    }


}
