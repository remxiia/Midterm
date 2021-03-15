using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    //private Camera _cfirstCamera; //process of trying to get the camera to switch when the first ball colides with the domino wish me luck
    public Transform followTransform;
    public BoxCollider2D worldBounds;

    float xMin;
    float yMin;
    float xMax;
    float yMax;

    float camX;
    float camY;

    float camRatio;
    float camSize;
    float camWidth;
    float camHeight;

    Camera mainCam;

    Vector3 smoothPos;

    public float smoothRate;
    
    // Start is called before the first frame update
    void Start()
    {
        //_cfirstCamera = GameObject.Find("firstCamera").GetComponentInChildren<Camera>(); //pt. 1 -----------------
        //GetComponent<Camera>().enabled = false;
        //_cfirstCamera.enabled = true; // pt. 2-----------------------------------

        xMin = worldBounds.bounds.min.x; //what xMin holds
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;

        mainCam = gameObject.GetComponent<Camera>();

        camSize = mainCam.orthographicSize;
        //camRatio = (xMax + camSize) / 8.0f;
        //apparently the above doesn't work? trying this instead:
        camWidth = mainCam.aspect * camSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.gameObject.name == "hit domino"){
    //        _cfirstCamera.enabled = false;
    //        GetComponent<Camera>().enabled = true;
    //        Debug.Log("you hit the domino. huzzah");
    //    }
    //}

    void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);

        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate); 
        //remember: [this] = [gameObject]

        gameObject.transform.position = smoothPos;
    }
}
