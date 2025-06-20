using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;


    private Rigidbody2D _rigidbody
    {
        get
        {
            __rigidbody ??= this.GetComponent<Rigidbody2D>();
            return __rigidbody;
        }
    }
    private Rigidbody2D __rigidbody;

    public void OnFlap(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _rigidbody.velocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rigidbody.velocity.y * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
}
