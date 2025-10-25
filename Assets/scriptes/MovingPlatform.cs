using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float amplitude = 2f;   // ��� ���� ������
    public float speed = 1f;       // ������ �����
    public float phase = 0f;       // ���� ���� ��� ��� ���� ����� ����
    public bool horizontal = true; // true=��/��, false=�����/����

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

    // ����� ���� ������/����� ����� ��� ��Pool
    public void Recenter()
    {
        origin = transform.position;
        t0 = Time.time;
    }
}

