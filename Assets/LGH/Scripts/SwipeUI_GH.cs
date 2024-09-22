using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI_GH : MonoBehaviour
{
    [SerializeField]
    Scrollbar scrollBar;
    [SerializeField]
    float swipeTime = 0.2f;
    [SerializeField]
    float swipeDistance = 50f;

    private float[] scrollPageValues;
    float valueDistance = 0;
    public int currentPage = 0;
    public int maxPage = 0;
    float startTouchX;
    float endTouchX;
    bool isSwipeMode = false;

    //페이지 번호 넘기기
    public TextMeshProUGUI scrollPagecNum;

    //메모매니저 갖고오기
    public MemoManager_GH memoManag;

    private void Awake()
    {
        
        // 스크롤 되는 페이지의 각 value값을 지정하는 배열 메모리 할당
        scrollPageValues = new float[memoManag.memoCount];

        // 스크롤 되는 페이지 사이의 거리
        valueDistance = 1f / (scrollPageValues.Length - 1f);

        // 스크롤 되는 페이지의 각 value 위치 설 정 [0 <= value <= 1]
        for (int i = 0; i < scrollPageValues.Length; ++i)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        //maxPage = memoManag.memoCount;
    }

    void Start()
    {
        // 최초 시작할 때 0번 페이지를 볼 수 있도록 설정
        //SetScrollBarValue(0);
    }

    public void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }


    void Update()
    {
        //최대 페이지의  수
        maxPage = memoManag.memoCount;

        UpdateInput();
        UpdatePageNum();
    }

    void UpdateInput()
    {
        // 현재 Swipe를 진행중이면 터치 불가
        if (isSwipeMode == true) return;
        //전처리기 #if - #endif 프로그램 실행 전에 #if 조건에 만족하는 코드는 활성화하고, 조건에 만족하지 않는 코드는 비활성화해서 아예 실행되지 않는다. UNITY_EDITOR : 현재 플레이 환경이 에디터일 때

#if UNITY_EDITOR
        // 마우스 왼쪽 버튼을 눌렀을 때 1회
        if (Input.GetMouseButtonDown(0))
        {
            //터치 시작 지점 (swipe 방향 구분)
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //터치 종료 지점 (swipe 방향 구분)
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif
    }

    void UpdateSwipe()
    {
        // 너무 작은 거리를 움직였을 때는 Swipe X
        if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            // 원래 페이지로 Swipe해서 돌아간다
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        // swipe 방향
        bool isLeft = startTouchX < endTouchX ? true : false;

        // 이동방향이 왼쪽일 때
        if (isLeft == true)
        {
            // 현재 페이지가 왼쪽 끝이면 종료
            if (currentPage == 0) return;

            // 왼쪽으로 이동을 위해 현재 페이지들 1 감소
            currentPage--;
        }
        // 이동 방향이 오른쪽일 때
        else
        {
            // 현재 페이지가 오른쪽 끝이면 종료
            if (currentPage == maxPage - 1) return;

            //오른쪽으로 이동을 위해 현재 페이지를 1 증가
            currentPage++;
        }
        // currentIndex번째 페이지로 Swipe해서 이동
        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    void UpdatePageNum()
    {
        scrollPagecNum.text = $"{currentPage + 1}/{maxPage}";
    }

    IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

            yield return null;
        }

        isSwipeMode = false;
    }

    public void memoPageUpdate(int pageCount)
    {
        // 스크롤 되는 페이지의 각 value값을 지정하는 배열 메모리 할당
        scrollPageValues = new float[pageCount];

        // 스크롤 되는 페이지 사이의 거리
        valueDistance = 1f / (scrollPageValues.Length - 1f);

        // 스크롤 되는 페이지의 각 value 위치 설 정 [0 <= value <= 1]
        for (int i = 0; i < scrollPageValues.Length; ++i)
        {
            scrollPageValues[i] = valueDistance * i;
        }

        //최대 페이지의  수
        maxPage = pageCount;

    }


}
