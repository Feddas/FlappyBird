using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateChangesTutorial : StateMachineAnimatorState<StateBridgeToTutorial>
{
    [SerializeField]
    private int tutorialId = 0;

    [Tooltip("replaces \"your join button\" with active players \"Flap\".bindings")]
    [SerializeField]
    private bool dynamicTutorialText = false;

    [TextArea]
    [SerializeField]
    private string tutorialText = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override protected void OnStateEntered()
    {
        if (tutorialId > 0)
        {
            if (dynamicTutorialText && GameManager.instance != null)
            {
                GameManager.instance.OnPlayerJoin += GameManager_OnPlayerJoin;
            }

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

        // memory cleanup
        if (dynamicTutorialText && GameManager.instance != null)
        {
            GameManager.instance.OnPlayerJoin -= GameManager_OnPlayerJoin;
        }

        // "-" flags this tutorial as completed
        stateMachine.TutorialManager.SetCurrentTutorial(-1 * tutorialId);
    }

    private void GameManager_OnPlayerJoin()
    {
        if (false == tutorialText.Contains("your join button"))
        {
            return;
        }

        GameManager.instance.StartCoroutine(InsertPlayerControls());
    }

    /// <summary>
    /// Waits a frame, then replaces TutorialText "your join button" with "Flap".bindings for all active players.
    /// </summary>
    private IEnumerator InsertPlayerControls()
    {
        // ShareDevice needs a frame to cleanup incorrect players made by PlayerInputManager
        yield return null;

        var buttonPaths = PlayerInput.all
            .SelectMany
            (p =>
                p.actions["Flap"].bindings
                .Where(b => b.groups == p.currentControlScheme) // binding matches players controlscheme
                .Select(b => b.path.Split("/")[1]) // convert `<Keyboard>/space` to `space`
            ).ToList();
        var replacement = string.Join(", ", buttonPaths);
        stateMachine.TutorialText.text = tutorialText.Replace("your join button", replacement);
    }
}
