using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FingerData : MonoBehaviour
{
    OVRHand hand;
    OVRSkeleton skeleton;

    Transform[] t_thumbs;
    Transform[] t_indexes;
    Transform[] t_middles;
    Transform[] t_rings;
    Transform[] t_pinkies;

    [HideInInspector] public float extentThumb;
    [HideInInspector] public float extentIndex;
    [HideInInspector] public float extentMiddle;
    [HideInInspector] public float extentRing;
    [HideInInspector] public float extentPinky;

    [SerializeField] TextMeshPro[] tmp_debugs;
    [SerializeField] TextMeshPro[] tmp_debugs2;
    [SerializeField] TextMeshPro[] tmp_debugs3;

    bool b_hasInitialized = false;
    bool b_hasHandExtents = false;

    [SerializeField] Transform[] t_debugs;

    float[] f_minExtents;
    float[] f_maxExtents;
    float[] f_extentions;

    // Start is called before the first frame update
    //void Start()
    //{
    //    Invoke("DelayStart", 0.3f);
    //}

    void Start()
    {
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();

        t_thumbs = new Transform[3];
        t_indexes = new Transform[3];
        t_middles = new Transform[3];
        t_rings = new Transform[3];
        t_pinkies = new Transform[3];
        
        f_extentions = new float[5];

        t_thumbs[0] = skeleton.Bones[3].Transform;
        t_thumbs[1] = skeleton.Bones[4].Transform;
        t_thumbs[2] = skeleton.Bones[5].Transform;

        t_indexes[0] = skeleton.Bones[6].Transform;
        t_indexes[1] = skeleton.Bones[7].Transform;
        t_indexes[2] = skeleton.Bones[8].Transform;

        t_middles[0] = skeleton.Bones[9].Transform;
        t_middles[1] = skeleton.Bones[10].Transform;
        t_middles[2] = skeleton.Bones[11].Transform;

        t_rings[0] = skeleton.Bones[12].Transform;
        t_rings[1] = skeleton.Bones[13].Transform;
        t_rings[2] = skeleton.Bones[14].Transform;

        t_pinkies[0] = skeleton.Bones[16].Transform;
        t_pinkies[1] = skeleton.Bones[17].Transform;
        t_pinkies[2] = skeleton.Bones[18].Transform;

        f_minExtents = new float[5];
        f_maxExtents = new float[5];
        for(int i=0;i<5;i++)
        {
            f_minExtents[i] = 99;
            f_maxExtents[i] = -99;
        }

        b_hasInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFingerData();

        if(!b_hasHandExtents)
        {
            UpdateHandExtents();
        }
    }

    void UpdateFingerData()
    {
        if (!b_hasInitialized) return;

        t_debugs[0].position = t_thumbs[0].position;
        t_debugs[1].position = t_thumbs[1].position;
        t_debugs[2].position = t_thumbs[2].position;

        t_debugs[3].position = t_indexes[0].position;
        t_debugs[4].position = t_indexes[1].position;
        t_debugs[5].position = t_indexes[2].position;

        t_debugs[6].position = t_middles[0].position;
        t_debugs[7].position = t_middles[1].position;
        t_debugs[8].position = t_middles[2].position;

        t_debugs[9].position = t_rings[0].position;
        t_debugs[10].position = t_rings[1].position;
        t_debugs[11].position = t_rings[2].position;

        t_debugs[12].position = t_pinkies[0].position;
        t_debugs[13].position = t_pinkies[1].position;
        t_debugs[14].position = t_pinkies[2].position;

        Vector3 dir1, dir2;

        dir1 = t_thumbs[1].position - t_thumbs[0].position;
        dir2 = t_thumbs[2].position = t_thumbs[1].position;
        dir1.Normalize();
        dir2.Normalize();

        extentThumb = Mathf.Clamp01(Vector3.Dot(dir1, dir2));
        f_extentions[0] = extentThumb;

        dir1 = t_indexes[1].position - t_indexes[0].position;
        dir2 = t_indexes[2].position = t_indexes[1].position;
        dir1.Normalize();
        dir2.Normalize();

        extentIndex = Mathf.Clamp01(Vector3.Dot(dir1, dir2));
        f_extentions[1] = extentIndex;

        dir1 = t_middles[1].position - t_middles[0].position;
        dir2 = t_middles[2].position = t_middles[1].position;
        dir1.Normalize();
        dir2.Normalize();

        extentMiddle = Mathf.Clamp01(Vector3.Dot(dir1, dir2));
        f_extentions[2] = extentMiddle;

        dir1 = t_rings[1].position - t_rings[0].position;
        dir2 = t_rings[2].position = t_rings[1].position;
        dir1.Normalize();
        dir2.Normalize();

        extentRing = Mathf.Clamp01(Vector3.Dot(dir1, dir2));
        f_extentions[3] = extentRing;

        dir1 = t_pinkies[1].position - t_pinkies[0].position;
        dir2 = t_pinkies[2].position = t_pinkies[1].position;
        dir1.Normalize();
        dir2.Normalize();

        extentPinky = Mathf.Clamp01(Vector3.Dot(dir1, dir2));
        f_extentions[4] = extentPinky;

        if(b_hasHandExtents)
        {
            extentThumb = Mathf.Clamp01(Mathf.InverseLerp(f_minExtents[0], f_maxExtents[0], extentThumb));
            extentIndex = Mathf.Clamp01(Mathf.InverseLerp(f_minExtents[1], f_maxExtents[1], extentIndex));
            extentMiddle = Mathf.Clamp01(Mathf.InverseLerp(f_minExtents[2], f_maxExtents[2], extentMiddle));
            extentRing = Mathf.Clamp01(Mathf.InverseLerp(f_minExtents[3], f_maxExtents[3], extentRing));
            extentPinky = Mathf.Clamp01(Mathf.InverseLerp(f_minExtents[4], f_maxExtents[4], extentPinky));

            tmp_debugs3[0].text = extentThumb.ToString();
            tmp_debugs3[1].text = extentIndex.ToString();
            tmp_debugs3[2].text = extentMiddle.ToString();
            tmp_debugs3[3].text = extentRing.ToString();
            tmp_debugs3[4].text = extentPinky.ToString();
        }
    }

    void UpdateHandExtents()
    {
        for(int i=0;i<5;i++)
        {
            if (f_extentions[i] < f_minExtents[i])
                f_minExtents[i] = f_extentions[i];

            if (f_extentions[i] > f_maxExtents[i])
                f_maxExtents[i] = f_extentions[i];

            //tmp_debugs[i].text = f_minExtents.ToString();
            //tmp_debugs2[i].text = f_maxExtents.ToString();
        }
    }

    public void FinalizeHandExtents()
    {
        b_hasHandExtents = true;
    }
}
