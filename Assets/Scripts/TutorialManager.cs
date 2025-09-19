using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    const string PlayerPrefsTutorialKey = "HasCompletedTutorial";

    public GameObject Scores;

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
        if (Score.instance.CurrentScore >= 2)
        {
            // TODO: reset game and score to 0

            Debug.Log("TODO: Go to step 2 of the tutorial:");
            // step 2 is: Flap each member of your flock individually. Reset: if any player dies. Pass: all members make it through 2 gates.
        }
    }

    public void FinishTutorial()
    {
        // TODO: PlayerPrefs.SetInt(PlayerPrefsTutorialKey, 1);
        Destroy(this.gameObject);
    }
}
