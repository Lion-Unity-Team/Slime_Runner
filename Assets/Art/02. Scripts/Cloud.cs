using System;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float destroyPos = -10;
    private void Update()
    {
        transform.position += Time.deltaTime * (Vector3.down * 0.7f + Vector3.right * 0.1f);
        
        if(transform.position.y < destroyPos)
            Destroy(gameObject);
    }
}
