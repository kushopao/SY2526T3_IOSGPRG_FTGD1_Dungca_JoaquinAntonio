using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{
    //private float timer = 60f;

    //private void Update()
    //{
    //    if (timer > 0)
    //    {
    //        timer -= Timer.deltaTime;
    //    }
    //}

    [SerializeField] private bool _detectedByPlayer = false;


    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _arrowSprites = new List<Sprite>();

    private void Start()
    {
        //StartCoroutine(CO_CountDownTimer(60));
        //StartCoroutine(CO_CountUpTimer(0));
        //StartCoroutine(CO_ArrowRotation());
        //StartCoroutine(CO_SpawnEnemyEveryXSeconds(5));
        StartCoroutine(CO_SpawnArrow(Color.red));

    }

    private IEnumerator CO_SpawnEnemyEveryXSeconds(float seconds)
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;
            Debug.Log($"spawnTimer: {currentTime} seconds");

            if (currentTime >= seconds)
            {
                Spawner.Instance.SpawnEnemy();
                currentTime = 0;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CO_SpawnArrow(Color color)
    {
        int index = 0;  

        while (!_detectedByPlayer)
        { 
            index = Random.Range(0, _arrowSprites.Count);

            _spriteRenderer.sprite = _arrowSprites[index];
            _spriteRenderer.color = color;

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    // This is for the Yellow Arrow
    private IEnumerator CO_ArrowRotation()
    {
        int index = 0;

        while (!_detectedByPlayer)
        {
            _spriteRenderer.sprite = _arrowSprites[index % 4];
            index++;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private IEnumerator CO_CountDownTimer(float startTime)
    {
        float _currentTime = startTime;

        while (_currentTime > 0)
        {
            Debug.Log($"Current Time: {_currentTime}");
            yield return new WaitForSeconds(1); // WaitForSecondsRealtime(1f); updaates based on ur system's time
            _currentTime--;
        }

        Debug.Log("Delayed Function goes here");
    }

    private IEnumerator CO_CountUpTimer(float startTime)
    {
        float _currentTime = startTime;

        while (_currentTime > 0)
        {
            Debug.Log($"Current Time: {_currentTime}");
            yield return new WaitForSeconds(1); // WaitForSecondsRealTime(1f); updaates based on ur system's time
            _currentTime++;
        }

        Debug.Log("Delayed Function goes here");
    }
}
