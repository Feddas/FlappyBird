using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 1.5f;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
