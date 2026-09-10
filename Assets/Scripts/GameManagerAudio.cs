using System;
using System.Collections;
using UnityEngine;

public enum SfxClip { Flap, Gameover, Heal, Point, Revived }

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class GameManagerAudio : MonoBehaviour
{
    private AudioSource audioSource
    {
        get
        {
            if (_audioSource == null)
            {
                _audioSource = this.GetComponent<AudioSource>();
            }
            return _audioSource;
        }
    }
    private AudioSource _audioSource;

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

    private IEnumerator Start()
    {
        // handle Unity's bug "UnassignedReferenceException: The variable '' has not been assigned." https://discussions.unity.com/t/the-variable-has-not-been-assigned-but-it-has/94274/11
        //yield return new WaitUntil(() => audioSource != null);

        while (audioSource == null)
        {
            Debug.LogError("audioSource still null");
            yield return null;
        }
    }

    public void PlaySfx(SfxClip clip)
    {
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
