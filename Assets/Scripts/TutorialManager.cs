using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public const string PlayerPrefsTutorialKey = "TutorialsFinished";

    public GameObject Scores;
    public event Action<int> OnFinishedTutorial;

    public int CurrentTutorialId { get; private set; }

    void Start()
    {
        int completedTutorialId = 0;
        if (PlayerPrefs.HasKey(PlayerPrefsTutorialKey))
        {
            completedTutorialId = PlayerPrefs.GetInt(PlayerPrefsTutorialKey);
        }
        bool hasStartedTutorial = completedTutorialId > 0;
        bool hasFinishedTutorials = completedTutorialId > 2;

        if (hasFinishedTutorials)
        {
            Destroy(this.gameObject);
        }
        if (hasStartedTutorial && OnFinishedTutorial != null)
        {
            OnFinishedTutorial(completedTutorialId);
        }
        else
        {
            Scores.SetActive(false);
        }
    }

    // step 4 is: Set HasCompletedTutorial to true.
    private void FinishTutorial()
    {
        Debug.Log($"TUTORIAL FINISHED!");
        Scores.SetActive(true);
        Destroy(this.gameObject);
    }
}
