using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextEvilAppears : TextDialogSystem
{
    [Space]
    public GameObject HidePanel;
    public ManageSceneTransition sceneTransition;
    public InteractionEvent interactionEvent;

    private bool afterInteraction = false;

    //// (true: On, false: Off)
    //// 캐릭터들의 상태
    //private bool KHSStateOn = true;
    //private bool CJWStateOn = false;

    private void Update()
    {
        DiaAction();
    }

    public void BattleStart()
    {
        // 대화 시작 시 캐릭터 상태 초기화
        InitializeCharacterState();

        ShowDialogue(interactionEvent.GetDialogue(afterInteraction));

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

                        // 각 캐릭터의 대사를 모두 마칠 때 이미지 상태 On / OFF 변경
                        ChangeCharactoreImage();


                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }

                        // 모든 대화가 끝났을 때
                        else
                        {
                            EndDialogue();
                            afterInteraction = true;

                            // 전투 영상 씬 전환
                            sceneTransition.FadeScene(7);
                            Debug.Log("전투 영상 재생");

                            // 대화 종료 시 캐릭터 상태 초기화
                            InitializeCharacterState();

                        }
                    }
                }
            }
        }
    }


    // 대화 시작 시 캐릭터 상태 초기화 함수
    void InitializeCharacterState()
    {
        KHSStateOn = true;
        PartnerStateOn = false;

        // 초기 캐릭터 이미지 설정
        KHS.GetComponent<SpriteRenderer>().sprite = KHSOnOffSprites[0];
        Partner.GetComponent<SpriteRenderer>().sprite = PartnerOnOffSprites[1];
    }


    public override void SettingUI(bool isAction)
    {
        base.SettingUI(isAction);
        HidePanel.SetActive(!isAction);
    }

    public override void ShowDialogue(Dialogue[] Parm_dialogues)
    {
        base.ShowDialogue(Parm_dialogues);
    }


    public override void EndDialogue()
    {
        base.EndDialogue();
    }

    public override IEnumerator TypeWriter()
    {
        return base.TypeWriter();
    }

    public override void ChangeCharactoreImage()
    {
        base.ChangeCharactoreImage();
    }
}
