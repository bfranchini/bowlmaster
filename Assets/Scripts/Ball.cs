using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 initialPos;
    private PinSetter pinSetter;
    public bool InPlay;
    public float secondsInPlay;    

    //Ben's Code for nudge buttons
    //public bool inPlay = false;

	// Use this for initialization
	void Start ()
	{
	    initialPos = transform.position;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	    pinSetter = FindObjectOfType<PinSetter>();
	    rigidBody.useGravity = false;
	}

    void Update()
    {
        if (InPlay)        
            secondsInPlay += Time.deltaTime;
        else        
            secondsInPlay = 0f;
    }

    public void Launch(Vector3 velocity)
    {
        var animator = pinSetter.gameObject.GetComponent<Animator>();
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //is there really no easier way to check if an animation is playing?
        if (!InPlay &&
           !currentStateInfo.IsName("Tidy.Raise") &&
           !currentStateInfo.IsName("Tidy.Swipe") &&
           !currentStateInfo.IsName("Tidy.Lower") &&
           !currentStateInfo.IsName("Reset.Renew") &&
           !currentStateInfo.IsName("Reset.Swipe"))
        {
            rigidBody.useGravity = true;
            rigidBody.velocity = velocity;
            InPlay = true;

            audioSource.Play();
        }        
    }

    public void MoveStart(float xNudge)
    {
        //only allow user to nudge ball if it isn't moving
        if (rigidBody.velocity == Vector3.zero)
        {
            var ballRadius = GetComponent<SphereCollider>().radius;

            var nudgeVector = transform.position;

            var floor = GameObject.Find("Floor");

            //get the width of half of the floor and subtract from it the radius of the ball            
            var xLimit = floor.transform.lossyScale.x/2f - (ballRadius * transform.lossyScale.x);

            //This will keep the sides of the ball from going over the edge of the floor
            nudgeVector.x = Mathf.Clamp(nudgeVector.x + xNudge, -xLimit, xLimit);

            transform.position = nudgeVector;
        }
    }

    public void Reset()
    {
        transform.position = initialPos;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.rotation = Quaternion.identity;       
        rigidBody.useGravity = false;
        InPlay = false;
    }

    public void Strike()
    {
        rigidBody.velocity = new Vector3(0,0, 600f);
        rigidBody.useGravity = true;
        InPlay = true;
    }
}
