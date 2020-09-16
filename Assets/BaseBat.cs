using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBat : Weapon
{
    private BoxCollider col;
    private bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        base.checkForAlienRobotHand();
        if(GetInAlienHand())
        {
            col.enabled = false;
            check = false;
        }
        else if(!check)
        {
            col.enabled = true;
            check = true;
        }
    }
}
