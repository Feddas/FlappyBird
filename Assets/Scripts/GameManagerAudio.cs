using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SfxClip { Flap, Gameover, Heal, Point, Revived }

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class GameManagerAudio : MonoBehaviour
{
    [Serializable]
    public struct SfxClips
    {
        public AudioClip Flap;
        public AudioClip Gameover;
        public AudioClip Heal;
        public AudioClip Point;
        public AudioClip Revived;
    }

    [SerializeField]
    [Tooltip("All sound effect audio clips used in this game")]
    private SfxClips sfxClip;
    private AudioSource audioSource;

    // https://discussions.unity.com/t/no-sound-clips-in-webgl-build/819174/14
    // check build log for .wav, AudioClip, and ffmpeg %LOCALAPPDATA%\Unity\Editor\Editor.log
    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>(); // note: not using GetComponent in property setter due to not working in WebGL builds

        if (PlayerPrefs.HasKey(PlayerPref.Of[PlayerPref.Key.AudioMuted].Key))
        {
            var isMuted = PlayerPrefs.GetInt(PlayerPref.Of[PlayerPref.Key.AudioMuted].Key) == 1;
            audioSource.enabled = false == isMuted;
        }
    }

    public void ToggleAudioMuted(bool isMuted)
    {
        PlayerPrefs.SetInt(PlayerPref.Of[PlayerPref.Key.AudioMuted].Key, isMuted ? 1 : 0);
        audioSource.enabled = false == isMuted;

        // HACK: The toggle UI is toggled again if the player causes it to lose focus. Workaround, lose focus via the line below in this script instead.
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlaySfx(SfxClip clip)
    {
        if (false == audioSource.enabled)
        {
            return;
        }

        switch (clip)
        {
            case SfxClip.Flap:
                audioSource.PlayOneShot(sfxClip.Flap);
                break;
            case SfxClip.Gameover:
                audioSource.PlayOneShot(sfxClip.Gameover);
                //StartCoroutine(playInSeconds(sfxClip.Gameover.length, sfxClip.Heal)); // uncomment this line to make it more like the original https://youtu.be/RJbgvKWFrCU
                break;
            case SfxClip.Heal:
                audioSource.PlayOneShot(sfxClip.Heal);
                break;
            case SfxClip.Point:
                audioSource.PlayOneShot(sfxClip.Point);
                break;
            case SfxClip.Revived:
                audioSource.PlayOneShot(sfxClip.Revived);
                break;
            default:
                Debug.LogError("GameManagerAudio.cs PlaySfx() errored on " + clip.ToString());
                break;
        }
    }

    /// <param name="delay">in seconds</param>
    private IEnumerator playInSeconds(float delay, AudioClip clip)
    {
        yield return new WaitForSecondsRealtime(delay); // Use Realtime because during GameOver timescale = 0
        audioSource.PlayOneShot(clip);
    }
}
