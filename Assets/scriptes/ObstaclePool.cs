using UnityEngine;
using System.Collections.Generic;

public class ObstaclePool : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int poolSize = 4;
    public float levelHalfWidth = 3f;
    public float minGapY = 3f;
    public float maxGapY = 6f;
    public float spawnChance = 0.35f;
    public Transform follow;

    public LayerMask platformLayer;
    public float avoidRadius = 0.6f;
    public int maxTries = 20;

    readonly List<GameObject> pool = new List<GameObject>();
    float nextY;

    void Start()
    {
        nextY = (follow ? follow.position.y : 0f) + 4f;

        for (int i = 0; i < poolSize; i++)
        {
            var o = Instantiate(obstaclePrefab, SpawnPos(), Quaternion.identity);
            o.tag = "Obstacle";
            EnsureCollider(o);

            var rc = o.GetComponent<RecycleWhenBelow>();
            if (!rc) rc = o.AddComponent<RecycleWhenBelow>();
            rc.InitObstacle(this);

            SetObstacleEnabled(o, true);
            pool.Add(o);
        }
    }

    Vector2 SpawnPos()
    {
        nextY += Random.Range(minGapY, maxGapY);

        Vector2 pos = new Vector2(Random.Range(-levelHalfWidth, levelHalfWidth), nextY);
        int tries = 0;

        while (Physics2D.OverlapCircle(pos, avoidRadius, platformLayer) && tries++ < maxTries)
            pos = new Vector2(Random.Range(-levelHalfWidth, levelHalfWidth), nextY);

        return pos;
    }

    void EnsureCollider(GameObject go)
    {
        if (!go.GetComponent<Collider2D>())
            go.AddComponent<PolygonCollider2D>();

        var col = go.GetComponent<Collider2D>();
        col.isTrigger = false;
    }

    void SetObstacleEnabled(GameObject obj, bool enabled)
    {
        var rends = obj.GetComponentsInChildren<Renderer>(true);
        foreach (var r in rends) r.enabled = enabled;

        var cols = obj.GetComponentsInChildren<Collider2D>(true);
        foreach (var c in cols) c.enabled = enabled;
    }

    public void Recycle(GameObject obj)
    {
        obj.transform.position = SpawnPos();
        bool show = Random.value <= spawnChance;
        SetObstacleEnabled(obj, show);
    }
}
