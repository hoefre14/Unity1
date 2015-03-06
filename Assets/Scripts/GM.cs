using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
using System;

public class GM : MonoBehaviour
{

    public int lives = 3;
    private int bricks = 72;
    public int Bricks
    {
        get { return this.bricks; }
        set { this.bricks = value; }
    }
    public float resetDelay = 1f;
    public GameObject paddle;
    //public GameObject ball;
    
    public Text livesText;
    public Text ScoreText;
    public Text timeText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject youWonSound;
    public GameObject gameOverSound;
    public GameObject unstoppableSound;
    public GameObject holyShitSound;
    public GameObject whickedSickSound;
    public GameObject rampageSound;
    public GameObject godlikeSound;
    public GameObject unstoppableSound2;
    public GameObject dominatingSound2;
    public GameObject godlikeSound2;
    public GameObject backGroundMusicLev1;
    public GameObject backGroundMusicLev2;
    int secondsCounter = 0;
    private static Timer timer;
    public bool timerStarted = false;
    
    
    public GameObject bricksPrefab;
    public static GM instance = null;

    private GameObject clonePaddle;
    private int paddleHitCount = 0;
    public int PaddleHitCount {
        get { return this.paddleHitCount; }
        set { this.paddleHitCount = value; }
    }

    private int paddleHitCountWithBricksDestroyedInBetween= 0;
    public int PaddleHitCountWithBricksDestroyedInBetween
    {
        get { return this.paddleHitCountWithBricksDestroyedInBetween; }
        set { this.paddleHitCountWithBricksDestroyedInBetween = value; }
    }

    private int bricksHitInARow = 0;
    public int BricksHitInARow
    {
        get { return this.bricksHitInARow; }
        set { this.bricksHitInARow = value; }
    }

    private int score = 0;
    public int Score
    {
        get { return this.score; }
        set { this.score = value; }
    }
    
    // Use this for initialization
    void Awake()
    {

        Screen.showCursor = false;
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        switch (getCurrentLevel())
        {
            case 1: Instantiate(backGroundMusicLev1); break;
            case 2: Instantiate(backGroundMusicLev2); break;
            default: break;
        }
        
        Setup();

    }

    public void startTimer()
    {
        timerStarted = true;
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += new ElapsedEventHandler(OnTick);
        timer.Enabled = true;
    }

    public void pauseTimer()
    {
        timer.Stop();
    }

    public void resumeTimer(){
        timer.Start();
    }

    public void Setup()
    {
        score = 0;
        secondsCounter = 0;

        Time.timeScale = 1f;
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);
    }

    void CheckGameOver()
    {
        if (bricks < 1)
        {
            timer.Stop();
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("loadNextLevel", 1f);
            this.BricksHitInARow = 0;
            this.Score = 0;
        }

        if (lives < 1)
        {
            timer.Stop();
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
            this.BricksHitInARow = 0;
            this.Score = 0;
        }

    }

    void Update()
    {
        TimeSpan ts = TimeSpan.FromSeconds(secondsCounter);
        string seconds;
        seconds = ts.Seconds < 10 ? "0" + ts.Seconds : ts.Seconds.ToString();
        timeText.text = "Time: " + (ts.Minutes == 0 ? seconds : ts.Minutes + ":" + seconds);
    }

    void OnApplicationQuit()
    {
        //timer.Stop();
        //timer = null;
    }

    void Reset()
    {
        this.timerStarted = false;
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
        ScoreText.text = "Score: 0";
    }

    void OnTick(object sender, System.EventArgs e)
    {
        secondsCounter++;
    }

    public void LoseLife()
    {
       // backGroundMusicLev1.GetComponent<AudioSource>().volume = 0.1f;
        // backGroundMusicLev1.audio.volume = 0.1f;
        //Invoke("setMaxVolumeForBackGroundMusic", 3);
        this.paddleHitCountWithBricksDestroyedInBetween = 0;
        this.BricksHitInARow = 0;
        resetHitCount();
        lives--;
        livesText.text = "Lives: " + lives;
        Destroy(clonePaddle);
        Invoke("SetupPaddle", resetDelay);
        CheckGameOver();
    }

    private void resetHitCount()
    {
        this.PaddleHitCount = 0;
    }

    private void setMaxVolumeForBackGroundMusic()
    {
        Debug.Log("setmaxvolum called");
        backGroundMusicLev1.GetComponent<AudioSource>().volume = 1f;
    }

    void SetupPaddle()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        calculateNewScore();
        ScoreText.text = "Score: " + this.Score;
        bricks--;
        CheckGameOver();
        Invoke("checkAwesomeness", 0.5f);
    }

    private void calculateNewScore()
    {
        int scoreToBeAdded = 0;
        if (this.BricksHitInARow <= 1)
        {
            scoreToBeAdded += 1;
        }
        else
        {
            scoreToBeAdded += this.BricksHitInARow * this.BricksHitInARow;
        }

        if (this.PaddleHitCountWithBricksDestroyedInBetween > 0)
        {

            scoreToBeAdded = (this.PaddleHitCountWithBricksDestroyedInBetween * 4) + scoreToBeAdded;
        }

        //int x = ((500 - Mathf.Clamp(secondsCounter, 0, 499)) * 2 / 10) + 1;
        this.Score += scoreToBeAdded;
    }

    private void checkAwesomeness()
    {
        int currentLevel = getCurrentLevel();

        if(this.BricksHitInARow == 4)
        {
            if(currentLevel == 1)
            {
                GameObject.Instantiate(whickedSickSound);
            }
            else if(currentLevel == 2)
                GameObject.Instantiate(dominatingSound2);
        }
        else
        {
            switch (currentLevel)
            {
                case 1:
                    if (bricks == 60 || bricks == 30)
                    {
                        GameObject.Instantiate(holyShitSound);
                    }
                    else if (bricks == 40 || bricks == 15)
                    {
                        GameObject.Instantiate(rampageSound);
                    }
                    else if (bricks == 20 || bricks == 50)
                    {
                        GameObject.Instantiate(unstoppableSound);
                    }
                    else if (bricks == 2 || bricks == 35)
                    {
                        GameObject.Instantiate(godlikeSound);
                    }
                    break;
                case 2:
                    if (bricks == 30)
                    {
                        GameObject.Instantiate(dominatingSound2);
                    }
                    else if (bricks == 40 || bricks == 15)
                    {
                        GameObject.Instantiate(godlikeSound2);
                    }
                    else if (bricks == 20 || bricks == 50)
                    {
                        GameObject.Instantiate(unstoppableSound2);
                    }break;
                default: break;
            }
        }
    }

    public int getCurrentLevel()
    {
        switch (Application.loadedLevelName)
        {
            case "Splash": return -2;
            case "Splash2": return -1;
            case "Menu2": return 0;
            case "Scene1": return 1;
            case "Scene2": return 2;
            default: return 0;
        }
    }

    public void loadNextLevel(bool loadMenu = false)
    {
        if(loadMenu)
            Application.LoadLevel("Menu2");
        else
        {
            switch (getCurrentLevel())
            {
                case -2: Application.LoadLevel("Splash2"); break;
                case -1: Application.LoadLevel("Menu2"); break;
                case 0: Application.LoadLevel("Scene1"); break;
                case 1: Application.LoadLevel("Scene2"); break;
                case 2: Application.LoadLevel("Menu2"); break;
            }
        }
    }

    private void SaveHighScore(string name)
    {
        try
        {
            string key = String.Empty;
            for (int i = 0; i < 1000; i++)
            {
                if (!PlayerPrefs.HasKey(i + String.Empty))
                {
                    key = i + String.Empty;
                    break;
                }
            }

            PlayerPrefs.SetString(key, "Name : " + name + " - " + this.Score + "points (" + this.secondsCounter + "seconds)");
        }
        catch(Exception)
        {
            // Show error message
        }
    }

    private String LoadHighScores()
    {
        try
        {
            String highscore = String.Empty;
            string key = "0";
            int x = 0;
            while (PlayerPrefs.HasKey(key))
            {
                highscore += PlayerPrefs.GetString(key);
                highscore += System.Environment.NewLine;

                x++;
                key = x + String.Empty;
            }

            if(!String.IsNullOrEmpty(highscore))
                return highscore;
        }
        catch (Exception)
        {
            return "Couldnt load highscores";
            // Show error message
        }

        return "No highscores available. Doh!";
       
    }
}