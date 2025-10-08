using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    public static ScrollViewController instance;
    
    [SerializeField] private float cellHeight;
    [SerializeField] private GameObject useSkinInfo;
    [SerializeField] private RectTransform useSkinInfoPos;
    
    

    [SerializeField] private GameObject SkinUI;
    
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;

    private List<Portrait> _portraits; // Cell에 표시할 Item 정보
    private LinkedList<Cell> _visibleCells; // 화면에 표시되고 있는 Cell 정보

    private bool secondLock;
    private float _lastYValue = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _scrollRect = GetComponent<ScrollRect>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {
        FirstLoadData();
    }
    
    
    /// <summary>
    /// 모든 스크롤뷰 콘텐츠를 지우는 메서드
    /// </summary>
    public void ClearAllContent()
    {
        // portraits 리스트를 초기화
        if (_portraits != null)
        {
            _portraits.Clear();
        }

        // 표시된 모든 Cell들을 오브젝트 풀에 반환
        if (_visibleCells != null)
        {
            foreach (var cell in _visibleCells)
            {
                if (cell != null) // 혹시 null이 있을 경우를 대비하여 체크
                {
                    ObjectPool.Instance.ReturnObject(cell.gameObject);
                }
            }
            _visibleCells.Clear(); // 링크드 리스트도 비웁니다.
        }
        else
        {
            _visibleCells = new LinkedList<Cell>(); // null일 경우 초기화
        }

        // 스크롤 위치를 초기화
        if (_scrollRect != null && _scrollRect.content != null)
        {
            _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, 0);
            _scrollRect.content.anchoredPosition = Vector2.zero; // 스크롤 위치 초기화
        }
        
        _lastYValue = 1f;
    }

    /// <summary>
    /// 현재 보여질 Cell 인덱스를 반환하는 메서드
    /// </summary>
    /// <returns>startIndex: 가장 위에 표시될 Cell 인덱스, endIndex: 가장 아래에 표시될 Cell Index</returns>
    private (int startIndex, int endIndex) GetVisibleIndexRange()
    {
        var visibleRect = new Rect(
            _scrollRect.content.anchoredPosition.x,
            _scrollRect.content.anchoredPosition.y,
            _rectTransform.rect.width,
            _rectTransform.rect.height);

        // 스크롤 위치에 따른 시작 인덱스 계산
        var startIndex = Mathf.FloorToInt(visibleRect.y / cellHeight);

        // 화면에 보이게 될 Cell 개수 계산
        int visibleCount = Mathf.CeilToInt(visibleRect.height / cellHeight);

        // 버퍼 추가
        startIndex = Mathf.Max(0, startIndex - 1); // startIndex가 0보다 크면 startIndex - 1, 아니면 0
        visibleCount += 2;

        return (startIndex, startIndex + visibleCount - 1);
    }

    /// <summary>
    /// 특정 인덱스가 화면에 보여야 하는지 여부를 판단하는 메서드
    /// </summary>
    /// <param name="index">특정 인덱스</param>
    /// <returns>true, false</returns>
    private bool IsVisibleIndex(int index)
    {
        var (startIndex, endIndex) = GetVisibleIndexRange();
        endIndex = Mathf.Min(endIndex, _portraits.Count - 1);
        return startIndex <= index && index <= endIndex;
    }

    /// <summary>
    /// _items에 있는 값을 Scroll View에 표시하는 함수
    /// _items에 새로운 값이 추가되거나 기존 값이 삭제되면 호출됨
    /// </summary>
    private void ReloadData()
    {
        // _visibleCell 초기화
        _visibleCells = new LinkedList<Cell>();

        // Content의 높이를 _items의 데이터의 수만큼 계산해서 높이를 지정
        var contentSizeDelta = _scrollRect.content.sizeDelta;
        contentSizeDelta.y = _portraits.Count * cellHeight;
        _scrollRect.content.sizeDelta = contentSizeDelta;

        // 화면에 보이는 영역에 Cell 추가
        var (startIndex, endIndex) = GetVisibleIndexRange();
        var maxEndIndex = Mathf.Min(endIndex, _portraits.Count - 1);
        for (int i = startIndex; i < maxEndIndex; i++)
        {
            // 셀 만들기
            var cellObject = ObjectPool.Instance.GetObject();
            var cell = cellObject.GetComponent<Cell>();
            cell.SetItem(_portraits[i], i, _portraits[i].isHave);
            cell.transform.localPosition = new Vector3(0, -i * cellHeight, 0);

            _visibleCells.AddLast(cell);
        }
    }

    private void FirstLoadData()
    {
        secondLock = false;
        ClearAllContent();
        _portraits = new List<Portrait>();
        foreach (SkinData portrait in SkinManager.instance.portraits)
        {
            if (portrait.index % 3 == 1)
            {
                string story;
                if (portrait.isHave)
                {
                    story = portrait.story2;
                }
                else
                {
                    string[] splitStory = portrait.story1.Split('`');
                    string tem = "";
                    switch (portrait.index / 3)
                    {
                        case 0:
                            tem = $"{PlayerManager.instance.PlayerData.eatFruit}/{portrait.limitNum}";
                            break;
                        case 1:
                            tem = $"{PlayerManager.instance.PlayerData.playTime1}/{portrait.limitNum}";
                            break;
                        case 2:
                            tem = $"{PlayerManager.instance.PlayerData.playTime2}/{portrait.limitNum}";
                            break;
                        case 3:
                            tem = $"{PlayerManager.instance.PlayerData.sideTouch}/{portrait.limitNum}";
                            break;
                        case 4:
                            tem = $"{PlayerManager.instance.PlayerData.killSlime}/{portrait.limitNum}";
                            break;
                        case 5:
                            tem = $"{PlayerManager.instance.PlayerData.turnStage}/{portrait.limitNum}";
                            break;
                    }

                    story = splitStory[0] + tem + splitStory[1];
                }
                
                Portrait setPortrait = new Portrait
                {
                    index = portrait.index,
                    imageFileName = portrait.ImageFile.name,
                    jobName = portrait.jobName,
                    isHave = portrait.isHave,
                    subtitle = story,
                };
                _portraits.Add(setPortrait);
            }
            
        }
        ReloadData();
    }

    public void SecondLoadData(int index)
    {
        SkinData[] portraits = SkinManager.instance.portraits;
        if (secondLock)
        {
            AnimatorManager.Instance.ChangeAnimator(index + 1);
            GameObject skinInfo = Instantiate(useSkinInfo, SkinUI.transform);
            var skinText = skinInfo.GetComponent<TextMeshProUGUI>();
            skinText.text = portraits[index].jobName + " 선택됨.";
            var rect = skinInfo.GetComponent<RectTransform>();
            rect.anchoredPosition = useSkinInfoPos.anchoredPosition;
            rect.DOAnchorPosY(rect.anchoredPosition.y - 15f, 1f).SetEase(Ease.OutQuad).SetDelay(0.7f);
            skinText.DOFade(0f, 1f).SetDelay(0.7f).OnComplete(() =>
            {
                Destroy(skinInfo);
            });
            return;
        }
        
        ClearAllContent();
        _portraits = new List<Portrait>();

        for (int i = index; i < index+3; i++)
        {
            string story;
            if (portraits[i].isHave)
            {
                story = portraits[i].story2;
            }
            else
            {
                string[] splitStory = portraits[i].story1.Split('`');
                string tem = "";
                switch (i / 3)
                {
                    case 0:
                        tem = $"{PlayerManager.instance.PlayerData.eatFruit}/{portraits[i].limitNum}";
                        break;
                    case 1:
                        tem = $"{PlayerManager.instance.PlayerData.playTime1}/{portraits[i].limitNum}";
                        break;
                    case 2:
                        tem = $"{PlayerManager.instance.PlayerData.playTime2}/{portraits[i].limitNum}";
                        break;
                    case 3:
                        tem = $"{PlayerManager.instance.PlayerData.sideTouch}/{portraits[i].limitNum}";
                        break;
                    case 4:
                        tem = $"{PlayerManager.instance.PlayerData.killSlime}/{portraits[i].limitNum}";
                        break;
                    case 5:
                        tem = $"{PlayerManager.instance.PlayerData.turnStage}/{portraits[i].limitNum}";
                        break;
                }

                story = splitStory[0] + tem + splitStory[1];
            }
            Portrait setPortrait = new Portrait
            {
                index = portraits[i].index,
                imageFileName = portraits[i].ImageFile.name,
                jobName = portraits[i].jobName,
                isHave = portraits[i].isHave,
                subtitle = story
            };
            _portraits.Add(setPortrait);
        }
        
        ReloadData();
        secondLock = true;
    }

    public void ExitOnclick()
    {
        if (secondLock)
        {
            FirstLoadData();
        }
        else
        {
            SkinUI.SetActive(false);
        }
        
    }

    #region Scroll Rect Events

    public void OnValueChanged(Vector2 value)
    {
        if (_lastYValue < value.y)
        {
            ////////////////////////////////////////
            // 위로 스크롤

            // 1. 상단에 새로운 셀이 필요한지 확인 후 필요하면 추가
            var firstCell = _visibleCells.First.Value;
            var newFirstIndex = firstCell.CellIndex - 1;

            if (IsVisibleIndex(newFirstIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetItem(_portraits[newFirstIndex], newFirstIndex, _portraits[newFirstIndex].isHave);
                cell.transform.localPosition = new Vector3(0, -newFirstIndex * cellHeight, 0);
                _visibleCells.AddFirst(cell);
            }

            // 2. 하단에 있는 셀이 화면에서 벗어나면 제거
            var lastCell = _visibleCells.Last.Value;

            if (!IsVisibleIndex(lastCell.CellIndex))
            {
                ObjectPool.Instance.ReturnObject(lastCell.gameObject);
                _visibleCells.RemoveLast();
            }
        }
        else
        {
            ////////////////////////////////////////
            // 아래로 스크롤

            // 1. 하단에 새로운 셀이 필요한지 확인 후 필요하면 추가
            var lastCell = _visibleCells.Last.Value;
            var newLastIndex = lastCell.CellIndex + 1;

            if (IsVisibleIndex(newLastIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetItem(_portraits[newLastIndex], newLastIndex, _portraits[newLastIndex].isHave);
                cell.transform.localPosition = new Vector3(0, -newLastIndex * cellHeight, 0);
                _visibleCells.AddLast(cell);
            }

            // 2. 상단에 있는 셀이 화면에서 벗어나면 제거
            var firstCell = _visibleCells.First.Value;

            if (!IsVisibleIndex(firstCell.CellIndex))
            {
                ObjectPool.Instance.ReturnObject(firstCell.gameObject);
                _visibleCells.RemoveFirst();
            }
        }

        _lastYValue = value.y;
    }

    #endregion
}
