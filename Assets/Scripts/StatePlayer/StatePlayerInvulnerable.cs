using UnityEngine;

public class StatePlayerInvulnerable : StateMachineAnimatorState<StateBridgeToPlayer>
{
    protected override void OnStateUpdated()
    {
        if (Time.timeScale == 0) // Note: this check fixes deployed web error "RangeError: Maximum call stack size exceeded". cause seemed to be animator.updateMode == normal but Update is still run when Time.timeScale == 0 https://docs.unity3d.com/6000.3/Documentation/ScriptReference/AnimatorUpdateMode.html
        {
            return;
        }

        stateMachine.Invulnerable.FixHeight();
    }

    /// <summary>
    /// This StateMachineBehaviour correctly handles incorporting the Looping 4 times via "Exit Time" causes the state to take 4 seconds.
    /// </summary>
    protected override void OnStateExited()
    {
        if (stateMachine == null || stateMachine.Invulnerable == null)
        {
            return;
        }

        // note: the usage of `?.` here should negate the need for the guard clause above, but without that guard clause, starting a new game may have nullrefs from the previous game. "MissingReferenceException: The object of type 'Invulnerable' has been destroyed but you are still trying to access it."
        stateMachine?.Invulnerable?.FixHeight();
    }
}
