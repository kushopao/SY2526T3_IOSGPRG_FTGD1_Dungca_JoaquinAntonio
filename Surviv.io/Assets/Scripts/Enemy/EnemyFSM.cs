using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Passive,
    Chase,
    Aggressive
}

public class EnemyFSM : MonoBehaviour
{
    private List<Transform> _targetList = new List<Transform>();

    Vector3 _targetPos = Vector3.zero;

    [SerializeField] private State _currentState;
    [SerializeField] private State _prevState;

    [SerializeField] private float _currentTravelTime = 0;
    [SerializeField] private float _maxTravelTime = 5f;

    [SerializeField] private float _chaseDistance = 4f;
    [SerializeField] private float _chaseSpeed = 5f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
