/*
 * Authors: Isha Afzaal, Sammy Elrafih, Ainslie Veltheon
 * Info: CellBehavior.cs specifies agent behaviour in the quorum-sensing system of l. pneumophila.
 *       Cells use quorum-sensing to understand their population's cell density. Once the cell density passes a
 *       certain threshold, agents start exhibiting emergent behaviour.
 * References:
 *  Using timers in Unity: https://answers.unity.com/questions/1453479/how-to-slow-down-random-enemy-spawn.html
 *  Making objects spawn other objects in Unity: https://answers.unity.com/questions/420177/how-do-i-make-an-object-spawn-another-one.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellBehaviour : MonoBehaviour
{

    public UISriptable UISettings;

    public GameObject panel;

    // Cell Reproduction Configuration
    private int cells_reproduced = 0;

    // Quorum Sensing (QS) Configuration
    public GameObject LAI_1;
    

    // Cell Movement/Life and Death Control
    private Rigidbody physicsBody;
    Vector3 force;
    private bool quorum_sensing_switch = false;
    
    private int energy;

    private int maxEnergy = 100;

    //Evolutionary Algorithm Variables
    //TODO: make getters
    public int qsThreshold = 5; // FOR EA??
    public float target_time_for_LAI_1 = 4.0f; // FOR EA    
    public float target_time = 5.0f; // FOR EA

    // When you use these, don't actually use these variables here, but instead do "UISettings.qsThresholdMutationRate".
    // This way if they are changed mid simulation, they will update
    public float LAI_1MutationRate;
    public float reproductionMutationRate;
    public float qsThresholdMutationRate;
    

    // Start is called before the first frame update
    void Start()
    {
        energy = UISettings.energy;

        physicsBody = GetComponent<Rigidbody>();

        panel = GameObject.FindGameObjectWithTag("singleCellStats");

        StartCoroutine(consume_energy());

}


    // Update is called once per frame
    void Update()
    {
        movement();
        quorum_sensing();
        release_signalling_molecule();
    }

    /*
     * Make cells move. Credit to 
     */
    private void movement()
    {
        var multiplier = 0.002f;
        force = multiplier *  new Vector3(
            (Mathf.PerlinNoise( Time.time +transform.position.x , 1)-0.5f),
            0,
            (Mathf.PerlinNoise(Time.time + transform.position.x , 2)-0.5f));
        physicsBody.transform.localPosition +=  force;
    }
    /*
     * Specify the emergent behavior the cells should have here. 
     * For now, will simply change cell colours
     * Here will be antibiotic resistence I think
     */
    private void emergent_behavior ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.red;
        quorum_sensing_switch = true;
    }

    /*
     * Specify the emergent behavior the cells should have here. 
     * For now, will simply change cell colours
     * Here will be antibiotic resistence I think
     */
    private void deactivate_emergent_behavior ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.white;
        quorum_sensing_switch = false;
    }


    /*
     * Adapted from Sammy's cellSpawner.cs
     * replicates then mutates a cell
     */
    private void createCell(Vector3 spawn_location)
    {
        var go = this.gameObject;
        if (go != null)
        {
            Vector3 offset = new Vector3(0.15f,0,0);
            var newCell = Instantiate(go, spawn_location + offset, Quaternion.identity);
            newCell.GetComponent<CellBehaviour>().setEA(mutateInt(qsThreshold,2),mutateFloat(target_time_for_LAI_1,2f),mutateFloat(target_time,2f));
            SimulationStats.Instance.cellCount++;
        }
    }
    private int mutateInt(int original,int range)
    {
        int modifier = Random.Range(-range,range);
        return original + modifier;
    }
    private float mutateFloat(float original,float range)
    {
        float modifier = Random.Range(-range,range);
        return original + modifier;
    }
    public void setEA(int qsThreshold,float target_time_for_LAI_1, float target_time)
    {
        this.qsThreshold = qsThreshold;
        this.target_time_for_LAI_1 = target_time_for_LAI_1;
        this.target_time = target_time;
    }

    /*
     * Quorum-sensing: Agents detect cell density and exhibit emergent 
     * behavior if threshold reached or reproduce if threshold not reached
     */
    private void quorum_sensing ()
    {
        

        // QS Step 1: Sense the number of surrounding molecules
        List<Collider> nearby_molecules = new List<Collider>();

        // Get all nearby objects
        // TODO: ADJUST THE RADIUS TO A GOOD VALUE TO BE HARD CODED AT
        Collider[] nearby_objects = Physics.OverlapSphere(transform.position,
            1);

        // Sort through objects and save the molecules
        foreach (Collider c in nearby_objects)
        {
            if (c.CompareTag("LAI_1"))
            {
                nearby_molecules.Add(c);
            }
        }

        //Debug.Log(nearby_molecules.Count);

        // QS Step 2: Check surrounding signalling molecule concentration
        if (!quorum_sensing_switch)
        {
            // Case 1: Molecule concentration (therefore cell density)
            // has reached the threshold value - emergent behaviour 
            if (nearby_molecules.Count >= qsThreshold)
            {
                //Debug.Log("Activating emergent behavior");
                emergent_behavior();

                // Cells produce more LAI_1 with higher population densities
                release_signalling_molecule();
            }

            // Case 2: Cell density lower than threshold value - reproduce a new cell
            else
            {
                // Wait for some time before spawning
                target_time -= Time.deltaTime;
                if (target_time <= 0.0f)
                {
                    target_time = 5.0f;
                    Debug.Log("Creating New Bacteria.");

                    if (cells_reproduced < UISettings.reproductionLimit
                        && energy > UISettings.splitThreshold)
                    {
                        if(antiBioticPresent())
                        {
                            Destroy(gameObject);
                            SimulationStats.Instance.cellCount--;
                        }else{
                        // Create new game object
                        createCell(transform.position);

                        cells_reproduced++;

                        energy -= 10;
                        }
                    }
                }
            }
        }else{
            if (nearby_molecules.Count < qsThreshold)
            {
                deactivate_emergent_behavior();
            }
        }
    }

    /*
     * As the cells move, they consume energy and will die upon its depletion
     */
    private bool antiBioticPresent()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.
            transform.position, UISettings.ABRadius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "AntiBiotic")
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator consume_energy()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); 
            //Debug.Log("Consuming Energy.");
            energy -= 1;

            if (SimulationStats.Instance.agarNutrientLevel >= 2 && energy<maxEnergy)
            {
                energy += 2; Debug.Log("2");
                SimulationStats.Instance.agarNutrientLevel -= 2; 
            }

            // QS Step 3: Cells die upon having no energy
            if (energy <= 0)
            {
                // TODO: Keep as setActive or Destroy??
                //gameObject.SetActive(false);
                Destroy(gameObject);
                SimulationStats.Instance.cellCount--;
            }
        }   
    }

    /*
     * Cell releases a signalling molecule at a certain rate
     */
    private void release_signalling_molecule()
    {
        target_time_for_LAI_1 -= Time.deltaTime;

        if (target_time_for_LAI_1 <= 0.0f)
        {
            float x = transform.position.x + ((Mathf.PerlinNoise(Time.time + transform.position.x , 2)-0.5f)*2);
            float y = 1;
            float z = transform.position.z;
            target_time_for_LAI_1 = 1.0f;
            Instantiate(LAI_1, new Vector3(x, y, z), Quaternion.identity);

            energy -= 2;
        }
    }


    private void OnMouseDown()
    {
        Debug.Log("CLICKED");
        SingleCellUI.Instance.openPanel(quorum_sensing_switch, qsThreshold,
            target_time_for_LAI_1, energy, cells_reproduced, target_time);
    }
}
