using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Fills a slider based on how many players are flapping above a height.
/// </summary>
public class SliderByFlapping : MonoBehaviour
{
    [Tooltip("Minimum height players must fly at to contribute to filling the slider")]
    [SerializeField]
    private float minFlapHeight = 1.0f;

    [Tooltip("exponential base for how the slider fills. The exponent being the number of players at minFlapHeight.")]
    [Range(0.001f, 0.02f)]
    [SerializeField]
    private float fillPower = 0.01f;

    [SerializeField]
    private Slider slider;

    [Tooltip("Action to do when slider.value = 1")]
    [SerializeField]
    private UnityEvent onSliderFull;

    void Update()
    {
        int numAtHeight = PlayerInput.all.Count(p => p.transform.position.y > minFlapHeight);
        float sliderDelta = numAtHeight <= 1
            ? 0
            : fillPower * Mathf.Pow(4f, numAtHeight);
        slider.value += sliderDelta * Time.deltaTime;
        if (slider.value >= 1)
        {
            onSliderFull.Invoke();
        }
    }
}
