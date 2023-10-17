// 이 코드 어디에 쓰이는지 파악하고, 쓸데 없으면 지워버리기

using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ObjData : MonoBehaviour

{
    [Header("대사 출력할 오브젝트 정보")]
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
        // 처음 크기 저장한다.
       defaultScale = transform.localScale;

    }

    public void ScaleUP()
    {
        transform.localScale = defaultScale * 1.2f;
    }

    public void ScaleDOWN()
    {
        // 처음 크기로 되돌아가기
        transform.localScale = defaultScale;
    }
}
