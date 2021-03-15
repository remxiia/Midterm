using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this might completely fail but let's give it a try
//IT DIDNT HALLELUJAH okay time to make some text
//...making text was a failure. gonna try again after I rewatch the lecture don't forget to do that

public class ItemBehavior : MonoBehaviour

{
    BoxCollider2D myCollider;
    public Text keyText;
    //private Text firstText;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    //    firstText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
        //    firstText.enabled = true;
        }
    }

    //void OnCollisionExit2D(Collision2D other){
    //    if(other.gameObject.tag == "Player"){
        //    firstText.enabled = false;
    //    }
    //}
}
