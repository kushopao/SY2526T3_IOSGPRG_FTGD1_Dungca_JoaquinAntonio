using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum State
{
    Wander,
    Seek,
    Destroy
}

public class EnemyFSM : MonoBehaviour
{
    public List<Transform> targetList = new List<Transform>();

    Vector3 _targetPos = Vector3.zero;

    [SerializeField] private State _currentState;
    [SerializeField] private State _prevState;

    [SerializeField] private float _currentTravelTime = 0;
    [SerializeField] private float _maxTravelTime = 5f;

    [SerializeField] private float _chaseDistance = 4f;
    [SerializeField] private float _chaseSpeed = 5f;

    [SerializeField] private float _minWaitTime = 3f;
    [SerializeField] private float _maxWaitTime = 5f;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _timeTick;

    void Awake()
    {
        _currentState = State.Wander;

        _waitTime = Random.Range(_minWaitTime, _maxWaitTime);
        _timeTick = 0;
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Wander:
                WanderUpdate();
                break;

            case State.Seek:
                SeekUpdate();
                break;

            case State.Destroy:
                DestroyUpdate();
                break;

            default:
                break;
        }
    }

    private void WanderUpdate()
    {
        _currentTravelTime += Time.deltaTime;

        if (targetList.Count > 0)
        {
            ChangeState(State.Seek);
            _currentTravelTime = 0f;
            return;
        }

        if (_targetPos == Vector3.zero)
        {
            RandomizeWanderPoint();
        }

        float step = gameObject.GetComponent<Unit>().movementSpeed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                      _targetPos,
                                                      step);

        if (Vector3.Distance(transform.position, _targetPos) <= 0.1f)
        {
            _targetPos = Vector3.zero;
            _currentTravelTime = 0f;
            ChangeState(State.Wander);
        }
        else if (Vector3.Distance(transform.position, _targetPos) > 0.1f && _currentTravelTime >= _maxTravelTime)
        {
            ResetWanderPointAndTravelTime();
            Debug.Log("Changed destination due to enemy not reaching it on time");
        }
    }

    private void SeekUpdate()
    {
        if (targetList.Count <= 0)
        {
            ChangeState(_prevState);
            return;
        }

        if (Vector3.Distance(this.transform.position, targetList[0].position) > _chaseDistance)
        {
            float step = _chaseSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          targetList[0].position,
                                                          step);
        }
        else if (Vector3.Distance(this.transform.position, targetList[0].position) <= _chaseDistance)
        {
            ChangeState(State.Destroy);
        }
    }

    private void DestroyUpdate()
    {
        if (targetList.Count <= 0)
        {
            ChangeState(State.Wander);
            return;
        }

        if (Vector3.Distance(this.transform.position, targetList[0].position) > _chaseDistance)
        {
            ChangeState(State.Seek);
        }

        GetComponentInChildren<Weapon>().Fire();
    }

    private void ChangeState(State state)
    {
        if (_currentState == state)
            return;

        _prevState = _currentState;
        _currentState = state;
        Debug.Log($"Changed {_prevState} State to {state} State");
    }

    private void RandomizeWanderPoint()
    {
        _targetPos = new Vector3(Random.Range(this.transform.position.x - 20,
                                              this.transform.position.x + 20),
                                 Random.Range(this.transform.position.y - 20,
                                              this.transform.position.y + 20));
    }

    public void ResetWanderPointAndTravelTime()
    {
        RandomizeWanderPoint();
        _currentTravelTime = 0f;
    }
}
