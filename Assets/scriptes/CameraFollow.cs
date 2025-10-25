using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 2f;

    float topY;

    public void SnapTo(Transform t)
    {
        target = t;
        topY = t.position.y;
        var p = transform.position;
        p.y = topY;
        transform.position = p;
    }

    void LateUpdate()
    {
        if (!target) return;

        topY = Mathf.Max(topY, target.position.y);

        var pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, topY, smooth * Time.deltaTime);
        transform.position = pos;
    }
}
