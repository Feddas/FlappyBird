using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start the game at half speed. (speed reaches 1 as score increments)
    public const float TimeScaleAtStart = 0.5f;

    public static GameManager Instance;

    public event Action OnPlayerJoin;

    // Unity Inspector fields
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private UnityEngine.UI.Button _playButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = TimeScaleAtStart;
    }

    /// <summary> invoked by PlayerInputManager </summary>
    public void OnPlayerJoined()
    {
        if (OnPlayerJoin != null)
        {
            OnPlayerJoin();
        }
    }

    public void GameOver()
    {
        GameManagerAudio.Instance.PlaySfx(SfxClip.Gameover);

        // save score
        Score.instance.UpdateHighScore();

        // reset
        Time.timeScale = 0f;

        // main menu UI
        StartCoroutine(ShowGameoverUi());
    }

    IEnumerator ShowGameoverUi()
    {
        _gameOverCanvas.SetActive(true);

        // focus play button so space bar is initalized to click that play button
        yield return null;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_playButton.gameObject);
    }

    /// <summary> Called by OnClick from the PlayButton and the SkipTutorial button </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
