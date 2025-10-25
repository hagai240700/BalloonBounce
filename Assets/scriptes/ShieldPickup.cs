using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        var b = other.GetComponent<BalloonController>();
        if (!b) return;

        var shield = b.GetComponent<Shield>();
        if (!shield) shield = b.gameObject.AddComponent<Shield>();

        shield.Activate(duration);
        Destroy(gameObject);
    }
}
