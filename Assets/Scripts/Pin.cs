using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    //degrees to determine whether pin is standing or not
    public float standingThreshold;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(name + IsStanding());
    }

    public bool IsStanding()
    {
        var rotationInEuler = transform.rotation.eulerAngles;
        var xTilt = Mathf.Abs(rotationInEuler.x);
        var zTilt = Mathf.Abs(rotationInEuler.z);

        if (xTilt < standingThreshold && zTilt < standingThreshold)
            return true;

        return false;
    }
}
