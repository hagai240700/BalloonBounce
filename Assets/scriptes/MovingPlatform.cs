using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float amplitude = 2f;   // כמה רחוק לצדדים
    public float speed = 1f;       // מהירות תנודה
    public float phase = 0f;       // היסט פאזה כדי שלא כולן ינועו ביחד
    public bool horizontal = true; // true=ימ/שמ, false=למעלה/למטה

    Rigidbody2D rb;
    Vector2 origin;
    float t0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
        t0 = Time.time;
    }

    void FixedUpdate()
    {
        float t = (Time.time - t0) * speed;
        float offset = Mathf.Sin(t + phase) * amplitude;

        Vector2 target = origin;
        if (horizontal) target.x += offset;
        else target.y += offset;

        if (rb && rb.bodyType == RigidbodyType2D.Kinematic)
            rb.MovePosition(target);
        else
            transform.position = target;
    }

    // נקראת אחרי מיחזור/שינוי מיקום ע״י ה־Pool
    public void Recenter()
    {
        origin = transform.position;
        t0 = Time.time;
    }
}

