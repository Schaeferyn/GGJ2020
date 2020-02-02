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

    Transform t_this;
    [SerializeField] Rigidbody r_this;
    [SerializeField] Collider c_this;

    ParticleSystem ps_element;

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
        t_this = transform;
        ps_element = r_this.GetComponent<ParticleSystem>();

        //c_this = GetComponent<Collider>();
        //r_this = c_this.attachedRigidbody;
    }

    public void BeginGrab(Transform t_hand)
    {
        r_this.isKinematic = true;
        t_this.SetParent(t_hand);

        ps_element.Play();
    }

    public void EndGrab()
    {
        r_this.isKinematic = false;
        t_this.SetParent(null);

        ps_element.Stop();
    }
}
