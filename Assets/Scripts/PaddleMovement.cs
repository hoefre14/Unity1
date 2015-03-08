﻿using UnityEngine;
using System.Collections;


public class PaddleMovement : MonoBehaviour {

    

    public float paddleSpeed = 0.1f;
    private int brickCheck = 0;
    private float force = 1f;


    private Vector3 playerPos = new Vector3(0, -2f, 0);

    void Update()
    {
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -5.3f, 5.3f), -4f, 0f);
        transform.position = playerPos;
    }

    void OnCollisionEnter(Collision col)
    {
        if (GM.instance.Bricks != this.brickCheck)
        {
            GM.instance.PaddleHitCountWithBricksDestroyedInBetween++;
        }
        this.brickCheck = GM.instance.Bricks;
        GM.instance.BricksHitInARow = 0;
        GM.instance.PaddleHitCount++;
        int hits = GM.instance.PaddleHitCount;

        Rigidbody rigid = col.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        /*
        if(hits < 4)
        {
            force = 1f;
        }*/
        if (hits == 4)
        {
            force = 1.2f;
            rigid.velocity = new Vector3(rigid.velocity.x + xDistance / 2, rigid.velocity.y, rigid.velocity.z) * force;
        }
        else if (hits == 12)
        {
            force = 1.3f;
            rigid.velocity = new Vector3(rigid.velocity.x + xDistance / 2, rigid.velocity.y, rigid.velocity.z) * force;
        }
        else
        {
            rigid.velocity = new Vector3(rigid.velocity.x + xDistance / 2, rigid.velocity.y, rigid.velocity.z);
        }

    }
}

