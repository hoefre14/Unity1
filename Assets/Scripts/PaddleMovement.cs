using UnityEngine;
using System.Collections;


public class PaddleMovement : MonoBehaviour {

    

    public float paddleSpeed = 0.1f;
    private int brickCheck = 0;


    private Vector3 playerPos = new Vector3(0, -2f, 0);

    void Update()
    {
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -5.7f, 5.7f), -4f, 0f);
        transform.position = playerPos;
    }

    void OnCollisionEnter(Collision col)
    {
/*
        Vector3 PO = col.other.transform.position;
        Vector3 NO = new Vector3(0, 1, 0);
        Vector3 NO_PO_Scaled = Vector3.Scale(NO, PO);
        NO_PO_Scaled = Vector3.Scale(NO_PO_Scaled, new Vector3(2, 2, 2));
        NO_PO_Scaled = Vector3.Scale(NO_PO_Scaled, NO);
        Vector3 OQ = PO - NO_PO_Scaled;
        
        col.other.rigidbody.AddForceAtPosition(new Vector3(650f, 0, 0), OQ);     
 * 
 * */
        /*
        Debug.Log("Bricks total: " + GM.instance.Bricks);
        Debug.Log("last checked bricks: " + GM.instance.LastNumberOfBricksRemaining);
        Debug.Log("hit count: " + GM.instance.PaddleHitCountWithBricksDestroyedInBetween);
         * */
        if (GM.instance.Bricks != this.brickCheck)
        {
            GM.instance.PaddleHitCountWithBricksDestroyedInBetween++;
        }
        this.brickCheck = GM.instance.Bricks;
        GM.instance.BricksHitInARow = 0;
        GM.instance.PaddleHitCount++;
        int hits = GM.instance.PaddleHitCount;

        /*
        Vector3 PO = col.other.transform.position;
        Vector3 NO = new Vector3(0, 1, 0);
        Vector3 NO_PO_Scaled = Vector3.Scale(NO, PO);
        NO_PO_Scaled = Vector3.Scale(NO_PO_Scaled, new Vector3(2, 2, 2));
        NO_PO_Scaled = Vector3.Scale(NO_PO_Scaled, NO);
        Vector3 OQ = PO - NO_PO_Scaled;

        col.collider.rigidbody.AddForce(OQ);
         * */

        float force = 350;
        if(hits < 4)
        {
            force = 350;
        }
        else if(hits >= 4 && hits <= 11)
        {
            force = 400;
        }
        else if(hits >= 12) 
        {
            force = 450;
        }
        
        
        foreach (ContactPoint contact in col.contacts)
        {
            if (contact.thisCollider == collider)
            {
                float z = contact.point.x - transform.position.x;
                contact.otherCollider.rigidbody.AddForce(z + 100f, z + 100f, 0);
            }
        }
    }
    /*

    public float paddleSpeed = 1f;


    private Vector3 playerPos = new Vector3(0, -9.5f, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9.5f, 0f);
        transform.position = playerPos;
         * */
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("LEFT!");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("RIGHT!");
        }
         /*
 

        transform.Translate(0.1f * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
        
	}
    */
}
