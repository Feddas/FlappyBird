using TMPro;
using UnityEngine;

public class StateBridgeToTutorial : StateMachineGameObject<StateBridgeToTutorial>
{
    [Header("Modified by Animator")]
    public GameObject GameOverCanvas;
    public TextMeshProUGUI TutorialText;
    public TutorialManager TutorialManager;

    [Header("readonly")]
    [SerializeField]
    private int currentTutorial = 1;

    void Start()
    {
        TutorialManager.OnNextTutorial += TutorialManager_OnNextTutorial;
    }

    private void TutorialManager_OnNextTutorial()
    {
        SetTutorialsFinished(currentTutorial);
        currentTutorial++;
    }

    // void Update() { }

    public void SetTutorialsFinished(int count)
    {
        animator.SetInteger("TutorialsFinished", count); // Send information to let the Animator determine game state.
    }
}
