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
            // 'battle_action_on_text' 오브젝트를 활성화
            if (gameObject.name == "Battle_CHA")
            {
                BattleText.text = "차지원을 현재 선택할 수 없습니다.";
                // BattleButton.SetActive(true);
                // BattleSelectChar = "지원";
            }
            else if (gameObject.name == "Battle_GANG")
            {
                BattleText.text = "강혜성을 선택했다.";
                BattleButton.SetActive(true);
                BattleSelectChar = "혜성";
            } 
            else if (gameObject.name == "Battle_HYUN")
            {
                BattleText.text = "현 우를 현재 선택할 수 없습니다.";
                // BattleButton.SetActive(true);
                // BattleSelectChar = "현 우";
            }

        }
        else
        {
            Debug.LogError("BattleButton 할당하지 않았습니다!"); // 에러 처리
        }

    }
}
