using System.Collections;
using UnityEngine;

/// <summary>
/// This gameobject can't die for a few seconds after it's first created
/// </summary>
public class Invulnerable : MonoBehaviour
{
    private Animator animator
    {
        get
        {
            _animator ??= this.GetComponent<Animator>();
            return _animator;
        }
    }
    private Animator _animator;

    private new Rigidbody2D rigidbody
    {
        get
        {
            _rigidbody ??= this.GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;
    [Tooltip("Min ground height player can be at while invulnerable (collider disabled from animator)")]
    [SerializeField]
    private float minInvulnerableHeight = -0.6f;

    private IEnumerator Start()
    {
        // handle Unity's bug "UnassignedReferenceException: The variable '' has not been assigned." https://discussions.unity.com/t/the-variable-has-not-been-assigned-but-it-has/94274/11
        yield return new WaitUntil(() => animator != null);

        StartCoroutine(invulnerableMinHeight());
    }

    /// <summary> Forces back to the Invulnerable state. </summary>
    public void Trigger()
    {
        StartCoroutine(trigger());
    }

    /// <returns> when no longer isInvulnerable </returns>
    private IEnumerator trigger()
    {
        animator.SetTrigger("TriggerInvulnerable");
        yield return new WaitUntil(() => isInvulnerable());
        yield return invulnerableMinHeight(); // wait until no longer isInvulnerable
    }

    /// <summary> Prevents the player from falling through the ground while invulnerable.
    /// Assumes the player starts in the "Invulnerable" State.
    /// Sets a min height for the player during the first time they are in that state. </summary>
    private IEnumerator invulnerableMinHeight()
    {
        while (isInvulnerable())
        {
            if (transform.position.y < minInvulnerableHeight)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    minInvulnerableHeight,
                    transform.position.z);

                rigidbody.velocity = Vector2.zero;
            }
            yield return null;
        }
    }

    /// <summary>
    /// https://stackoverflow.com/questions/50446427/how-to-check-if-a-certain-animation-state-from-an-animator-is-running
    /// </summary>
    private bool isInvulnerable()
    {
        return animator.GetCurrentAnimatorStateInfo(1)
            .IsName("Invulnerable.Invulnerable"); // "LayerName.StateName"
    }
}
