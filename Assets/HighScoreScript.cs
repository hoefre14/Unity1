using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets;
using System.Collections.Generic;

public class HighScoreScript : MonoBehaviour {

    public Text scoreText;
    public GameObject backGroundMusic;
	// Use this for initialization
	void Start () {
        Instantiate(backGroundMusic);
        string scores = LoadHighScores();
        scoreText.text = scores;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private string LoadHighScores()
    {
        try
        {
            /*
            for(int i = 0; i < 10; i++)
            {
                PlayerPrefs.SetString(i + "", "Name : " + " random navn " + " - " + i*3 + " points (" + i*10 + "seconds)");
            }*/

            List<HighScore> scores = new List<HighScore>();

            string highscore = String.Empty;
            string key = "0";
            int count = 0;
            while (PlayerPrefs.HasKey(key))
            {
                string s = PlayerPrefs.GetString(key) + System.Environment.NewLine;
                int start = s.IndexOf(" - ");
                int end = s.IndexOf(" points");
                int points = int.Parse(s.Substring(start + 3, end - start - 3));
                HighScore newScore = new HighScore(points, s);
                scores.Add(newScore);
                count++;
                key = count + String.Empty;
            }

            scores.Sort((s1, s2) => s2.score.CompareTo(s1.score));

            int placement = 1;
            foreach(var s in scores)
            {
                if(placement <= 25) //tar bare med de 25 beste
                     highscore += placement + " - " + s.text;
                placement++;
            }


            if (!String.IsNullOrEmpty(highscore))
                return highscore;
        }
        catch (Exception)
        {
            return "Couldnt load highscores";
        }

        return "No highscores available. Doh!";
    }

    private class HighScore
    {
        public int score { get; set; }
        public string text { get; set; }
        public HighScore(int score, string text)
        {
            this.score = score;
            this.text = text;
        }
    }
}
