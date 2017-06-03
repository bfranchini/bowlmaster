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
        
	    rigidBody.rotation = Quaternion.Euler(0, yRotation + 1 * Time.deltaTime, 0);
	}
}
