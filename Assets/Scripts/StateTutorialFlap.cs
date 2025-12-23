using UnityEngine;

/// <summary> Logic for: Use your join button to flap. Flap to go between thorns without touching them. </summary>
[CreateAssetMenu(fileName = "StateTutorial1Flap", menuName = "Scriptable Objects/StateTutorial1Flap", order = 1)]
public class StateTutorialFlap : TutorialLogic
{
    public override void OnEnter(StateBridgeToTutorial stateMachine)
    {
    }

    public override bool IsExitCondition(StateBridgeToTutorial stateMachine)
    {
        return Score.instance.CurrentScore >= 2;
    }

    public override void OnExit(StateBridgeToTutorial stateMachine)
    {
    }
}
