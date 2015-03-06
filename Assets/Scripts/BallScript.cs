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


	// Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody>();

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
}
