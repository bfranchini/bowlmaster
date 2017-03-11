using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour
{
    private Ball ball;
    private float dragStartTime;
    private Vector3 dragStartPos, dragEndPos;

	// Use this for initialization
	void Start ()
	{
	    ball = GetComponent<Ball>();
	}

    public void DragStart()
    {
        //capture time & position of drag start
        dragStartTime = Time.time;
        dragStartPos = Input.mousePosition;
    }

    public void DragEnd()
    {        
        //launch ball based on speed and direction of swipe
        dragEndPos = Input.mousePosition;

        //speed = distance/time
        var dragDuration = Time.time - dragStartTime;

        //we map y to z since we want the ball to move forward in the z direction
        float launchSpeedX = (dragEndPos.x - dragStartPos.x) / dragDuration;
        float launchSpeedZ = (dragEndPos.y - dragStartPos.y)/ dragDuration;

        var launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);

        ball.Launch(launchVelocity);
    }
}
