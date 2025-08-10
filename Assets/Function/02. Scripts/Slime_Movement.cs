using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime_Movement : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector2 mousePos; // 기존 마우스포스
#endif
#if UNITY_ANDROID || UNITY_IOS
    private Vector2 touchPos;
#endif
    public bool canMove=true;

    void Update()
    {
        if (!canMove) return; //UI켜져있을때 클릭 무시
#if UNITY_EDITOR    // 유니티에디터에서만 실행
        if (EventSystem.current.IsPointerOverGameObject()) return; //UI위 클릭은 이동무시

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveSlime(mousePos);
        } // 기존 마우스 클릭 코드 
#endif
#if UNITY_ANDROID || UNITY_IOS  // 모바일에서만 실행
        if (IsPointerOverUI()) return; // 모바일 UI위 터치 이동무시

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                MoveSlime(touchPos);
            }
        }
#endif
    }

#if UNITY_ANDROID || UNITY_IOS  // 모바일에서만 실행
    private bool IsPointerOverUI()
    {
        if(Input.touchCount>0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return false;
    }
#endif

    private void MoveSlime(Vector2 position)    // 슬라임 움직임 함수
    {
        if (position.x > 1)
            transform.position = new Vector2(2, -6);
        else if (position.x < -1)
        {
            transform.position = new Vector2(-2, -6);
        }
        else
            transform.position = new Vector2(0, -6);

        SoundManager.instance.SfxPlay("Move");
    }

    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 적과 부딫혔을때 딜레이를 거는 함수
    {
        StartCoroutine(EnableMovement(0.5f));
    }

    IEnumerator EnableMovement(float delay) // 딜레이 코루틴
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
