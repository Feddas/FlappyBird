using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private UnityEngine.UI.Button _playButton;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = .5f;
    }

    public void GameOver()
    {
        // TODO: if tutorial, don't activate _gameOverCanvas, Just RestartGame with current tutorial step
        _gameOverCanvas.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_playButton.gameObject);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
