using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance;
   public List<InventoryItem> Items = new List<InventoryItem>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject InventoryPanel;
    private bool isInventoryActive = false;

    [Space]
    [Header("������ Info")]
    [SerializeField] private Text UI_itemType;
    [SerializeField] private Text UI_itemName;
    [SerializeField] private Text UI_itemDescription;
    [SerializeField] private Image UI_itemImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
       
    }


    // �������� ����Ʈ�� �߰��ϰ� �ߺ��� Ȯ���ϴ� �޼���
    // ū ������ ���տ����� HashSet�� ����ϴ� ���� �ð� ���⵵�鿡�� ȿ����!
    public bool Add(InventoryItem item)
    {
        // ����Ʈ�� �������� �ִ��� Ȯ��
        if (!Items.Contains(item))
        {
            Items.Add(item);

            
            return true;
        }

        // �̹� �����ϴ� �������̹Ƿ� �߰����� ����
        else { return false; }
    }


    public void Remove(InventoryItem item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // �κ��丮 â�� �� �� ���� ������ �Ծ��� ������ŭ �ٽ� �����Ǵ� ���� ����
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    public void ShowInventory()
    {
        // ���� ���
        isInventoryActive = !isInventoryActive;
        // ������Ʈ ���¿� ���� Ȱ��/��Ȱ�� ����
        InventoryPanel.SetActive(isInventoryActive);
        
    }

    public void SetInventoryInfo()
    {
        // ���� Ŭ���� ��ư��(������ ����) ���ɴϴ�.
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // Ŭ���� ��ư�� �ؽ�Ʈ ������Ʈ�� �����ɴϴ�.
        Text itemNameText = clickedButton.transform.Find("ItemName").GetComponent<Text>();

        // Ŭ���� �������� �̸��� �����ɴϴ�.
        string itemName = itemNameText.text;

        // ������ �̸��� ������� �ش� �������� ã�ƿɴϴ�.
        InventoryItem clickedItem = Items.Find(item => item.itemName == itemName);

        // Ŭ���� �������� ������ ����մϴ�.
        if (clickedItem != null)
        {
            //Debug.Log("������ Ÿ��: " + clickedItem.itemType);
            //Debug.Log("������ �̸�: " + clickedItem.itemName);
            //Debug.Log("������ ����: " + clickedItem.itemInfo);


            UI_itemType.text = clickedItem.itemType;
            UI_itemName.text = clickedItem.itemName;
            UI_itemDescription.text = clickedItem.itemInfo;
            UI_itemImage.sprite = clickedItem.icon;

        }
    }
}
