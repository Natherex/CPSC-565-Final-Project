using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AntiBioticSpawner : MonoBehaviour
{
    Ray ray;
    RaycastHit location;
    public GameObject antiBiotic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out location))
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject newAntibiotic = Instantiate(antiBiotic) as GameObject;
                newAntibiotic.transform.position = new Vector3(location.point.x,1,location.point.z); 
                newAntibiotic.gameObject.tag = "AntiBiotic";
            }
        }
    }
    private void createAntiBiotic()
    {
        GameObject newCell = Instantiate(antiBiotic) as GameObject;
        newCell.transform.position = new Vector3(Random.Range(-5,5),1,Random.Range(-4,4));        
    }
}
