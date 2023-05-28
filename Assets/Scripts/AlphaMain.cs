using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaMain : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Scene_1";
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;

    public static string TimePrefsKey = "time";
    public static string ScorePrefsKey = "score";

    private void Awake()
    {
        string t = "0m 0s";
        if (PlayerPrefs.HasKey(TimePrefsKey)) t = PlayerPrefs.GetString(TimePrefsKey);
        timeText.text = "Time: " + t;
        int s = 0;
        if (PlayerPrefs.HasKey(ScorePrefsKey)) s = PlayerPrefs.GetInt(ScorePrefsKey);
        scoreText.text = "Score: " + s;
    }

    public void Play()
    {
        AudioManager.S.Click();
        Debug.Log("Play");
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}