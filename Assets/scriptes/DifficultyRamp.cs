using UnityEngine;

public class DifficultyRamp : MonoBehaviour
{
    public PlatformPool platforms;
    public ObstaclePool obstacles;
    public Transform follow;

    public float minGapFloor = 0.9f;
    public float maxGapFloor = 1.6f;
    public float spawnChanceCeil = 0.7f;

    void Update()
    {
        if (!follow || !platforms || !obstacles) return;
        float h = Mathf.Max(0f, follow.position.y);

        platforms.minGapY = Mathf.Lerp(1.2f, minGapFloor, h / 100f);
        platforms.maxGapY = Mathf.Lerp(2.2f, maxGapFloor, h / 100f);
        obstacles.spawnChance = Mathf.Lerp(0.35f, spawnChanceCeil, h / 120f);
    }
}

