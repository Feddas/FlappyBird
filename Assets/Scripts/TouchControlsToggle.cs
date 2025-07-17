using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Activates <seealso cref="MobileControls"/> when <seealso cref="tapOn"/> is performed.
/// copied from https://youtu.be/aI-r7ILNDug?t=220
/// </summary>
public class TouchControlsToggle : MonoBehaviour
{
    public InputActionAsset InputActions;
    public GameObject MobileControls;

    private InputAction tapOn;
    private InputAction tapOff;

    void Awake()
    {
        tapOn = InputActions.FindAction("TapOn");
        tapOff = InputActions.FindAction("TapOff");
    }

    void Update()
    {
        if (tapOn.WasPerformedThisFrame())
        {
            MobileControlsActive(true);
        }
        if (tapOff.WasPerformedThisFrame())
        {
            MobileControlsActive(false);
        }
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Playing").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Playing").Disable();
    }

    private void MobileControlsActive(bool isActive)
    {
        MobileControls.SetActive(isActive);
    }
}
