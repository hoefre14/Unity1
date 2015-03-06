/*
using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float ballInitialVelocity = 600f;


    private Rigidbody rb;
    private bool ballInPlay;
    
    void Awake () {

        rb = GetComponent<Rigidbody>();
    
    }

    void Update () 
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
        }
    }
}
*/


using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float ballInitialVelocity = 400f;

    private Rigidbody rb;
    private bool ballInPlay;
    Vector3 oldVel;


	// Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        oldVel = rb.velocity;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            if (!GM.instance.timerStarted)
                GM.instance.startTimer();

            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(40f, ballInitialVelocity, 0));
        }
    }

    void OnCollisionEnter(Collision c)
    {
        ContactPoint cp = c.contacts[0];
        // calculate with addition of normal vector
         rb.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;

        // calculate with Vector3.Reflect
        rb.velocity = Vector3.Reflect(oldVel, cp.normal);

        // bumper effect to speed up ball
        //rb.velocity += cp.normal * 0.5f;
    }
}
