using UnityEngine;
using System.Collections;
using System;

public class Bricks : MonoBehaviour {

    public GameObject sound;
    public GameObject sound2;
    public GameObject sound3;

    public GameObject soundLevel2;
    public GameObject sound2Level2;
    public GameObject sound3Level3;

    public GameObject particles;


    void awake()
    {

    }

    void OnCollisionEnter (Collision other)
    {
        System.Random r = new System.Random();
        int random = r.Next(1, 4);
        GM.instance.BricksHitInARow++;
        Instantiate(particles, this.transform.position, Quaternion.identity);

        switch (GM.instance.getCurrentLevel())
        {
            case 1:
                switch (random)
                {
                    case 1: GameObject.Instantiate(sound); break;
                    case 2: GameObject.Instantiate(sound2); break;
                    case 3: GameObject.Instantiate(sound); break;
                    default: GameObject.Instantiate(sound); break;
                }
                break;
            case 2:
                switch (random)
                {
                    case 1: GameObject.Instantiate(soundLevel2); break;
                    case 2: GameObject.Instantiate(sound2Level2); break;
                    case 3: GameObject.Instantiate(sound3Level3); break;
                    default: GameObject.Instantiate(soundLevel2); break;
                }
                break;
            default: break;
        }


        GM.instance.DestroyBrick();
        Destroy(gameObject); 
    }   


}