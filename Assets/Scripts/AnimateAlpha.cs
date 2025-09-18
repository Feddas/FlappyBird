using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AnimateAlpha : MonoBehaviour
{
    private CanvasGroup canvasGroup
    {
        get
        {
            _canvasGroup ??= this.GetComponent<CanvasGroup>();
            return _canvasGroup;
        }
    }
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private AnimationCurve curve = new(
        new Keyframe(0, 0),
        new Keyframe(0.5f, 1),
        new Keyframe(1.5f, 1),
        new Keyframe(2, 0)
    );

    private Coroutine routineToBlink;

    private void OnEnable()
    {
        routineToBlink = StartCoroutine(animateAlpha());
    }

    private void OnDisable()
    {
        if (routineToBlink != null)
        {
            StopCoroutine(routineToBlink);
            routineToBlink = null;
        }
    }

    private IEnumerator animateAlpha()
    {
        float curveSeconds = curveDuration();
        yield return new WaitUntil(() => canvasGroup != null);
        while (true)
        {
            // on
            float atSeconds = Time.realtimeSinceStartup % curveSeconds;
            canvasGroup.alpha = curve.Evaluate(atSeconds);
            //Debug.Log(Time.frameCount + this.name + atSeconds + "s/" + curveSeconds + "s " + canvasGroup.alpha);
            yield return null;
        }
    }

    private float curveDuration()
    {
        if (curve != null && curve.keys.Length > 0)
        {
            float startTime = curve.keys[0].time; // Time of the first keyframe
            float endTime = curve.keys[curve.keys.Length - 1].time; // Time of the last keyframe
            float duration = endTime - startTime;

            return duration;
        }
        else
        {
            Debug.LogWarning(this.name + "'s AnimateAlpha AnimationCurve is empty or null.");
            return 0;
        }
    }
}
