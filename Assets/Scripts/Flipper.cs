using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    private HingeJoint hinge;
    public bool OnLeft = true;
    public string AxisName = "Horizontal";

    // Start is called before the first frame update
    void Start()
    {
        hinge = gameObject.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JointSpring hingeSpring = hinge.spring;
        JointLimits hingeLimits = hinge.limits;

        bool SwingBat = (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")) < 0);
        if(!OnLeft)
        {
            SwingBat = (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")) > 0);
        }

        if(SwingBat)
        {
            hingeSpring.targetPosition = hingeLimits.max;
        }
        else
        {
            hingeSpring.targetPosition = hingeLimits.min;
        }
        hinge.spring = hingeSpring;
    }
}
