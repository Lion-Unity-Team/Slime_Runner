using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI enemyText;
    private RaycastHit2D hit;
    
    
    public float rayDist;
    public float speed;
    private int enemyScore;

    private void Start()
    {
        enemyScore = int.Parse(enemyText.text);
    }

    private void Update()
    {

        Move();
        
        hit = Physics2D.Raycast(transform.position, Vector2.down, rayDist); 
        Debug.DrawRay(transform.position, Vector2.down * rayDist, Color.red);

        AttackCheak(); 
    }

    private void Move()
    {
        // y방향으로 -1씩 움직이는 명령어 * 속도 * 시간당프레임속도
        transform.position += new Vector3(0, -1, 0) * (speed * Time.deltaTime);   
    }

    private void AttackCheak() // 현재 슬라임이 플레이어 보다 점수가 높다면 공격
    {
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (PlayerSlime.playerHp <= enemyScore)
                {
                    animator.Play("Attack");
                }
            }
        }
    }
    
    private void DestroySlime()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            DestroySlime();
        }

        if (collision.CompareTag("Player"))
        {
            speed = 0;
            animator.Play("Dead");
        }
    }
}
