using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandTest : MonoBehaviour
{
    OVRHand hand;
    OVRSkeleton skeleton;

    bool b_hasInitialized = false;
    
    int i_numBones;
    [SerializeField] Transform[] t_debugs;
    Transform[] t_targets;
    float[] f_pinchStrengths;
    
    [SerializeField] TextMeshPro[] tmp_amounts;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();

        i_numBones = skeleton.Bones.Count;
        t_targets = new Transform[i_numBones];
        for(int i=0;i<i_numBones;i++)
        {
            t_targets[i] = skeleton.Bones[i].Transform;
        }

        f_pinchStrengths = new float[5];

        b_hasInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_hasInitialized) return;

        for(int i=0;i<i_numBones;i++)
        {
            t_debugs[i].position = t_targets[i].position;
        }

        f_pinchStrengths[0] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        f_pinchStrengths[1] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        f_pinchStrengths[2] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        f_pinchStrengths[3] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        f_pinchStrengths[4] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        for(int i=0;i<5;i++)
        {
            tmp_amounts[i].text = f_pinchStrengths[i].ToString() + " %";
        }
    }
}
