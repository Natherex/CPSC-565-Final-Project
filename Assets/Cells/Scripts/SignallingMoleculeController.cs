using UnityEngine;
using System.Collections;

public class SignallingMoleculeController : MonoBehaviour
{
    private Rigidbody physicsBody;
    private Vector3 force;
    private float target_time_until_molecule_removed = 5.0f;
    private Vector3 initialPosition;
    private float diffusionRadius = 1.0f;

    // Use this for initialization
    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!diffusedTooFar()) 
            diffuse();


        // Wait for some time before removing molecule
        target_time_until_molecule_removed -= Time.deltaTime;
        if (target_time_until_molecule_removed <= 0.0f)
        {
            Destroy(gameObject);

            // Reset timer, not necessary since object destroyed?
            target_time_until_molecule_removed = 3.0f;
        }
    }




    private void diffuse()
    {
        force = new Vector3(Random.Range(-10, 10), -0.1f, Random.Range(-10, 10));
        physicsBody.AddForce(force / 500);
    }


    private bool diffusedTooFar()
    {
        // Distance between where particle was instantiated and where it is currently
        var dist = Vector3.Distance(initialPosition, transform.position);

        if (dist > diffusionRadius)
            return true;

        return false;
    }
    

}

// https://answers.unity.com/questions/43676/limiting-distance.html 
