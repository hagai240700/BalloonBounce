using UnityEngine;

public class RecycleWhenBelow : MonoBehaviour
{
    // ��� ������ ���� ������ ���� ����� ���� ������ (�����)
    public float padding = 1f;
    public bool allowDestroyIfNoPool = false;

    PlatformPool platformPool;
    ObstaclePool obstaclePool;

    public void Init(PlatformPool p) { platformPool = p; obstaclePool = null; }
    public void InitObstacle(ObstaclePool p) { obstaclePool = p; platformPool = null; }

    void Update()
    {
        var cam = Camera.main;
        if (!cam) return;

        // �� ������: ����� ������ �� ������ ���� �����
        float cutoffY = cam.transform.position.y - cam.orthographicSize - padding;

        if (transform.position.y < cutoffY)
        {
            if (platformPool != null) platformPool.Recycle(gameObject);
            else if (obstaclePool != null) obstaclePool.Recycle(gameObject);
            else if (allowDestroyIfNoPool) Destroy(gameObject);
        }
    }
}
