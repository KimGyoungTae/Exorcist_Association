using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text TalkText;
    public GameObject scanObject;
    public bool isAction; 

    public int talkIndex;

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

    private bool isDisplayingText = false; // ��ȭ ��� ������ ���θ� ��Ÿ���� ����
    private Coroutine currentCoroutine; // ���� ���� ���� �ڷ�ƾ�� �����ϴ� ����

    public void Action(GameObject getObjcet)
    {
        scanObject = getObjcet;
        
      // Debug.Log(isDisplayingText);
       
        PointObject pointObject = scanObject.GetComponent<PointObject>();

        //Debug.Log(objData.id);
        Talk(pointObject.id, pointObject.isNPC);

        talkPanel.SetActive(isAction);   // isAction == false , ���� Ul â �ݰڴ�.
    }


    //// Ŭ�� �̺�Ʈ�� �����ϴ� �Լ�
    //public void HandleClick(GameObject clickedObject)
    //{



    //    if (!isDisplayingText)
    //    {
    //        isDisplayingText = true;
    //        // StartCoroutine(ProcessClick(clickedObject));

    //        //if (currentCoroutine != null)
    //        //{
    //        //    StopCoroutine(currentCoroutine); // �̹� ���� ���� �ڷ�ƾ�� ����
    //        //}
    //        currentCoroutine = StartCoroutine(ProcessClick(clickedObject));

    //    }
    //}

    //// Ŭ���� ������Ʈ�� ������ ó���ϰ� ��ȭ ����� �����ϴ� �Լ�
    //IEnumerator ProcessClick(GameObject clickedObject)
    //{
    //    scanObject = clickedObject;

    //   // Debug.Log(scanObject.name);  -> ���Ⱑ ����!

    //    ObjData objData = scanObject.GetComponent<ObjData>();
    //    Talk(objData.id, objData.isNPC);
    //    talkPanel.SetActive(isAction);

    //    while (isDisplayingText)
    //    {
    //        yield return null; // ��ȭ�� ���� ������ ���
    //    }


    //    // �ڷ�ƾ ����
    //    currentCoroutine = null;
    //    yield break;

    //}


    void Talk(int id, bool isNPC)
    {
      //  Debug.Log(id); // ->scanObject�� ������������, id�� �̹��� �Ȱ�������..

       string talkData = talkManager.GetTalk(id, talkIndex);

      
        if (talkData == null )
        {
            isAction = false;
            talkIndex = 0; //��ȭ�� ���� �� 0���� �ʱ�ȭ , �ٸ� �繰�ϰ� ��� ��ȭ�� ���� �ϱ� ����.

            // ��ȭ�� ���� �� scanObject �ʱ�ȭ
            scanObject = null;


            //StopCoroutine(currentCoroutine); // �̹� ���� ���� �ڷ�ƾ�� ����

            return;  // �� , void �Լ����� return�� ���� ���� ����.
        }

        else
        {
            if (!isDisplayingText)
            {
             //   Debug.Log("��ȭ ����");
               
             // �ڷ�ƾ�� ����Ͽ� �ؽ�Ʈ�� �����̿� �Բ� �� �ܾ �����ݴϴ�.
                StartCoroutine(DisplayText(talkData));
                isDisplayingText = true;
                isAction = true;
                talkIndex++;
            }
           
        //  StartCoroutine(DisplayText(talkData));
            //isAction = true;
            //talkIndex++;
        }
           
        

    }

    IEnumerator DisplayText(string text)
    {
        TalkText.text = ""; // �ؽ�Ʈ�� �����ֱ� ���� ���� �ʱ�ȭ�մϴ�.

        //foreach (char character in text)
        //{
        //    TalkText.text += character;
        //    yield return new WaitForSeconds(textDelay);
        //    // ���� ���ڸ� �����ֱ� ���� ������ �����̸�ŭ ����մϴ�.       
        //}

        for (int i = 0; i < text.Length; i++)
        {
            
            TalkText.text += text[i];
            yield return new WaitForSeconds(textDelay);
        }

       // Debug.Log("��ȭ ����");
        isDisplayingText = false; // ��ȭ�� ������ ������ �����Ͽ� ���� Ŭ���� ���
    }
}
