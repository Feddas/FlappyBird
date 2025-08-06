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
        animator.runtimeAnimatorController = animators[schemeIndex % animators.Count]; // re-use animators if there are more schemes than animators
        this.gameObject.name += playerInput.playerIndex + "c" + schemeIndex;
    }
}
