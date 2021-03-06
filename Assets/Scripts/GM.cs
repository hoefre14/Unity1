﻿using UnityEngine;
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

    public GameObject nameInputCanvas;
    int secondsCounter = 0;
    private static Timer timer;
    public bool timerStarted = false;

    static public int scoreFromLevel1 = 0;
    static public int timeUsedInLevel1 = 0;
    
    
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
        Screen.showCursor = true;
        try
        {
            timer.Stop();
        }
        catch(Exception)
        {

        }
    }

    public void resumeTimer(){
        Screen.showCursor = false;
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

            if(getCurrentLevel() == 1)
            {
                scoreFromLevel1 = this.Score;
                timeUsedInLevel1 = this.secondsCounter;

                Time.timeScale = .25f;
                Invoke("loadNextLevel", 1f);
                this.BricksHitInARow = 0;
                this.Score = 0;
            }
            else if(getCurrentLevel() == 2)
            {
                Screen.showCursor = true;
                this.pauseTimer();
                Time.timeScale = 0;
                nameInputCanvas.SetActive(true);

            }
        }

        else if (lives < 1)
        {
            gameOver.SetActive(true);
            if (getCurrentLevel() == 1)
            {
                Screen.showCursor = true;
                this.pauseTimer();
                Time.timeScale = 0;
                nameInputCanvas.SetActive(true);
            }
            else if (getCurrentLevel() == 2)
            {
                Screen.showCursor = true;
                this.pauseTimer();
                Time.timeScale = 0;
                nameInputCanvas.SetActive(true);
            }
        }

    }

    void Update()
    {
        TimeSpan ts = TimeSpan.FromSeconds(secondsCounter);
        string seconds;
        seconds = ts.Seconds < 10 ? "0" + ts.Seconds : ts.Seconds.ToString();
        timeText.text = "Time: " + (ts.Minutes == 0 ? seconds : ts.Minutes + ":" + seconds);
    }

    void Reset()
    {
        this.timerStarted = false;
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
        this.Score = 0;
        ScoreText.text = "Score: 0";
    }

    void OnTick(object sender, System.EventArgs e)
    {
        secondsCounter++;
    }

    public void LoseLife()
    {
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

    public void loadNextLevel()
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

    public void loadNextLevel(bool loadMenu = false)
    {
        if (loadMenu)
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

    private void SaveHighScore(string name, int points, int time)
    {
        try
        {
            string key = String.Empty;
            for (int i = 0; i < 2000; i++)
            {
                if (!PlayerPrefs.HasKey(i + String.Empty))
                {
                    key = i + String.Empty;
                    break;
                }
            }
            string timeString = "";
            TimeSpan ts = TimeSpan.FromSeconds(time);
            timeString = (ts.Minutes == 0 ? ts.Seconds + " seconds" : ts.Minutes + " minutes, " + ts.Seconds + " seconds");

            PlayerPrefs.SetString(key, name + " - " + points + " points (" + timeString + ")");
        }
        catch(Exception)
        {
            // Show error message
        }
    }   

    public void getNameEnteredAndSaveHighScore(string name)
    {
        nameInputCanvas.SetActive(false);

        switch(getCurrentLevel())
        {
            case 1:
                SaveHighScore(name, this.Score, secondsCounter);
                        timer.Stop();
                        Time.timeScale = .25f;
                        Invoke("Reset", 0f);
                        this.BricksHitInARow = 0;
                        this.Score = 0;
                break;
            case 2:
                SaveHighScore(name, this.Score + scoreFromLevel1, timeUsedInLevel1 + secondsCounter);
                this.BricksHitInARow = 0;
                this.Score = 0;
                Application.LoadLevel("Highscores");
                break;
            default: break;
        }
    }

}