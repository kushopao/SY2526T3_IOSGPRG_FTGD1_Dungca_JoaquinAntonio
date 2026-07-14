using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float baseSpeed;
    public float speed;

    [SerializeField] private Player _player;
    [SerializeField] private Renderer bgRenderer;

    private void Awake()
    {
        //_player = GameManager.Instance.Player;
    }

    void Update()
    {
        //if (_player.isDashing)
        //{
        //    speed *= speed;
        //}
        //else
        //{
        //    speed = baseSpeed;
        //}

        bgRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }

}
