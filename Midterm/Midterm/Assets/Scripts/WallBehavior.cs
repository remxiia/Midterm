using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{

    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if(other.gameObject.tag == "Wall"){
    //         Debug.Log("congratulations, you hit a wall.");
    //     }
    // }
}
