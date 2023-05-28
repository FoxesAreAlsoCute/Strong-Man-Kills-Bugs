using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rifleHandle;
    public GameObject collar;
    public GameObject prefabProjectile;
    public Transform anchorProjectiles;
    public float projectileSpeed = 30f;

    private void Update()
    {
        if (Main.GameState != eGameState.playing) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = mousePos - rifleHandle.transform.position;
        rifleHandle.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        if (Input.GetMouseButtonDown(0)) Shoot();
    }

    private void Shoot()
    {
        GameObject go = Instantiate(prefabProjectile, anchorProjectiles);
        go.transform.position = collar.transform.position;
        go.transform.rotation = rifleHandle.transform.rotation;
        go.GetComponent<Rigidbody>().velocity = go.transform.up * projectileSpeed;
    }
}