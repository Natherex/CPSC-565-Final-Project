using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellSpawner : MonoBehaviour
{
    public int numberOfCells = 4;
    public GameObject cell;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < globals.Instance.numberOfCells ; i++)
        {
            createCell();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void createCell()
    {
        GameObject newCell = Instantiate(cell) as GameObject;
        newCell.transform.position = new Vector3(Random.Range(-5,5),1,Random.Range(-4,4));        
    }
}
