using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Parent class for interactive objects that animate their velocity on a bidirectional axis.
 * Handles the common functionality of referencing an animation curve over a period of time
 * to get the animated velocity value.
 */
public abstract class InteractiveElementVelocityAnimate : MonoBehaviour
{
    public AnimationCurve VelocityCurve;
    public float MaxVelocity;
    public float EasingDuration;
    public bool ReverseDirection = true;
    public string AxisName = "Horizontal";

    protected Rigidbody rigidBody;

    //Represents how committed we are to the current velocity animation.
    //Our position along the velocity animation curve is represented by (EasingProgress / RotationEasingDuration).
    protected float EasingProgress;
    
    protected float animateVelocityFromInput(int axisDirection)
    {
        if(axisDirection < 0)
        {
            EasingProgress -= Time.fixedDeltaTime;
        }
        else if(axisDirection > 0)
        {
            EasingProgress += Time.fixedDeltaTime;
        }
        else //Not moving
        {
            //Stabilize EasingProgress to 0
            if(EasingProgress != 0.0f)
            {
                if(EasingProgress > 0.0f)
                {
                    EasingProgress -= Time.fixedDeltaTime;
                }
                else
                {
                    EasingProgress += Time.fixedDeltaTime;
                }

                //If we're close enough to 0, clamp to 0.
                float clampToZeroRange = 0.09f;
                if(Mathf.Abs(EasingProgress) < clampToZeroRange)
                {
                    EasingProgress = 0.0f;
                }
            }
        }

        //Clamp EasingProgress to [-EasingDuration, EasingDuration]
        EasingProgress = Mathf.Clamp(EasingProgress, 0 - EasingDuration, EasingDuration);

        //Get velocity. Velocity is defined by progress along a mirrored animation curve
        float velocityIntensity = VelocityCurve.Evaluate(EasingProgress / EasingDuration) * MaxVelocity;
        //Account for negative axis input, as velocity curve is always positive
        if(EasingProgress < 0)
        {
            velocityIntensity *= -1;
        }

        return velocityIntensity;
    }

    protected abstract Rigidbody applyVelocity(Rigidbody rb, float velocity);

    void Start()
    {
        EasingProgress = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
