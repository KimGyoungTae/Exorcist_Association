// �� �ʿ� ��ȣ�ۿ� �� ������ �� ���Ŀ�� ���콺 �����Ͱ� ����� �� �̹��� ��ȭ�� ���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour
{
    [Header("��� ����� ������Ʈ ����")]
    public int id;
    public bool isNPC;

    [Header("���콺 ���ͷ��� �̹���")]
    public Sprite changeSprite;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    public static PointObject instance;



    private void Awake()
    {
        instance = this;
    }

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
