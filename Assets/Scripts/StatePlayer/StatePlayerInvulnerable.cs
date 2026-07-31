using UnityEngine;

public class StatePlayerInvulnerable : StateMachineAnimatorState<StateBridgeToPlayer>
{
    protected override void OnStateUpdated()
    {
        stateMachine.Invulnerable.FixHeight();
    }

    /// <summary>
    /// This StateMachineBehaviour correctly handles incorporting the Looping 4 times via "Exit Time" causes the state to take 4 seconds.
    /// </summary>
    protected override void OnStateExited()
    {
        stateMachine.Invulnerable.FixHeight();
    }
}
