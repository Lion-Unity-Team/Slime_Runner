using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Enemy_Spawner enemySpawner;
    private Vector3 _resetPos;
    
    private float _speed;

    private void Awake()
    {
        _resetPos = new Vector3(0, 10, 0);
    }

    private void Start()
    {
        enemySpawner = FindFirstObjectByType<Enemy_Spawner>();
        _speed = enemySpawner.enemySpeed;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * _speed * Vector3.down;

        if (transform.position.y <= -_resetPos.y)
        {
            var len = transform.position + _resetPos;
            transform.position = _resetPos + len;
        }
    }
}
