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
    public void SetInAlienHand(bool val)
    {
        inAlienHand = val;
    }
    public void SetInHand(bool val)
    {
        inhand = val;
    }
    private void Update()
    {
        checkForAlienRobotHand();
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
                    transform.parent = roboHand.transform;
                    transform.position = roboHand.transform.position;
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.localRotation = Quaternion.Euler(robohandRotation.x, robohandRotation.y, robohandRotation.z);
                    inhand = true;
                }
            }
            inAlienHand = false;
        }
    }
    
}
