using UnityEngine;

/// <summary> Logic for: Revive other members of the flock by flying over to them twice. </summary>
[CreateAssetMenu(fileName = "StateTutorial2Revive", menuName = "Scriptable Objects/StateTutorial2Revive", order = 2)]
public class StateTutorialRevive : TutorialLogic
{
    public override void OnEnter(StateBridgeToTutorial stateMachine)
    {
        stateMachine.TutorialRevive.Enabled(true);
    }

    public override bool IsExitCondition(StateBridgeToTutorial stateMachine)
    {
        return stateMachine.TutorialRevive.IsFlockFullyRevived();
    }

    public override void OnExit(StateBridgeToTutorial stateMachine)
    {
        stateMachine.TutorialRevive.Enabled(false);
    }
}
