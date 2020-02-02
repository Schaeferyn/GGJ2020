using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : PinchActionTarget
{
    [SerializeField] GameObject obj_elementToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void OnPinchAction(PinchGrabber pincher)
    {
        base.OnPinchAction(pincher);


    }
}
