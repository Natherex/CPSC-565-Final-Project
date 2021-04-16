/**
 * Authors: Isha Afzaal, Sammy Elrafih, Ainslie Veltheon
 * SignallingMoleculeController.cs is used by CellBehaviour.cs to diffuse
 * signalling molecules for cells.
 * Reference: https://answers.unity.com/questions/43676/limiting-distance.html 
 **/

using UnityEngine;
using System.Collections;

public class SignallingMoleculeController : MonoBehaviour
{
    private Rigidbody physicsBody;
    private Vector3 force;
    private float target_time_until_molecule_removed = 5.0f;
    private Vector3 initialPosition;
    private float diffusionRadius = 1.0f;

    /*
     * Initialization
     */
    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    /*
     * Diffuse signalling molecules and destroy them
     * after a certain amount of time.
     */
    void Update()
    {
        if (!diffusedTooFar()) 
            diffuse();

        // Wait for some time before removing molecule
        target_time_until_molecule_removed -= Time.deltaTime;
        if (target_time_until_molecule_removed <= 0.0f)
        {
            Destroy(gameObject);
            target_time_until_molecule_removed = 3.0f; // Object removed already so is the timer reset necessary?
        }
    }

    /*
     * Diffuse signalling molecules
     */
    private void diffuse()
    {
        force = new Vector3(Random.Range(-10, 10), -0.1f, Random.Range(-10, 10));
        physicsBody.AddForce(force / 500);
    }

    /*
     * Check if signalling molecules diffused too far
     */
    private bool diffusedTooFar()
    {
        // Distance between where particle was instantiated and where it is currently
        var dist = Vector3.Distance(initialPosition, transform.position);
        if (dist > diffusionRadius)
            return true;
        return false;
    }

}