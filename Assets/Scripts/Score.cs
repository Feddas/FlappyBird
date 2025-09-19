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

    public void UpdateScore()
    {
        score++;
        _currentScoreText.text = score.ToString();
        UpdateHighScore();

        if (Time.timeScale < 1)
            Time.timeScale += 0.05f;
    }
}
