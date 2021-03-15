using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject TextBG;
    public GameObject TextObj;
    public Text TextComponent;
    
    public float textTimeReset;
    float textTime;
    bool countDown = false;

    // Start is called before the first frame update
    void Start()
    {
        textTime = textTimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        // if(hit.collider.name == "First Encounter"){
        //     //NEEDS A RAYCAST
        //     //gameManager.ShowText("Oh dear, you really shouldn't be here... But since you are, go and gather the objects scattered about. You'll need them to find out what happened here.");
        // }

    }

    public void ShowText(string textToShow)
    {
        //TextBG.SetActive(true);
        TextObj.SetActive(true);
        //TextComponent.SetActive = textToShow;
        countDown = true;
    }

}
