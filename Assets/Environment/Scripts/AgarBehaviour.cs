using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgarBehaviour : MonoBehaviour
{

    [SerializeField] float agarStartNutrientLevel;
    public TextMeshProUGUI quorumText;
    public TextMeshProUGUI nutrientLevelText;
    public TextMeshProUGUI antibioticText;

    private bool isAntibioticPresent;
    private bool isQuorumSensed;
    private float agarCurrentNutrientLevel;

    private void Awake()
    {
        isAntibioticPresent = false;
        isQuorumSensed = false;
        agarCurrentNutrientLevel = agarStartNutrientLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        // TODO: Replace the bool toStrings w/method to check value and return
        // text to place so we have nicer messages than true and false
        quorumText.text = isQuorumSensed.ToString();
        nutrientLevelText.text = agarStartNutrientLevel.ToString();
        antibioticText.text = isAntibioticPresent.ToString();
            
    }

    // Update is called once per frame
    void Update()
    {

        changeUIWithAgarLevel();
        
    }

    private void changeUIWithAgarLevel()
    {
        // Get the new agar nutrient level 
        agarCurrentNutrientLevel = globals.total_agar;

        nutrientLevelText.text = agarCurrentNutrientLevel.ToString();


    }
}
