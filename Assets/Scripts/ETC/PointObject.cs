using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 각 맵에 상호작용 할 아이템 및 브로커들 마우스 포인터가 닿았을 때 이미지 변화를 담당
/// </summary>
public class PointObject : MonoBehaviour
{
    
    [Header("마우스 인터랙션 이미지")]
    public Sprite changeSprite;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

   

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


        if(changeSprite != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    public void ChangeState()
    {
        spriteRenderer.sprite = changeSprite;
    }

    public void ResetState()
    {
        // 원래 Sprite로 복원
        spriteRenderer.sprite = originalSprite;
    }


}
