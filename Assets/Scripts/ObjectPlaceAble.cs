using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceAble : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private string groundTagName;
    [SerializeField] private Vector3 overlapboxRange;

    public bool CheckIfObjectIsAllowedToBePlaced()
    {
        
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, overlapboxRange, Quaternion.identity, m_LayerMask);
        if (hitColliders.Length == 1 && hitColliders[0].gameObject.tag == groundTagName)
        {
            print("testje");
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, overlapboxRange);
    }
}