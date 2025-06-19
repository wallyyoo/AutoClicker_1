using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 클릭 입력을 감지하고, 클릭 위치를 이벤트로 전달하는 매니저
/// </summary>
public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }
    public static System.Action<Vector3> OnClick;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // 선택적: 씬 이동 시 유지하고 싶다면
    }

    void Update()
    {
        //마우스 왼쪽 버튼 클릭했고, UI 위가 아닌 경우에만 클릭으로 인정
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            //마우스 클릭 위치를 월드 좌표로 변환
            //z = 0으로 평면상의 위치로 보정
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;

            //클릭 위치를 구독 중인 다른 스크립트들에게 전달
            //?.는 null 체크(구독자가 있을 때만 호출)
            OnClick?.Invoke(clickPosition);//Invoke()는 “등록된 함수들을 실행해줘!” 
            
        }
    }

    bool IsPointerOverUI()
    {
        //마우스가 UI 위에 있는지 체크하는 함수
        //UI 클릭은 게임 오브젝트 클릭과 구분하기 위함
        return EventSystem.current.IsPointerOverGameObject();
    }
}