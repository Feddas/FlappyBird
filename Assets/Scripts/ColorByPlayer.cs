using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(SpriteRenderer))]
public class ColorByPlayer : MonoBehaviour
{
    private PlayerInput playerInput
    {
        get
        {
            _playerInput ??= this.GetComponent<PlayerInput>();
            return _playerInput;
        }
    }
    private PlayerInput _playerInput;

    private SpriteRenderer spriteRenderer
    {
        get
        {
            _spriteRenderer ??= this.GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        int maxPlayers = 4;
        float hue = Mathf.Lerp(0, 1, Mathf.InverseLerp(0, maxPlayers, playerInput.playerIndex));
        spriteRenderer.color = Color.HSVToRGB(hue, 1, 0.8f);
    }
}
