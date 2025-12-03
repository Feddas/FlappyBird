using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Moves a mask to behave like a slider. Does so without moving its children </summary>
[RequireComponent(typeof(RectMask2D))]
[RequireComponent(typeof(RectTransform))]
public class SliderMaskRect : MonoBehaviour
{
    private RectMask2D rectMask2d
    {
        get
        {
            return _rectMask2d ??= this.GetComponent<RectMask2D>();
        }
    }
    private RectMask2D _rectMask2d;

    private RectTransform rectTransform
    {
        get
        {
            return _rectTransform ??= this.GetComponent<RectTransform>();
        }
    }
    private RectTransform _rectTransform;

    [Range(0f, 1f)]
    [SerializeField]
    private float PercentFilled = 0.5f;

    public void SetSliderPercentage(float value)
    {
        var offset = (1 - value) * rectTransform.rect.width;
        rectMask2d.padding = new Vector4(0, 0, offset, 0); // ref: https://discussions.unity.com/t/how-do-i-move-a-mask-without-moving-the-children/850910/2
    }

    private void Start()
    {
        SetSliderPercentage(PercentFilled);
    }

    private void OnValidate()
    {
        SetSliderPercentage(PercentFilled);
    }

    /// <inheritdoc/>
    private void OnRectTransformDimensionsChange()
    {
        SetSliderPercentage(PercentFilled);
    }
}
