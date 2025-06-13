using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLooper : MonoBehaviour
{
    // 배경이 왼쪽으로 움직이는 속도 (단위: 초당 거리)
    public float moveSpeed = 2f;

    // 현재 스프라이트의 가로 길이 (바깥으로 나갔는지 판단용)
    private float spriteWidth;

    void Start()
    {
        // 이 오브젝트에 붙은 SpriteRenderer 컴포넌트를 가져옴
        Renderer _spriteWidth = GetComponent<Renderer>();
        
        

        // 해당 스프라이트의 실제 가로 크기 (픽셀 → 월드 단위 변환된 값)
        spriteWidth = _spriteWidth.bounds.size.x;
    }

    void Update()
    {
        // 매 프레임마다 왼쪽으로 이동
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // 만약 배경이 화면 왼쪽 바깥까지 나갔다면
        // (X 위치가 -spriteWidth보다 작다면 = 완전히 안 보이게 됐다면)
        if (transform.position.x < -spriteWidth)
        {
            // spriteWidth * 2 만큼 오른쪽으로 위치 이동
            // → 다음 배경 뒤로 자연스럽게 연결됨
            transform.position += new Vector3(spriteWidth * 1.99f, 0f, 0f);
        }
    }
}
