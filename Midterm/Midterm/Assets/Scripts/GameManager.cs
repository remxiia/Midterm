using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject TextBG;
    public GameObject TextObj;
    public Text TextComponent;
    public static int collisionCount = 0;
    
    public float textTimeReset;
    float textTime;
    //bool countDown = false;

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
        //countDown = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.tag == "Exit door"){
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName:"GameOver"); //after you "talk" to the four ghosts, the door should take you to the game over screen
        }
    }

}
