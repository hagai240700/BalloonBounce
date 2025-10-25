using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var b = other.GetComponent<BalloonController>();
        if (!b) return;
        var gm = FindObjectOfType<BalloonGameManager>();
        if (gm) gm.HandleBalloonDeath();
        Destroy(b.gameObject);
    }
}
