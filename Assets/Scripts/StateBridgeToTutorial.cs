using TMPro;
using UnityEngine;

public class StateBridgeToTutorial : StateMachineGameObject<StateBridgeToTutorial>
{
    [Header("Modified by Animator")]
    public GameObject GameOverCanvas;
    public TextMeshProUGUI TutorialText;
    public TutorialManager TutorialManager;

    protected override void Awake()
    {
        base.Awake();

        // Note: Done in Awake() because TutorialManager may raise this event on Start()
        TutorialManager.OnFinishedTutorial += TutorialManager_OnFinishedTutorial;
    }

    // void Update() { }

    private void OnDestroy()
    {
        TutorialManager.OnFinishedTutorial -= TutorialManager_OnFinishedTutorial;
    }

    private void TutorialManager_OnFinishedTutorial(int tutorialId)
    {
        animator.SetInteger("TutorialsFinished", tutorialId); // Send information to let Animator determine game state.
    }
}
