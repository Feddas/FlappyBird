using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary> Logic for: Flap a flock, of at least 2, through 2 obstacles. </summary>
[CreateAssetMenu(fileName = "StateTutorial3Flock", menuName = "Scriptable Objects/StateTutorial3Flock", order = 3)]
public class StateTutorialFlock : TutorialLogic
{
    public override void OnEnter(StateBridgeToTutorial stateMachine)
    {
    }

    public override bool IsExitCondition(StateBridgeToTutorial stateMachine)
    {
        if (atLeast2PlayersAlive())
        {
            int playersActive = PlayerInput.all.Count;
            return Score.instance.CurrentScore >= 2 * playersActive;
        }
        else
        {
            return false;
        }
    }

    public override void OnExit(StateBridgeToTutorial stateMachine)
    {
    }

    private bool atLeast2PlayersAlive()
    {
        int playersActive = PlayerInput.all.Count;
        if (playersActive < 2)            // Need at least 2 players for this Tutorial step
        {
            Score.instance.ResetScore();
            return false;
        }

        int playersAlive = PlayerInput.all.Count(p => isAlive(p));
        int score = Score.instance.CurrentScore;
        if (playersAlive < playersActive) // All players in the flock need to stay alive
        {
            Score.instance.ResetScore();
            return false;
        }

        return true;
    }

    private bool isAlive(PlayerInput player)
    {
        var health = player.GetComponentInChildren<PlayerHealth>();
        return health != null && health.IsAlive;
    }
}
