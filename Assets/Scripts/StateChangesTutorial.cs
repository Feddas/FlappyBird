using UnityEngine;

public class StateChangesTutorial : StateMachineAnimatorState<StateBridgeToTutorial>
{
    [SerializeField] private int tutorialId = 0;
    [SerializeField] private string tutorialText = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override protected void OnStateEntered()
    {
        if (tutorialId > 0)
        {
            // manipulate gameobject
            stateMachine.GameOverCanvas.SetActive(false);
            stateMachine.TutorialText.text = tutorialText;
            stateMachine.TutorialManager.SetCurrentTutorial(tutorialId);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdated() { }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override protected void OnStateExited()
    {
        if (stateMachine.TutorialManager != null) // skip if Application.Quit or OnDestroy
        {
        }
    }
}
