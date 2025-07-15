using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;
    [SerializeField] private float _width = 6f;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider2d;
    private Vector2 _startSize;
    private Vector3 _startPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2d = GetComponent<BoxCollider2D>();
        _startSize = new Vector2(_spriteRenderer.size.x, _spriteRenderer.size.y);
        _startPosition = transform.position;
    }

    private void Update()
    {
        // "/ 2" accounts for pivot being in center, size increasing both left and right side
        transform.position += Vector3.left * _speed / 2 * Time.deltaTime;

        _collider2d.size
            = _spriteRenderer.size
            = new Vector2(_spriteRenderer.size.x + _speed * Time.deltaTime, _spriteRenderer.size.y);

        if (_spriteRenderer.size.x > _width)
        {
            _collider2d.size
                = _spriteRenderer.size
                = _startSize;
            transform.position = _startPosition;
        }
    }
}
