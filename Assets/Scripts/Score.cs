using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    public int CurrentScore { get { return score; } }
    private int score;

    public static Score instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Singleton violation: " + instance.name + " & " + this.name);
            Destroy(this);
        }
    }

    private void Start()
    {
        _currentScoreText.text = score.ToString();
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            _highScoreText.text = score.ToString();
        }
    }

    public void IncrementScore()
    {
        setScore(++score);
    }

    public void ResetScore()
    {
        setScore(0);
    }

    private void setScore(int newScore)
    {
        score = newScore;
        _currentScoreText.text = score.ToString();

        if (Time.timeScale < 1)
        {
            var rampTo1sAt10Points = score * (1 - GameManager.TimeScaleAtStart) / 10;
            Time.timeScale = GameManager.TimeScaleAtStart + rampTo1sAt10Points;
        }
    }
}
