using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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