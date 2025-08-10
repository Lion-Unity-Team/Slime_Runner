using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime_Movement : MonoBehaviour
{
    private Vector2 mousePos; // 기존 마우스포스
    private Vector2 touchPos;
    public bool canMove=true;

    private void OnEnable()
    {
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return; //UI켜져있을때 클릭 무시

        if (EventSystem.current.IsPointerOverGameObject()) return; //UI위 클릭은 이동무시 모바일에서 테스트시 주석처리필수

        if (IsPointerOverUI()) return; // 모바일 UI위 터치 이동무시

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveSlime(mousePos);

            SoundManager.instance.SfxPlay("Move");
        } // 기존 마우스 클릭 코드 모바일환경에서 테스트시 주석처리필수

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                MoveSlime(touchPos);
            }
        }//모바일환경에서 테스트 필요
    }

    private bool IsPointerOverUI()
    {
        if(Input.touchCount>0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return false;
    }
    

    private void MoveSlime(Vector2 position)
    {
        if (position.x > 1)
            transform.position = new Vector2(2, -4);
        else if (position.x < -1)
        {
            transform.position = new Vector2(-2, -4);
        }
        else
            transform.position = new Vector2(0, -4);

        SoundManager.instance.SfxPlay("Move");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(EnableMovement(0.5f));
    }

    IEnumerator EnableMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
