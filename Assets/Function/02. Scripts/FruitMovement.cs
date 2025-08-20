using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] private float fruitSpeed = 7f;
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3(0, -1, 0) * (fruitSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
