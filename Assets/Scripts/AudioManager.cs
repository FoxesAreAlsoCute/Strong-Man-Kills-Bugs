using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;

    private AudioSource source;

    private void Awake()
    {
        if (S == null) S = this;
        else if (S != this) Destroy(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void Click()
    {
        source.Play();
    }
}
