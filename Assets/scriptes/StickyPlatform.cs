using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.GetComponent<BalloonController>())
            c.collider.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.collider.GetComponent<BalloonController>())
            c.collider.transform.SetParent(null);
    }
}

