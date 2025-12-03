using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlappyFlock
{
    /// <summary> Positions players when <seealso cref="PlayerInputManager"/> SendMessages <seealso cref="OnPlayerJoined"/> </summary>
    [RequireComponent(typeof(PlayerInputManager))]
    public class PositionOnJoin : MonoBehaviour
    {
        [Tooltip("Position of the first player to join")]
        [SerializeField] private Vector3 startPostion;

        [Tooltip("How far the 2nd player is from the first player")]
        [SerializeField] private Vector3 incrementDelta = Vector3.right;

        [Tooltip("If false, all players are positioned to the right of the first player. If true, odd player are incremented to the right, even to the left.")]
        [SerializeField] private bool oscillate = true;

        private void OnPlayerJoined(PlayerInput player)
        {
            Vector3 result = startPostion;

            int signedPlayerIndex = oscillate
                ? negateOddNumbers(player.playerIndex)
                : player.playerIndex;

            var delta = signedPlayerIndex * incrementDelta;

            // set player to spawn position
            player.transform.position = startPostion + delta;
        }

        /// <returns> <paramref name="number"/> redistributed around 0.
        /// If the number is even, return it divided by two.
        /// If the number is odd, return it divided by two and times -1.</returns>
        private int negateOddNumbers(int number)
        {
            if (number < 0)
            {
                throw new NotImplementedException();
            }
            else if (number % 2 == 0)
            {
                return number / 2;
            }
            else
            {
                return (number / 2 + 1) * -1;
            }
        }
    }
}
