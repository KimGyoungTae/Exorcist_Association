using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_HP : MonoBehaviour
{
    public Image HpBar;
    public Text BattleText;
    private float MaxHp = 100f;
    private float CurHp = 100f;

    private void Start()
    {
        HpBar.fillAmount = CurHp / MaxHp;
    }

    public void Update()
    {
        HpBar.fillAmount = Mathf.Lerp(HpBar.fillAmount, CurHp / MaxHp, Time.deltaTime * 2);
    }

    public void Battle_Attack()
    {
        //HpBar.fillAmount = Mathf.Lerp(HpBar.fillAmount, HpBar.fillAmount -= (float)0.2, Time.deltaTime * 10);
        //hp -= 0.2f;
        //OnBattleActionOnTextClick();
        //gameObject.SetActive(false);
        //BattleTrue = true;

        CurHp -= 10f;

    }

    private void DamageHp(float damge)
    {

    }

    private void OnBattleActionOnTextClick()
    {
        // text�� Ȱ��ȭ�ϰ� ������ ����
        //if (BattleText != null)
        //{
        //    BattleText.gameObject.SetActive(true);

        //    // Text ���� ����
        //    BattleText.text = "������ ������ ���۵Ǿ���.";

        //}
    }
}
