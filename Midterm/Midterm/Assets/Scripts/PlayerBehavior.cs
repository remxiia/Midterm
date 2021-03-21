using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //need this so I can reference Text
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{

    //personal note: probably going to have to go through this and cut what I'm not using --------------------------------------
    
    public Text KeyText; //gonna need this to get text to show up to the game screen, I think
    public float speed; //honestly not sure if I'm gonna need this, might get deleted later
    public float framerate; //how many frames p/second

    public Sprite walkSprite;
    //private bool stopMovement = false;
    //private bool hitItem = false;
    private Vector3 nextPos;
    public GameManager gameManager; //lets me reference the game manager
    private collisionDir currentDir;

    public float sightDist;

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

    //component ref(s)
    BoxCollider2D myCollider;
    SpriteRenderer myRenderer;
    Animator myAnimator; //will need for getting the sprite animations to run once I have those ready to go

    public directionState currentState = directionState.none; //currently not moving (I think....)
    public static bool faceLeft = true; //start the game facing left (hopefully)

    private Text firstText; //****SHOULD help to start getting the text to show on screen when the char bumps into an obj

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    void Update() //following last week's tutorial just to see if I can get a raycast working because I am *struggling*
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, nextPos, sightDist); //creating a ray that's being cast in the direction of the position we're moving towards
        //only see within a certain distance, which is set in the inspector
        if(hit.collider != null){ //if the ray reached something,
            if(hit.collider.tag == "Item"){//and if the thing it reached is an Item,
                Debug.Log("Hit the item with the ray"); //show the text on the next line.
                gameManager.ShowText("Oh dear, you really shouldn't be here... But since you are, go and gather the objects scattered about. You'll need them to find out what happened here.");
            } //....none of this is working, the ray isn't hitting the Item HELPs
            //is it because I delete the object when it's hit? but the ray should reach the item before the player does
        }
        //} else{ //otherwise if the ray hit nothing,
        //    stopMovement = false;//this stays false
        //}  I THINK I SOMEHOW NEED TO ERASE THE TEXT HERE, OR HAVE IT SET SOMETHING THAT CONTAINS THE TEXT TO FALSE SO I CAN HAVE IT CHANGE/ERASE IT ON SCREEN
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

}
