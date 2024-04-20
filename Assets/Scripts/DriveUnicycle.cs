using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveUnicycle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _tireRB;
    [SerializeField] private float _speed = 150f;
    private float _moveInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate()
    {
        _tireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
    }
}
