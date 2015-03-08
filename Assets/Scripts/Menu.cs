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

    public void SelectLevel()
    {
        Application.LoadLevel("Scene1");
    }
    public void SelectLevel2()
    {
        Application.LoadLevel("Scene2");
    }
    public void HighScores()
    {
        Application.LoadLevel("Highscores");
    }
    public void ManuFromHS()
    {
        Application.LoadLevel("Menu2");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
    public void MuteGame(int index){

        if (index == 1)
        {
            AudioListener.volume = 1;
        }
        if (index == 2)
        {
            AudioListener.volume = 0;
        }
   
    }


}
