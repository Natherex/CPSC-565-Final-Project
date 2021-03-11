using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    private Rigidbody physicsBody;
    Vector3 force;
    // Start is called before the first frame update

    public void constructor()
    {

    }

    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        force = new Vector3 (Random.Range(-10,10),-0.1f,Random.Range(-10,10));
        physicsBody.AddForce(force/500);
    }
}
