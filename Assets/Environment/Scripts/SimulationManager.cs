using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationManager : Singleton<SimulationManager>
{
    public GameObject cell;
    public GameObject antiBiotic;
    public UISriptable UISettings;

    // For antibiotic spawning
    Ray ray;
    RaycastHit location;

    // Start is called before the first frame update
    void Start()
    {
        createCells();
    }

    // Update is called once per frame
    void Update()
    {
        spawnAntibiotic();   
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
            // Prevent antibiotic being spawned when clicking a cell
            if (Input.GetMouseButtonDown(0) && !location.transform.gameObject.CompareTag("cell"))
            {
                GameObject newAntibiotic = Instantiate(antiBiotic) as GameObject;
                newAntibiotic.transform.position = new Vector3(location.point.x, 1, location.point.z);
                newAntibiotic.gameObject.tag = "AntiBiotic";
            }
        }
    }
}
