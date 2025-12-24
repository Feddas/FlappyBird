using TMPro;
using UnityEngine;

public class StateBridgeToTutorial : StateMachineGameObject<StateBridgeToTutorial>
{
    [Header("Modified by Animator")]
    public GameObject GameOverCanvas;
    public TextMeshProUGUI TutorialText;
    public TutorialManager TutorialManager;
    public TutorialRevive TutorialRevive;
}
