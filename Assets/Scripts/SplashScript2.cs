using UnityEngine;
using System.Collections;

public class SplashScript2 : MonoBehaviour {
    public float delayTime = 2;


    IEnumerator Start () {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delayTime);
        
        Application.LoadLevel ("Menu2");

	}
}
