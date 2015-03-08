using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {
    public float delayTime = 3;


    IEnumerator Start () {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delayTime);
        
        Application.LoadLevel ("Splash2");

	}
}
