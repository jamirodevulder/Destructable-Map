using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public bool inhand = false;
    public Rigidbody rb;
    public LayerMask itemLayer;
    private bool inAlienHand = false;
    public GameObject roboHand;
    [SerializeField] private Vector3 robohandRotation;
    [SerializeField] private Vector3 holdsterRotation;
    [SerializeField] private Vector3 alienHandPosition;
    [SerializeField] private float holdRange = 0.15f;
    public void SetInAlienHand(bool val)
    {
        inAlienHand = val;
    }
    public bool GetInAlienHand()
    {
        return inAlienHand;
    }
    public void SetInHand(bool val)
    {
        inhand = val;
    }
    private void Update()
    {
        checkForAlienRobotHand();
    }
    public Vector3 GetItemPositionForAlienHand()
    {
        return alienHandPosition;
    }
    public void checkForAlienRobotHand()
    {
        if (!rb.isKinematic && inAlienHand)
        {
            print("latenValle");
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.15f, itemLayer);
            if (0 < colliders.Length)
            {
                if (!colliders[0].GetComponent<Occupie>().GetOccuPied())
                { 
                    print("test");
                    GameObject thisTtem = colliders[0].gameObject;
                    transform.parent = colliders[0].gameObject.transform;
                    transform.position = colliders[0].gameObject.transform.position;
                    GetComponent<Rigidbody>().isKinematic = true;
                    if (colliders[0].gameObject.tag != "Holder")
                    {
                        inhand = true;
                        transform.localRotation = Quaternion.Euler(robohandRotation);
                        transform.position = colliders[0].gameObject.transform.position;
                    }
                    else
                    {
                        transform.localRotation = Quaternion.Euler(holdsterRotation);
                        transform.position = new Vector3(colliders[0].gameObject.transform.position.x, colliders[0].gameObject.transform.position.y + 0.2f, colliders[0].gameObject.transform.position.z);
                    }
                }
            }
            inAlienHand = false;
        }
    }
    
}
