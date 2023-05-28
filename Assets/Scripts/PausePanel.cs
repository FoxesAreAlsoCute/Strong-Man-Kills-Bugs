using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum ePauseState
{
    idle,
    paused,
    gameOver,
    showingScore,
    tryAgain
}

public class PausePanel : MonoBehaviour, IPointerClickHandler
{
    public static PausePanel S;
    public static ePauseState PauseState = ePauseState.idle;

    private Animator animator;
    public string appearingState = "PauseAppear";
    public string disappearingState = "PauseDisappear";
    public float animationDuration = 0.5f;

    public TMP_Text gameOverText;
    private string scoreString;
    private float gameOverTime;
    public float gameOverDuration = 3f;

    public GameObject buttonYes;
    public GameObject buttonNo;
    public GameObject buttonExit;
    public GameObject buttonRestart;
    public GameObject buttonResume;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PauseState = ePauseState.idle;
    }

    public void Appear()
    {
        animator.CrossFade(appearingState, 0);
        switch (PauseState)
        {
            case ePauseState.paused:
                gameOverText.text = "";
                break;
            case ePauseState.gameOver:
                gameOverText.text = "Game Over";
                break;
            case ePauseState.showingScore:
                gameOverText.text = scoreString;
                break;
            case ePauseState.tryAgain:
                gameOverText.text = "Good game!\nWhanna try again?";
                break;
        }

        ButtonsAppear();
    }

    private void ButtonsAppear()
    {
        buttonYes.SetActive(PauseState == ePauseState.tryAgain);
        buttonNo.SetActive(PauseState == ePauseState.tryAgain);
        buttonExit.SetActive(PauseState == ePauseState.paused);
        buttonRestart.SetActive(PauseState == ePauseState.paused);
        buttonResume.SetActive(PauseState == ePauseState.paused);
    }

    public void Disappear()
    {
        animator.CrossFade(disappearingState, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (PauseState)
        {
            case ePauseState.gameOver:
                if (Time.time - gameOverTime < gameOverDuration) return;
                ShowScore();
                break;
            case ePauseState.showingScore:
                ShowTryAgain();
                break;
        }
    }

    public void ShowGameOver(string scores)
    {
        PauseState = ePauseState.gameOver;
        gameOverTime = Time.time;
        scoreString = scores;
        Appear();
    }

    private void ShowScore()
    {
        Disappear();
        PauseState = ePauseState.showingScore;
        Invoke("Appear", animationDuration);
    }

    private void ShowTryAgain()
    {
        Disappear();
        PauseState = ePauseState.tryAgain;
        Invoke("Appear", animationDuration);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (PauseState)
            {
                case ePauseState.idle:
                    PauseState = ePauseState.paused;
                    Main.S.Pause(true);
                    Appear();
                    break;
                default:
                    Debug.Log("Quit");
                    Application.Quit();
                    break;
            }
        }
    }

    //====Buttons====

    public void YesOrNoButton(bool yes)
    {
        AudioManager.S.Click();
        if (yes) SceneManager.LoadScene("Scene_1");
        else SceneManager.LoadScene("Scene_0");
    }

    public void RestartButton()
    {
        AudioManager.S.Click();
        Debug.Log("Restart");
        SceneManager.LoadScene("Scene_1");
    }

    public void ResumeButton()
    {
        AudioManager.S.Click();
        Debug.Log("Resume");
        Disappear();
        PauseState = ePauseState.idle;
        Main.S.Pause(false);
    }

    public void ExitButton()
    {
        AudioManager.S.Click();
        Debug.Log("Exit");
        SceneManager.LoadScene("Scene_0");
    }
}