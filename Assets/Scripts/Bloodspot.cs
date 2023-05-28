using UnityEngine;

public class Bloodspot : MonoBehaviour
{
    public float fadingDelay = 1f;
    public float fadingDuration = 3f;

    public bool startFading = false;
    private float startFadingTime;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Invoke("StartFading", fadingDelay);
    }

    private void StartFading()
    {
        startFading = true;
        startFadingTime = Time.time;
    }

    private void Update()
    {
        if (!startFading) return;

        float u = (Time.time - startFadingTime) / fadingDuration;
        rend.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), u);

        if (u >= 1) Destroy(gameObject);
    }
}