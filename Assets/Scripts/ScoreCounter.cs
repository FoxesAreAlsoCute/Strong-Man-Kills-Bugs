using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter S;

    public int highScore;
    public int score;
    public int bugsCount;
    public int maxBugs = 10;
    public int bugsKilled;
    public int bugsRanOff;

    public TMP_Text bugsCountText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public string bugsCountString = "Bugs left: ";
    public string scoreString = "Score: ";
    public string timeString = "Time: ";

    public string gotHighScoreString = "Highscore!";
    public string highScoreString = "Highscore: ";
    public string bugsKilledString = "Bugs killed: ";
    public string bugsRanOffString = "Bugs ran off: ";

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        if (PlayerPrefs.HasKey(AlphaMain.ScorePrefsKey))
            highScore = PlayerPrefs.GetInt(AlphaMain.ScorePrefsKey);
        else highScore = 0;

        score = 0;
        bugsCount = 0;
        bugsKilled = 0;
        bugsRanOff = 0;
        UpdateGUI();
    }

    public void AddBug()
    {
        bugsCount++;
        UpdateGUI();
        if (bugsCount >= maxBugs) GameOver();
    }

    public void RemoveBug(int scorePoints)
    {
        bugsCount--;
        score += scorePoints;
        if (scorePoints == 1) bugsRanOff++;
        else if (scorePoints > 1) bugsKilled++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        bugsCountText.text = bugsCountString + bugsCount;
        if (bugsCount > 7) bugsCountText.color = Color.red;
        else if (bugsCount > 5) bugsCountText.color = Color.yellow;
        else bugsCountText.color = Color.white;
        scoreText.text = scoreString + score;
    }

    private void Update()
    {
        if (Main.GameState != eGameState.playing) return;

        float t = Time.time;
        string timeFormat = "mm':'ss";
        if (t >= 3600) timeFormat = "hh':'mm':'ss";
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        timeText.text = timeSpan.ToString(timeFormat);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        Main.GameState = eGameState.over;
        string outputString = "";
        if (highScore < score)
        {
            outputString = $"{gotHighScoreString}";
            highScore = score;
            PlayerPrefs.SetInt(AlphaMain.ScorePrefsKey, highScore);
            PlayerPrefs.SetString(AlphaMain.TimePrefsKey, timeText.text);
        }
        else outputString = $"{highScoreString}{highScore}";

        outputString += $"\n{scoreString}{score}\n{timeString}{timeText.text}\n{bugsKilledString}{bugsKilled}\n{bugsRanOffString}{bugsRanOff}";
        PausePanel.S.ShowGameOver(outputString);
    }
}