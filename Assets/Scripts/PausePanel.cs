using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PausePanel : MonoBehaviour, IPointerClickHandler
{
    public static PausePanel S;

    private Animator animator;
    public string appearingState = "PauseAppear";
    public string disappearingState = "PauseDisappear";
    public float animationDuration = 1f;

    public TMP_Text gameOverText;
    public string gameOverString = "Game Over";
    private string scoreString;
    private bool showingScore = false;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        animator = GetComponent<Animator>();
    }

    public void Appear()
    {
        animator.CrossFade(appearingState, 0);
        if (showingScore) gameOverText.text = scoreString;
    }

    public void Disappear()
    {
        animator.CrossFade(disappearingState, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Main.GameState == eGameState.over)
        {
            if (!showingScore) ShowScore();
        }
    }

    public void ShowGameOver(string scores)
    {
        scoreString = scores;
        gameOverText.text = gameOverString;
        Appear();
    }

    private void ShowScore()
    {
        showingScore = true;
        Disappear();
        Invoke("Appear", animationDuration);
    }
}
