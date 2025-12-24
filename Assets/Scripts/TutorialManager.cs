using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public const string PlayerPrefsTutorialKey = "TutorialsFinished";

    [SerializeField]
    [Tooltip("Animator that is being used as a State Machine for the game")]
    private Animator stateMachine;

    public GameObject Scores;

    public int CurrentTutorialId { get; private set; }

    void Start()
    {
        int completedTutorialId = 0;
        if (PlayerPrefs.HasKey(PlayerPrefsTutorialKey))
        {
            completedTutorialId = PlayerPrefs.GetInt(PlayerPrefsTutorialKey);
            stateMachine.SetInteger("TutorialsFinished", completedTutorialId); // Animator transitions determine game state.
        }
        bool hasStartedTutorial = completedTutorialId > 0;
        bool hasFinishedTutorials = completedTutorialId > 2;

        if (hasFinishedTutorials)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Scores.SetActive(false);
        }
    }

    /// <summary> Called by click event on UI button. </summary>
    public void SkipTutorial()
    {
        PlayerPrefs.SetInt(TutorialManager.PlayerPrefsTutorialKey, 4);
    }

    public void OnTutorialFinished()
    {
        Debug.Log($"TUTORIAL FINISHED!");
        Scores.SetActive(true);
        Destroy(this.gameObject);
    }
}
