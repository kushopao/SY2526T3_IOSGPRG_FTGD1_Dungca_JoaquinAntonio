using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float _baseSpeed = 0.75f;
    private float _dashSpeed = 1.5f;
    private float _speed;


    [SerializeField] private Player _player;
    [SerializeField] private Renderer bgRenderer;

    private void Awake()
    {
    }

    void Update()
    {
        if (_player._isDashing)
            _speed = _dashSpeed;
        else
            _speed = _baseSpeed;

        bgRenderer.material.mainTextureOffset += new Vector2(0, _speed * Time.deltaTime);
    }

}
