using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanedChecker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            print("hij is schoon!");
        }
    }
}
