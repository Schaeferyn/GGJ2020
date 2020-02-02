using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarSmashZone : MonoBehaviour
{
    [SerializeField] AltarHoverZone hoverZone;

    Altar altar;

    // Start is called before the first frame update
    void Start()
    {
        altar = GetComponentInParent<Altar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PalmHoverCheck") return;

        if(hoverZone.IsReady)
        {
            altar.CombineElements();
        }
    }
}
