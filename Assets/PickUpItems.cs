using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PickUpItems : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean leftButtonInput;
    [SerializeField] private SteamVR_Input_Sources leftButtonInputSource = SteamVR_Input_Sources.LeftHand;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private bool itemsInTrigger = false;
    [SerializeField] private Vector3 itemposition;
    private GameObject heldItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (leftButtonInput.GetState(leftButtonInputSource))
        {
            PickUp();
        }
        else if(!leftButtonInput.GetState(leftButtonInputSource))
        {
            Release();
        }    
        
    }
    private void PickUp()
    {
        anim.SetBool("idle", true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.15f, itemLayer);
        if(0 < colliders.Length && heldItem == null)
        {
            GameObject thisTtem = colliders[0].gameObject;
            thisTtem.transform.parent = gameObject.transform;
            thisTtem.transform.localPosition = itemposition;
            thisTtem.GetComponent<Rigidbody>().isKinematic = true;
            thisTtem.transform.rotation = transform.rotation;
            heldItem = thisTtem;
        }
        

    }
    private void Release()
    {
        anim.SetBool("idle", false);
        if(heldItem != null)
        {
            heldItem.transform.parent = null;
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem = null;
        }
    }
 
}
