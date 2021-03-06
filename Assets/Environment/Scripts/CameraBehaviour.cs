/**
 * CameraBehaviour is used to control and provide camera movement to the
 * user in the simulation. Gives a bird's-eye-view of the petri-dish
 * Reference: All code is taken from CPSC 565 Lecture 8 with a few edits
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float camSens = 0.25f;
    private float totalRun = 1.0f;
    private float mainSpeed = 20.0f; // regular speed
    private float shiftAdd = 250.0f; // multiplied by how long shift is held, makes movements faster
    private float maxShift = 1000.0f; // max speed when holding shift

    Camera mainCam;

    /*
     * Initialization
     */
    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }


    /*
     * Update is called once per frame
     */
    void Update()
    {
        // p is the direction we should be moving based on input
        Vector3 p = getBaseInput();

        if (Input.GetKey(KeyCode.LeftShift))
        {

            // Time.deltaTime = How long it has been since the last update method
            totalRun += Time.deltaTime;

            p = p * totalRun * shiftAdd;

            // Clamp them!
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;

        // Grabs the x,y,z of the object this script is on
        Vector3 newPosition = transform.position;

        // Only moves the x and z
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }

    }

    /*
     * Get keyboard letter values and adjust direction based on them
     */
    private Vector3 getBaseInput()
    {
        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += Vector3.up;
        }
        if (Input.GetKey(KeyCode.E))
        {
            p_Velocity += Vector3.down;
        }
        // Gives what the direction/velocity should be
        return p_Velocity;
    }
}
