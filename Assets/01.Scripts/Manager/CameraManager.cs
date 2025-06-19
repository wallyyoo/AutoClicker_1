
using UnityEngine;


/// <summary>
/// 배경 스프라이트를 카메라 크기에 맞게 자동 조정하는 스크립트
/// </summary>
public class FitBackgroundToCamera : MonoBehaviour
{
    [SerializeField] private float widthMultiplier = 1f;
    [SerializeField] private float heightMultiplier = 1f;

    private void Start()
    {
        FitToCamera();
    }

    void FitToCamera()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        transform.localScale = new Vector3(
            (width / spriteWidth) * widthMultiplier,
            (height / spriteHeight) * heightMultiplier,
            1f
        );
    }
}