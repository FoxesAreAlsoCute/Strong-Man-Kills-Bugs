using UnityEngine;

public class Bug : MonoBehaviour
{
    public float speedStart = 1f;
    public float speedAfterShot = 2f;
    public int health = 2;
    public int scorePoints = 2;

    private BoundsCheck bndCheck;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float speed;
    private bool tookShot = false;

    private Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake()
    {
        ScoreCounter.S.AddBug();

        bndCheck = GetComponent<BoundsCheck>();

        //Set start position
        float x = 0;
        float y = 0;
        float r = Random.Range(0, 2f);
        //Right bound
        if (r > 1.5f)
        {
            x = Background.Width - bndCheck.size;
            y = Random.Range(Background.Height, -Background.Height);
        }
        //Upper bound
        else if (r > 1f)
        {
            y = Background.Height - bndCheck.size;
            //x = Random.Range(-Background.Width, Background.Width);
            x = Random.Range(0, Background.Width);
        }
        //Left bound
        else if (r > 0.5f)
        {
            x = -Background.Width + bndCheck.size;
            //y = Random.Range(Background.Height, -Background.Height);
            y = Random.Range(0, -Background.Height);
        }
        //Bottom bound
        else
        {
            y = -Background.Height + bndCheck.size;
            x = Random.Range(-Background.Width, Background.Width);
        }
        pos = new Vector3(x, y, 0);

        speed = speedStart;
        ChooseDirection();
    }

    private void ChooseDirection()
    {
        startPos = transform.position;

        float x = 0;
        float y = 0;
        //Set x
        if (startPos.x > 0) x = Random.Range(-Background.Width, 0);
        else if (startPos.x < 0) x = Random.Range(0, Background.Width);
        else x = Random.Range(-Background.Width, Background.Width);
        //Set y
        if (startPos.y > 0) y = Random.Range(-Background.Height, 0);
        else if (startPos.y < 0) y = Random.Range(0, Background.Height);
        else y = Random.Range(-Background.Height, Background.Height);

        if (tookShot)
        {
            if (x < 0 && y > 0)
            {
                if (Random.Range(0, 1f) > 0.5f) x *= -1;
                else y *= -1;
            }
        }

        targetPos = new Vector3(x, y, 0);

        Vector3 direction = targetPos - startPos;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void Update()
    {
        pos += transform.up * speed * Time.deltaTime;

        if (bndCheck.keepOnScreen && bndCheck.isOnScreen)
        {
            if (bndCheck.tryKeeping) ChooseDirection();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Projectile")
        {
            Destroy(go);
            if (!tookShot)
            {
                tookShot = true;
                speed = speedAfterShot;
                bndCheck.keepOnScreen = false;
                ChooseDirection();
            }
            TakeHealth();
        }
    }

    private void TakeHealth()
    {
        health -= 1;
        if (health <= 0)
        {
            Main.S.MakeBloodSpot(pos);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ScoreCounter.S.RemoveBug(health > 0 ? 1 : scorePoints);
    }
}
