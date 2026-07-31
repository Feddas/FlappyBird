using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive { get { return reviveProgress >= 1; } }

    public bool IsDead { get { return false == IsAlive; } }

    [Tooltip("percent the player is revived")]
    [Range(0f, 1f)]
    [SerializeField] private float reviveProgress = 1; // default to revived/alive

    [Tooltip("Reference to the Rigidbody at the root of the player")]
    [SerializeField] private new Rigidbody2D rigidbody;

    [Tooltip("Manages invulnerable state")]
    [SerializeField] private Invulnerable invulnerable;

    [Tooltip("Layer player is in while IsAlive. Layer that doesn't collide with itself in Layer Collision Matrix")]
    [SerializeField] private int physicsLayerOnAlive = 6;

    private float lastReviveProgress;

    /// <summary> The skull. Shows how un-revived the player is. </summary>
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
        reviveProgress = 0f;
        Dirty();

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
        if (reviveProgress >= 1)
        {
            Debug.LogWarning("Resurrect was called on a player that was already alive");
            return;
        }

        reviveProgress += percent;
        Dirty();
    }

    private IEnumerator Start()
    {
        // handle Unity's bug "UnassignedReferenceException: The variable '' has not been assigned." https://discussions.unity.com/t/the-variable-has-not-been-assigned-but-it-has/94274/11
        yield return new WaitUntil(() => spriteRenderer != null);

        lastReviveProgress = 1 - reviveProgress; // This negation ensures the first run is marked as fully Dirty()
        initialSpriteSize = spriteRenderer.size;
        OnValidate(); // Handle player starting dead ("Bird TutRevive.prefab" starts dead)
    }

    private void OnValidate()
    {
        Dirty();
    }

    /// <summary> Redraw sprite to represent current health (revived) state </summary>
    private void Dirty()
    {
        // guard clause: Start() hasn't run
        if (initialSpriteSize == Vector2.zero)
        {
            return;
        }

        if (reviveProgress >= 1 && lastReviveProgress < 1) // player just changed from dead to alive
        {
            setAlive();
            invulnerable.Trigger();
            lastReviveProgress = reviveProgress;
        }
        else // player is dead
        {
            // fill percent of sprite using tiling hack from https://discussions.unity.com/t/sprite-fill-amount/925219/2
            spriteRenderer.size = new Vector2(
                Mathf.Lerp(initialSpriteSize.x, 0, reviveProgress),
                spriteRenderer.size.y);

            if (reviveProgress < 1 && lastReviveProgress >= 1) // player just changed from alive to dead
            {
                setDead();
                lastReviveProgress = reviveProgress;
            }
        }
    }

    private void setAlive()
    {
        spriteRenderer.enabled = false;
        setConstraintsAndPhysicsLayer(RigidbodyConstraints2D.FreezePositionX, physicsLayerOnAlive);
    }

    private void setDead()
    {
        spriteRenderer.enabled = true;
        setConstraintsAndPhysicsLayer(RigidbodyConstraints2D.FreezeAll, physicslayer: 0);
    }

    private void setConstraintsAndPhysicsLayer(RigidbodyConstraints2D constraints, int physicslayer)
    {
        if (rigidbody != null)
        {
            rigidbody.constraints = constraints;
            rigidbody.gameObject.layer = physicslayer;
        }
        else // handle "Bird TutRevive.prefab" not having rigidbody (since it never moves)
        {
            this.transform.parent.gameObject.layer = physicslayer;
        }
    }
}
