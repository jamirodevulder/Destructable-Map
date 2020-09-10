using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupie : MonoBehaviour
{
    private bool occupied = false;
    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
    }

    public bool GetOccuPied()
    {
        return occupied;
    }
}
