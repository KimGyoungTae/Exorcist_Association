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
 
    // ��ũ��Ʈ ����
    public Text BattleText;
    public string BattleSelectChar = null;
    private string[] dialogues;
    private int currentDialogueIndex = 0;

    public UnityEngine.UI.Button[] Battle_Button;
    private bool Battle_DialoguesFin = false;
    public int Battle_DialougIndex = -1;


    // ���� HP
    public UnityEngine.UI.Image BossHpBar;
    public UnityEngine.UI.Image GangHpBar;
    private float CurBossHp = 1f;
    private float CurGangsHp = 1f;
    public float BossHp = 1f;
    private float GangHp = 1f;
    public Text BossHpText;

    // ���� ������ �� ��
    private float PlayerDam = 0f;
    private float PlayerHeal = 0f;
    private float PlayerDefen = 0f;
    private float BossDam = 0f;

    private bool SeleChecked = false;
    private bool PlayerTurn = true;

    // �ִϸ��̼� & ����
    public AudioSource BattleAttackSoundEffect;
    public Animator BossAttackAnim;
    private bool BossAttackPlay;


    [SerializeField] private ManageSceneTransition sceneTransition;

    [Space]
    [Header("���� �ִϸ��̼� ����")]

    [SerializeField] private GameObject KHSSelectImage;
    [SerializeField] private GameObject BattleStart;
    [SerializeField] private GameObject FadeObjects;
    [SerializeField] private GameObject MoveObject;
    [SerializeField] private GameObject SelectButtons;

    [Space]
    [Header("���� �ǰ� ����")]
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
            "���̾�... �ʵ� ���� ���� ����",
            "�ʵ� ���� ���� �ִ� �� �����...?"
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

                BattleText.text = "�ǹ��� �ǱͰ� ��Ÿ����. �츮�� ������ �ұ�?";
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

            // ���� �ǰ� 0�� �� ���� ������ ����..
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


            // ���콺 ��ġ�� ���� PointerEventData ����
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            // UI ����ĳ��Ʈ ����
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

        // UI ��Ұ� Ŭ���� ���
        if (raycastResults.Count > 0)
        {
            // Ŭ���� UI ����� �̸� Ȯ��
            string objectName = raycastResults[0].gameObject.name;

            // �̸��� "a"���� Ȯ��
            if (objectName == "Battle_CHA")
            {
                BattleText.text = "�������� ���� ������ �� �����ϴ�.";
                BattleSelectChar = "����";
                KHSSelectImage.SetActive(false);
                BattleStart.SetActive(false);
            }
            else if (objectName == "Battle_GANG")
            {
                BattleText.text = "�������� �����ߴ�.";
                BattleSelectChar = "����";
                if (SeleChecked == false)
                {
                    ShowKHSSelect();
                }
            }
            else if (objectName == "Battle_HYUN")
            {
                BattleText.text = "�� �츦 ���� ������ �� �����ϴ�.";
                BattleSelectChar = "�� ��";
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

    // ����, ���� ���� ī�尡 �Ʒ��� �̵�
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

    // ������ ī�尡 �������� �̵��Ѵ�.
    void MoveAnimation()
    {
        //  LeanTween.move(MoveObject, new Vector3(-285f, 0f, 0f), 2f);
        MoveObject.transform.LeanMoveLocal(new Vector3(-285, 0, 0), 0.5f).setOnComplete(ActiveSelectButton);
    }

    // 4���� ���� ��ư�� �����Ѵ�.
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
        Debug.Log("���� Ŭ��");
        LeanTween.alpha(HitImage, 1f, 0.5f).setOnComplete(DisappearHitImage);
        LeanTween.alpha(AfterHitImage, 1f, 0.5f).setDelay(0.5f).setOnComplete(DisappearAfterHitImage);

        Battle_DialougIndex += 1;
        BossAttackPlay = true;
        PlayerDam = UnityEngine.Random.Range(1, 13);
        BattleAttackSoundEffect.Play();
        //   Debug.Log(PlayerDam);
        BossHp -= PlayerDam * 0.01f;
        BattleText.text = BattleSelectChar + "�� ����!\n�ǱͰ� " + PlayerDam + "��ŭ�� ���ظ� �Ծ���.";
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
        BattleText.text = BattleSelectChar + "�� ġ��!\n������ " + PlayerHeal + "��ŭ�� ���ظ� ġ���Ѵ�.";
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
        BattleText.text = BattleSelectChar + "�� ���!\n������ " + PlayerDefen + "��ŭ�� ���ظ� ����Ѵ�.";
        //  BattleButton.SetActive(false);
        PlayerTurn = false;
        ResetPosition();
    }

    public void Battle_Run()
    {
        //PlayerDefen = UnityEngine.Random.Range(1, 13);
        //   Debug.Log(PlayerDam);
        // BossHp -= PlayerDam * 0.01f;
        BattleText.text = "�ȵ�! ���� ����ġ�� ���� ��Ͽ� �� �� ������ ������!\n�׷��� ���� �س��߸���!";
        //  BattleButton.SetActive(false);
        // PlayerTurn = false;
        //ResetPosition();
    }

    public void Battle_Text_Update()
    {

        if (Battle_DialougIndex == 0)
        {
            dialogues = new string[] {
            "���̾�... �ʵ� ���� ���� ����",
            "�ʵ� ���� ���� �ִ� �� �����...?"
            };
        }
        else if (Battle_DialougIndex == 1)
        {
            dialogues = new string[] {
            "��, �� �� ������ ���� ���� �˾����� ����...?",
            "���̾�, �ʵ� �츮�� �پ��� ������ �׸��ݴ�.",
            "�ƾ�... ����� ���� �Ҹ��� �鸮�� �� ����..."
            };
        }
        else if (Battle_DialougIndex == 2)
        {
            dialogues = new string[] {
            "���ο� ����Ʈ�� ����鼭 ����� ���Ͱ� �������...",
            "�Բ� ���� �̵鵵 �� �̻� ���� ���� �ʾ���...",
            "��... ��... �� �Ƶ�..."
            };
        }
        else if (Battle_DialougIndex == 3)
        {
            dialogues = new string[] {
            "�׷��� ���� ���� �� �Ƶ��� �� ���� �� �� �־��ٸ�...",
            "�׷��� ���ϴ� �� �Ƶ�...",
            };
        }
        else if (Battle_DialougIndex == 4)
        {
            dialogues = new string[] {
            "�Ź� �簳�� ���Ǽ��� ������ �϶�� �κε鵵...",
            "���� �հ����� �ϴ� �� �ֹε鵵...",
            "���... ���...!",
            "���� �˾����� �ʴ� ��ΰ�...!"
            };
        }
        else if (Battle_DialougIndex == 5)
        {
            dialogues = new string[] {
            "���̾�... ���� �������ַ�...",
            "�ʵ� �ٸ� ���̵�ó�� ���͸� �پ���...",
            };
        }
        else if (Battle_DialougIndex == 6)
        {
            dialogues = new string[] {
            "�ƾ�...!!",
            "�� �̰����� �����߰ھ�...!!!",
            };
        }
        else if (Battle_DialougIndex == 7)
        {
            dialogues = new string[] {
            "(�Ǳʹ� �� �̻� �ƹ� ���� ���� �ʴ´�.)",
            "(������ �� �Ǳ͸� �̷��Ա��� ������ ����ɱ�.)"
            };
        }


    }


    public void Boss_Attack()
    {
        if (PlayerTurn == false)
        {
            BossDam = UnityEngine.Random.Range(1, 6);
            GangHp -= BossDam * 0.01f;
            BattleText.text = "�г��� �ǱͰ�" + BattleSelectChar + "�� ���� �����մϴ�.";
            PlayerTurn = true;
        }

    }

}
