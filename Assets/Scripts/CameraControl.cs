using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Ball ball;
    private Vector3 offset;    

	// Use this for initialization
	void Start ()
	{
        //the offset is based on the camera's initial position
	    offset = transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //checking that the ball's transform hasn't gone over 1829(the position of the first pin) 
        //will allow the camera to follow the ball around when it is reset
        if (ball.transform.position.z <= 1829)        
            transform.position = ball.transform.position + offset;                
    }
}
