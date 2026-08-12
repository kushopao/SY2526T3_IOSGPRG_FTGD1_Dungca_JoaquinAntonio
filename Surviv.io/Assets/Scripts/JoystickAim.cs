using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAim : MonoBehaviour
{
    public Joystick aimJoystick;
    public float rotationSpeed = 5f;
    public Transform player;
    [SerializeField] public float orbitDistance = 1f;

    // Update is called once per frame
    void Update()
    {
        if (aimJoystick.Direction.sqrMagnitude > 0.1f)
        {
            // Calculate the angle based on the joystick's direction
            float angle = Mathf.Atan2(aimJoystick.Direction.y, aimJoystick.Direction.x) * Mathf.Rad2Deg;

            // Set the gun's position relative to the character at the desired distance
            Vector3 offset = new Vector3(aimJoystick.Direction.x, aimJoystick.Direction.y, 0).normalized * orbitDistance;
            transform.position = player.position + offset;

            // Rotate the gun to look in the joystick's direction
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
