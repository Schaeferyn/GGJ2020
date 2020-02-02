using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Water,
    Leaf,
    Fire,
    Sand,
    Cloud,
    Bird,
    Snake,
    Monkey,
    Panther,
    Dolphin
}

public class Element : MonoBehaviour
{
    [SerializeField] ElementType elementType;

    [SerializeField] Rigidbody r_this;
    [SerializeField] Collider c_this;

    public Collider MyCollider
    {
        get { return c_this; }
    }

    public Rigidbody MyBody
    {
        get { return r_this; }
    }

    public void Initialize()
    {
        //c_this = GetComponent<Collider>();
        //r_this = c_this.attachedRigidbody;
    }

    //public void AttachToHand(Transform hand)
    //{

    //}
}
