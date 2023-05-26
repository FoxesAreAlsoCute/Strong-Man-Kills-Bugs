using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameState
{
    idle,
    pause,
    playing,
    over
}

public class Main : MonoBehaviour
{
    public static Main S;
    public static eGameState GameState = eGameState.idle;

    public Transform AnchorBugs;
    public GameObject[] prefabsBug;
    public float makingBugsDelay = 1.5f;
    public float makingBugIntervalMin = 1f;
    public float makingBugIntervalMax = 1.5f;

    public Transform AnchorBloodspots;
    public GameObject prefabBloodSpot;
    public Sprite[] bloodSpotsSprites;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);
    }

    private void Start()
    {
        GameState = eGameState.playing;
        Invoke("MakeBug", makingBugsDelay);
    }

    private void MakeBug()
    {
        if (GameState != eGameState.playing) return;

        int r = Random.Range(0, prefabsBug.Length);
        Instantiate(prefabsBug[r], AnchorBugs);

        Invoke("MakeBug", Random.Range(makingBugIntervalMin, makingBugIntervalMax));
    }

    public void MakeBloodSpot(Vector3 pos)
    {
        GameObject go = Instantiate(prefabBloodSpot, AnchorBloodspots);
        go.transform.position = pos;
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-180, 180)));
        int r = Random.Range(0, bloodSpotsSprites.Length);
        go.GetComponent<SpriteRenderer>().sprite = bloodSpotsSprites[r];
    }

    public void Pause(bool pause)
    {
        GameState = pause ? eGameState.pause : eGameState.playing;
        Invoke("MakeBug", Random.Range(makingBugIntervalMin, makingBugIntervalMax));
    }
}
