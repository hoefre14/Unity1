using UnityEngine;
using System.Collections;

public class SplashScript2 : MonoBehaviour {
    public float delayTime = 2;


    IEnumerator Start () {
        yield return new WaitForSeconds(delayTime);
        
        Application.LoadLevel ("Menu2");

	}
}
