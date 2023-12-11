using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ������� ���� ��ư�鿡�� ������ �ȴ�.
/// ��ư�� Ŭ���ϸ� ���� ����� �� ���ٰ� ��ȭ ������ �߰� �ȴ�.
/// </summary>
public class ButtonLock : MonoBehaviour
{
    [Header("��� ����� ������Ʈ ����")]
    [SerializeField] private int id;

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;
    [Space]

    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text TalkText;

    [SerializeField] private bool isAction;
    [SerializeField] private int talkIndex;

    [Space]
    [Header("���� �ð� �� �ڵ� ��ȭ �г� ��Ȱ��ȭ �ð�")]
    public float displayTime = 1.3f;   // ��ȭ �г��� Ȱ��ȭ�� ���·� ������ �ð�

    public void Action()
    {
        Talk(id);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        
        // ��ȭ ������ �� ����ϰ� Panel�� ���� �� 
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0; //��ȭ�� ���� �� 0���� �ʱ�ȭ , �ٸ� �繰�ϰ� ��� ��ȭ�� ���� �ϱ� ����.

            return;  // �� , void �Լ����� return�� ���� ���� ����.
        }

        // �ڷ�ƾ�� ����Ͽ� �ؽ�Ʈ�� �����̿� �Բ� �� �ܾ �����ݴϴ�.
        StartCoroutine(DisplayText(talkData));
        // ���� �ð� �Ŀ� ��ȭ �г��� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(HideDialogueAfterTime());


        isAction = true;
        talkIndex++;
    }

    IEnumerator DisplayText(string text)
    {
        TalkText.text = ""; // �ؽ�Ʈ�� �����ֱ� ���� ���� �ʱ�ȭ�մϴ�.

        foreach (char character in text)
        {
            TalkText.text += character;
            yield return new WaitForSeconds(textDelay);
            // ���� ���ڸ� �����ֱ� ���� ������ �����̸�ŭ ����մϴ�.       
        }
    }

    private IEnumerator HideDialogueAfterTime()
    {
        // ���� �ð� ���� ��ȭ �г��� Ȱ��ȭ�� �� ��Ȱ��ȭ
        yield return new WaitForSeconds(displayTime);
        talkPanel.SetActive(false);
    }
}
