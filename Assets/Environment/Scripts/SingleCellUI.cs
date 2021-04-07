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

            panelText.text = "<b>QS triggered: </b>" + isQSTriggered.ToString()
                + "\n\n<b>QS threshold: </b>" + qsThreshold.ToString()
                + "\n\n<b>LAI-1 production rate: </b>" + target_time_for_LAI_1.ToString()
                + "\n\n<b>Energy: </b>" + energy.ToString()
                + "\n\n<b>Cells reproduced: </b>" + cellsReproduced.ToString()
                + "\n\n<b>Reproduction rate(if enough energy): </b>" + target_time;

        };
    }
}
