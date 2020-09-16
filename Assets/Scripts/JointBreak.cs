using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    [SerializeField] private FixedJoint joint;
    private FixedJoint thisJoint;
    private bool ownJointBroken = false;
    [SerializeField] private float breakforce;

    void Start()
    {
        thisJoint = GetComponent<FixedJoint>();    
    }
    void Update()
    {
        if(joint == null & ownJointBroken)
        {
            gameObject.tag = "Trash";
        }
    }
    private void OnJointBreak(float breakForce)
    {
        ownJointBroken = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && thisJoint != null)
            thisJoint.breakForce = 0;
    }
}
