using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    const string PlayerPrefsTutorialKey = "HasCompletedTutorial";

    public GameObject Scores;
    public event Action OnNextTutorial;

    private Action CurrentTutorial;

    void Awake()
    {
        bool hasCompletedTutorial = PlayerPrefs.HasKey(PlayerPrefsTutorialKey) && PlayerPrefs.GetInt(PlayerPrefsTutorialKey) == 1;

        if (hasCompletedTutorial)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Scores.SetActive(false);
        }
    }

    void Update()
    {
        if (CurrentTutorial != null)
        {
            CurrentTutorial();
        }
    }

    public void SetCurrentTutorial(int tutorialId)
    {
        switch (tutorialId)
        {
            case 1:
                CurrentTutorial = DoTutorial1;
                break;
            case 2:
                CurrentTutorial = DoTutorial2;
                break;
            case 3:
                CurrentTutorial = DoTutorial3;
                break;
            default:
                FinishTutorial();
                break;
        }
    }

    public void DoTutorial1()
    {
        if (Score.instance.CurrentScore >= 2)
        {
            // TODO: reset game and score to 0

            Debug.Log("TODO: Go to step 2 of the tutorial:");
            if (OnNextTutorial != null)
            {
                OnNextTutorial();
            }
            // step 2 is: Flap each member of your flock individually. Reset: if any player dies. Pass: all members make it through 2 gates.
        }
    }

    public void DoTutorial2()
    { }

    public void DoTutorial3()
    { }

    public void FinishTutorial()
    {
        // TODO: PlayerPrefs.SetInt(PlayerPrefsTutorialKey, 1);
        Destroy(this.gameObject);
    }
}
