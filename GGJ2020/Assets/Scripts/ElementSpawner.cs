using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : PinchActionTarget
{
    [SerializeField] GameObject obj_elementToSpawn;

    Transform t_this;

    // Start is called before the first frame update
    void Start()
    {
        t_this = transform;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void OnPinchAction(PinchGrabber pincher)
    {
        base.OnPinchAction(pincher);

        GameObject obj = (GameObject)Instantiate(obj_elementToSpawn, t_this.position, Random.rotation);
        Element elem = obj.GetComponent<Element>();
        elem.Initialize();

        Transform t = obj.transform;
        t.SetParent(t_this);
        t.localScale = Vector3.one;

        pincher.AddGrabObject(elem);
    }
}
