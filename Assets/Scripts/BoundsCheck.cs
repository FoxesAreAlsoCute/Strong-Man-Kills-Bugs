using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public bool keepOnScreen = false;
    public bool isOnScreen = false;
    public float size = 0;

    private bool right, left, top, bottom;
    public bool tryKeeping = false;

    private Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void LateUpdate()
    {
        right = pos.x + size > Background.Width;
        left = pos.x - size < -Background.Width;
        top = pos.y + size > Background.Height;
        bottom = pos.y - size < -Background.Height;

        if (right || left || top || bottom) isOnScreen = false;
        else isOnScreen = true;

        if (!keepOnScreen && !isOnScreen) Destroy(gameObject);
        else if (keepOnScreen)
        {
            if (pos.x - size > Background.Width ||
                pos.x + size < -Background.Width ||
                pos.y - size > Background.Height ||
                pos.y + size < -Background.Height)
                tryKeeping = true;
            else tryKeeping = false;
        }
    }
}