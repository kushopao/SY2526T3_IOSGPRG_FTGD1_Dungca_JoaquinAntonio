using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        if (joystick.Direction.y != 0)
        {
            rigidBody.velocity = new Vector2(joystick.Direction.x * gameObject.GetComponent<Unit>().movementSpeed, joystick.Direction.y * gameObject.GetComponent<Unit>().movementSpeed);
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
}
