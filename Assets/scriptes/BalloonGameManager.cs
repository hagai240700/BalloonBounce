using UnityEngine;
using System.Collections;

public class BalloonGameManager : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Transform spawnPoint;
    public CameraFollow cameraFollow;
    public PlatformPool platforms;
    public ObstaclePool obstacles;
    public ScoreByHeight score;   // ?? הוסף שדה ניקוד כאן
    public float respawnDelay = 1f;

    GameObject currentBalloon;
    bool isRespawning;

    void Start()
    {
        var existing = FindObjectOfType<BalloonController>();
        if (existing)
        {
            currentBalloon = existing.gameObject;
            AttachFollows(existing.transform);
            if (score) score.ResetScore(existing.transform);
        }
        else
        {
            SpawnBalloon();
        }
    }

    public void HandleBalloonDeath()
    {
        if (isRespawning) return;
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);
        SpawnBalloon();
        isRespawning = false;
    }

    void SpawnBalloon()
    {
        if (currentBalloon) Destroy(currentBalloon);
        Vector3 pos = spawnPoint ? spawnPoint.position : Vector3.zero;
        currentBalloon = Instantiate(balloonPrefab, pos, Quaternion.identity);
        AttachFollows(currentBalloon.transform);

        if (score) score.ResetScore(currentBalloon.transform);
        else
        {
            var s = FindObjectOfType<ScoreByHeight>();
            if (s) s.ResetScore(currentBalloon.transform);
        }
    }

    void AttachFollows(Transform t)
    {
        if (cameraFollow) cameraFollow.SnapTo(t);
        if (platforms) platforms.follow = t;
        if (obstacles) obstacles.follow = t;
    }
}
