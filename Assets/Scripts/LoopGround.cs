using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;
    [SerializeField] private float _width = 6f;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _startSize;
    private Vector3 _startPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startSize = new Vector2(_spriteRenderer.size.x, _spriteRenderer.size.y);
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;

        _spriteRenderer.size = new Vector2(_spriteRenderer.size.x + _speed * Time.deltaTime, _spriteRenderer.size.y);

        if (_spriteRenderer.size.x > _width)
        {
            _spriteRenderer.size = _startSize;
            transform.position = _startPosition;
        }
    }
}
