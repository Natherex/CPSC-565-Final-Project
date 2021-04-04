using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationManager : Singleton<SimulationManager>
{
    public TextMeshProUGUI quorumText;
    public TextMeshProUGUI nutrientLevelText;
    public TextMeshProUGUI antibioticText;
    public GameObject cell;
    public GameObject antiBiotic;
    public UISriptable UISettings;

    private float agarStartNutrientLevel;
    private bool isAntibioticPresent;
    private bool isQuorumSensed;
    public float agarCurrentNutrientLevel;

    // For antibiotic spawning
    Ray ray;
    RaycastHit location;
    
    private void Awake()
    {
        agarStartNutrientLevel = UISettings.agarLevel;
        isAntibioticPresent = false;
        isQuorumSensed = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Replace the bool toStrings w/method to check value and return
        // text to place so we have nicer messages than true and false
        quorumText.text = isQuorumSensed.ToString();
        nutrientLevelText.text = agarStartNutrientLevel.ToString();
        antibioticText.text = isAntibioticPresent.ToString();

        agarCurrentNutrientLevel = agarStartNutrientLevel;

        createCells();
            
    }

    // Update is called once per frame
    void Update()
    {

        changeUIWithAgarLevel();
        spawnAntibiotic();
        
    }






    private void changeUIWithAgarLevel()
    {
        nutrientLevelText.text = agarCurrentNutrientLevel.ToString();
    }


    private void createCells()
    {
        for (int i = 0; i < UISettings.numberOfCells; i++)
        {
            GameObject newCell = Instantiate(cell) as GameObject;
            newCell.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-4, 4));
        }
    }

    private void spawnAntibiotic()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out location))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newAntibiotic = Instantiate(antiBiotic) as GameObject;
                newAntibiotic.transform.position = new Vector3(location.point.x, 1, location.point.z);
                newAntibiotic.gameObject.tag = "AntiBiotic";
            }
        }
    }
}
