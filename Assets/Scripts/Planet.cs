using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    private Rigidbody rigidBody { get; set; }

	// Use this for initialization
	void Start ()
	{
	    rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var yRotation = rigidBody.rotation.eulerAngles.y;
        
        //rotate planet at a rate of 1 degree per second
	    rigidBody.rotation = Quaternion.Euler(0, yRotation + Time.deltaTime, 0);
	}
}
