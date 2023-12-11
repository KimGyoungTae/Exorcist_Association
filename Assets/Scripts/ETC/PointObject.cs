using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� �ʿ� ��ȣ�ۿ� �� ������ �� ���Ŀ�� ���콺 �����Ͱ� ����� �� �̹��� ��ȭ�� ���
/// </summary>
public class PointObject : MonoBehaviour
{
    
    [Header("���콺 ���ͷ��� �̹���")]
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
        // ���� Sprite�� ����
        spriteRenderer.sprite = originalSprite;
    }


}
