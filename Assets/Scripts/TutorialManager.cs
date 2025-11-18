using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    const string PlayerPrefsTutorialKey = "TutorialsFinished";

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
    // TODO: query all active players to replace "Use your join button" with "Use [player 1 bind]/[player 2 bind]"
    private void DoTutorial1()
    {
        if (Score.instance.CurrentScore >= 2)
        {
            // TODO: reset game and score to 0
            Score.instance.ResetScore();

            if (OnFinishedTutorial != null)
            {
                OnFinishedTutorial(CurrentTutorialId);
            }
        }
    }

    // step 2 is: Flap each member of your flock individually. Reset: if any player dies. Pass: all members make it through 2 gates.
    private void DoTutorial2()
    {
        Debug.Log("TODO: Step 2 of the tutorial:");
    }

    // step 3 is: Revive other members of the flock by flying over to them twice. [gates removed] Pass: NPC at top, middle, and bottom are all revived.
    private void DoTutorial3()
    { }

    // step 4 is: Display "Tutorial Finished". Set HasCompletedTutorial to true. Load 04FlockPlay.
    private void FinishTutorial()
    {
        // TODO: PlayerPrefs.SetInt(PlayerPrefsTutorialKey, 1);
        Destroy(this.gameObject);
    }
}
