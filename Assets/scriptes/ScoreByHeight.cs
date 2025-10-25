using UnityEngine;
using TMPro;

public class ScoreByHeight : MonoBehaviour
{
    public Transform target;
    public TextMeshProUGUI scoreText;

    float baseY;
    float best;

    void Start()
    {
        if (target) baseY = target.position.y;
        UpdateScoreText(0);
    }

    void Update()
    {
        if (!target || !scoreText) return;

        float height = Mathf.Max(0f, target.position.y - baseY);
        best = Mathf.Max(best, height);
        int score = Mathf.RoundToInt(best * 10f);
        UpdateScoreText(score);
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ResetScore(Transform newTarget)
    {
        if (newTarget) target = newTarget;
        baseY = target ? target.position.y : 0f;
        best = 0f;
        UpdateScoreText(0);
    }
}

