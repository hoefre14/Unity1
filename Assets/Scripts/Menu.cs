using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{


   public bool muteToggle = false;

    public void ChangeToScene()
    {
        if (GM.instance == null)
            Application.LoadLevel("Scene1");
        else
            GM.instance.loadNextLevel(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
    public void MuteGame(){

        if (muteToggle == true)
        {
            AudioListener.volume = 0;
        }
        else if (muteToggle == false)
        {
            AudioListener.volume = 1;
        }
   
    }


}
