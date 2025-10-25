using UnityEngine;
using System.Collections.Generic;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformPrefab;
    public int poolSize = 12;
    public float levelHalfWidth = 3f;
    public float minGapY = 1.2f;
    public float maxGapY = 2.2f;
    public Transform follow;

    readonly List<GameObject> pool = new List<GameObject>();
    float nextY;

    void Awake()
    {
        if (levelHalfWidth <= 0f) levelHalfWidth = 3f;
        if (minGapY <= 0f) minGapY = 1.2f;
        if (maxGapY <= minGapY) maxGapY = minGapY + 0.8f;
    }

    void Start()
    {
        nextY = follow ? follow.position.y - 1f : 0f;
        for (int i = 0; i < poolSize; i++)
        {
            var go = CreateOne(SpawnPos());
            pool.Add(go);
        }
    }

    Vector2 SpawnPos()
    {
        nextY += Random.Range(minGapY, maxGapY);
        float x = Random.Range(-levelHalfWidth, levelHalfWidth);
        return new Vector2(x, nextY);
    }

    GameObject CreateOne(Vector2 pos)
    {
        var go = Instantiate(platformPrefab, pos, Quaternion.identity);
        go.tag = "Platform";

        var rc = go.GetComponent<RecycleWhenBelow>() ?? go.AddComponent<RecycleWhenBelow>();
        var all = go.GetComponents<RecycleWhenBelow>();
        for (int i = 0; i < all.Length; i++)
            if (all[i] != rc) Destroy(all[i]);
        rc.Init(this);

        var mv = go.GetComponent<MovingPlatform>() ?? go.AddComponent<MovingPlatform>();
        mv.amplitude = Random.Range(1.2f, 2.2f);
        mv.speed = Random.Range(0.8f, 1.4f);
        mv.phase = Random.Range(0f, Mathf.PI * 2f);
        mv.horizontal = true;

        var sticky = go.GetComponent<StickyPlatform>() ?? go.AddComponent<StickyPlatform>();

        return go;
    }

    public void Recycle(GameObject obj)
    {
        obj.transform.position = SpawnPos();
        var mv = obj.GetComponent<MovingPlatform>();
        if (mv) mv.Recenter();
    }
}
