using UnityEngine;
using UnityEngine.InputSystem;

/// <summary> Changes SpriteRenderer sort order of players when <seealso cref="PlayerInputManager"/> SendMessages <seealso cref="OnPlayerJoined"/> </summary>
[RequireComponent(typeof(PlayerInputManager))]
public class OnJoinSpriteZOrder : MonoBehaviour
{
    [Tooltip("Z-Order of the first player to join")]
    [SerializeField] private int startZOrder = 1;

    private void OnPlayerJoined(PlayerInput player)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            return;
        }

        spriteRenderer.sortingOrder = startZOrder + player.playerIndex;
    }
}
