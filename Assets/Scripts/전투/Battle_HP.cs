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
        // text를 활성화하고 내용을 변경
        //if (BattleText != null)
        //{
        //    BattleText.gameObject.SetActive(true);

        //    // Text 내용 설정
        //    BattleText.text = "혜성의 공격이 시작되었다.";

        //}
    }
}
