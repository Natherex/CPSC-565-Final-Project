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
        spawnAntibiotic1();  
        spawnAntibiotic2();   
        spawnAntibiotic9();
    }

    private void createCells()
    {
        for (int i = 0; i < UISettings.numberOfCells; i++)
        {
            GameObject newCell = Instantiate(cell) as GameObject;
            newCell.name = "Cell";
            newCell.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-4, 4));
        }
    }

    private void spawnAntibiotic1()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out location))
        {
            // Prevent antibiotic being spawned when clicking a cell
            if (Input.GetKeyDown("1") && !location.transform.gameObject.CompareTag("cell"))
            {
                GameObject newAntibiotic = Instantiate(antiBiotic) as GameObject;
                newAntibiotic.transform.position = new Vector3(location.point.x, 1, location.point.z);
                newAntibiotic.gameObject.tag = "AntiBiotic1";
            }
        }
    }
    private void spawnAntibiotic2()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out location))
        {
            // Prevent antibiotic being spawned when clicking a cell
            if (Input.GetKeyDown("2") && !location.transform.gameObject.CompareTag("cell"))
            {
                GameObject newAntibiotic = Instantiate(antiBiotic) as GameObject;
                newAntibiotic.GetComponent<Renderer>().material.color = Color.yellow;
                newAntibiotic.transform.position = new Vector3(location.point.x, 1, location.point.z);
                newAntibiotic.gameObject.tag = "AntiBiotic2";
            }
        }
    }
    private void spawnAntibiotic9()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out location))
        {
            // Prevent antibiotic being spawned when clicking a cell
            if (Input.GetKeyDown("9") && (location.transform.gameObject.CompareTag("AntiBiotic1")
                                        ||location.transform.gameObject.CompareTag("AntiBiotic2")))
            {
                Destroy(location.transform.gameObject);
            }
        }
    }


    

}
