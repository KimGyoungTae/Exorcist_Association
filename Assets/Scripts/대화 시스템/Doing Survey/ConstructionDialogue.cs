using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionDialogue : TextDialogSystem
{
    [Space]
    [Header("캐릭터 가져오기")]
    public GameObject KHS;
    public GameObject ConstructionNPC;
    public GameObject NameTag;

    [Space]
    [Header("캐릭터 대화 상태 가져오기")]
    // 각각의 캐릭터들의 On상태 / OFF 상태 이미지를 순서대로 저장한 배열
    public Sprite[] KHSOnOffSprites;
    public Sprite[] ConstructionNPCOnOffSprites;

    // (true: On, false: Off)
    // 캐릭터들의 상태
    private bool KHSStateOnOff;
    private bool ConstructionNPCOnOff;

    [Space]
    [Header("백그라운드 오브젝트 가져오기")]
    public GameObject BackGroundBlueprint;

    private bool skipAction = false;


    void Start()
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
            if (name == "설계도")
            {
                BackGroundBlueprint.SetActive(true);
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
        BackGroundBlueprint.SetActive(isAction);
    }

    void CharactorOff(bool isAction)
    {
        KHS.SetActive(isAction);
        ConstructionNPC.SetActive(isAction);
        NameTag.SetActive(isAction);
    }


    // 현재 대화 하는 사람의 이름을 파악하여 알맞은 상태변화로 이어지게 함.
    void ChangeCharactoreImage()
    {
        if (dialogues[lineCount].name == "건설 인부")
        {
            ManageChangeState(false, true);
        }


        else if (dialogues[lineCount].name == "강혜성")
        {
            ManageChangeState(true, false);
        }

        else Debug.Log("캐릭터 상태 변화가 이루어지지 않습니다.");

    }

    void ManageChangeState(bool parm_KHSStateOnOff, bool parm_ConstructionNPCOnOff)
    {
        // 강햬성의 상태를 변경합니다.
        KHSStateOnOff = parm_KHSStateOnOff;
        // true이면 0번 인덱스의 On 이미지를, false이면 1번 인덱스의 Off 이미지를 가져옵니다.
        Sprite KHSSprite = KHSOnOffSprites[KHSStateOnOff ? 0 : 1];
        // 강혜성의 SpriteRenderer를 사용하여 이미지를 변경합니다.
        KHS.GetComponent<SpriteRenderer>().sprite = KHSSprite;

        ConstructionNPCOnOff = parm_ConstructionNPCOnOff;
        Sprite NPCSprite = ConstructionNPCOnOffSprites[ConstructionNPCOnOff ? 0 : 1];
        ConstructionNPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
    }


    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        ManageChangeState(false, false);
    }

    public void ClickSkipButton()
    {
        EndDialogue();
        // Skip 버튼 누른 후 다시 대화 진행을 위해 강제적으로 대화 여부를 false로 진행
        dialogueStarted = false;
        TalkText.text = "";
        skipAction = true;

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
