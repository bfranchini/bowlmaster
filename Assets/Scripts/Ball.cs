using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public float speed = 300.0f;
    public Vector3 launchVelocity;
    private Rigidbody rigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Launch();
	}

    public void Launch()
    {

        rigidBody.velocity = launchVelocity;//new Vector3(0, 0, speed);
        audioSource.Play();
    }

    // Update is called once per frame
	void Update ()
    {
	}
}
