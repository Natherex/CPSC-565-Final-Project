using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Authors: Isha Afzaal, Sammy Elrafih, Ainslie Veltheon
 * CellBehavior.cs specifies agent behaviour in the quorum-sensing system of L. pneumophila.
 *       Cells use quorum-sensing to understand their population's cell density. Once the cell density passes a
 *       certain threshold, agents start exhibiting emergent behaviour.
 * References:
 *  Using timers in Unity: https://answers.unity.com/questions/1453479/how-to-slow-down-random-enemy-spawn.html
 *  Making objects spawn other objects in Unity: https://answers.unity.com/questions/420177/how-do-i-make-an-object-spawn-another-one.html
 */
public class CellBehaviour : MonoBehaviour
{
    
    public UISriptable UISettings; // Reference to scriptable object holding data

    public GameObject panel;  // Reference to the single cell UI panel

    public int cellsReproduced = 0; // Keep track of how many times a cell reproduces

    public GameObject LAI_1;  // Prefab of the signalling molecule

    private Rigidbody physicsBody;

    Vector3 force;

    public bool qsOn = false; // Keeps track of when a cell senses a quorum
    
    public int energy; // How much energy a cell has at any given moment

    public float stress = 10;
    private int maxEnergy = 100; 

    public int chanceOfDeath = 99;
    //TODO: make getters
    private int qsThreshold; // How many signalling molecules a cell needs to sense before sensing a quorum

    private float targetTimeLAI_1; // How quickly cell releases a new signalling molecule. In seconds   
    private float targetTimeLAI_1Counter;

    private float targetTimeReproduction; // How quickly cell reproduces another cell. In seconds
    private float targetTimeReproductionCounter;

    
    public System.Random rand;

    public void setQsThreshold(int qsThreshold)
    {
        this.qsThreshold = qsThreshold;
    }

    public void setTarget_time_for_LAI_1(float targetTimeLAI_1)
    {
        this.targetTimeLAI_1 = targetTimeLAI_1;
    }

    public void setTarget_time(float targetTimeReproduction)
    {
        this.targetTimeReproduction = targetTimeReproduction;
    }


    // Start is called before the first frame update
    void Start()
    {
        targetTimeLAI_1Counter = targetTimeLAI_1;

        targetTimeReproductionCounter = targetTimeReproduction;

        energy = UISettings.energy;

        physicsBody = GetComponent<Rigidbody>();

        panel = GameObject.FindGameObjectWithTag("singleCellStats");

        StartCoroutine(consumeEnergy());

    }


    // Update is called once per frame
    void Update()
    {
        movement();
        quorumSensingFromLqsS();
        releaseSignallingMoleculeFromLqsA();
    }

    public void setSeed(int seed)
    {
        rand = new System.Random(seed);
    }
    /*
     * Make cells move. Credit to 
     */
    private void movement()
    {
        var multiplier = .1f;
        force = multiplier *  new Vector3(
            (rand.Next(-30,30)),
            0,
            (rand.Next(-30,30)));
        physicsBody.AddForce(force);
    }
    /*
     * Specify the emergent behavior the cells should have here. 
     * For now, will simply change cell colours
     * Here will be antibiotic resistence I think
     */
    private void activateEmergentBehaviorFromLqsR ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.red;
        qsOn = true;
    }

    /*
     * Specify the emergent behavior the cells should have here. 
     * For now, will simply change cell colours
     * Here will be antibiotic resistence I think
     */
    private void deactivateEmergentBehavior ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.white;
        qsOn = false;
    }


    /*
     * replicates then mutates a cell
     */
    private void createCell(Vector3 spawn_location)
    {
        var go = this.gameObject;
        if (go != null)
        {
            Vector3 offset = new Vector3(0.15f, 0, 0);
            var newCell = Instantiate(go, spawn_location + offset, Quaternion.identity);
            newCell.name = "Cell";
            newCell.GetComponent<CellBehaviour>().setSeed(rand.Next());
            newCell.GetComponent<CellBehaviour>().setEA(mutateInt(qsThreshold, UISettings.qsThresholdMutationRadius),
                mutateFloat(targetTimeLAI_1 ,UISettings.LAI_1MutationRadius), mutateFloat(targetTimeReproduction,UISettings.reproductionMutationRadius));

            SimulationStats.Instance.cellCount++;
        }
    }
    private int mutateInt(int original, float mutationRadius)
    {
        int range = (int)(mutationRadius*10);
        //range = 2;
        int modifier = Random.Range(-range, range);
        
        return Mathf.Abs(original + modifier);
    }
    private float mutateFloat(float original, float mutationRadius)
    {
        float range = original*mutationRadius;
        //range = 2f;
        float modifier = (rand.Next((int)(-range*100),(int)(range*100)))/100f;
        return Mathf.Abs(original + modifier);
    }
    public void setEA(int qsThreshold, float targetTimeLAI_1, float targetTimeReproduction)
    {
        this.qsThreshold = qsThreshold;
        this.targetTimeLAI_1 = targetTimeLAI_1;
        this.targetTimeReproduction = targetTimeReproduction;
        targetTimeLAI_1Counter = targetTimeLAI_1;
        targetTimeReproductionCounter = targetTimeReproduction;
    }

    /*
     * Quorum-sensing: Agents detect cell density and exhibit emergent 
     * behavior if threshold reached or reproduce if threshold not reached
     */
    private void quorumSensingFromLqsS ()
    {
        

        // QS Step 1: Sense the number of surrounding molecules
        List<Collider> nearby_molecules = new List<Collider>();
        int num = 0;
        // Get all nearby objects
        // TODO: ADJUST THE RADIUS TO A GOOD VALUE TO BE HARD CODED AT
        Collider[] nearby_objects = Physics.OverlapSphere(transform.position,
            0.5f);
        //Debug.Log("nearby_objects.length " + nearby_objects.Length);
        // Sort through objects and save the molecules
        foreach (Collider c in nearby_objects)
        {
            if (c.CompareTag("LAI_1"))
            {
                //nearby_molecules.Add(c);
                num++;
            }
        }

        //Debug.Log("nearby_molecules.count " + nearby_molecules.Count);
        //Debug.Log("num " + num);

        //Debug.Log(nearby_molecules.Count);

        // QS Step 2: Check surrounding signalling molecule concentration
        if (!qsOn)
        {
            // Case 1: Molecule concentration (therefore cell density)
            // has reached the threshold value - emergent behaviour 
            if (num >= qsThreshold)
            {
                //Debug.Log("Activating emergent behavior");
                activateEmergentBehaviorFromLqsR();

                // Cells produce more LAI_1 with higher population densities
                releaseSignallingMoleculeFromLqsA();
            }

            // Case 2: Cell density lower than threshold value - reproduce a new cell
            else
            {
                // Wait for some time before spawning
                targetTimeReproductionCounter -= Time.deltaTime;

                if (targetTimeReproductionCounter <= 0.0f)
                {
                    targetTimeReproductionCounter = targetTimeReproduction;
                    Debug.Log("Creating New Bacteria.");

                    if (cellsReproduced < UISettings.reproductionLimit
                        && energy > UISettings.splitThreshold)
                    {
                        if(isAntiBiotic1Present())
                        {
                            SimulationStats.Instance.cellCount--;
                            Destroy(gameObject);
                            
                        }else{
                            // Create new game object
                            createCell(transform.position);

                            cellsReproduced++;

                            energy -= 10;
                        }
                    }
                }
            }
        }else{
            if (num < qsThreshold)
            {
                deactivateEmergentBehavior();
            }
        }
    }

    // As the cells move, they consume energy and will die upon its depletion
    private bool isAntiBiotic1Present()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.
            transform.position, UISettings.ABRadius);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "AntiBiotic1")
            {
                return true;
            }
        
        }
        return false;
    }

    private bool isAntiBiotic2LethalPresent()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.
            transform.position, UISettings.ABRadius);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "AntiBiotic2" && Random.Range(0f,1f) >= (UISettings.tetResistance))
            {
                return true;
            }
        }
        return false;
    }

    // 
    IEnumerator consumeEnergy()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); 
            

            // Metabolism uses up energy
            energy -= 1;

            int x = (int) Mathf.Round(transform.position.x);
            int z = (int) Mathf.Round(transform.position.z);

            if (energy < maxEnergy)
            {
                // Check the agar level at the cells location and get the grid unit cell is at
                int currentGridLevel = SimulationManager.Instance.grid.getGridLevel(x, z, out int gridX, out int gridZ);
                
                if (currentGridLevel > 2)
                {
                    Debug.Log("Consuming Energy.");
                    energy += 1;
                    SimulationManager.Instance.grid.subtractNutrientLevel(gridX, gridZ);
                    UISettings.agarLevel -= 2;
                }
               
            }

            // Cells die upon having no energy or by chance being in presence of antiBioticand are removed from the simulation
            if (energy <= 0 || isAntiBiotic2LethalPresent())
            {
                SimulationStats.Instance.cellCount--;
                Destroy(gameObject);
                
            }
    
            stress -= Time.deltaTime;
            if(stress <= 0)
            {
                SimulationStats.Instance.cellCount--;
                Destroy(gameObject);
                
            }
        }   
    }


    // Cell releases a signalling molecule at a certain rate
    private void releaseSignallingMoleculeFromLqsA()
    {
        targetTimeLAI_1Counter -= Time.deltaTime;

        if (targetTimeLAI_1Counter <= 0.0f)
        {
            float x = transform.position.x + ((Mathf.PerlinNoise(Time.time +
                transform.position.x , 2)-0.5f)*2);
            float y = 1;
            float z = transform.position.z;
            targetTimeLAI_1Counter = targetTimeLAI_1;
            GameObject sm = Instantiate(LAI_1, new Vector3(x, y, z), Quaternion.identity);

            sm.name = "LAI-1";

            energy -= 2;
        }
    }

    // When clicked send stats to the panel to display
    private void OnMouseDown()
    {
        SingleCellUI.Instance.openPanel(qsOn, qsThreshold,
            targetTimeLAI_1, energy, cellsReproduced, targetTimeReproduction);
    }
}
