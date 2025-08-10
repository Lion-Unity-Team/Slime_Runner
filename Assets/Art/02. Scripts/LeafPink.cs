using System;
using UnityEngine;

public class LeafPink : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += Time.deltaTime * (Vector3.down * 0.2f + Vector3.right * 0.1f);
    }
}
