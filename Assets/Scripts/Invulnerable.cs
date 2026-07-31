using System.Collections;
using UnityEngine;

/// <summary>
/// Uses the "Invulnerable" layer in the Aniamtor so that this gameobject can't die for a few seconds after it's first created
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

    /// <summary> Forces back to the Invulnerable state. </summary>
    public void Trigger()
    {
        animator.SetTrigger("TriggerInvulnerable");
    }

    /// <summary> Move the player if they are under <seealso cref="minInvulnerableHeight"/>
    /// This is called by the animator's invulnerable state by using a StateMachineBehaviour </summary>
    public void FixHeight()
    {
        if (transform.position.y < minInvulnerableHeight)
        {
            transform.position = new Vector3(
                transform.position.x,
                minInvulnerableHeight,
                transform.position.z);

            rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
