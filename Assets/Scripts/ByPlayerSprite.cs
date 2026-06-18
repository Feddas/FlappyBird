using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Swaps Animator Controller used depending on Players PlayerInput index.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class ByPlayerSprite : MonoBehaviour
{
    [Tooltip("Player animator will be chosen based on their playerindex")]
    [SerializeField]
    private List<RuntimeAnimatorController> animators;

    [Tooltip("Materials used to recolor players that use gamepads or mouse")]
    [SerializeField]
    private Material ColorMouse
                   , Color1LeftGamepad
                   , Color1RightGamepad
                   , Color2LeftGamepad
                   , Color2RightGamepad;

    private PlayerInput playerInput
    {
        get
        {
            _playerInput ??= this.GetComponent<PlayerInput>();
            return _playerInput;
        }
    }
    private PlayerInput _playerInput;

    private Animator animator
    {
        get
        {
            _animator ??= this.GetComponent<Animator>();
            return _animator;
        }
    }
    private Animator _animator;

    private void Start()
    {
        var schemeIndex = playerInput.actions.controlSchemes
            .IndexOf(s => s.name.Equals(playerInput.currentControlScheme));
        colorByDevice(schemeIndex);
        animator.runtimeAnimatorController = animators[schemeIndex % animators.Count]; // re-use animators if there are more schemes than animators
        this.gameObject.name += playerInput.playerIndex + "c" + schemeIndex;
    }

    /// <summary> Device type changes material is used to color the sprite </summary>
    /// <param name="schemeIndex"> Gamepad uses this to tell if Left or Right player </param>
    private void colorByDevice(int schemeIndex)
    {
        var device = playerInput.devices[0];
        var renderer = this.GetComponent<Renderer>();

        switch (device)
        {
            case Mouse:
                renderer.material = ColorMouse;
                break;
            case Gamepad:
                var gamepadIndex = Gamepad.all.IndexOf(g => g.deviceId == device.deviceId);
                if (gamepadIndex == 0)
                {
                    renderer.material = schemeIndex % 2 == 0 ? Color1RightGamepad : Color1LeftGamepad;
                }
                else if (gamepadIndex == 1)
                {
                    renderer.material = schemeIndex % 2 == 0 ? Color2RightGamepad : Color2LeftGamepad;
                }
                // else if more than 2 gamepads, there's so many players the colors don't matter anymore
                break;
            default:
                break;
        }
    }
}
