using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SingleCellUI : Singleton<SingleCellUI>
{

    public GameObject panel;
    public Text panelText;

    // Use this for initialization
    void Start()
    {

    }

    // TODO: Change the rates
    public void openPanel(bool isQSTriggered, int qsThreshold,
        float target_time_for_LAI_1, int energy, int cellsReproduced,
        float target_time)
    {
        if (panel != null)
        {
            panel.SetActive(true);

            panelText.text = "QS triggered: " + isQSTriggered.ToString()
                + "\nQS threshold: " + qsThreshold.ToString()
                + "\nLAI-1 production rate: " + target_time_for_LAI_1.ToString()
                + "\nEnergy: " + energy.ToString()
                + "\nCells reproduced: " + cellsReproduced.ToString()
                + "\nReproduction rate(if enough energy): " + target_time;

        };
    }
}
