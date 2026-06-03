using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SwipeType
{
    UP
    , RIGHT
    , DOWN
    , LEFT
    , NONE
}

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchPositionAction;

    private Vector2 _touchStart;
    private Vector2 _touchEnd;
    private Vector2 _distance;

    [SerializeField] private float minDistance = 50f;

    public SwipeType swipeType;

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

        DetectSwipe();
    }

    private void DetectSwipe()
    {
        _distance = _touchStart - _touchEnd;

        if (_distance.magnitude >= minDistance)
        {
            CheckDirection();
        }
        else
        {
            return;
        }
    }

    private void CheckDirection()
    {
        float x = _distance.x;
        float y = _distance.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                swipeType = SwipeType.LEFT;
            }
            else
            {
                swipeType = SwipeType.RIGHT;
            }
        }
        else
        {
            if (y > 0)
            {
                swipeType = SwipeType.DOWN;
            }
            else
            {
                swipeType = SwipeType.UP;
            }
        }

            /*if (_touchEnd.x < _touchStart.x)
            {
                swipeType = SwipeType.LEFT;
            }
            else if (_touchEnd.x > _touchStart.x)
            {
                swipeType = SwipeType.RIGHT;
            }
            */

            Debug.Log($"Swipe Type = {swipeType}");
            _distance = Vector2.zero;
        }
        else if (_touchEnd.y < _touchStart.y)
        {
            Debug.Log("Player Swiped Down");
        }
        else if (_touchEnd.y > _touchStart.y)
        {
            Debug.Log("Player Swiped Up");
        }
    }
