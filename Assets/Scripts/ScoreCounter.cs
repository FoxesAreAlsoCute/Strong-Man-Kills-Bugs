using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter S;

    public int highScore;
    public int score;
    public int bugsCount;
    public int maxBugs = 10;
    public int highBugsCount;
    public int lowBugsCount;
    public int bugsKilled;
    public int bugsRanOff;

    public TMP_Text bugsCountText;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    private float timeStart;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        if (PlayerPrefs.HasKey(AlphaMain.ScorePrefsKey))
            highScore = PlayerPrefs.GetInt(AlphaMain.ScorePrefsKey);
        else highScore = 0;

        lowBugsCount = maxBugs / 2;
        highBugsCount = maxBugs - (lowBugsCount / 2);

        score = 0;
        bugsCount = 0;
        bugsKilled = 0;
        bugsRanOff = 0;
        UpdateGUI();
    }

    private void Start()
    {
        timeStart = Time.time;
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
        bugsCountText.text = "Bugs left: " + bugsCount;
        if (bugsCount >= highBugsCount) bugsCountText.color = Color.red;
        else if (bugsCount >= lowBugsCount) bugsCountText.color = Color.yellow;
        else bugsCountText.color = Color.white;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if (Main.GameState != eGameState.playing) return;

        float t = Time.time - timeStart;
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
            outputString = "Highscore!";
            highScore = score;
            PlayerPrefs.SetInt(AlphaMain.ScorePrefsKey, highScore);
            PlayerPrefs.SetString(AlphaMain.TimePrefsKey, timeText.text);
        }
        else outputString = $"Highscore: {highScore}";

        outputString += $"\nScore: {score}\nTime: {timeText.text}\nBugs killed: {bugsKilled}\nBugs ran off: {bugsRanOff}";
        PausePanel.S.ShowGameOver(outputString);
    }
}