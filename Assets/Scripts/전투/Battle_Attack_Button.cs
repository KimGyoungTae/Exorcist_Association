using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Attack_Button : MonoBehaviour
{
    public GameObject BattleButton;
    public Text BattleText;
    public string BattleSelectChar = null;

    public void OnBattleButton()
    { 
        if (BattleButton != null)
        {
            // 'battle_action_on_text' ������Ʈ�� Ȱ��ȭ
            if (gameObject.name == "Battle_CHA")
            {
                BattleText.text = "�������� ���� ������ �� �����ϴ�.";
                // BattleButton.SetActive(true);
                // BattleSelectChar = "����";
            }
            else if (gameObject.name == "Battle_GANG")
            {
                BattleText.text = "�������� �����ߴ�.";
                BattleButton.SetActive(true);
                BattleSelectChar = "����";
            } 
            else if (gameObject.name == "Battle_HYUN")
            {
                BattleText.text = "�� �츦 ���� ������ �� �����ϴ�.";
                // BattleButton.SetActive(true);
                // BattleSelectChar = "�� ��";
            }

        }
        else
        {
            Debug.LogError("BattleButton �Ҵ����� �ʾҽ��ϴ�!"); // ���� ó��
        }

    }
}
