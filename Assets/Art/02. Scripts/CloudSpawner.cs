using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    
    private float _scaleX;
    private float _scaleY;
    
    private float _spawnTime;
    private float _spawnPosX;

    private IEnumerator Start()
    {
        while (true)
        {
            _spawnTime = Random.Range(2, 6);
            yield return new WaitForSeconds(_spawnTime);

            _spawnPosX = Random.Range(-6.5f, 0);
            var spawnPos = new Vector2(_spawnPosX, 6);

            _scaleX = Random.Range(2, 6);
            _scaleY = Random.Range(2, 6);
            var scale = new Vector2(_scaleX, _scaleY);

            GameObject obj = Instantiate(cloud, spawnPos, Quaternion.identity);
            obj.transform.localScale = scale;
        }
    }
}
