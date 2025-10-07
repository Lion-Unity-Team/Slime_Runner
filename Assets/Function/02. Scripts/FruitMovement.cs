using System;
using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] private float fruitSpeed = 7f;
    [SerializeField] private bool canMove;

    void Update()
    {
        if (fruitSpeed == 0f) return;

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

    private void OnEnable()
    {
        if (UIManager.Instance != null)
        {
            canMove = UIManager.Instance.IsMenuOpen;
            if (canMove) fruitSpeed = 0f;

            UIManager.Instance.OnMenuToggle += HandleMenuToggle;
        }
    }

    private void OnDisable()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.OnMenuToggle -= HandleMenuToggle;
    }

    private void HandleMenuToggle(bool active)
    {
        canMove = active;
        fruitSpeed = canMove ? 0f : 7f;
    }
}
