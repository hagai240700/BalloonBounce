using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shield : MonoBehaviour
{
    public GameObject vfx; // אופציונלי: ילד בשם "ShieldVFX"
    float until;
    public bool IsActive => Time.time < until;

    void Awake()
    {
        if (!vfx)
        {
            var t = transform.Find("ShieldVFX");
            if (t) vfx = t.gameObject;
        }
        if (vfx) vfx.SetActive(false);
    }

    public void Activate(float seconds)
    {
        until = Time.time + seconds;
        if (vfx) vfx.SetActive(true);
    }

    public void Consume()
    {
        until = 0f;
        if (vfx) vfx.SetActive(false);
    }

    void Update()
    {
        if (vfx && !IsActive) vfx.SetActive(false);
    }
}
