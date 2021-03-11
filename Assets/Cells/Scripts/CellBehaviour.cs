using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    // Reference to cell prefabs used for replication
    public GameObject bacteria_prefab;
    
    private Rigidbody physicsBody;
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
        }
        // Theshold value not reached: Divide and create new bacteria
        else
        {
            Debug.Log("Creating new bacteria.");

            // Create new game object
            //  Instantiate(bacteria_prefab, transform.position, Quaternion.identity);
            Instantiate(bacteria_prefab);
            // Update global signalling molecule value
            Debug.Log("Update signalling molecule +1");

        }

    }

    private void emergent_behavior ()
    {
        // Change the current agent cell's colour
        GetComponent<Renderer>().material.color = Color.red;
    }
}
