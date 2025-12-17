using UnityEngine;

/// <summary> This is a base class. Don't instatiate from it, inherit it into another class.
/// Sub-classes are used by StateMachineBehaviour StateChangesTutorial.cs
/// Holds logic specific to a single tutorial type. AKA what happens in the revive tutorial that doesn't happen in any other tutorials </summary>
public class TutorialLogic : ScriptableObject
{
    public virtual void OnEnter(StateBridgeToTutorial stateMachine)
    {
    }

    /// <summary> checked every frame this animator state is active </summary>
    public virtual bool IsExitCondition(StateBridgeToTutorial stateMachine)
    {
        return false;
    }

    public virtual void OnExit(StateBridgeToTutorial stateMachine)
    {
    }
}
