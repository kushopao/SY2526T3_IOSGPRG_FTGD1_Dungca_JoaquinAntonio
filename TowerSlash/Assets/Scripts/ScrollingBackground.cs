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
    }

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }

}
