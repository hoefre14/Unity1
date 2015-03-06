using UnityEngine;
using System.Collections;



public class Dead : MonoBehaviour
{
    public GameObject sound;
    public GameObject soundLevel2;
    void OnTriggerEnter(Collider col)
    {
        GM.instance.LoseLife();

        switch (Application.loadedLevelName)
        {
            case "Scene1": Instantiate(sound); break;
            case "Scene2": Instantiate(soundLevel2); break;
            default: break;
        }

    }
}