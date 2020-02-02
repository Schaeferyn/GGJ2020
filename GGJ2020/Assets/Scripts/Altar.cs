using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    Collider c_this;

    List<Element> elements = new List<Element>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Element") return;

        Element elem = other.GetComponentInParent<Element>();
        elements.Add(elem);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Element") return;

        Element elem = other.GetComponentInParent<Element>();
        elements.Remove(elem);
    }
}
