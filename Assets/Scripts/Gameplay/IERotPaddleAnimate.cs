using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IERotPaddleAnimate : InteractiveElementVelocityAnimate
{
    protected override Rigidbody applyVelocity(Rigidbody rb, float rotationalVelocity)
    {
        //Apply rotational velocity. Don't apply forces, since we have an animation curve.
        //This is why this MUST be called in FixedUpdate, not Update.
        rb.angularVelocity = Vector3.forward * (rotationalVelocity * Time.fixedDeltaTime);

        return rb;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int directionMultiplier = (ReverseDirection ? -1 : 1);
        //Bin input axis to [-1, 0, 1]
        int intendedRotationDirection = directionMultiplier * Mathf.RoundToInt(Input.GetAxisRaw(AxisName));

        float rotVelocity = animateVelocityFromInput(intendedRotationDirection);
        applyVelocity(rigidBody, rotVelocity);
    }
}
