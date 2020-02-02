using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class PinchGrabber : MonoBehaviour
{
    Transform t_this;
    Hand hand;

    [SerializeField] float[] f_grabThresholds;
    string handLabel = "";

    TextMeshPro[] tmp_debugs;

    float[] f_strengths;

    bool b_wasGrabbing;
    bool b_isGrabbing;

    const float f_grabDist = 0.05f;
    Transform t_thumbTip;
    List<Collider> grabbedObjects = new List<Collider>();
    int i_numGrabs;

    public Transform HandTransform
    {
        get { return t_this; }
    }

    public Transform ThumbTip
    {
        get { return t_thumbTip; }
    }

    // Start is called before the first frame update
    void Start()
    {
        t_this = transform;
        hand = GetComponent<Hand>();
        
        if(hand.HandType == OVRPlugin.Hand.HandLeft)
        {
            handLabel = "left";
        }
        else if(hand.HandType == OVRPlugin.Hand.HandRight)
        {
            handLabel = "right";
        }

        //tmp_debugs = new TextMeshPro[5];
        //for(int i=0;i<5;i++)
        //{
        //    string key = "debug" + handLabel + i.ToString();
        //    tmp_debugs[i] = GameObject.Find(key).GetComponent<TextMeshPro>();
        //}

        //f_strengths = new float[5];

        t_thumbTip = hand.Skeleton.Bones[19].transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHand();
    }

    void UpdateHand()
    {
        //f_strengths[0] = hand.PinchStrength(OVRPlugin.HandFinger.Thumb);
        //f_strengths[1] = hand.PinchStrength(OVRPlugin.HandFinger.Index);
        //f_strengths[2] = hand.PinchStrength(OVRPlugin.HandFinger.Middle);
        //f_strengths[3] = hand.PinchStrength(OVRPlugin.HandFinger.Ring);
        //f_strengths[4] = hand.PinchStrength(OVRPlugin.HandFinger.Pinky);

        //for(int i=0;i<5;i++)
        //{
        //    tmp_debugs[i].text = f_strengths[i].ToString();
        //}

        b_wasGrabbing = b_isGrabbing;
        b_isGrabbing = hand.PinchStrength(OVRPlugin.HandFinger.Thumb) > 0.95f;

        if(b_isGrabbing != b_wasGrabbing)
        {
            if (b_isGrabbing)
                OnGrabBegin();
            else
                OnGrabEnd();
        }
    }

    void OnGrabBegin()
    {
        Collider[] hits = Physics.OverlapSphere(t_thumbTip.position, f_grabDist);
        i_numGrabs = hits.Length;
        for(int i=0;i<i_numGrabs;i++)
        {
            PinchActionTarget actionTarget = hits[i].GetComponent<PinchActionTarget>();
            if(actionTarget)
            {
                actionTarget.OnPinchAction(this);
            }

            PinchGrabTarget grabTarget = hits[i].GetComponent<PinchGrabTarget>();           
            if (!grabTarget) continue;
                
            grabbedObjects.Add(hits[i]);
            hits[i].transform.SetParent(t_this);
            
            Rigidbody body = hits[i].GetComponent<Rigidbody>();
            if(body)
            {
                body.isKinematic = true;
            }
            
        }
    }

    void OnGrabEnd()
    {
        for(int i=0;i<i_numGrabs;i++)
        {
            Rigidbody body = grabbedObjects[i].GetComponent<Rigidbody>();
            if (body)
            {
                body.isKinematic = false;
                body.useGravity = true;
            }

            grabbedObjects[i].transform.SetParent(null);
        }

        grabbedObjects.Clear();
        i_numGrabs = 0;
    }
}
