using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private new Rigidbody2D rigidbody
    {
        get
        {
            _rigidbody ??= this.GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;

    /// <summary> This method called by PlayerInput component </summary>
    public void OnFlap(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            rigidbody.velocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.y * _rotationSpeed);
    }
}
