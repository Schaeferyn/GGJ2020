using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AltarHoverZone : MonoBehaviour
{
    [SerializeField] float f_hoverTimeNeeded = 2.0f;
    float f_hoverTimer;

    [SerializeField] MeshRenderer mr_hover;
    Material mat_hover;
    float f_hoverPercent;
    bool b_isHovering = false;

    public bool IsReady
    {
        get { return f_hoverPercent > 0.9f; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mat_hover = mr_hover.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_isHovering)
        {
            if(f_hoverPercent < 1)
            {
                f_hoverPercent += Time.deltaTime;
                mat_hover.SetFloat("_Percent", f_hoverPercent);
            }
        }
        else
        {
            if(f_hoverPercent > 0)
            {
                f_hoverPercent -= Time.deltaTime * 2;
                mat_hover.SetFloat("_Percent", f_hoverPercent);
            }
        }

        //tm_debug.text = f_hoverPercent.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PalmHoverCheck") return;

        b_isHovering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "PalmHoverCheck") return;

        b_isHovering = false;
    }
}
