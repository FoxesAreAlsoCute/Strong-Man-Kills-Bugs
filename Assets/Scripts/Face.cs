using UnityEngine;

public class Face : MonoBehaviour
{
    public static Face S;

    public Sprite commonFace;
    public Sprite happyFace;
    public Sprite notSoHappyFace;
    public Sprite worriedFace;
    public Sprite sadFace;

    public float happinessDuration = 1f;
    private bool showingHappy;

    private SpriteRenderer sRend;

    private static bool noExecutable;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        sRend = GetComponent<SpriteRenderer>();
        showingHappy = false;
        noExecutable = false;
    }

    private void Start()
    {
        if (commonFace == null || happyFace == null || notSoHappyFace == null || worriedFace == null || sadFace == null)
        {
            noExecutable = true;
            Destroy(gameObject);
        }
    }

    public void ShowHappyFace()
    {
        if (noExecutable) return;

        sRend.sprite = happyFace;
        showingHappy = true;
        Invoke("StopShowingHappiness", happinessDuration);
    }

    private void StopShowingHappiness()
    {
        showingHappy = false;
    }

    private void LateUpdate()
    {
        if (showingHappy) return;

        int bugsCount = ScoreCounter.S.bugsCount;
        if (bugsCount >= ScoreCounter.S.maxBugs) sRend.sprite = sadFace;
        else if (bugsCount >= ScoreCounter.S.highBugsCount) sRend.sprite = worriedFace;
        else if (bugsCount >= ScoreCounter.S.lowBugsCount) sRend.sprite = notSoHappyFace;
        else sRend.sprite = commonFace;
    }
}