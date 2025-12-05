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

    void Update()
    {
        DoTutorial(CurrentTutorialId);
    }

    /// <summary> Sync's Animator's tutorialId with the game scene. This function should only be called by the current Animator state's StateMachineBehaviour. </summary>
    public void SetCurrentTutorial(int tutorialId)
    {
        CurrentTutorialId = tutorialId;
    }

    private void DoTutorial(int tutorialId)
    {
        switch (tutorialId)
        {
            case 0:
                // do nothing
                break;
            case 1:
                DoTutorial1();
                break;
            case 2:
                DoTutorial2();
                break;
            case 3:
                DoTutorial3();
                break;
            default:
                FinishTutorial();
                break;
        }
    }

    // step 1 is: Use your join button to flap. Flap to go between thorns without touching them. Pass: at least one player makes it through 2 gates.
    private void DoTutorial1()
    {
        if (Score.instance.CurrentScore >= 2)
        {
            finishedCurrentStepOfTutorial();
        }
    }

    // step 2 is: Revive other members of the flock by flying over to them twice. [gates removed] Pass: NPC at top, middle, and bottom are all revived.
    private void DoTutorial2()
    {
        Debug.Log("TODO: implement DoTutorial2");

        finishedCurrentStepOfTutorial();
    }

    // step 3 is: Flap each member of your flock individually. Reset: if any player dies. Pass: all members make it through 2 gates.
    private void DoTutorial3()
    {
        int playersActive = PlayerInput.all.Count;
        if (playersActive < 2)
        {
            // Debug.Log("Need at least 2 players for this Tutorial step.");
            Score.instance.ResetScore();
            return;
        }

        int playersAlive = PlayerInput.all.Count(p => isAlive(p));
        int score = Score.instance.CurrentScore;
        if (playersAlive < playersActive)
        {
            // All players in the flock need to stay alive
            Score.instance.ResetScore();
        }
        else
        {
            if (Score.instance.CurrentScore >= 2 * playersActive)
            {
                // Debug.Log($"{playersActive}:{playersAlive} TUTORIAL 3 FINISHED Score:{score}");
                finishedCurrentStepOfTutorial();
            }
            else
            {
                // Debug.Log($"{playersActive}:{playersAlive} progress... Score:{score}");
            }
        }
    }

    // step 4 is: Set HasCompletedTutorial to true.
    private void FinishTutorial()
    {
        Debug.Log($"TUTORIAL FINISHED!");
        Destroy(this.gameObject);
    }

    /// <summary> Move to next tutorial </summary>
    private void finishedCurrentStepOfTutorial()
    {
        // cleanup for next tutorial or game
        Score.instance.ResetScore();

        // save progress
        PlayerPrefs.SetInt(TutorialManager.PlayerPrefsTutorialKey, CurrentTutorialId);

        // notify animator
        if (OnFinishedTutorial != null)
        {
            OnFinishedTutorial(CurrentTutorialId);
        }
    }

    private bool isAlive(PlayerInput player)
    {
        var health = player.GetComponentInChildren<PlayerHealth>();
        return health != null && health.IsAlive;
    }
}
