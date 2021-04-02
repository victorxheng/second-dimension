using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeRenderBG : MonoBehaviour { // basically height * screen aspect ratio

    public SpriteRenderer spriteRenderer;
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        Sprite s = spriteRenderer.sprite;
        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
    }
}
