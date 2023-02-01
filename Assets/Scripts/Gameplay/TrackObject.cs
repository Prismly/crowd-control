using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TrackObject : InteractiveElementVelocityAnimate
{
    public SplineAnimate splineAnimateScript;


    protected override Rigidbody applyVelocity(Rigidbody rb, float trackVelocity)
    {
        //Apply track velocity.
        float elapsed = splineAnimateScript.ElapsedTime + (trackVelocity * Time.deltaTime);
        if(elapsed < 0)
        {
            elapsed += splineAnimateScript.Duration;
        }
        if(elapsed > splineAnimateScript.Duration)
        {
            elapsed -= splineAnimateScript.Duration;
        }
        splineAnimateScript.ElapsedTime = elapsed;
        return rb;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        splineAnimateScript.ElapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int directionMultiplier = (ReverseDirection ? -1 : 1);
        //Bin input axis to [-1, 0, 1]
        int intendedMovementDirection = directionMultiplier * Mathf.RoundToInt(Input.GetAxisRaw(AxisName));

        float trackVelocity = animateVelocityFromInput(intendedMovementDirection);
        applyVelocity(rigidBody, trackVelocity);
    }
}
