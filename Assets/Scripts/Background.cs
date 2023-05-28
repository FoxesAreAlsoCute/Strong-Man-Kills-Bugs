using UnityEngine;

public class Background : MonoBehaviour
{
    public static float Width;
    public static float Height;

    private void Awake()
    {
        Height = Camera.main.orthographicSize;
        Width = Height * Camera.main.aspect;

        transform.localScale = new Vector3(Width * 2f, Height * 2f, 1);
    }
}