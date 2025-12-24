using UnityEngine;

/// <summary> Logic for: Completion of the tutorial </summary>
[CreateAssetMenu(fileName = "StateTutorial4Finished", menuName = "Scriptable Objects/StateTutorial4Finished", order = 4)]
public class StateTutorialFinished : TutorialLogic
{
    private const float secondsToShowFinished = 2;
    private float startTime;

    public override void OnEnter(StateBridgeToTutorial stateMachine)
    {
        startTime = Time.fixedUnscaledTime;
    }

    public override bool IsExitCondition(StateBridgeToTutorial stateMachine)
    {
        return Time.fixedUnscaledTime - startTime > secondsToShowFinished;
    }

    public override void OnExit(StateBridgeToTutorial stateMachine)
    {
        stateMachine.TutorialManager.OnTutorialFinished();
    }
}
