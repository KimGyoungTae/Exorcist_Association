using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreetDialogue : TextDialogSystem
{

    [Space]
    [Header("캐릭터 가져오기")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject HWoo;
    public GameObject AcademyNPC;
    public GameObject CafeNPC;
    public GameObject FlowerNPC;

    public GameObject AcademyTag;
    public GameObject CafeTag;
    public GameObject FlowerTag;

    [Space]
    [Header("캐릭터 대화 상태 가져오기")]
    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] HWooOnOffSprites;
    public Sprite[] AcademyNPCOnOffSprites;
    public Sprite[] CafeNPCOnOffSprites;
    public Sprite[] FlowerNPCOnOffSprites;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOnOff;
    private bool CJWStateOnOff;
    private bool HWooStateOnOff;
    private bool AcademyNPCOnOff;
    private bool CafeNPCOnOff;
    private bool FlowerNPCOnOff;

    private bool skipAction = false;

   

    void Start()
    {
        VisitManager.instance.VisitScene();

        // 대화가 시작되지 않았음을 표시
        dialogueStarted = false;

        if (SkipButton != null)
        {
            // SKip 버튼 클릭 이벤트 추가
            SkipButton.onClick.AddListener(ClickSkipButton);
        }
    }


    void Update()
    {
        CheckDialogueState();

        // 대화 진행도를 체크하는 부분도 Update()에서 처리합니다.
        DiaAction();

        if (skipAction)
        {
            //  Debug.Log("스킵 발동");
            StopAllCoroutines();
            skipAction = false;
            return;
        }
    }

    void DiaAction()
    {
        if (isDialogue) // 대화중 이면서
        {
            if (isNext) // 다음 키 입력이 가능할 때
            {
                if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 누를 때
                {

                    isNext = false;
                    TalkText.text = "";


                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {

                        // 대사 카운트 초기화
                        contextCount = 0;

                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                            Debug.Log(dialogues[lineCount].name);

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            dialogueStarted = false;
                            //  afterInteraction = true;
                            //  startInteraction = false;
                        }
                    }
                }
            }
        }
    }

    public override void ChangeCharacterUI(string tag, string name)
    {
        if (tag == "NPC")
        {
            if (name == "영어학원")
            {
                KHS.SetActive(isDialogue);
                CJW.SetActive(isDialogue);
                AcademyNPC.SetActive(isDialogue);
                AcademyTag.SetActive(isDialogue);
            }

            else if (name == "카페")
            {
                KHS.SetActive(isDialogue);
                CJW.SetActive(isDialogue);
                HWoo.SetActive(isDialogue);
                CafeNPC.SetActive(isDialogue);
                CafeTag.SetActive(isDialogue);
            }

            else if (name == "꽃집")
            {
                KHS.SetActive(isDialogue);
                FlowerNPC.SetActive(isDialogue);
                FlowerTag.SetActive(isDialogue);
            }

            else Debug.Log("오브젝트 이름이 매칭이 잘못되었습니다..");
        }

        else Debug.Log("오브젝트 태그를 찾을 수 없습니다..");
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        HWoo.SetActive(isAction);

        AcademyNPC.SetActive(isAction);
        CafeNPC.SetActive(isAction);
        FlowerNPC.SetActive(isAction);

        AcademyTag.SetActive(isAction);
        CafeTag.SetActive(isAction);
        FlowerTag.SetActive(isAction);
    }

    // 현재 대화 하는 사람의 이름을 파악하여 알맞은 상태변화로 이어지게 함.
    void ChangeCharactoreImage()
    {
        if(hit.collider.name == "영어학원")
        {
            if (dialogues[lineCount].name == "강사")
            {
                // 강햬성의 상태를 변경합니다.
                KHSStateOnOff = false;
                // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                // 차지원의 상태를 변경합니다.
                CJWStateOnOff = false;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                AcademyNPCOnOff = true;
                Sprite NPCSprite = AcademyNPCOnOffSprites[AcademyNPCOnOff ? 0 : 1];
                AcademyNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else if (dialogues[lineCount].name == "차지원")
            {
                // 강햬성의 상태를 변경합니다.
                KHSStateOnOff = false;
                // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                // 차지원의 상태를 변경합니다.
                CJWStateOnOff = true;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                AcademyNPCOnOff = false;
                Sprite NPCSprite = AcademyNPCOnOffSprites[AcademyNPCOnOff ? 0 : 1];
                AcademyNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else if (dialogues[lineCount].name == "강혜성")
            {
                
                // 강햬성의 상태를 변경합니다.
                KHSStateOnOff = true;
                // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                // 차지원의 상태를 변경합니다.
                CJWStateOnOff = false;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                AcademyNPCOnOff = false;
                Sprite NPCSprite = AcademyNPCOnOffSprites[AcademyNPCOnOff ? 0 : 1];
                AcademyNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else
            {
                // 중간에 나레이션 대사인 상황..
                KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
                AcademyNPC.GetComponent<SpriteRenderer>().sprite = AcademyNPCOnOffSprites[1];
            }

        }

        else if (hit.collider.name == "카페")
        {
            if (dialogues[lineCount].name == "카페 주인")
            {
                KHSStateOnOff = false;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                CJWStateOnOff = false;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                HWooStateOnOff = false;
                Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
                HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;

                CafeNPCOnOff = true;
                Sprite NPCSprite = CafeNPCOnOffSprites[CafeNPCOnOff ? 0 : 1];
                CafeNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;

            }
            else if (dialogues[lineCount].name == "현우")
            {
                KHSStateOnOff = false;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                CJWStateOnOff = false;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                HWooStateOnOff = true;
                Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
                HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;

                CafeNPCOnOff = false;
                Sprite NPCSprite = CafeNPCOnOffSprites[CafeNPCOnOff ? 0 : 1];
                CafeNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else if (dialogues[lineCount].name == "차지원")
            {
                KHSStateOnOff = false;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                CJWStateOnOff = true;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                HWooStateOnOff = false;
                Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
                HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;

                CafeNPCOnOff = false;
                Sprite NPCSprite = CafeNPCOnOffSprites[CafeNPCOnOff ? 0 : 1];
                CafeNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else if (dialogues[lineCount].name == "강혜성")
            {
                KHSStateOnOff = true;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                CJWStateOnOff = false;
                Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
                CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

                HWooStateOnOff = false;
                Sprite HWooSprite = HWooOnOffSprites[HWooStateOnOff ? 0 : 1];
                HWoo.GetComponent<SpriteRenderer>().sprite = HWooSprite;

                CafeNPCOnOff = false;
                Sprite NPCSprite = CafeNPCOnOffSprites[CafeNPCOnOff ? 0 : 1];
                CafeNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else
            {
                // 나레이션 대사..
            }
        }


        else if (hit.collider.name == "꽃집")
        {
            if (dialogues[lineCount].name == "꽃집 주인")
            {
                KHSStateOnOff = false;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                FlowerNPCOnOff = true;
                Sprite NPCSprite = FlowerNPCOnOffSprites[FlowerNPCOnOff ? 0 : 1];
                FlowerNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else if (dialogues[lineCount].name == "강혜성")
            {
                KHSStateOnOff = true;
                Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
                KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

                FlowerNPCOnOff = false;
                Sprite NPCSprite = FlowerNPCOnOffSprites[FlowerNPCOnOff ? 0 : 1];
                FlowerNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
            }

            else
            {
                // 나레이션 대사..
            }
        }

        else Debug.Log("캐릭터 상태 변화가 이루어지지 않습니다.");

    }

    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        if (hit.collider.name == "영어학원")
        {
            KHSStateOnOff = false;
            CJWStateOnOff = true;
            AcademyNPCOnOff = false;

            // 초기 캐릭터 이미지 설정
            KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[0];
            AcademyNPC.GetComponent<SpriteRenderer>().sprite = AcademyNPCOnOffSprites[1];
        }

        else if (hit.collider.name == "카페")
        {
            KHSStateOnOff = false;
            CJWStateOnOff = false;
            CafeNPCOnOff = false;
            HWooStateOnOff = false;

            // 초기 캐릭터 이미지 설정
            KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
            CJW.GetComponent<SpriteRenderer>().sprite = CJWOnOffSprites[1];
            CafeNPC.GetComponent<SpriteRenderer>().sprite = CafeNPCOnOffSprites[1];
            HWoo.GetComponent<SpriteRenderer>().sprite = HWooOnOffSprites[1];
        }

        else if (hit.collider.name == "꽃집")
        {
            KHSStateOnOff = false;
            FlowerNPCOnOff = false;

            // 초기 캐릭터 이미지 설정
            KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[1];
            FlowerNPC.GetComponent<SpriteRenderer>().sprite = FlowerNPCOnOffSprites[1];
        }

        else Debug.Log("콜라이더 이름을 찾을 수 없습니다");
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;
    }

    public override void CheckDialogueState()
    {
        base.CheckDialogueState();
    }

    public override void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        base.ShowDialogue(Parm_dialogues);
        InitializeCharacterState();
    }

    public override void EndDialogue()
    {
        base.EndDialogue();
        CharactorOff(false);
    }

    public override void ShowDialogueName()
    {
        base.ShowDialogueName();
    }

    public override IEnumerator TypeWriter()
    {
        return base.TypeWriter();
    }

    public override void SettingUI(bool isAction)
    {
        base.SettingUI(isAction);
    }

}
