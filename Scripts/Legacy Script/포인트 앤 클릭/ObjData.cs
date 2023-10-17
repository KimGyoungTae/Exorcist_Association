// �� �ڵ� ��� ���̴��� �ľ��ϰ�, ���� ������ ����������

using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ObjData : MonoBehaviour

{
    [Header("��� ����� ������Ʈ ����")]
    public int id;
    public bool isNPC;

    Vector3 defaultScale;

    public static ObjData instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ó�� ũ�� �����Ѵ�.
       defaultScale = transform.localScale;

    }

    public void ScaleUP()
    {
        transform.localScale = defaultScale * 1.2f;
    }

    public void ScaleDOWN()
    {
        // ó�� ũ��� �ǵ��ư���
        transform.localScale = defaultScale;
    }
}
