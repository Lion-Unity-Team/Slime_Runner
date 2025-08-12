using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime_Movement : MonoBehaviour
{
    // 레인 위치 (0=왼, 1=중앙, 2=오)
    private Animator _Anime;
    private int currentLane = 1;
    private float laneDistance = 2f; // 레인간 거리
    [SerializeField] private float moveDuration = 0.2f; //이동 시간(초
    public bool canMove = true;
    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;

//#if UNITY_EDITOR || UNITY_WEBGL
    //private Vector2 mousePos; // 기존 마우스포스
//#endif

//#if UNITY_ANDROID || UNITY_IOS
    private Vector2 touchPos;
//#endif

    private void Start()
    {
        _Anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (UI1.activeSelf || UI2.activeSelf || UI3.activeSelf)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (!canMove)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            PlayerMove(touchPos);
        }

        //if (Input.touchCount > 0)
        //{
        //    if (IsPointerOverUI())
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        Touch touch = Input.GetTouch(0);

        //        if (touch.phase == TouchPhase.Began)
        //        {
        //            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //            PlayerMove(touchPos);
        //            //MoveSlime(touchPos);
        //        }
        //    }
        //}
    }

    private bool IsPointerOverUI()
    {
        if(Input.touchCount>0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return false;
    }

    private void PlayerMove(Vector2 position)
    {
        // 화면 왼쪽 절반 클릭 -> 왼쪽 이동
        if (position.x < 0)
        {
            if(currentLane>0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                _Anime.SetTrigger("Move");
                currentLane--;
                MoveToLane(currentLane);
            }
        }
        // 화면 오른쪽 절반 클릭 -> 오른쪽 이동
        else
        {
            if(currentLane<2)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                _Anime.SetTrigger("Move");
                currentLane++;
                MoveToLane(currentLane);
            }
        }
    }

    private void MoveToLane(int laneIndex)
    {
        Vector2 targetPos = new Vector2((laneIndex - 1) * laneDistance, -6);
        StopAllCoroutines(); // 이동 중 다시 이동하면 이전 코루틴 중단
        StartCoroutine(SlideToPosition(targetPos, moveDuration));
        SoundManager.instance.SfxPlay("Move");
    }

    private IEnumerator SlideToPosition(Vector2 targetPos, float duration)
    {
        Vector2 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos; //마지막 위치 보정
    }

    //private void OnTriggerEnter2D(Collider2D collision) //플레이어가 적과 부딫혔을때 딜레이를 거는 함수
    //{
    //    StartCoroutine(EnableMovement(0.5f));
    //}

    //IEnumerator EnableMovement(float delay) // 딜레이 코루틴
    //{
    //    canMove = false;
    //    yield return new WaitForSeconds(delay);
    //    canMove = true;
    //}
}
