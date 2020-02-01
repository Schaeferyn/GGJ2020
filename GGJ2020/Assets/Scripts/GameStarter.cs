using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    [SerializeField] Collider c_target;
    [SerializeField] FingerData fingersToLock;
    [SerializeField] GameStarter otherStarter;

    [SerializeField] UnityEvent OnHoverStart;
    [SerializeField] UnityEvent OnHoverEnd;

    Collider c_this;
    MeshRenderer mr_this;

    [SerializeField] float f_hoverTimeNeeded = 1;
    float f_hoverTimer;
    bool b_isHovering = false;
    bool b_isCompleted = false;

    public bool IsCompleted
    {
        get { return b_isCompleted; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mr_this = GetComponent<MeshRenderer>();
        c_this = GetComponent<Collider>();
    }

    void Update()
    {
        if(b_isHovering)
        {
            f_hoverTimer -= Time.deltaTime;
            if (f_hoverTimer <= 0)
            {
                b_isCompleted = true;
                fingersToLock.FinalizeHandExtents();

                c_this.enabled = false;
                mr_this.enabled = false;

                if(otherStarter.IsCompleted)
                {
                    StartGame();
                }
            }
        }
    }

    void StartGame()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        OnHoverStart.Invoke();

        Debug.Log(other.name + " has entered at " + Time.time);

        if (other != c_target) return;

        b_isHovering = true;
        //f_hoverTimer = f_hoverTimeNeeded;
    }

    private void OnTriggerExit(Collider other)
    {
        OnHoverEnd.Invoke();

        Debug.Log(other.name + " has exited at " + Time.time);

        if (other != c_target) return;

        b_isHovering = false;
    }
}
