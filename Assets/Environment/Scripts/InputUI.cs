using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    public string value;
    public GameObject inputField;

    public void storeValue()
    {
        value = inputField.GetComponent<Text>().text;
        Debug.Log(value);
    }
    
}
