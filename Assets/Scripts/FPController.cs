﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour {

    [SerializeField] [Tooltip("snelheid van speler")] private float speed = 10.0f;
    private float translation;
    private float straffe;
    private float flying;
    private Rigidbody rb;
    private Collider col;
    private bool noclipmodes = false;
    

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        col = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(straffe, 0, translation);
        if (Input.GetKeyDown("escape")) {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            noclipmodes = !noclipmodes;
            rb.isKinematic = !rb.isKinematic;
            rb.detectCollisions = !rb.detectCollisions;
            col.enabled = !col.enabled;
        }

        if (noclipmodes)
        {
            if (Input.GetKey(KeyCode.Space)) transform.Translate(0, speed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            translation = Input.GetAxis("Vertical") * (speed * 3) * Time.deltaTime;
            straffe = Input.GetAxis("Horizontal") * (speed * 3) * Time.deltaTime;
        }
        else
        {
            straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            translation = Input.GetAxis("Vertical") * speed  * Time.deltaTime;
        }
    }
}
