using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
 
    // 스크립트 관련
    public Text BattleText;
    public string BattleSelectChar = null;
    private string[] dialogues;
    private int currentDialogueIndex = 0;

    public UnityEngine.UI.Button[] Battle_Button;
    private bool Battle_DialoguesFin = false;
    public int Battle_DialougIndex = -1;


    // 전투 HP
    public UnityEngine.UI.Image BossHpBar;
    public UnityEngine.UI.Image GangHpBar;
    private float CurBossHp = 1f;
    private float CurGangsHp = 1f;
    public float BossHp = 1f;
    private float GangHp = 1f;
    public Text BossHpText;

    // 전투 데미지 값 등
    private float PlayerDam = 0f;
    private float PlayerHeal = 0f;
    private float PlayerDefen = 0f;
    private float BossDam = 0f;

    private bool SeleChecked = false;
    private bool PlayerTurn = true;

    // 애니메이션 & 사운드
    public AudioSource BattleAttackSoundEffect;
    public Animator BossAttackAnim;
    private bool BossAttackPlay;


    [SerializeField] private ManageSceneTransition sceneTransition;

    [Space]
    [Header("전투 애니메이션 정보")]

    [SerializeField] private GameObject KHSSelectImage;
    [SerializeField] private GameObject BattleStart;
    [SerializeField] private GameObject FadeObjects;
    [SerializeField] private GameObject MoveObject;
    [SerializeField] private GameObject SelectButtons;

    [Space]
    [Header("전투 피격 정보")]
    [SerializeField] private GameObject HitImage;
    [SerializeField] private GameObject AfterHitImage;

    private bool isAttack = true;

   

    void Start()
    {
        Battle_DialougIndex = -1;

        KHSSelectImage.SetActive(false);
        BattleStart.SetActive(false);

        SelectButtons.transform.localScale = Vector3.zero;
        SelectButtons.SetActive(false);

        dialogues = new string[] {
            "아이야... 너도 나와 같이 가자",
            "너도 나와 같이 있는 게 즐겁지...?"
            };


    }

 
    void Update()
    {
        Battle_Text_Update();

        //Debug.Log(Battle_DialougIndex);

        BossHpBar.fillAmount = Mathf.Lerp(BossHpBar.fillAmount, BossHp, Time.deltaTime * 2);
        GangHpBar.fillAmount = Mathf.Lerp(GangHpBar.fillAmount, GangHp, Time.deltaTime * 2);

        if (PlayerTurn == false && Input.GetMouseButtonDown(0))
        {
            // Boss_Attack();

            Debug.Log(currentDialogueIndex);

            if (currentDialogueIndex < dialogues.Length)
            {
                BattleText.text = dialogues[currentDialogueIndex];
                currentDialogueIndex++;

                foreach (UnityEngine.UI.Button button in Battle_Button)
                {
                    button.interactable = false;
                }
            }
            else
            {
                foreach (UnityEngine.UI.Button button in Battle_Button)
                {
                    button.interactable = true;
                }

                BattleText.text = "건물주 악귀가 나타났다. 우리는 무엇을 할까?";
                //Battle_DialoguesFin = true;
                PlayerTurn = true;
                currentDialogueIndex = 0;
            }

            //if (currentDialogueIndex < dialogues.Length)
            //{
            //    BattleText.text = dialogues[currentDialogueIndex];
            //    currentDialogueIndex++;
            //}
        }

        if (BossHp < 0.01)
        {
            // BossHp = 0;
            // SceneManager.LoadScene("Ending");

            // 보스 피가 0일 때 엔딩 씬으로 입장..
            sceneTransition.FadeScene(9);
        }

        if (BossAttackPlay == true)
        {
            BossAttackAnim.SetBool("Battle_Attack", true);
            BossAttackPlay = false;
        }
        else if (BossAttackPlay == false)
        {
            BossAttackAnim.SetBool("Battle_Attack", false);
        }

        BossHpText.text = (int)(BossHp * 100) + "/100";
    }

    

    public void OnBattleButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


            // 마우스 위치에 대한 PointerEventData 생성
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            // UI 레이캐스트 수행
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

        // UI 요소가 클릭된 경우
        if (raycastResults.Count > 0)
        {
            // 클릭한 UI 요소의 이름 확인
            string objectName = raycastResults[0].gameObject.name;

            // 이름이 "a"인지 확인
            if (objectName == "Battle_CHA")
            {
                BattleText.text = "차지원을 현재 선택할 수 없습니다.";
                BattleSelectChar = "지원";
                KHSSelectImage.SetActive(false);
                BattleStart.SetActive(false);
            }
            else if (objectName == "Battle_GANG")
            {
                BattleText.text = "강혜성을 선택했다.";
                BattleSelectChar = "혜성";
                if (SeleChecked == false)
                {
                    ShowKHSSelect();
                }
            }
            else if (objectName == "Battle_HYUN")
            {
                BattleText.text = "현 우를 현재 선택할 수 없습니다.";
                BattleSelectChar = "현 우";
                KHSSelectImage.SetActive(false);
                BattleStart.SetActive(false);
            }
        }
    }

    void ShowKHSSelect()
    {
        KHSSelectImage.SetActive(true);
        BattleStart.SetActive(true);
    }

    // 지원, 현우 선택 카드가 아래로 이동
    public void AciveBattleAnimation()
    {
        KHSSelectImage.SetActive(false);
        BattleStart.SetActive(false);

        SeleChecked = true;

        if (isAttack)
        {
            FadeObjects.transform.LeanMoveLocal(new Vector3(0, -400, 0), 0.5f).setOnComplete(MoveAnimation);
            isAttack = false;
        }
    }

    // 강혜성 카드가 왼쪽으로 이동한다.
    void MoveAnimation()
    {
        //  LeanTween.move(MoveObject, new Vector3(-285f, 0f, 0f), 2f);
        MoveObject.transform.LeanMoveLocal(new Vector3(-285, 0, 0), 0.5f).setOnComplete(ActiveSelectButton);
    }

    // 4개의 선택 버튼이 등장한다.
    void ActiveSelectButton()
    {
        SelectButtons.SetActive(true);
        SelectButtons.transform.LeanScale(Vector3.one, 0.5f);
    }

    private void ResetPosition()
    {
        SelectButtons.SetActive(false);
        SelectButtons.transform.LeanScale(Vector3.zero, 0.5f);
        MoveObject.transform.LeanMoveLocal(new Vector3(0, 0, 0), 0.5f);
        FadeObjects.transform.LeanMoveLocal(new Vector3(0, 0, 0), 0.5f);
        isAttack = true;
        SeleChecked = false;
    }


    public void Battle_Attack()
    {
        Debug.Log("공격 클릭");
        LeanTween.alpha(HitImage, 1f, 0.5f).setOnComplete(DisappearHitImage);
        LeanTween.alpha(AfterHitImage, 1f, 0.5f).setDelay(0.5f).setOnComplete(DisappearAfterHitImage);

        Battle_DialougIndex += 1;
        BossAttackPlay = true;
        PlayerDam = UnityEngine.Random.Range(1, 13);
        BattleAttackSoundEffect.Play();
        //   Debug.Log(PlayerDam);
        BossHp -= PlayerDam * 0.01f;
        BattleText.text = BattleSelectChar + "의 공격!\n악귀가 " + PlayerDam + "만큼의 피해를 입었다.";
      //  BattleButton.SetActive(false);
        PlayerTurn = false;
        ResetPosition();

    }

    void DisappearHitImage()
    {
        LeanTween.alpha(HitImage, 0f, 0.3f).setDelay(0.5f);
    }

    void DisappearAfterHitImage()
    {
        LeanTween.alpha(AfterHitImage, 0f, 0.3f).setDelay(0.5f);
    }


    public void Battle_Heal()
    {
        Battle_DialougIndex += 1;
        PlayerHeal = UnityEngine.Random.Range(1, 13);
        //   Debug.Log(PlayerDam);
        // BossHp -= PlayerDam * 0.01f;
        BattleText.text = BattleSelectChar + "의 치료!\n혜성이 " + PlayerHeal + "만큼의 피해를 치료한다.";
        //  BattleButton.SetActive(false);
        PlayerTurn = false;
        ResetPosition();
    }

    public void Battle_Defens()
    {
        Battle_DialougIndex += 1;
        PlayerDefen = UnityEngine.Random.Range(1, 13);
        //   Debug.Log(PlayerDam);
        // BossHp -= PlayerDam * 0.01f;
        BattleText.text = BattleSelectChar + "의 방어!\n혜성이 " + PlayerDefen + "만큼의 피해를 방어한다.";
        //  BattleButton.SetActive(false);
        PlayerTurn = false;
        ResetPosition();
    }

    public void Battle_Run()
    {
        //PlayerDefen = UnityEngine.Random.Range(1, 13);
        //   Debug.Log(PlayerDam);
        // BossHp -= PlayerDam * 0.01f;
        BattleText.text = "안돼! 지금 도망치면 지원 언니와 현 우 오빠가 위험해!\n그러니 내가 해내야만해!";
        //  BattleButton.SetActive(false);
        // PlayerTurn = false;
        //ResetPosition();
    }

    public void Battle_Text_Update()
    {

        if (Battle_DialougIndex == 0)
        {
            dialogues = new string[] {
            "아이야... 너도 나와 같이 가자",
            "너도 나와 같이 있는 게 즐겁지...?"
            };
        }
        else if (Battle_DialougIndex == 1)
        {
            dialogues = new string[] {
            "왜, 왜 그 누구도 나의 뜻을 알아주지 않지...?",
            "아이야, 너도 우리가 뛰어놀던 시절이 그립잖니.",
            "아아... 모두의 웃음 소리가 들리던 그 때가..."
            };
        }
        else if (Battle_DialougIndex == 2)
        {
            dialogues = new string[] {
            "새로운 아파트가 생기면서 모두의 공터가 사라졌어...",
            "함께 웃던 이들도 더 이상 내게 오지 않았지...",
            "아... 아... 내 아들..."
            };
        }
        else if (Battle_DialougIndex == 3)
        {
            dialogues = new string[] {
            "그렇게 가기 전에 내 아들을 한 번만 볼 수 있었다면...",
            "그렇게 착하던 내 아들...",
            };
        }
        else if (Battle_DialougIndex == 4)
        {
            dialogues = new string[] {
            "매번 재개발 동의서에 사인을 하라던 인부들도...",
            "나를 손가락질 하던 상가 주민들도...",
            "모두... 모두...!",
            "나를 알아주지 않던 모두가...!"
            };
        }
        else if (Battle_DialougIndex == 5)
        {
            dialogues = new string[] {
            "아이야... 나를 이해해주렴...",
            "너도 다른 아이들처럼 공터를 뛰어놀던...",
            };
        }
        else if (Battle_DialougIndex == 6)
        {
            dialogues = new string[] {
            "아아...!!",
            "난 이곳에서 나가야겠어...!!!",
            };
        }
        else if (Battle_DialougIndex == 7)
        {
            dialogues = new string[] {
            "(악귀는 더 이상 아무 말을 하지 않는다.)",
            "(무엇이 이 악귀를 이렇게까지 슬프게 만든걸까.)"
            };
        }


    }


    public void Boss_Attack()
    {
        if (PlayerTurn == false)
        {
            BossDam = UnityEngine.Random.Range(1, 6);
            GangHp -= BossDam * 0.01f;
            BattleText.text = "분노한 악귀가" + BattleSelectChar + "을 향해 공격합니다.";
            PlayerTurn = true;
        }

    }

}
