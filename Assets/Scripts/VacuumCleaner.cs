using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VacuumCleaner : Weapon
{
    [SerializeField] private SteamVR_Action_Boolean rightButtonInput;
    [SerializeField] private SteamVR_Input_Sources rightButtonInputSource = SteamVR_Input_Sources.RightHand;
    [SerializeField] private Vector3 overlapboxrange;
    [SerializeField] private LayerMask trashLayerMask;
    [SerializeField] private LayerMask raycastlayermask;
    
    private LineRenderer line;
    private CapsuleCollider capsulecoll;
    [SerializeField] private float length;
    void Awake()
    {
        capsulecoll = GetComponent<CapsuleCollider>();
        capsulecoll.enabled = false;
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        base.checkForAlienRobotHand();
        if (inhand && rightButtonInput.GetState(rightButtonInputSource))
        {
            line.enabled = true;
            capsulecoll.enabled = true;
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, overlapboxrange, Quaternion.identity, trashLayerMask);
            if (hitColliders.Length > 0)
            {
                if (hitColliders[0].gameObject.tag == "Trash" && hitColliders[0].gameObject.transform.localScale.x < 25f)
                {
                    Destroy(hitColliders[0].gameObject);
                }
            }
        }
        else if(!inhand || !rightButtonInput.GetState(rightButtonInputSource))
        {
            line.enabled = false;
            capsulecoll.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Trash" && rightButtonInput.GetState(rightButtonInputSource))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            float step = 3 * Time.deltaTime;
            other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, gameObject.transform.position, step);
            rb.mass = 0;
            rb.useGravity = false;
            if (other.gameObject.transform.localScale.x >  20f)
            {
                other.gameObject.transform.localScale -= new Vector3(1f, 1f, 1f); 
            }
        }
        else if(other.gameObject.tag == "Trash" && !rightButtonInput.GetState(rightButtonInputSource))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.mass = 1;
            rb.useGravity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trash")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.mass = 1;
            rb.useGravity = true;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), overlapboxrange);

    }
}
