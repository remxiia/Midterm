using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //need this so I can reference Text
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{

    //component ref(s)
    SpriteRenderer sprRenderer; //renders whatever the other object is 
    BoxCollider2D myCollider;
    SpriteRenderer myRenderer;
    Animator myAnimator; //will need for getting the sprite animations to run once I have those ready to go

    public directionState currentState = directionState.none; //currently not moving (I think....)
    public static bool faceLeft = true; //start the game facing left (hopefully)

    public float speed; //honestly not sure if I'm gonna need this, might get deleted later

    //public Sprite walkSprite;
    //private Vector3 nextPos; //will need these when it's time to animate so I'll leave them here
    public GameManager gameManager; //lets me reference the game manager

    //all of my lovely game objects for pulling up and getting rid of text
    public GameObject introText;
    public GameObject journalText;
    public GameObject boneText;
    public GameObject spoopyText;
    public GameObject ghostOne;
    public GameObject ghostTwo;
    public GameObject ghostThree;
    public GameObject ghostFour;

    public enum directionState{ //def gonna need this, gotta get my char to move somehow
        up, 
        down, 
        left, 
        right, 
        none
    }

    private enum collisionDir { //the relative position of the last collision
        up, 
        down, 
        left, 
        right, 
        none
    }

    private collisionDir currentDir;

    private Text firstText; //****SHOULD help to start getting the text to show on screen when the char bumps into an obj

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() 
    {
        
    }

    void FixedUpdate() //fixedUpdate stops the jittery-walking effect
    {
        CheckKey();
        MoveMe();
    }

    void CheckKey(){ //changes the walking state
        if(Input.GetKey(KeyCode.W)){
            currentState = directionState.up;
        } else if(Input.GetKey(KeyCode.S)){
            currentState = directionState.down;
        } else if(Input.GetKey(KeyCode.A)){
            faceLeft = false;
            myRenderer.flipX = false;
            currentState = directionState.left;
        } else if(Input.GetKey(KeyCode.D)){
            faceLeft = true;
            myRenderer.flipX = true;
            currentState = directionState.right;
        } else {
            currentState = directionState.none;
        }
    }

    void MoveMe(){ // state machine that checks the walking state, moves the player in that direction
        switch(currentState){
            case directionState.up:
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                //Debug.Log("W key pressed");
                break;
            case directionState.down:
                transform.Translate(Vector3.down * Time.deltaTime * speed);
                //Debug.Log("S key pressed");
                break;
            case directionState.left:
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                //Debug.Log("A key pressed");
                break;
            case directionState.right:
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                //Debug.Log("D key pressed");
                break;
            default:
                //Debug.Log("Nothing pressed");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other){ //this is going to figure out which direction to push the player back when they collide with something
        switch(currentState){
            case directionState.up:
                currentDir = collisionDir.up;
            break;
            case directionState.down:
                currentDir = collisionDir.down;
            break;
            case directionState.left:
                currentDir = collisionDir.left;
            break;
            case directionState.right:
                currentDir = collisionDir.right;
            break;
            default:
            break;
        }

    }

    //okay we're gonna try this move code instead to see if we can fix all the collision issues:
    void OnCollisionStay2D(Collision2D other){      
        switch(currentDir){
            case collisionDir.up:
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            break;
            case collisionDir.down:
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            break;
            case collisionDir.right:
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            break;
            case collisionDir.left:
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            break;
            default:
            break;
        }

        if(other.gameObject.tag == "Exit door"){
            Debug.Log("exit was hit");
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName:"GameOver");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        GameObject otherObj = other.gameObject;
        sprRenderer = otherObj.GetComponent<SpriteRenderer>();
        if(other.gameObject.tag == "First Item Text"){
            introText.SetActive(true);
        } else if(other.gameObject.tag == "Second Item Text"){
            journalText.SetActive(true);
        } else if(other.gameObject.tag == "Third Item Text"){
            boneText.SetActive(true);
        } else if(other.gameObject.tag == "Spoopy Text"){
            spoopyText.SetActive(true);
        } else if(other.gameObject.tag == "First ghost"){
            ghostOne.SetActive(true);
        } else if(other.gameObject.tag == "Second ghost"){
            ghostTwo.SetActive(true);
        } else if(other.gameObject.tag == "Third ghost"){
            ghostThree.SetActive(true);
        } else if(other.gameObject.tag == "Fourth ghost"){
            ghostFour.SetActive(true);
        } else {
            introText.SetActive(false);
            journalText.SetActive(false);
            boneText.SetActive(false);
            spoopyText.SetActive(false);
            ghostOne.SetActive(false);
            ghostTwo.SetActive(false);
            ghostThree.SetActive(false);
            ghostFour.SetActive(false);
        } //OKAY there is probably a much better way to do this but since I've struggled with this for more than eight hours now I'm going to keep it and I will just be grateful it works
    } //Basically I have public gameObjects for the text of each of the items with text; they each reference a box collider over the object they correspond to...
    //which, when triggered, show the text. once the item is collected, the text goes away. (there has to be a seperate box collider since it has to be set to IsTrigger)

}
