using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;


public class EstateDialogue : TextDialogSystem
{
    [Space]
    [Header("캐릭터 가져오기")]
    public GameObject KHS;
    public GameObject CJW;
    public GameObject EstateNPC;
    public GameObject NameTag;

    [Space]
    [Header("캐릭터 대화 상태 가져오기")]
    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] CJWOnOffSprites;
    public Sprite[] EstateNPCOnOffSprites;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOnOff;
    private bool CJWStateOnOff;
    private bool EstateNPCOnOff;

    [Space]
    [Header("백그라운드 오브젝트 가져오기")]
    public GameObject BackGroundContract;
    public GameObject BackGroundLeaflet;

    private bool skipAction = false;

    private void Start()
    {
        VisitManager.instance.VisitScene();

        // 대화가 시작되지 않았음을 표시
        dialogueStarted = false;
        BackGroundObjectOff(false);

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

        // Skip 버튼 클릭 이후 코루틴이 한 번 실행이 되면서 이후 데이터가 Text에 남아있는 현상이 발생
        // 버튼 클릭 여부를 판단하여 만약 Skip 버튼을 클릭 했다면
        // 실행되어 있는 코루틴을 모두 종료하면서, 데이터가 Text에 갱신되는 문제를 방지함.
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
                            //   Debug.Log(dialogues[lineCount].name);

                            ChangeCharactoreImage();
                            ShowDialogueName();
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            dialogueStarted = false;

                            // 아이템 오브젝트 상호작용도 SetActive(false)로 만들기
                            BackGroundObjectOff(false);
                        }
                    }
                }
            }
        }
    }



    public override void ChangeCharacterUI(string tag, string name)
    {
        if (tag == "Item")
        {
            CharactorOff(false);

            // Name에 따라 캐릭터 & 백그라운드 이미지 보이게 할 지 유무 판단 이어가기..
            if (name == "재개발")
            {
                BackGroundContract.SetActive(true);
                HitCheckObject(hit);
            }

            else if (name == "신축 아파트")
            {
                BackGroundLeaflet.SetActive(true);
                HitCheckObject(hit);
            }

            else Debug.Log("백그라운드 오브젝트가 존재하지 않습니다..");
        }

        if (tag == "NPC")
        {
            CharactorOff(isDialogue);
        }
    }



    void HitCheckObject(RaycastHit2D targeted)
    {
        ItemSaves clickInterface = targeted.transform.GetComponent<ItemSaves>();

        if (clickInterface != null)
        {
            InventoryItem item = clickInterface.ClickItem();
            InventoryManager.Instance.Add(item);
            //target = null;
            //currenttarget = null;
        }
    }

    void BackGroundObjectOff(bool isAction)
    {
        BackGroundContract.SetActive(isAction);
        BackGroundLeaflet.SetActive(isAction);
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        CJW.SetActive(isAction);
        EstateNPC.SetActive(isAction);
        NameTag.SetActive(isAction);
    }

    // 현재 대화 하는 사람의 이름을 파악하여 알맞은 상태변화로 이어지게 함.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "부동산 업자")
        {
            ManageChangeState(false, false, true);
        }

        else if (dialogues[lineCount].name == "차지원")
        {
            ManageChangeState(false, true, false);
        }

        else if (dialogues[lineCount].name == "강혜성")
        {
            ManageChangeState(true, false, false);
        }
        else Debug.Log("캐릭터 상태 변화를 등록하지 않았습니다");
    }

    void ManageChangeState(bool parm_KHSStateOnOff, bool parm_CJWStateOnOff, bool parm_EstateNPCOnOff)
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOnOff = parm_KHSStateOnOff;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        // 차지원의 상태를 변경합니다.
        CJWStateOnOff = parm_CJWStateOnOff;
        Sprite CJWSprite = CJWOnOffSprites[CJWStateOnOff ? 0 : 1];
        CJW.GetComponent<SpriteRenderer>().sprite = CJWSprite;

        EstateNPCOnOff = parm_EstateNPCOnOff;
        Sprite NPCSprite = EstateNPCOnOffSprites[EstateNPCOnOff ? 0 : 1];
        EstateNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }

    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        ManageChangeState(false, false, true);
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;

        // 아이템 오브젝트 상호작용도 SetActive(false)로 만들기
        BackGroundObjectOff(false);
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
