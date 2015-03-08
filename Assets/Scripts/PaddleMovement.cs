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
        Vector3 PO = col.other.rigidbody.velocity;
        Vector3 NO = col.rigidbody.velocity;
        float dot = Vector3.Dot(NO, PO);

        Vector3 OQ = PO - (2 * dot * NO);
       // col.collider.rigidbody.velocity = OQ;     

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
        
       // col.collider.rigidbody.AddForce(col.contacts[0].normal * 20f, ForceMode.VelocityChange);

        /*
        Vector2 ballPosition = col.collider.rigidbody.transform.position;
        Vector2 delta = ballPosition - paddlePosition;
        Vector2 direction = delta.normalized;
        col.collider.rigidbody.velocity = direction * 500f;
        */
        float force = 1f;
        if(hits < 4)
        {
            force = 1f;
        }
        else if(hits == 4)
        {
            force = 1.08f;
        }
        else if(hits == 12) 
        {
            force = 1.15f;
        }

        Rigidbody rigid = col.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance / 2, rigid.velocity.y, rigid.velocity.z) * force;
        
        /*
        foreach (ContactPoint contact in col.contacts)
        {
            if (contact.thisCollider == collider)
            {
                float z = contact.point.x - transform.position.x;
                contact.otherCollider.rigidbody.velocity = new Vector3(z * 20f, z * 20f, 0);
            }
        }
         * */
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
