using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals : MonoBehaviour
{
    public static int total_agar = 100;
    public static globals Instance { get; private set; }
    // Start is called before the first frame update
    //[5]Reso Coder (2017, Mar 24) Singletons in Unity - Simple Tutorial for Beginners - https://www.youtube.com/watch?v=CPKAgyp8cno&ab_channel=ResoCoder
    public int numberOfCells;
    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
