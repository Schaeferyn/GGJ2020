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
    List<Rigidbody> grabbedBodies = new List<Rigidbody>();
    int i_numGrabs;

    [SerializeField] LayerMask layers_grab;
    [SerializeField] LayerMask layers_action;

    bool b_isActionDelayed = false;
    bool b_hasCompletedAction = false;
    const float f_thresholdTimeNeeded = 0.0f;
    float f_thresholdTimer;

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
            t_this.Find("Attachments").Rotate(0, 0, 180);
        }
        else if(hand.HandType == OVRPlugin.Hand.HandRight)
        {
            handLabel = "right";
        }

        t_thumbTip = hand.Skeleton.Bones[19].transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHand();
    }

    void UpdateHand()
    {
        b_wasGrabbing = b_isGrabbing;
        b_isGrabbing = hand.PinchStrength(OVRPlugin.HandFinger.Thumb) > 0.95f;

        if(b_isGrabbing != b_wasGrabbing)
        {
            b_isActionDelayed = true;
            b_hasCompletedAction = false;
            f_thresholdTimer = f_thresholdTimeNeeded;
        }

        if(b_isActionDelayed && !b_hasCompletedAction)
        {
            f_thresholdTimer -= Time.deltaTime;
            if(f_thresholdTimer <= 0)
            {
                if (b_isGrabbing)
                    OnGrabBegin();
                else
                    OnGrabEnd();

                b_hasCompletedAction = true;
            }
        }
    }

    void OnGrabBegin()
    {
        //grab targets
        Collider[] hits = Physics.OverlapSphere(t_thumbTip.position, f_grabDist, layers_grab);
        i_numGrabs = hits.Length;
        for(int i=0;i<i_numGrabs;i++)
        {
            PinchGrabTarget grabTarget = hits[i].GetComponent<PinchGrabTarget>();           
            if (!grabTarget) continue;
                
            grabbedObjects.Add(hits[i]);
            grabbedBodies.Add(hits[i].attachedRigidbody);
            hits[i].enabled = false;
            hits[i].transform.SetParent(t_this);

            Rigidbody body = hits[i].attachedRigidbody;
            if(body)
            {
                body.isKinematic = true;
            }
            
        }

        //action targets
        hits = Physics.OverlapSphere(t_thumbTip.position, f_grabDist, layers_action);
        i_numGrabs = hits.Length;

        PinchActionTarget closestTarget = null;
        float dist;
        float closestDist = 999;
        for (int i = 0; i < i_numGrabs; i++)
        {
            PinchActionTarget actionTarget = hits[i].GetComponent<PinchActionTarget>();
            if (actionTarget)
            {
                dist = Vector3.Distance(t_thumbTip.position, hits[i].ClosestPoint(t_thumbTip.position));
                if(dist < closestDist)
                {
                    closestTarget = actionTarget;
                    closestDist = dist;
                }
            }
        }

        closestTarget.OnPinchAction(this);
    }

    void OnGrabEnd()
    {
        for(int i=0;i<grabbedObjects.Count;i++)
        {
            //Rigidbody body = grabbedObjects[i].attachedRigidbody;
            Rigidbody body = grabbedBodies[i];
            if (body)
            {
                body.isKinematic = false;
                body.useGravity = true;
            }

            grabbedObjects[i].enabled = true;
            grabbedObjects[i].transform.SetParent(null);
        }

        grabbedObjects.Clear();
        i_numGrabs = 0;
    }

    public void AddGrabObject(Element element)
    {
        element.transform.SetParent(t_this);
        element.MyBody.isKinematic = true;
        element.MyCollider.enabled = false;

        i_numGrabs++;

        if (grabbedObjects.Contains(element.MyCollider)) return;

        grabbedObjects.Add(element.MyCollider);
        grabbedBodies.Add(element.MyBody);
    }
}
