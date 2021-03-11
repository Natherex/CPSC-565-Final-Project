using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    // Reference to cell prefabs used for replication
  //  public GameObject cell;
 //   public GameObject cell_prefab;
    private Rigidbody physicsBody;
    private int reproduction_limit = 5;
    private int cells_reproduced = 0;
    private bool quorum_sensing_switch = false;
    private float targetTime = 5.0f;   //how long it will count down
    Vector3 force;
   

    public void constructor()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        force = new Vector3 (Random.Range(-10,10),-0.1f,Random.Range(-10,10));
        physicsBody.AddForce(force/500);
        
        // Sensing Signalling Molecules: Check the total number of bacteria around me. Multiply and increment the total signalling molecule value
        int radius = 1;
        int threshold_value = 5;
        Collider[] nearby_objects = Physics.OverlapSphere(transform.position, radius);

        // Threshold value check
        if (nearby_objects.Length >= threshold_value)
        { 
            Debug.Log("Activating emergent behavior.");
            emergent_behavior();
            quorum_sensing_switch = true;
        }
        // Theshold value not reached: Divide and create new bacteria
        else if (!quorum_sensing_switch)
        {
            // Wait a bit before spawning
            targetTime -= Time.deltaTime; //reduce target time by 1 every second

            if (targetTime <= 0.0f)       //timer ended
            {
                targetTime = 5.0f;
              //  timerEnded();
                Debug.Log("Creating new bacteria.");

                if (cells_reproduced < reproduction_limit)
                {
                    // Create new game object
                    createCell(transform.position);
                    cells_reproduced++;
                }
                //  Instantiate(cell, transform.position, Quaternion.identity);
                //  Instantiate(bacteria_prefab);
                // Update global signalling molecule value
                Debug.Log("Update signalling molecule +1");
            
            }

        }

    }

    private void emergent_behavior ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.red;
        quorum_sensing_switch = true;
    }

    // Adapted from Sammy's cellSpawner.cs
    private void createCell(Vector3 spawn_location)
    {
        var go = GameObject.Find("Cell(Clone)");
        if (go != null)
        {
            // GameObject new_cell = Instantiate(go, transform.position, Quaternion.identity) as GameObject;
            Instantiate(go, spawn_location, Quaternion.identity);
           // new_cell.transform.position = spawn_location;
        }
        else { Debug.Log("Failed to find Cell."); }
    }
}
