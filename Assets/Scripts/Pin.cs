using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    //degrees to determine whether pin is standing or not
    public float standingThreshold;
    public float distanceToRaise = 60f;
    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(name + IsStanding());
    }

    public bool IsStanding()
    {
        var rotationInEuler = transform.rotation.eulerAngles;

        //pins have a rotation offset in the x-axis since we took off their child objects.
        var xTilt = Mathf.Abs(270 - rotationInEuler.x);
        var zTilt = Mathf.Abs(rotationInEuler.z);

        if (xTilt < standingThreshold && zTilt < standingThreshold)
            return true;

        return false;
    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            //we need the Spance.World parameter because we're moving the object relative to world space
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);

            rigidBody.useGravity = false;
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        rigidBody.useGravity = true;
    }
}
