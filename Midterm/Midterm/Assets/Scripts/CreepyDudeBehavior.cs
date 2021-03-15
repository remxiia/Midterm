using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreepyDudeBehavior : MonoBehaviour
{
    BoxCollider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName:"SecondLevel"); //throws you into the second level after you "talk" to the skull-dude
        }
    }
}
