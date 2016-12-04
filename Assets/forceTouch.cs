using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceTouch : MonoBehaviour {

    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust);
    }
}

