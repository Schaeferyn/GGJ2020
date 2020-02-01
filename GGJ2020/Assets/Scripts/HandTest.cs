using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour
{
    OVRHand hand;
    OVRSkeleton skeleton;

    [SerializeField] Transform t_debug;
    bool b_hasDebug = false;

    Transform t_target;
    bool b_hasTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();

        b_hasDebug = t_debug != null;

        t_target = skeleton.Bones[0].Transform;
        b_hasTarget = t_target != null;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_hasDebug)
        {
            t_debug.position = t_target.position;
        }
    }
}
