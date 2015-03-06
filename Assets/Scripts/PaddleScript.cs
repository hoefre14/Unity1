using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

    public float yposition = 0;
    public float zposition = 0;
    public float xboundary = 0;
    public float maxBoundary = 10;
    public float speed = 115;
    public GameObject Ball;
    public GameObject attachedBall = null;
    public Rigidbody BallRigidbody;
    public float ballspeed = 4250;
    public AudioClip paddlesound;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        //PaddleMovement

        if(Input.GetAxis("Horizontal")!=0)
        {
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, yposition, zposition);

            if (transform.position.x < -xboundary + maxBoundary)
            {
                transform.position = new Vector3(-xboundary + maxBoundary, yposition, zposition);
            }
            else  if (transform.position.x > xboundary -maxBoundary)
            {
                transform.position = new Vector3(xboundary - maxBoundary, yposition, zposition);
            }
        }

	
	}
}
