using UnityEngine;
using System.Collections;

public class SignallingMoleculeController : MonoBehaviour
{
    private Rigidbody physicsBody;
    private Vector3 force;
    private float target_time = 2.0f;

    // Use this for initialization
    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diffuse();


        // Wait for some time before removing molecule
        target_time -= Time.deltaTime;
        if (target_time <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


    // Diffusion

    private void diffuse()
    {
        // Make the cell move
        force = new Vector3(Random.Range(-10, 10), -0.1f, Random.Range(-10, 10));
        physicsBody.AddForce(force / 500);
    }
    

}
