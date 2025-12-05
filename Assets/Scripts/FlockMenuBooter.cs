using LibrarySf;
using UnityEngine;

namespace FlappyFlock
{
    public class FlockMenuBooter : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Scene to load when HasCompletedTutorial == false")]
        [ContextMenuItem("ForceTutorialNotStarted()", "ForceTutorialNotStarted")]
        private SceneField onTutorialNotStarted;

        [SerializeField]
        [Tooltip("Scene to load when Tutorial has been started, but not finished")]
        private SceneField onTutorialNotFinished;

        [Header("Debug")]
        [SerializeField]
        [Tooltip("When true, 'PlayerPrefs.TutorialsFinished` is set to 0 (not started) on Start.")]
        bool forceTutorialAtStart;

        [SerializeField]
        [Tooltip("When true, 'PlayerPrefs.TutorialsFinished` is set to 3 (completed) on Start.")]
        bool forceTutorialFinished;

        void Start()
        {
            if (forceTutorialAtStart)
            {
                ForceTutorialNotStarted();
            }
            else if (forceTutorialFinished)
            {
                forceTutorialTo(3);
            }

            loadPlayerPrefsTutorialScene();
        }

        /// <summary> Invoked onClick by UI button </summary>
        public void PlayerPressedPlayButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(onTutorialNotFinished);
        }

        /// <summary> Invoked onClick by UI button </summary>
        public void RedoTutorial()
        {
            ForceTutorialNotStarted();
            loadPlayerPrefsTutorialScene();
        }

        /// <summary> this method is public only because of [ContextMenuItem("SetTutorialNotStarted()", "SetTutorialNotStarted")] </summary>
        public void ForceTutorialNotStarted()
        {
            forceTutorialTo(0);
        }

        /// <summary> Reads player prefs to determine and load the correct scene </summary>
        private void loadPlayerPrefsTutorialScene()
        {
            // determine tutorial progress
            int completedTutorialId = 0;
            if (PlayerPrefs.HasKey(TutorialManager.PlayerPrefsTutorialKey))
            {
                completedTutorialId = PlayerPrefs.GetInt(TutorialManager.PlayerPrefsTutorialKey);
            }
            bool hasStartedTutorial = completedTutorialId > 0;
            bool hasFinishedTutorial = completedTutorialId >= 3;

            // jump to tutorial
            Debug.Log("FlockMenuBooter completedTutorialId:" + completedTutorialId);
            if (hasStartedTutorial == false)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(onTutorialNotStarted);
            }
            else if (hasFinishedTutorial == false)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(onTutorialNotFinished);
            }
        }

        private void forceTutorialTo(int step)
        {
            PlayerPrefs.SetInt(TutorialManager.PlayerPrefsTutorialKey, step);
        }
    }
}
