/**
 * Authors: Sammy Elrafih, Ainslie Veltheon, Isha Afzaal
 * SimulationManager.cs is used to do operations on the petri-dish; it can
 * clear the dish, create cells and spawn antibiotics
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationManager : Singleton<SimulationManager>
{
    public GameObject cell;
    public GameObject antiBiotic;
    public UISriptable UISettings;
    public Grid grid;

    // For antibiotic spawning
    Ray ray;
    RaycastHit location;

    /*
     * Initialization
     */
    void Start()
    {
        createCells();
        grid = new Grid(10, 10, UISettings.agarLevel / 100);
    }

    /*
     * Spawn antibiotics
     */
    void Update()
    {
        spawnAntibiotic1();  
        spawnAntibiotic2();   
        spawnAntibiotic9();
    }

    /*
     * Empty the petri-dish
     */
    public void clearDish()
    {
        // Get all the game objects in the scene
        GameObject[] GameObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < GameObjects.Length; i++)
        {
            // Destroy all the cells and signalling molecules
            if (GameObjects[i].CompareTag("cell") || GameObjects[i].
                CompareTag("LAI_1") || GameObjects[i].CompareTag("AntiBiotic1") || GameObjects[i].CompareTag("AntiBiotic2"))
            {
                Destroy(GameObjects[i]);
            }
        }

        // Reset agar for a new plate.
        UISettings.agarLevel = 1000;
        grid.resetNutrientLevels(1000);
    }

    /*
     * Create cells on the petri-dish
     */
    private void createCells()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < UISettings.numberOfCells; i++)
        {
            GameObject newCell = Instantiate(cell) as GameObject;
            newCell.name = "Cell";
            newCell.GetComponent<CellBehaviour>().setSeed(rand.Next());
            newCell.GetComponent<CellBehaviour>().setEA(rand.Next(3,10),rand.Next(100,500)/100f,rand.Next(100,500)/100f);
            newCell.GetComponent<Renderer>().material.color = Color.white;
            // TODO: Update the location where cells first spawn and change the random we use to System
            newCell.transform.position = new Vector3(Random.Range(0, 10), 1, Random.Range(0, 8));
        }
    }

    /*
     * Spawn antibiotic type #1 on the dish, according to where the user's mouse is pointing to
     */
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

    /*
     * Spawn antibiotic type #2, according to where the user's mouse is pointint to
     */
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

    /*
     * Delete AntiBiotics by pressing 9
     */
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
