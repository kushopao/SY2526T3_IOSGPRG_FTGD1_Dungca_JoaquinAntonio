using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchPositionAction;

    private Vector2 _touchStart;
    private Vector2 _touchEnd;

    private void Awake()
    {
        _touchPressedAction = _playerInput.actions["TouchPress"];
        _touchPositionAction = _playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        _touchPressedAction.started += OnTouchStarted;
        _touchPressedAction.canceled += OnTouchReleased;
        Debug.Log("Swipe Detection Enabled");
    }

    private void OnDisable()
    {
        _touchPressedAction.started -= OnTouchStarted;
        _touchPressedAction.canceled -= OnTouchReleased;
        Debug.Log("Swipe Detection Disabled");
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _touchStart = _touchPositionAction.ReadValue<Vector2>();
        //Debug.Log($"Started: {_touchStart}");
    }

    private void OnTouchReleased(InputAction.CallbackContext context)
    {
        _touchEnd = _touchPositionAction.ReadValue<Vector2>();
        //Debug.Log($"Released: {_touchEnd}");

        if (_touchEnd.x < _touchStart.x)
        {
            Debug.Log("Player Swiped Left");
        }
        else if (_touchEnd.x > _touchStart.x)
        {
            Debug.Log("Player Swiped Right");
        }
    }
}
