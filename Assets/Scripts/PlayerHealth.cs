using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive { get { return revived >= 1; } }

    public bool IsDead { get { return false == IsAlive; } }

    [Tooltip("percent the player is revived")]
    [Range(0f, 1f)]
    [SerializeField] private float revived;

    [Tooltip("Reference to the Rigidbody at the root of the player")]
    [SerializeField] private new Rigidbody2D rigidbody;

    private SpriteRenderer spriteRenderer
    {
        get
        {
            _spriteRenderer ??= this.GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }
    private SpriteRenderer _spriteRenderer;

    private Vector2 initialSpriteSize;

    /// <summary> Set players health immediately to 0 </summary>
    public void Kill()
    {
        // kill this player
        revived = 0f;
        spriteRenderer.enabled = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        OnValidate();

        // check if game over
        bool allPlayersDead = UnityEngine.InputSystem.PlayerInput.all.All(p => p.GetComponentInChildren<PlayerHealth>().IsDead);
        if (allPlayersDead)
        {
            GameManager.instance.GameOver();
        }
    }

    /// <summary> Adds <paramref name="percent"/> to players current resurrection progress. </summary>
    public void Resurrect(float percent)
    {
        if (revived >= 1)
        {
            return;
        }

        revived += percent;
        OnValidate();
    }

    private void Start()
    {
        initialSpriteSize = spriteRenderer.size;
    }

    private void OnValidate()
    {
        // guard clause: Start() hasn't run
        if (initialSpriteSize == Vector2.zero)
        {
            return;
        }

        if (revived >= 1) // player is alive
        {
            spriteRenderer.enabled = false;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else // player is dead
        {
            // fill percent of sprite using tiling hack from https://discussions.unity.com/t/sprite-fill-amount/925219/2
            spriteRenderer.size = new Vector2(
                Mathf.Lerp(initialSpriteSize.x, 0, revived),
                spriteRenderer.size.y);
        }
    }
}
