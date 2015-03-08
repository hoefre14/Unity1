using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class nameInputButtonScript : MonoBehaviour {

    public InputField nameInput;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OkButtonPressed()
    {
        if(!string.IsNullOrEmpty(nameInput.text))
        {
            GM.instance.getNameEnteredAndSaveHighScore(nameInput.text);
        }
    }
}
