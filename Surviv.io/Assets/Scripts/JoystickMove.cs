using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    private Joystick _joystick;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (_joystick.Direction.y != 0)
        {
            _rigidBody.velocity = new Vector2(_joystick.Direction.x * gameObject.GetComponent<Unit>().movementSpeed, _joystick.Direction.y * gameObject.GetComponent<Unit>().movementSpeed);
        }
        else
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
